using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using TS.Result;

namespace LunavexSurveyServer.Business.Services;

public interface ISurveyService
{
    Task<Result<string>> CreateSurvey(CreateSurveyDto request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateSurvey(UpdateSurveyDto request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteSurvey(Guid request, CancellationToken cancellationToken);
    Task<Result<List<Survey>>> GetAll(CancellationToken cancellationToken);
    Task<Result<Survey>> GetById(Guid id, CancellationToken cancellationToken);
}
