using eHospitalServer.Entities.DTOs;
using TS.Result;

namespace eHospitalServer.Business.Services;
public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
    Task<Result<LoginResponseDto>> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<Result<string>> SendConfirmEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result<string>> ConfirmVerificationEmail(int emailConfirmCode, CancellationToken cancellationToken);
    Task<Result<string>> SendPasswordResetEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result<string>> ChangePassword(int passwordResetCode,string oldPassword,string newPassword, CancellationToken cancellationToken);
}