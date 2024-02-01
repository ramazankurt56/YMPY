using BookStoreServer.WebApi.Context;
using BookStoreServer.WebApi.DTOs;
using BookStoreServer.WebApi.Models;
using BookStoreServer.WebApi.Services;
using BookStoreServer.WebApi.ValueObjects;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class ShoppingCartsController : ControllerBase
    {
        private readonly AppDbContext context;

        public ShoppingCartsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{bookId}/{quantity}")]
        public IActionResult CheckBookQuantityIsAvailable(int bookId, int quantity)
        {
            Book book = context.Books.Find(bookId);
            if (book.Quantity<quantity)
            {
                throw new Exception("Stokta bu kadar adet kitap yok");
            }
            return NoContent();
        }


        [HttpGet("{bookId}/{quantity}")]
        public IActionResult ChangeBookQuantityInShoppingCart(int bookId,int quantity)
        {
            ShoppingCart cart = context.ShoppingCarts.Where(p => p.BookId == bookId).FirstOrDefault();
            if (cart is null)
            {
                throw new Exception("Kitap sepette bulunamadı");
            }
            Book book = context.Books.Find(bookId);
            if (quantity<=0)
            {
                book.Quantity += 1;
                context.Remove(cart);
               
                context.Update(book);
            }
            else
            {
                cart.Quantity=quantity;
                if (book.Quantity<cart.Quantity)
                {
                    throw new Exception("Stokta bu kadar kitap yok!");
                }
                context.Update(cart);
            }
            context.SaveChanges();
            return NoContent();
        }
        [HttpPost]
        public IActionResult Add(AddShoppingCartDto request)
        {
            Book book = context.Books.Find(request.BookId);
            if(book is null)
            {
                throw new Exception("kitap bulunamadı!");
            }
            if (book.Quantity<request.Quantity)
            {
                throw new Exception("kitap Stokta Kalmadı!");
            }
            ShoppingCart cart = context.ShoppingCarts.Where(p => p.BookId == request.BookId).FirstOrDefault();
            if (cart is not null)
            {
                cart.Quantity += 1;
                context.Update(cart);
            }
            else
            {
                cart = new()
                {
                    BookId = request.BookId,
                    Quantity = request.Quantity,
                    Price = request.Price,
                    UserId = request.UserId
                };
                context.Add(cart);
            }
            
            context.SaveChanges();
            return NoContent();
        }


        [HttpGet("{id}")]
        public IActionResult RemoveById(int id)
        {
            var shoppingCart = context.ShoppingCarts.Where(p => p.Id == id).FirstOrDefault();
            if (shoppingCart != null)
            {
                context.Remove(shoppingCart);
                context.SaveChanges();
            }
            return NoContent();
        }
        [HttpGet("{userId}")]
        public IActionResult GetAll(int userId)
        {
            List<ShoppingCartResponseDto> books = context.ShoppingCarts.AsNoTracking().Include(p => p.Book).Select(s => new ShoppingCartResponseDto()
            {

                Id = s.Book.Id,
                Title = s.Book.Title,
                Author = s.Book.Author,
                Summary = s.Book.Summary,
                CoverImageUrl = s.Book.CoverImageUrl,
                Price = s.Price,
                Quantity = s.Quantity,
                IsActive = s.Book.IsActive,
                IsDeleted = s.Book.IsDeleted,
                ISBN = s.Book.ISBN,
                CreateAt = s.Book.CreateAt,
                ShoppingCartId = s.Id
            }).ToList();
            return Ok(books);
        }
        [HttpPost]
        public IActionResult SetShoppingCartsFromLocalStorage(List<SetShoppinCartsDto> request)
        {
            List<ShoppingCart> shoppingCarts = new();
            foreach (var shop in request)
            {
                ShoppingCart shoppingCart = new()
                {
                    BookId = shop.BookId,
                    UserId = shop.UserId,
                    Quantity = shop.Quantity,
                    Price = shop.Price,
                };
                shoppingCarts.Add(shoppingCart);
            }
            context.AddRange(shoppingCarts);
            context.SaveChanges();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentDto requestDto)
        {
            foreach (var item in requestDto.Books)
            {
                Book checkBook = context.Books.Find(item.Id);
                if (checkBook.Quantity<item.Quantity)
                {
                    throw new Exception($"{item.Title} Kitap stokta kalamdı");
                }
            }
            decimal total = 0;
            decimal commission = 0;//komisyon

            foreach (var book in requestDto.Books)
            {
                total += book.Price.Value;
            }
            commission = total;
            //commission = total * 1.2m / 100;
            Currency currency = Currency.TRY;
            string? requestCurrency = requestDto.Books[0]?.Price?.Currency;
            if (!string.IsNullOrEmpty(requestCurrency))
            {
                switch (requestCurrency)
                {
                    case "₺":
                        currency = Currency.TRY;
                        break;
                    case "$":
                        currency = Currency.USD;
                        break;
                    case "€":
                        currency = Currency.EUR;
                        break;
                    default:
                        throw new Exception("Para birimi bulunamadı.");
                        break;
                }
            }
            else
            {
                throw new Exception("Sepette ürün bulunamadı");
            }

            //Default configuration ile bağlantı bilgilerim
            Iyzipay.Options options = new();
            options.ApiKey = "sandbox-3D5dmlUDIpfpUsq5KDZAfqHbfmhXpX7H";
            options.SecretKey = "sandbox-STRKw6vW59dkE6RhrcU3fcjhatdudAsw";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();//Benzersiz Id verilmelidir.
            request.Price = total.ToString();//ödeme tutarı
            request.PaidPrice = commission.ToString();//komisyon + ödeme tutarı
            request.Currency = currency.ToString();//Para tipi
            request.Installment = 1;//Taksit seçeneği
            request.BasketId = Order.GetNewOrderNumber();//Sepet Id TNR2023000000001
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = requestDto.PaymentCard;
            request.PaymentCard = paymentCard;

            Buyer buyer = requestDto.Buyer;
            buyer.Id = Guid.NewGuid().ToString();
            request.Buyer = buyer;

            Address shippingAddress = requestDto.ShippingAddress;
            request.ShippingAddress = shippingAddress;

            Address billingAddress = requestDto.BillingAddress;
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            foreach (var book in requestDto.Books)
            {
                BasketItem item = new BasketItem();
                item.Category1 = "Book";
                item.Category2 = "Book";
                item.Id = book.Id.ToString();
                item.Name = book.Title;
                item.ItemType = BasketItemType.PHYSICAL.ToString();
                item.Price = book.Price.Value.ToString();
                basketItems.Add(item);
            }

            request.BasketItems = basketItems;

            Payment payment = Iyzipay.Model.Payment.Create(request, options);
            if (payment.Status == "success")
            {
                try
                {

                    string orderNumber = Order.GetNewOrderNumber();
                    List<Order> orders = new();
                    foreach (var book in requestDto.Books)
                    {
                        Book changeBookQuantity = context.Books.Find(book.Id);
                        changeBookQuantity.Quantity-=book.Quantity;
                        context.Update(changeBookQuantity);
                        Order order = new()
                        {
                            OrderNumber = orderNumber,
                            BookId = book.Id,
                            Quantity=book.Quantity,
                            Price = new Money(book.Price.Value, book.Price.Currency),
                            PaymentDate = DateTime.Now,
                            PaymentType = "Credit Cart",
                            PaymentNumber = payment.PaymentId,
                            CreatedAt = DateTime.Now
                        };
                        orders.Add(order);
                    }



                    OrderStatus orderStatus = new()
                    {
                        OrderNumber = orderNumber,
                        Status = OrderStatusEnum.AwaitingApproval,
                        StatusDate = DateTime.Now,
                    };

                    context.Orders.AddRange(orders);
                    context.OrderStatuses.Add(orderStatus);

                    Models.User user = context.Users.Find(requestDto.UserId);


                    if (user is not null)
                    {
                        var shoppingCarts = context.ShoppingCarts.Where(p => p.UserId == requestDto.UserId).ToList();
                        context.RemoveRange(shoppingCarts);
                    }

                    context.SaveChanges();
                    string response = await MailService.SendEmailAsync(requestDto.Buyer.Email, "Siparişiniz Alındı", $@"
                 <h1>Siparişiniz Alındı</h1>
                 <p>Sipariş Numaranız:{orderNumber}</p>
                    <p> Ödeme Numaranız: {payment.PaymentId}</p>
                    <p> Ödeme Tutarınız: {payment.PaidPrice}</p>
                    <p> Ödeme Tarihiniz: {DateTime.Now}</p>
                    <p> Ödeme Tipiniz: Kredi Kartı</p>
                    <p> Ödeme Durumunuz: Onay Bekleniyor</p>)");
                }

                catch (Exception)
                {
                    CreateRefundRequest refundRequest=new CreateRefundRequest();
                    refundRequest.ConversationId = request.ConversationId;
                    refundRequest.Locale = Locale.TR.ToString() ;
                    refundRequest.PaymentTransactionId = "1";
                    refundRequest.Price =request.Price;
                    refundRequest.Ip = "85.34.78.112";
                    refundRequest.Currency=currency.ToString();
                    Refund refund = Refund.Create(refundRequest, options);
                    return BadRequest(new { message = "İşlem sırasında bir hata aldık. Paranızı iade ettik. Lütfen daha sonra tekrar deneyiniz. Müşteri temsilcisiyle iletişime geçiniz." });
                }
                return NoContent();
            }
            else
            {

                return BadRequest(payment.ErrorMessage);
            }

            //status:success failure 
            //Error:Hata mesajı
        }
    }
}
