using Log.WebAPI.Context;
using Log.WebAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace Log.WebAPI.Filters;

public sealed class LogFilterAttribute() : Attribute, IActionFilter
{
    private readonly ApplicationDbContext _dbContext;

    public LogFilterAttribute(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //var dbContext = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();
        LogEntry log = new()
        {
            MethodName = context.HttpContext.Request.Path.Value!,
            Body = JsonSerializer.Serialize(context.ActionArguments.First().Value),
            UserName = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            Date = DateTime.Now,
        };
        //dbContext!.LogEntries.Add(log);
        //dbContext.SaveChanges();
    }
}
