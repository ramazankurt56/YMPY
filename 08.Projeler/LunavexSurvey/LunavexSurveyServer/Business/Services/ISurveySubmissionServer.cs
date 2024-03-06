using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using TS.Result;

namespace LunavexSurveyServer.Business.Services;

public interface ISurveySubmissionServer
{
    Task<Result<string>> CreateSurveySubmission(CreateSurveySubmissionDto request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateSurveySubmission(UpdateSurveySubmissionDto request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteSurveySubmission(Guid request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteQuestionValue(Guid request, CancellationToken cancellationToken);
    Task<Result<List<SurveySubmission>>> GetAll(CancellationToken cancellationToken);
    Task<Result<SurveySubmission>> GetByIdSurveySubmission(Guid id, CancellationToken cancellationToken);
    Task<Result<QuestionValue>> GetByIdQuestionValue(Guid id, CancellationToken cancellationToken);
}
