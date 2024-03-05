using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Services;
using LunavexSurveyServer.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunavexSurveyServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class SurveysController(ISurveyService surveyService) : ControllerBase
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
    [HttpGet]
    public async Task<IActionResult> DeleteSurvey(Guid request,CancellationToken cancellationToken)
    {
        var response = await surveyService.DeleteSurvey(request,cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateSurvey(UpdateSurveyDto request, CancellationToken cancellationToken)
    {
        var response = await surveyService.UpdateSurvey(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
