using Azure.Core;
using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Context;
using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace LunavexSurveyServer.DataAccess.Services;

public class QuestionService(AppDbContext context) : IQuestionService
{
    public async Task<Result<string>> CreateQuestion(List<CreateQuestionDto> request, Guid surveyId, CancellationToken cancellationToken)
    {
        foreach (var item in request)
        {
            Question question = new()
            {
                Name = item.Name,
                Description = item.Description,
                Type = item.Type,
                IsRequired = item.IsRequired,
                SurveyId = surveyId
            };
            context.Add(question);
            await context.SaveChangesAsync();
            foreach (var choiceValue in item.Choices)
            {
                var choice = new Choice
                {
                    Value = choiceValue,
                    QuestionId = question.Id
                };

                context.Choices.Add(choice);
            }
            await context.SaveChangesAsync();
        }
        return  Result<string>.Succeed("Create success");
    }

    public async Task<Result<string>> DeleteQuestion(Guid request, CancellationToken cancellationToken)
    {
        var question = await GetByIdQuestion(request, cancellationToken);
        if (question.Data is not null)
        {
            context.Remove(question.Data);
            context.SaveChanges();

        }
        return Result<string>.Succeed("Survey delete is successful");
    }
    public async Task<Result<string>> DeleteChoice(Guid request, CancellationToken cancellationToken)
    {
        var choice = await GetByIdChoice(request, cancellationToken);
        if (choice.Data is not null)
        {
            context.Remove(choice.Data);
            context.SaveChanges();

        }
        return Result<string>.Succeed("Survey delete is successful");
    }

    public async Task<Result<List<Question>>> GetAll(CancellationToken cancellationToken)
    {
        List<Question> questionList = await context.Question.Include(p => p.Choices).AsNoTracking().ToListAsync();
        return questionList;
    }

    public async Task<Result<Question>> GetByIdQuestion(Guid id, CancellationToken cancellationToken)
    {
        Question? question = await context.Question.FindAsync(id);
        if (question is not null)
        {
            return question;
        }
        return (500, "Question not found");
    }
    public async Task<Result<Choice>> GetByIdChoice(Guid id, CancellationToken cancellationToken)
    {
        Choice? choice = await context.Choices.FirstOrDefaultAsync(p => p.Id == id);
        if (choice is not null)
        {
            return choice;
        }
        return (500, "Choice not found");
    }

    public async Task<Result<string>> UpdateQuestion(UpdateQuestionDto request, CancellationToken cancellationToken)
    {
        try
        {
            await context.Database.BeginTransactionAsync();
            var question = await GetByIdQuestion(request.Id, cancellationToken);
            if (question.Data is not null)
            {

                question.Data.Name = request.Name;
                question.Data.Description = request.Description;
                question.Data.IsRequired = request.IsRequired;
                question.Data.Type = request.Type;
                question.Data.SurveyId = request.SurveyId;

                foreach (var item in request.UpdateChoiceDtos)
                {
                    var choice = await GetByIdChoice(item.Id, cancellationToken);
                    if (choice.Data is null)
                    {
                        Choice choiceAdd = new()
                        {
                            QuestionId = request.Id,
                            Value = item.Value,
                        };
                        context.Add(choiceAdd);
                        await context.SaveChangesAsync();
                    }
                    if (choice.Data is not null)
                    {
                        choice.Data.Value = item.Value;
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
