using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using TS.Result;

namespace LunavexSurveyServer.Business.Services;

public interface IQuestionValueService
{
    Task<Result<string>> CreateQuestionValue(CreateQuestionValueDto request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateQuestionValue(UpdateQuestionValueDto request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteQuestionValue(Guid request, CancellationToken cancellationToken);
    Task<Result<List<QuestionValue>>> GetAll(CancellationToken cancellationToken);
    Task<Result<QuestionValue>> GetByIdQuestionValue(Guid id, CancellationToken cancellationToken);
}
