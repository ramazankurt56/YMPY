using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services;
public class AuthService(UserManager<AppUser> _userManager,
    IJwtProvider jwtProvider)
{
    public async Task<string> LoginAsync(LoginDto request)
    {
        AppUser? user =
            await _userManager.Users
            .FirstOrDefaultAsync(p =>
                    p.Email == request.UserNameOrEmail ||
                    p.UserName == request.UserNameOrEmail);

        if (user is null)
        {
            throw new ArgumentException("Kullanıcı bulunamadı");
        }

        bool result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
        {
            throw new ArgumentException("Şifre yanlış");
        }

        return jwtProvider.CreateToken();
    }
    public async Task<string> UpdateAsync(UpdateAppUserDto request)
    {
        AppUser? appUser = await _userManager.FindByIdAsync(request.Id);
        if (appUser is null)
        {
            throw new ArgumentException( "Kullanıcı bulunamadı!" );
        }
        if (!string.IsNullOrEmpty(request.NewPassword))
        {
            var passwordValidator = new PasswordValidator<AppUser>();
            var validationResult = await passwordValidator.ValidateAsync(_userManager, appUser, request.NewPassword);
            if (!validationResult.Succeeded)
            {
                var errors = validationResult.Errors.Select(e => e.Description);
                throw new ArgumentException("Parola güncelleme işlemi başarısız!");
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(appUser, request.OldPassword, request.NewPassword);
            if (!resultChangePassword.Succeeded)
            {
                throw new ArgumentException("Parola güncelleme işlemi başarısız!" );
            }
        }

        appUser.FirstName = request.FirstName;
        appUser.LastName = request.LastName;
        appUser.Email = request.Email;
        appUser.UserName = request.UserName;

        var result = await _userManager.UpdateAsync(appUser);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Güncelleme başarısız");
        }
        return "Güncelleme Başarılı";
    }
    public async Task<string> RegisterAsync(CreateAppUserDto request)
    {
        var userEntity = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName
        };

        var result = await _userManager.CreateAsync(userEntity, request.Password);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Kayıt işlemi başarısız");
        }

        return "Kayıt işlemi başarılıdır.";
    }
}
