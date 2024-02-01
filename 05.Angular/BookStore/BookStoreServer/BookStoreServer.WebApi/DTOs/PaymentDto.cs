using BookStoreServer.WebApi.Models;
using Iyzipay.Model;

namespace BookStoreServer.WebApi.DTOs
{
    public sealed record PaymentDto(
        int UserId,
        List<Book> Books,
        Buyer Buyer,
        Address ShippingAddress,Address BillingAddress,
        PaymentCard PaymentCard);
}
