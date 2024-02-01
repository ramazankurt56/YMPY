using EntityFrameworkCore.First.WebApi.Context;
using EntityFrameworkCore.First.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EntityFrameworkCore.First.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodosController(ApplicationDbContext context) : ControllerBase//primary consructor
    {

        [HttpPost]//create
        public IActionResult Add(AddTodoDb addTodoDb)
        {
            Todo todo = new Todo
            {
                Work = addTodoDb.Work,
                DateToBeCompleted = addTodoDb.DateToBeCompleted,
                CreatedDate = DateTime.Now
            };

            context.Add(todo);
            context.SaveChanges();
            return Ok(new { Id = todo.Id });
        }
        [HttpGet]//read
        public IActionResult GetAll()
        {

            IEnumerable<Todo> todos = context.Todos.OrderByDescending(p => p.Work).ToList();
            return Ok(todos);
        }
        [HttpGet("{id}")]//Read
        public IActionResult GetById(int id)
        {

            Todo? todo = context.Todos.Find(id);
            if (todo is null)
            {
                return BadRequest("Kayıt bulunamadı");
            }

            return Ok(todo);
        }
        [HttpGet]//read
        public IActionResult GetByWork(string work)
        {
            IEnumerable<Todo> todos = context.Todos.Where(p => p.Work.ToLower().Trim().Contains(work.ToLower())).ToList();
            return Ok(todos);
        }
        [HttpGet]//read
        public IActionResult GetByExpression(Expression<Func<Todo, bool>> expression)
        {
            IEnumerable<Todo> todos = context.Todos.Where(expression).ToList();
            return Ok(todos);
        }
        [HttpPost("{id}")]//update
        public IActionResult Update(int id, UpdateTodoDb updateTodoDb)
        {
            Todo? todo = context.Todos.Find(id);
            if (todo is null)
            {
                return BadRequest("Kayıt bulunamadı");
            }
            todo.Work = updateTodoDb.Work;
            todo.DateToBeCompleted= updateTodoDb.DateToBeCompleted;

            //dbContext.Update(todo); tracking mekanizması olduğundan dolayı buna gerek yok
            context.SaveChanges();
            return Ok(new { Id = todo.Id ,Message= "Güncelleme başarılı" });
        }
        [HttpGet("{id}")]//update
        public IActionResult ChangeCompletedStatus(int id)
        {
            Todo? todo= context.Todos.Find(id);
            if (todo is null)
            {
                return BadRequest("Kayıt bulunamadı");
            }
            todo.IsCompleted = !todo.IsCompleted;
            todo.DateCompleted=todo.IsCompleted == false ? null : DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        [HttpGet("{id}")]//Remove
        public IActionResult RemoveById(int id)
        {
            Todo? todo= context.Todos.Find(id);
            if(todo is null)
            {
                return BadRequest("Kayıt bulunamadı");
            }
            context.Remove(todo);
            context.SaveChanges();
            return NoContent();
        }
    }
}
public class AddTodoDb
{
    public string Work { get; set; } = string.Empty;
    public DateTime DateToBeCompleted { get; set; }
}
public sealed class UpdateTodoDb
{
    public string Work { get; set; } = string.Empty;
    public DateTime DateToBeCompleted { get; set; }

}
