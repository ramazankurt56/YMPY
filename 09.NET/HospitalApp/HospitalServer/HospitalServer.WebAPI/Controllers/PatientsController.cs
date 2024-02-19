using HospitalServer.Business.Services.Abstract;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using Microsoft.AspNetCore.Mvc;

namespace HospitalServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreatePatientDto request)
    {

        var result = patientService.Create(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpPost]
    public IActionResult Update(UpdatePatientDto request)
    {
        var result = patientService.Update(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpGet("{id}")]
    public IActionResult GetPatientById(Guid id)
    {
        var response = patientService.GetPatientById(id);
        if (response.Success)
        {
            return Ok(response.Data);
        }
        return BadRequest(new { response.Message });
    }
    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        var result = patientService.DeleteById(id);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpPost]
    public async Task<IActionResult> GetAll(PaginationRequestDto request)
    {
        var response = await patientService.GetAll(request);
        return Ok(response);
    }
}
