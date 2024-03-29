using Log.WebAPI.Context;
using Log.WebAPI.Models;
using System;

namespace Log.WebAPI.Filters;

public interface ILoggingService
{
    void Log(string methodName, string body, string userName);

}
public class DatabaseLoggingService : ILoggingService
{
    private readonly ApplicationDbContext _dbContext;

    public DatabaseLoggingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Log(string methodName, string body, string userName)
    {
        var logEntry = new LogEntry
        {
            MethodName = methodName,
            Body = body,
            UserName = userName
        };

        _dbContext.LogEntries.Add(logEntry);
        _dbContext.SaveChanges();
    }
}