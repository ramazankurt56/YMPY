namespace BookStoreServer.WebApi.DTOs
{
    public sealed record  RequestDto(
        int PageSize,
        string Search,
        int? CategoryId);
}
