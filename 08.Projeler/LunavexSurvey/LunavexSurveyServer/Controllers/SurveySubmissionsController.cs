using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Services;
using LunavexSurveyServer.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunavexSurveyServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class SurveySubmissionsController(ISurveySubmissionServer surveySubmissionServer) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateSurveySubmission(CreateSurveySubmissionDto request, CancellationToken cancellationToken)
    {
        var response = await surveySubmissionServer.CreateSurveySubmission(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllSurveySubmission(CancellationToken cancellationToken)
    {
        var response = await surveySubmissionServer.GetAll(cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    public async Task<IActionResult> DeleteSurveySubmission(Guid request, CancellationToken cancellationToken)
    {
        var response = await surveySubmissionServer.DeleteSurveySubmission(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpGet]
    public async Task<IActionResult> DeleteQuestionValue(Guid request, CancellationToken cancellationToken)
    {
        var response = await surveySubmissionServer.DeleteQuestionValue(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateSurveySubmission(UpdateSurveySubmissionDto request, CancellationToken cancellationToken)
    {
        var response = await surveySubmissionServer.UpdateSurveySubmission(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

}
