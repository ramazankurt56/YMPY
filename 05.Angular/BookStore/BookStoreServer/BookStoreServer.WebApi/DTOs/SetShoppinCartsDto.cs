using BookStoreServer.WebApi.ValueObjects;

namespace BookStoreServer.WebApi.DTOs
{
    public sealed record SetShoppinCartsDto(
        int BookId,
        int UserId,
        Money Price,
        int Quantity);
}
