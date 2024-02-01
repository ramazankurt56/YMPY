using Azure.Core;
using ITDeskServer.DTOs;
using ITDeskServer.Models;
using ITDeskServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITDeskServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(UserManager<User> userManager, SignInManager<User> signInManager) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByNameAsync(request.UserNameOrMail);
            if (user is null)
            {
                user = await userManager.FindByEmailAsync(request.UserNameOrMail);
                if (user is null)
                {
                    return BadRequest(new { Message = "Kullanıcı bulunamadı" });
                }
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            if (result.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
                if (timeSpan is not null)
                {
                    return BadRequest(new { Message = $"Kullanıcınız 3 kere yanlış şifre girdiğinden dolayı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika kilitlenmiştirç" });
                }
                else
                {

                    return BadRequest(new { Message = $"Kullanıcınız 3 kere yanlış şifre girdiğinden dolayı 15 dakika kilitlenmiştir" });
                }
            }
            if (result.IsNotAllowed)
            {
                return BadRequest(new { Message = "Mail adresiniz onaylı değildir" });
            }
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Şifreniz Yanlış" });
            }

            JwtService jwtService = new();
            string token = jwtService.CreateToken(user, request.IsRememberMe);
            return Ok(new {AccessToken=token});

            #region Check
            //private async Task CheckPassword(User user, LoginDto request)
            //{
            //    if (user.WrongTryCount == 3)
            //    {
            //        TimeSpan timeSpan = user.LastWrongTry.Date - DateTime.Now.Date;
            //        if (timeSpan.TotalDays <= 0)
            //        {
            //            user.WrongTryCount = 0;
            //            await userManager.UpdateAsync(user);
            //        }
            //        else
            //        {
            //            timeSpan = user.LockDate - DateTime.Now;
            //            if (timeSpan.TotalMinutes <= 0)
            //            {
            //                user.WrongTryCount = 0;
            //                await userManager.UpdateAsync(user);
            //            }
            //            else
            //            {
            //                // return BadRequest(new { ErrorMessage = $"Şifrenizi yanlış girdiğinizden dolayı kullanıcı kilitlendi. {Math.Ceiling(timeSpan.TotalMinutes)} dakika daha beklemelisiniz." });
            //            }
            //        }

            //    }
            //    var checkPasswordIsCurrect = await userManager.CheckPasswordAsync(user, request.Password);
            //    if (!checkPasswordIsCurrect)
            //    {

            //        TimeSpan timeSpan = user.LastWrongTry.Date - DateTime.Now.Date;
            //        if (timeSpan.TotalDays <= 0)
            //        {
            //            user.WrongTryCount = 0;
            //            await userManager.UpdateAsync(user);
            //        }
            //        if (user.WrongTryCount < 3)
            //        {
            //            user.WrongTryCount++;
            //            user.LastWrongTry = DateTime.Now;
            //            await userManager.UpdateAsync(user);
            //        }

            //        if (user.WrongTryCount == 3)
            //        {

            //            user.LastWrongTry = DateTime.Now;
            //            user.LockDate = DateTime.Now.AddMinutes(15);
            //            await userManager.UpdateAsync(user);

            //            //  return BadRequest(new { Message = "Üç kere şifrenizi yanlış girdiğiniz için kullanıcınız 15 dakika kilitlendi." });
            //        }
            //        user.WrongTryCount = 0;
            //        await userManager.UpdateAsync(user);
            //        //return BadRequest(new { Message = "Şifre Yanlış!" });
            //    }
            //}

            #endregion
        }

    }
}
