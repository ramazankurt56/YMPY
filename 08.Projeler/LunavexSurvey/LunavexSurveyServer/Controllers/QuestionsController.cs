using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Services;
using LunavexSurveyServer.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunavexSurveyServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class QuestionsController(IQuestionService questionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateQuestion(CreateQuestionDto request, CancellationToken cancellationToken)
    {
        //var response = await questionService.CreateQuestion(request, cancellationToken);

        //return StatusCode(response.StatusCode, response);
        return NoContent();
    }
    [HttpGet]
    public async Task<IActionResult> GetAllQuestion(CancellationToken cancellationToken)
    {
        var response = await questionService.GetAll(cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    public async Task<IActionResult> DeleteQuestion(Guid request, CancellationToken cancellationToken)
    {
        var response = await questionService.DeleteQuestion(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    public async Task<IActionResult> DeleteChoice(Guid request, CancellationToken cancellationToken)
    {
        var response = await questionService.DeleteChoice(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateQuestion(UpdateQuestionDto request, CancellationToken cancellationToken)
    {
        var response = await questionService.UpdateQuestion(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
