namespace NTierArchitecture.Entities.DTOs
{
    public sealed record LoginDto(
       string UserNameOrEmail,
       string Password);
}
