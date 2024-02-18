using HospitalServer.Business.Services.Abstract;
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
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }
    [HttpPost]
    public IActionResult Update(UpdateDoctorDto request)
    {
        var result = doctorService.Update(request);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public IActionResult DeleteById(Guid id) //?id=asdasd => query Params || /asdasdasd => routing params
    {
        var result = doctorService.DeleteById(id);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        return BadRequest(result.Message);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = doctorService.GetAll();
        if (result.Success)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
}
