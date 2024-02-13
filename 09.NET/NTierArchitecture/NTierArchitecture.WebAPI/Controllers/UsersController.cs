using AutoMapper;
using Azure.Core;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Business.Validator;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.WebAPI.Service;
using System.Data;

namespace NTierArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(UserManager<AppUser> _userManager, SignInManager<AppUser> signInManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(CreateAppUserDto request)
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
                return BadRequest();
            }

            return Ok(new { Message = "Kayıt işlemi başarılıdır." });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(UpdateAppUserDto request)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(request.Id);
            if (appUser is null)
            {            
                    return BadRequest(new { Message = "Kullanıcı bulunamadı!" });
            }
            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var passwordValidator = new PasswordValidator<AppUser>();
                var validationResult = await passwordValidator.ValidateAsync(_userManager, appUser, request.NewPassword);
                if (!validationResult.Succeeded)
                {
                    var errors = validationResult.Errors.Select(e => e.Description);
                    return BadRequest(new { Message = "Parola güncelleme işlemi başarısız!", Errors = errors });
                }

                var resultChangePassword = await _userManager.ChangePasswordAsync(appUser, request.OldPassword, request.NewPassword);
                if (!resultChangePassword.Succeeded)
                {
                    return BadRequest(new { Message = "Parola güncelleme işlemi başarısız!" });
                }
            }

            appUser.FirstName= request.FirstName;
            appUser.LastName = request.LastName;
            appUser.Email = request.Email;
            appUser.UserName = request.UserName;

            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Güncelleme işlemi başarılıdır." });
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginDto request, CancellationToken cancellationToken)
        {
            LoginDtoValidator validator = new();
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                // RETURN UnprocessableEntity();
                return StatusCode(422, validationResult.Errors.Select(s => s.ErrorMessage));
            }

            AppUser? appUser = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (appUser is null)
            {
                appUser = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
                if (appUser is null)
                {
                    return BadRequest(new { Message = "Kullanıcı bulunamadı!" });
                }
            }

            var result = await signInManager.CheckPasswordSignInAsync(appUser, request.Password, true);

            if (result.IsLockedOut)
            {
                TimeSpan? timeSpan = appUser.LockoutEnd - DateTime.UtcNow;
                if (timeSpan is not null)
                    return BadRequest(new
                    {
                        Message = $"Kullanıcınız 3 kere yanlış şifre girşinden dolayı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika kitlenmiştir"
                    });
                else
                    return BadRequest(new { Message = $"Kullanıcınız 3 kere yanlış şifre girşinden dolayı 15 dakika kitlenmiştir" });
            }

            if (result.IsNotAllowed)
            {
                return BadRequest(new { Message = "Mail adresiniz onaylı değil!" });
            }

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Şifreniz yanlış" });
            }


            string token = JwtService.CreatToken(appUser);
            return Ok(new { AccessToken = token });
        }
    }

}
