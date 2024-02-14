using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.Business.Services;
using NTierArchitecture.Entities.DTOs;
namespace NTierArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController(AuthService authService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(CreateAppUserDto request)
        {
            var response = await authService.RegisterAsync(request);

            return Ok(new { Response = response });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(UpdateAppUserDto request)
        {
            var response = await authService.UpdateAsync(request);

            return Ok(new { Response = response });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var response = await authService.LoginAsync(request);

            return Ok(new { Response = response });
        }
    }

}
