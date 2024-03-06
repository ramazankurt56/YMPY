using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using TS.Result;

namespace LunavexSurveyServer.Business.Services;

public interface IQuestionService
{
    Task<Result<string>> CreateQuestion(List<CreateQuestionDto> request,Guid surveyId, CancellationToken cancellationToken);
    Task<Result<string>> UpdateQuestion(UpdateQuestionDto request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteQuestion(Guid request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteChoice(Guid request, CancellationToken cancellationToken);
    Task<Result<List<Question>>> GetAll(CancellationToken cancellationToken);
    Task<Result<Question>> GetByIdQuestion(Guid id, CancellationToken cancellationToken);
    Task<Result<Choice>> GetByIdChoice(Guid id, CancellationToken cancellationToken);
}
