using EntityFrameworkCore.First.WebApi.Context;
using EntityFrameworkCore.First.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EntityFrameworkCore.First.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LINQController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            //ApplicationDbContext context = new();
            //List<Todo> list = context.Todos.ToList();
            

            //List<string> name = new List<string>();
            //name.Add("Ramazan");
            //name.Remove("Ramazan");
            //List<string> name2 = new List<string>();
            //name2.Where(p => p == "Ramazan").ToList();
            //name.FirstOrDefault(p => p == "Ramazan");
            //name.SingleOrDefault(p => p == "Ramazan");
            //name.Where(p => p == "Ramazan").SingleOrDefault();

            //List<Example> example = new();
            //var newExample = example.Select(s => new Example2()
            //{
            //    Name=string.Join("",s.FirstName,s.LastName),
            //    Age=s.Age,
            //    City="Siirt"
            //}).ToList();
            //int result2=example.Sum(p => p.Age);
            //int result=example.Count();

            //ApplicationDbContext context = new ApplicationDbContext();
            //var todos=context.Todos.AsQueryable();
            //todos.Where(p => p.IsCompleted);

            return Ok();
        }
    }
}

public class Example
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Age { get; set; }

}
public class Example2
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}