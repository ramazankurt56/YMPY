using AutoMapper;
using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Enum;
using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace eHospitalServer.DataAccess.Services;
internal sealed class UserService(
    UserManager<User> userManager,
    IMapper mapper,
    MailService mailService) : IUserService
{
    public async Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken)
    {
        if (request.Email is not null)
        {
            bool isEmailExists = await userManager.Users.AnyAsync(p => p.Email == request.Email);
            if (isEmailExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Email already has taken");
            }
        }

        if (request.UserName is not null)
        {
            bool isUserNameExists = await userManager.Users.AnyAsync(p => p.UserName == request.UserName);
            if (isUserNameExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "User name already has taken");
            }
        }

        if (request.IdentityNumber != "11111111111")
        {
            bool isIdentityNumberExists = await userManager.Users.AnyAsync(p => p.IdentityNumber == request.IdentityNumber);
            if (isIdentityNumberExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Identity number already exists");
            }
        }

        User user = mapper.Map<User>(request);

        Random random = new();
                    
        user.EmailConfirmCode = random.Next(100000, 999999);
        user.EmailConfirmCodeSendDate = DateTime.UtcNow;

        if (request.Specialty is not null)
        {
            user.DoctorDetail = new DoctorDetail()
            {
                Specialty = (Specialty)request.Specialty,
                WorkingDays = request.WorkingDays ?? new()
            };
        }

        IdentityResult result;
        if (request.Password is not null)
        {
            result = await userManager.CreateAsync(user, request.Password);
        }
        else
        {
            result = await userManager.CreateAsync(user);
        }


        if (result.Succeeded)
        {
            if (request.Email is not null)
            {
                string body = "";
                string span = "";
                string code = user.EmailConfirmCode.ToString();
                foreach (char digit in code)
                {
                    span += $"<span style='display: inline-block; width: 30px; height: 30px; border: 2px solid #3498db; text-align: center; margin-right: 5px; border-radius: 10px; line-height: 30px; vertical-align: middle;  color: #3498db;'>{digit}</span>";
                }

                body += @"

                        <div style='display: flex; justify-content: center; align-items: center; height: 100vh; animation: fadeIn 1s ease-in-out;'>

                        <div style='width: 400px; padding: 20px; background-color: #fff; 
                        border: 2px solid #000; border-radius: 10px; text-align: center; 
                        animation: slideIn 0.5s ease-in-out;'>

                        <h1 style='color: #333; margin-bottom: 20px; font-size: 24px;'>Email Onay Kodunuz</h1>

                        <div style='display: block; margin-bottom: 20px;'>" + span + @"</div>

                        <p style='color: #666; font-size: 16px; line-height: 1.6; margin-bottom: 15px;'>
                        <strong>Kod oluşturulma tarihi:</strong> " + user.EmailConfirmCodeSendDate + @"</p>

                        <p style='color: #666; font-size: 16px; line-height: 1.6; margin-bottom: 15px;'>
                        <strong>Size gönderdiğimiz bu e-posta, hesabınızı doğrulamak için kullanmanız gereken onay kodunu içermektedir.
                        Bu onay kodu, hesabınıza erişim sağlamak ve kimlik doğrulama sürecini tamamlamak için kullanılacaktır.</strong></p>

                        <p style='color: #666; font-size: 16px; line-height: 1.6; margin-bottom: 15px;'>
                        <strong>Bu onay kodunu hiçbir şekilde başkalarıyla paylaşmayın ve güvenli bir yerde saklayın. Şirketimiz, hiçbir durumda sizden 
                        onay kodunuzu istemeyecektir. Eğer başka biri onay kodunuzu talep ederse veya paylaşmanızı isterse, lütfen şüpheli bir durum 
                        olarak değerlendirin ve bu durumu bize bildirin.</strong></p>

                        <button style='background-color: #3498db; color: #fff; border: none; padding: 10px 20px; border-radius: 5px; 
                        cursor: pointer; font-size: 16px;transition: background-color 0.3s ease-in-out;' >Hesabı Doğrula</button>

                        </div>
                        </div>";

                string emailContent = await mailService.SendEmailAsync(request.Email, "Onay Kodu", $@"
                <body>{body}</body>");

                // Diğer içerikler ve mail gönderme işlemi
            }
            return Result<string>.Succeed("User create is successful");


        }

        return Result<string>.Failure(500, result.Errors.Select(s => s.Description).ToList());
    }
}