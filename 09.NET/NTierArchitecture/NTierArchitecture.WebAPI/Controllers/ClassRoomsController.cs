using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.Business;
using NTierArchitecture.Business.Services;
using NTierArchitecture.Entities.DTOs;

namespace NTierArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassRoomsController(IClassRoomService classRoomService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(CreateClassRoomDto request)
        {
            string message = classRoomService.Create(request);
            return Ok(new { Message = message });
        }

        [HttpPost]
        public IActionResult Update(UpdateClassRoomDto request)
        {
            string message = classRoomService.Update(request);
            return Ok(new { Message = message });
        }

        [HttpGet("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            string message = classRoomService.DeleteById(id);
            return Ok(new { Message = message });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = classRoomService.GetAll();
            return Ok(response);
        }
    }
}
