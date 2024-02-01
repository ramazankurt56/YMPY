namespace BookStoreServer.WebApi.DTOs
{
    public sealed record LoginDto(string UsernameOrEmail,
        string Password);

}
