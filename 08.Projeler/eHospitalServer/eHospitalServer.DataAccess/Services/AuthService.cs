﻿using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eHospitalServer.DataAccess.Services;
internal class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    JwtProvider jwtProvider,
    MailService mailService
    ) : IAuthService
{
    public async Task<Result<string>> ChangePassword(int passwordResetCode, string passwordRepeat, string newPassword, CancellationToken cancellationToken)
    {
        if (passwordRepeat != newPassword)
        {
            return Result<string>.Failure(500, "Passwords do not match");
        }
        User? user = await userManager.Users.Where(p => p.PasswordResetCode == passwordResetCode).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure(500, "Password confirm code is not available");
        }

        if (user.PasswordResetCodeUsed==true)
        {
            return Result<string>.Failure(500, "The code has been used before. Please create a new code.");
        }
        
        user.PasswordResetCodeUsed = true;
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        await userManager.UpdateAsync(user);

        return Result<string>.Succeed("password reset successfully");
    }

    public async Task<Result<string>> ConfirmVerificationEmail(int emailConfirmCode, CancellationToken cancellationToken)
    {
        User? user = await userManager.Users.Where(p => p.EmailConfirmCode == emailConfirmCode).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure(500, "Email confirm code is not available");
        }

        if (user.EmailConfirmed)
        {
            return Result<string>.Failure(500, "User email already confirmed");
        }

        user.EmailConfirmed = true;
        await userManager.UpdateAsync(user);

        return Result<string>.Succeed("Email verification is succeed");
    }
  

    public async Task<Result<LoginResponseDto>> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        User? user = await userManager.Users.Where(p => p.RefreshToken == refreshToken).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return (500, "Refresh token unavailable");
        }

        var loginResponse = await jwtProvider.CreateToken(user, false);


        return loginResponse;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        string emailOrUserName = request.EmailOrUserName.ToUpper();
        User? user = await userManager.Users
            .FirstOrDefaultAsync(p =>
            p.NormalizedUserName == emailOrUserName ||
            p.NormalizedEmail == emailOrUserName,
            cancellationToken);

        if (user is null)
        {
            return (500, "User not found");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
            if (timeSpan is not null)
                return (500, $"Your user has been locked for {Math.Ceiling(timeSpan.Value.TotalMinutes)} minutes due to entering the wrong password 3 times.");
            else
                return (500, "Your user has been locked out for 5 minutes due to entering the wrong password 3 times.");
        }

        if (signInResult.IsNotAllowed)
        {
            return (500, "Your e-mail address is not confirmed");
        }

        if (!signInResult.Succeeded)
        {
            return (500, "Your password is wrong");
        }

        var loginResponse = await jwtProvider.CreateToken(user, request.RememberMe);


        return loginResponse;
    }

   

    public async Task<Result<string>> SendConfirmEmailAsync(string email, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result<string>.Failure(500, "User cannot be found");
        }

        if (user.EmailConfirmed)
        {
            return Result<string>.Failure(500, "User email already confirmed");
        }

        var dif = DateTime.UtcNow - user.EmailConfirmCodeSendDate;


        if (dif.TotalMinutes < 3)
        {
            return Result<string>.Failure(500, "Verification mail is send every 3 minutes.");
        }

        user.EmailConfirmCodeSendDate = DateTime.UtcNow;

        await userManager.UpdateAsync(user);

        #region Send Mail Verification
        string subject = "Verification Mail";
        string header = "Email Confirmation Code";
        string text = "Please use the following code to confirm your email";
        string body = CreateEmailBody(user.EmailConfirmCode.ToString(), header, text);

        var stringEmailResponse = await mailService.SendEmailAsync(user.Email ?? "", subject, body);
        #endregion

        return Result<string>.Succeed("Verification mail is sent successfully");
    }

    public async Task<Result<string>> SendPasswordResetEmailAsync(string email, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result<string>.Failure(500, "User cannot be found");
        }

        var dif = DateTime.UtcNow - user.PasswordResetCodeSendDate;


        if (dif.TotalMinutes < 5)
        {
            return Result<string>.Failure(500, "Verification mail is send every 5 minutes.");
        }
        Random random = new();

        bool isPasswordResetCodeExists = true;
        while (isPasswordResetCodeExists)
        {
            user.PasswordResetCode = random.Next(100000, 999999);
            if (!userManager.Users.Any(p => p.PasswordResetCode == user.PasswordResetCode))
            {
                isPasswordResetCodeExists = false;
                await userManager.UpdateAsync(user);
            }
        }
        user.PasswordResetCodeSendDate = DateTime.UtcNow;

        await userManager.UpdateAsync(user);

        #region Send Mail Verification
        string subject = "Password Reset Mail";
        string header = "Passwor Reset Code";
        string test = "Please use the following code for password reset\r\n";
        string body = CreateEmailBody(user.PasswordResetCode.ToString(), header,test);

        var stringEmailResponse = await mailService.SendEmailAsync(user.Email ?? "", subject, body);
        #endregion

        return Result<string>.Succeed("Verification mail is sent successfully");
    }

    private string CreateEmailBody(string emailConfirmCode,string header,string test)
    {
        string body = @"
                                <!DOCTYPE html>
                                <html lang=""en"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>"+header+ @"</title>
                                    <style>
                                        /* Stil özellikleri */
                                        body {
                                            font-family: Arial, sans-serif;
                                            background-color: #f4f4f4;
                                            padding: 20px;
                                        }
                                        .container {
                                            max-width: 600px;
                                            margin: 0 auto;
                                            background-color: #fff;
                                            border-radius: 10px;
                                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                            padding: 20px;
                                            text-align: center;
                                            justify-content: center;
                                            align-items: center;
                                        }
                                        .confirmation-code {
                                            display: flex;
                                            justify-content: center;
                                            align-items: center;
                                            margin-top: 20px;
                                            margin-left: 50px;
                                        }
                                        .digit-container {
                                            display: flex;
                                            width: auto; /* Kutu genişliğini artır */
                                            height: auto;
                                            border: 2px solid #007bff;
                                            border-radius: 10px;
                                            margin-right: 10px;
                                            font-size: 55px;
                                            font-weight: bold;
                                            color: #007bff;
                                            text-align: center;
                                            inherit: text-align;
                                        }
                                    </style>
                                </head>
                                <body>
                                    <div class=""container"">
                                        <h2 style=""color: #007bff;"">"+header+ @"</h2>
                                        <p>"+test+@":</p>
                                        <div class=""confirmation-code"">
                                            <!-- Her bir rakam için ayrı bir kutu oluşturuluyor -->
                                            <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[0] + @" </div></div>
                                            <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[1] + @" </div></div>
                                            <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[2] + @" </div></div>
                                            <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[3] + @" </div></div>
                                            <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[4] + @" </div></div>
                                            <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[5] + @" </div></div>
                                        </div>
                                        <p style=""margin-top: 20px;"">This code will expire in 10 minutes.</p>
                                    </div>
                                </body>
                                </html>
                                ";

        return body;
    }
 
}