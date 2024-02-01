using EntityFrameworkCore.Relational.WebApi.Context;
using EntityFrameworkCore.Relational.WebApi.DTOs;
using EntityFrameworkCore.Relational.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Relational.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(CreateProductDto createProductDto)
        {

            Product? product = _context.Products.FirstOrDefault(p => p.Name == createProductDto.ProductName);
            if (product is not null)
            {
                return BadRequest(new { message = "Bu ürün adı daha önce kullanılmıştır." });
            }
            product = new()
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.ProductName
            };
            AdditionalProduct additionalProduct = new()
            {
                Description = createProductDto.ProductDescription,
                Price = createProductDto.ProductPrice
            };
            product.AdditionalProduct = additionalProduct;
            Category? category = _context.Categories.FirstOrDefault(p => p.Name == createProductDto.CategoryName);
            if (category is null)
            {
                category = new()
                {
                    Id = Guid.NewGuid(),
                    Name = createProductDto.CategoryName
                };
                product.Category = category;
            }
            else
            {
                product.CategoryId = category.Id;
            }

            _context.Add(product);
            _context.SaveChanges();
            return Ok(new { Id = product.Id });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //List<Product> products = _context.Products.Include(p=>p.AdditionalProduct).ToList();
            //List<Product> products = (from p in _context.Products
            //                          join ad in _context.AdditionalProducts on p.Id equals ad.ProductId
            //                          join c in _context.Categories on p.CategoryId equals c.Id
            //                          select new Product()
            //                          {
            //                              Id = p.Id,
            //                              Name = p.Name,
            //                              AdditionalProduct = ad,
            //                              Category = c,
            //                              CategoryId = p.CategoryId
            //                          }).ToList();


            var products = (from p in _context.Products
                            join ad in _context.AdditionalProducts on p.Id equals ad.ProductId
                            join c in _context.Categories on p.CategoryId equals c.Id
                            select new
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Price = ad.Price,
                                Category = c,

                                CategoryId = p.CategoryId
                            }).ToList();
            return Ok(products);
        }
    }
}
