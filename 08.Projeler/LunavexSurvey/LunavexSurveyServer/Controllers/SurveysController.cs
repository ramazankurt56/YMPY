using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Services;
using LunavexSurveyServer.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunavexSurveyServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class SurveysController(ISurveyService surveyService,IQuestionService questionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSurvey(CreateSurveyDto request,CancellationToken cancellationToken)
    {
        var response = await surveyService.CreateSurvey(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSurvey(CancellationToken cancellationToken)
    {
        var response = await surveyService.GetAll(cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> DeleteSurvey(Guid id, CancellationToken cancellationToken)
    {
        var response = await surveyService.DeleteSurvey(id, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> DeleteQuestion(Guid id, CancellationToken cancellationToken)
    {
        var response = await questionService.DeleteQuestion(id, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBySurveyId(Guid id, CancellationToken cancellationToken)
    {
        var response = await surveyService.GetById(id, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateSurvey(UpdateSurveyDto request, CancellationToken cancellationToken)
    {
        var response = await surveyService.UpdateSurvey(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
