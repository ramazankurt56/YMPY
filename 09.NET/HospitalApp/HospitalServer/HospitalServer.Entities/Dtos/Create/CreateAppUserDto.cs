namespace HospitalServer.Entities.Dtos.Create;
public sealed record CreateAppUserDto(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string Password
        );
