﻿using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.WebAPI.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace eHospitalServer.WebAPI.Controllers;
public class AuthController(
    IAuthService authService) : ApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetTokenByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var response = await authService.GetTokenByRefreshTokenAsync(refreshToken, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SendConfirmMail(string email, CancellationToken cancellationToken)
    {
        var response = await authService.SendConfirmEmailAsync(email, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(int emailConfirmCode, CancellationToken cancellationToken)
    {
        var response = await authService.ConfirmVerificationEmail(emailConfirmCode, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SendPasswordReset(string email, CancellationToken cancellationToken)
    {
        var response = await authService.SendPasswordResetEmailAsync(email, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ChangePassword(int passwordResetCode, string passwordRepeat, string newPassword, CancellationToken cancellationToken)
    {
        var response = await authService.ChangePassword(passwordResetCode, passwordRepeat, newPassword, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}