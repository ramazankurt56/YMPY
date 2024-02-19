using HospitalServer.Business.Services.Abstract;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class MedicationsController(IMedicationService medicationService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateMedicationDto request)
    {

        var result = medicationService.Create(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpPost]
    public IActionResult Update(UpdateMedicationDto request)
    {
        var result = medicationService.Update(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        var result = medicationService.DeleteById(id);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(PaginationRequestDto request)
    {
        var response = await medicationService.GetAll(request);
        return Ok(response);
    }
}
