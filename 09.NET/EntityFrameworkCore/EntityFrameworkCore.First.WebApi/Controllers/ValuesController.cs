using EntityFrameworkCore.First.WebApi.Context;
using EntityFrameworkCore.First.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCore.First.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpDelete]
        public IActionResult Delete(string work,DateTime dateTime)
        {

            Todo todo = new Todo
            {
                Work = work,
                DateToBeCompleted = dateTime,
                CreatedDate = DateTime.Now
            };
            ApplicationDbContext dbContext = new ApplicationDbContext();
            dbContext.Add(todo);
            dbContext.SaveChanges();
            return Ok(new { message="İşlem başarılı" });
        }
        [HttpPut]
        public IActionResult Put(int Id)
        {
            return Ok(new { message = "Güncelleme İşlemi başarılı" });
        }
    }
}
