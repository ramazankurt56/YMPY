namespace BookStoreServer.WebApi.DTOs
{
    public sealed record LoginResponseDto(string token ,int UserId,string UserName);
}
