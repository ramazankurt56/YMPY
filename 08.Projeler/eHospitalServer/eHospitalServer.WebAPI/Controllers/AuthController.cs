using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace eHospitalServer.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

  
}
