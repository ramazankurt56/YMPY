namespace HospitalServer.Entities.Dtos.Update;
public sealed record UpdateAppUserDto(
      string Id,
      string OldPassword,
      string NewPassword,
      string FirstName,
      string LastName,
      string UserName,
      string Email
      );
