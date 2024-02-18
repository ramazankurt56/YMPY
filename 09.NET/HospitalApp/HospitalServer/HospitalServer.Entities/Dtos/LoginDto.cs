namespace HospitalServer.Entities.Dtos;
public sealed record LoginDto(
      string UserNameOrEmail,
      string Password);
