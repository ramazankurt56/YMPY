using HospitalServer.Business.Services.Abstract;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateAppointmentDto request)
    {

        var result = appointmentService.Create(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpPost]
    public IActionResult Update(UpdateAppointmentDto request)
    {
        var result = appointmentService.Update(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        var result = appointmentService.DeleteById(id);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }


    [HttpPost]
    public async Task<IActionResult> GetAll(PaginationRequestDto request)
    {
        var response = await appointmentService.GetAll(request);
        return Ok(response);
    }
}
