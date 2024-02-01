using BookStoreServer.WebApi.Context;
using BookStoreServer.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServer.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class ConfigurationsController : ControllerBase
    {
        private  readonly AppDbContext _context = new();
        [HttpGet]
        public IActionResult SeedData()
        {
            #region SeedCategories
            //List<Category> categories = new();
            //for (int i = 0; i < 10; i++)
            //{
            //    var category = new Category()
            //    {
            //        Name = "Category" + (i + 1),
            //        IsActive = true,
            //        IsDeleted = false
            //    };
            //    categories.Add(category);
            //}
            //_context.Categories.AddRange(categories);
            //_context.SaveChanges();
            #endregion

            List<Book> books= new();
            for(int i = 0; i<800;i++)
            {
                var book = new Book()
                {
                    Title=$"Book {i}",
                    Author=$"Author {i}",
                    Summary= $"Summary {i}",
                    CoverImageUrl= "https://m.media-amazon.com/images/I/71Qde+ZerdL._AC_UF1000,1000_QL80_.jpg",
                    Price=new(i*2,"₺"),
                    Quantity=i*1,
                    IsActive=true,
                    IsDeleted=false,
                    ISBN =$"ISBN {i}",
                    CreateAt=DateTime.Now
                };
                books.Add(book);
            }
            _context.Books.AddRange(books);
            _context.SaveChanges();
            List<Category> categories = _context.Categories.ToList();
            List<BookCategory> bookCategories = new();
            foreach (var book in books)
            {
                var bookCategory = new BookCategory()
                {
                    BookId = book.Id,
                    CategoryId = categories[new Random().Next(1, categories.Count)].Id
                };
                bookCategories.Add(bookCategory);
            }
            _context.BookCategories.AddRange(bookCategories);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
