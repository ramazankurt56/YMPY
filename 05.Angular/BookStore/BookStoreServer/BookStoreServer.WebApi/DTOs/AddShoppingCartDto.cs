using BookStoreServer.WebApi.ValueObjects;

namespace BookStoreServer.WebApi.DTOs
{
    public sealed record AddShoppingCartDto(
         int BookId,
         Money Price,
         int Quantity,
         int UserId);
}

