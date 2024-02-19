
namespace HospitalServer.Entities.Dtos;
public sealed record PaginationRequestDto(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = ""
    );