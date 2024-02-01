namespace ITDeskServer.DTOs
{
    public sealed record LoginDto(string UserNameOrMail,
        string Password,
        bool IsRememberMe=false);
}
