using Azure.Core;
using HospitalServer.Business.Services.Abstract;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class DoctorsController(IDoctorService doctorService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateDoctorDto request)
    {

        var result = doctorService.Create(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpPost]
    public IActionResult Update(UpdateDoctorDto request)
    {
        var result = doctorService.Update(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpGet("{id}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var response = doctorService.GetDoctorById(id);
        if (response.Success)
        {
            return Ok(response.Data );
        }
        return BadRequest(new { response.Message });
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        var result = doctorService.DeleteById(id);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(PaginationRequestDto request)
    {
        var response = await doctorService.GetAll(request);
        return Ok(response);
    }
}
