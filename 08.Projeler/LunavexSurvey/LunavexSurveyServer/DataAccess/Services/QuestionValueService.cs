using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Context;
using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace LunavexSurveyServer.DataAccess.Services;

public class QuestionValueService(AppDbContext context) : IQuestionValueService
{
    public async Task<Result<string>> CreateQuestionValue(CreateQuestionValueDto request, CancellationToken cancellationToken)
    {
        Question? questionValue = await context.Question.FirstOrDefaultAsync(p => p.Id == request.QuestionId);

        SurveySubmission surveySubmission = new()
        {
            SurveyId = questionValue.SurveyId
        };
        QuestionValue questionValue = new()
        {
             QuestionId= request.QuestionId,
             SurveySubmissionId = request.SurveySubmissionId,
             Value = request.Value,
        };
        context.Add(questionValue);
        await  context.SaveChangesAsync();
        return Result<string>.Succeed("Question value create is successful");
    }

    public async Task<Result<string>> DeleteQuestionValue(Guid request, CancellationToken cancellationToken)
    {
        var questionValue = await GetByIdQuestionValue(request, cancellationToken);
        if (questionValue.Data is not null)
        {
            context.Remove(questionValue.Data);
            context.SaveChanges();

        }
        return Result<string>.Succeed("Question value delete is successful");
    }

    public async Task<Result<List<QuestionValue>>> GetAll(CancellationToken cancellationToken)
    {
        List<QuestionValue> questionValueList = await context.QuestionValue.AsNoTracking().ToListAsync();
        return questionValueList;
    }

    public async Task<Result<QuestionValue>> GetByIdQuestionValue(Guid id, CancellationToken cancellationToken)
    {
        QuestionValue? questionValue = await context.QuestionValue.FindAsync(id);
        if (questionValue is not null)
        {
            return questionValue;
        }
        return (500, "Question not found");
    }
    public async Task<Result<QuestionValue>> Any(Guid id, CancellationToken cancellationToken)
    {
        QuestionValue? questionValue = await context.QuestionValue.FirstOrDefaultAsync(p => p.Question.Id == id);
        if (questionValue is not null)
        {
            return questionValue;
        }
        return (500, "Question not found");
    }
    public Task<Result<string>> UpdateQuestionValue(UpdateQuestionValueDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
