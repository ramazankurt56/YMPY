using HospitalServer.Business.Services.Abstract;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class PrescriptionsController(IPrescriptionService prescriptionService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreatePrescriptionDto request)
    {

        var result = prescriptionService.Create(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }
    [HttpPost]
    public IActionResult Update(UpdatePrescriptionDto request)
    {
        var result = prescriptionService.Update(request);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id)
    {
        var result = prescriptionService.DeleteById(id);
        if (result.Success)
        {
            return Ok(new { result.Message });
        }
        return BadRequest(new { result.Message });
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(PaginationRequestDto request)
    {
        var response = await prescriptionService.GetAll(request);
        return Ok(response);
    }
}
