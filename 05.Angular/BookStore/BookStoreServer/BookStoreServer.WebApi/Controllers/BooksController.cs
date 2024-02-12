using BookStoreServer.WebApi.DTOs;
using BookStoreServer.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using GSF.FuzzyStrings;
using BookStoreServer.WebApi.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace BookStoreServer.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class BooksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AppDbContext context;


        public BooksController(IMapper mapper,AppDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        [HttpPost]
       
        [HttpPost]
        public IActionResult GetAll(RequestDto request)
        {
            #region EskiGetall
            //ResponseDto<List<Book>> response = new();
            //var newBooks = new List<Book>();
            //if (request.CategoryId != null)
            //{
            //    newBooks = SeedData.BookCategories
            //        .Where(p => p.CategoryId == request.CategoryId)
            //        .Select(s => s.Book)
            //        .ToList();
            //}
            //else
            //{
            //    newBooks = SeedData.Books;
            //}
            //if (!string.IsNullOrEmpty(request.Search))
            //{
            //    newBooks = newBooks
            //   .Where(f => f.Title.ApproximatelyEquals(request.Search, FuzzyStringComparisonOptions.UseJaccardDistance,
            //   FuzzyStringComparisonTolerance.Strong)
            //   | f.Author.ApproximatelyEquals(request.Search, FuzzyStringComparisonOptions.UseJaccardDistance,
            //   FuzzyStringComparisonTolerance.Strong)).ToList();

            //}



            //response.Data = newBooks
            //    .Skip((request.PageNumber - 1) * request.PageSize)
            //    .Take(request.PageSize)
            //    .ToList();
            //response.PageNumber = request.PageNumber;
            //response.PageSize = request.PageSize;
            //response.TotalPageCount = (int)Math.Ceiling(newBooks.Count / (double)request.PageSize);
            //response.IsFirstPage = request.PageNumber == 1;
            //response.IsLastPage = request.PageNumber == response.TotalPageCount;
            //return Ok(response);
            #endregion
            List<Book> books = new();
            if (request.CategoryId==null)
            {
                books=context.Books.Where(p=>p.IsActive==true&& p.IsDeleted==false)
                    .Where(p=>p.Title.ToLower().Contains(request.Search.ToLower())||p.ISBN.ToLower().Contains(request.Search.ToLower()))
                    .OrderByDescending(p=>p.CreateAt)
                    .Take(request.PageSize)
                    .ToList();
            }
            else
            {
                books=context.BookCategories
                    .Where(p=>p.CategoryId==request.CategoryId)
                    .Include(p=>p.Book)
                    .Select(p=>p.Book)
                    .Where(p => p.IsActive == true && p.IsDeleted == false)
                    .Where(p => p.Title.ToLower().Contains(request.Search.ToLower()) || p.ISBN.ToLower().Contains(request.Search.ToLower()))
                    .OrderByDescending(p => p.CreateAt)
                    .Take(request.PageSize)
                    .ToList();
            }
            List<BookDto> requestDto = new();
            foreach (var book in books)
            {
                BookDto bookDto=mapper.Map<BookDto>(book);
                bookDto.Categories = context.BookCategories.Where(p => p.BookId == book.Id).Include(p => p.Category).Select(p => p.Category.Name).ToList();
                //var bookDto = new BookDto()
                //{
                //    Title = book.Title,
                //    ISBN = book.ISBN,
                //    Author = book.Author,
                //    CoverImageUrl = book.CoverImageUrl,
                //    CreateAt = book.CreateAt,
                //    IsActive = book.IsActive,
                //    IsDeleted = book.IsDeleted,
                //    Categories=context.BookCategories.Where(p=>p.BookId==book.Id).Include(p=>p.Category).Select(p=>p.Category.Name).ToList(),
                //    Price=book.Price,
                //    Quantity=book.Quantity,
                //    Summary = book.Summary
                //};
                requestDto.Add(bookDto);
            }
            return Ok(requestDto);
        }
    }
}