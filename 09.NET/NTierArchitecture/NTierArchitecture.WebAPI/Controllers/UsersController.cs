using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.WebAPI.Service;
using System.Data;

namespace NTierArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  //  [Authorize(AuthenticationSchemes = "Bearer")] //attribute
    public class UsersController(UserManager<AppUser> _userManager, ApplicationDbContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(CreateAppUserDto user)
        {
            // Identity kullanarak kullanıcıyı oluşturun
            var userEntity = new AppUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };

                
           

            var result = await _userManager.CreateAsync(userEntity, user.Password);

            if (result.Succeeded)
            {

                await context.SaveChangesAsync();
               
            }
            else
            {
                throw new Exception("Kullanıcı kaydedilemedi!");
            }
            string token = JwtService.CreatToken(userEntity);
            return Ok(new { AccessToken = token });
        }
    }
}
