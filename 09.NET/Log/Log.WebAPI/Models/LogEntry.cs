namespace Log.WebAPI.Models;

public class LogEntry
{
    public int Id { get; set; }
    public string MethodName { get; set; }=string.Empty;
    public string Body { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
