using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Context;
using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace LunavexSurveyServer.DataAccess.Services;

public class SurveySubmissionService(AppDbContext context) : ISurveySubmissionServer
{
    public async Task<Result<string>> CreateSurveySubmission(CreateSurveySubmissionDto request, CancellationToken cancellationToken)
    {
        try
        {
            await context.Database.BeginTransactionAsync();
            SurveySubmission surveySubmission = new()
            {
                SurveyId = request.SurveyId
            };
            await context.AddAsync(surveySubmission);
            await context.SaveChangesAsync();
            foreach (var questionValueItem in request.CreateQuestionValueDtos)
            {
                var questionValue = new QuestionValue
                {
                    QuestionId = questionValueItem.QuestionId,
                    SurveySubmissionId = surveySubmission.Id,
                    Value = questionValueItem.Value
                };

                context.QuestionValue.Add(questionValue);
            }
            await context.SaveChangesAsync();
            await context.Database.CommitTransactionAsync();
            return Result<string>.Succeed("SurveySubmission create is successful");

        }
        catch (Exception)
        {
            await context.Database.RollbackTransactionAsync();
            return (500, "Could not create record");
        }

    }

    public async  Task<Result<string>> DeleteQuestionValue(Guid request, CancellationToken cancellationToken)
    {
        var questionValue = await GetByIdQuestionValue(request, cancellationToken);
        if (questionValue.Data is not null)
        {
            context.Remove(questionValue.Data);
            context.SaveChanges();

        }
        return Result<string>.Succeed("Survey Submission delete is successful");
    }

    public async Task<Result<string>> DeleteSurveySubmission(Guid request, CancellationToken cancellationToken)
    {

        var surveySubmission = await GetByIdSurveySubmission(request, cancellationToken);
        if (surveySubmission.Data is not null)
        {
            context.Remove(surveySubmission.Data);
            context.SaveChanges();

        }
        return Result<string>.Succeed("Survey Submission delete is successful");
    }

    public async Task<Result<List<SurveySubmission>>> GetAll(CancellationToken cancellationToken)
    {
        List<SurveySubmission> questionList = await context.SurveySubmissions.Include(p => p.QuestionSubmissions).AsNoTracking().ToListAsync();
        return questionList;
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

    public async Task<Result<SurveySubmission>> GetByIdSurveySubmission(Guid id, CancellationToken cancellationToken)
    {
        SurveySubmission? surveySubmission = await context.SurveySubmissions.FindAsync(id);
        if (surveySubmission is not null)
        {
            return surveySubmission;
        }
        return (500, "SurveySubmission not found");
    }

    public async Task<Result<string>> UpdateSurveySubmission(UpdateSurveySubmissionDto request, CancellationToken cancellationToken)
    {
        try
        {
            await context.Database.BeginTransactionAsync();
            var surveySubmission = await GetByIdSurveySubmission(request.Id, cancellationToken);
            if (surveySubmission.Data is not null)
            {
                surveySubmission.Data.SurveyId = request.SurveyId;
                foreach (var item in request.UpdateQuestionValueDtos)
                {
                    var questionValue = await GetByIdQuestionValue(item.Id, cancellationToken);
                    if (questionValue.Data is null)
                    {
                        QuestionValue questionValueAdd = new()
                        {
                            QuestionId = item.QuestionId,
                            SurveySubmissionId = request.Id,
                            Value = item.Value
                        };
                        await context.AddAsync(questionValueAdd);
                        await context.SaveChangesAsync();
                    }
                    if (questionValue.Data is not null)
                    {
                        questionValue.Data.Value = item.Value;
                        await context.SaveChangesAsync();
                    }

                }
                await context.SaveChangesAsync();
            }
            await context.Database.CommitTransactionAsync();
            return Result<string>.Succeed("Question update is successful");
        }
        catch (Exception)
        {
            await context.Database.RollbackTransactionAsync();
            return (500, "Could not update record");
        }
    }
}
