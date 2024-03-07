namespace eHospitalServer.DataAccess.Options;
public sealed class EmailOptions
{
    public string SMTP { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool SSL { get; set; }
}
