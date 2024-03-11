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
            await context.AddAsync(question);
            await context.SaveChangesAsync();
            if (item.Choices is not null)
            {
                foreach (var choiceValue in item.Choices)
                {
                    var choice = new Choice
                    {
                        Value = choiceValue.Value,
                        QuestionId = question.Id
                    };

                    await context.Choices.AddAsync(choice);
                }
            }

            await context.SaveChangesAsync();
        }
        return Result<string>.Succeed("Create success");
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

    public async Task<Result<string>> UpdateQuestion(List<UpdateQuestionDto> request, Guid surveyId, CancellationToken cancellationToken)
    {

        foreach (var item in request)
        {
            var question = await GetByIdQuestion(item.Id, cancellationToken);
            if (question.Data is not null)
            {

                question.Data.Name = item.Name;
                question.Data.Description = item.Description;
                question.Data.IsRequired = item.IsRequired;
                question.Data.Type = item.Type;
                question.Data.SurveyId = surveyId;
                question.Data.IsDeleted = item.IsDeleted;
                
                if (item.Choices is not null)
                {
                    foreach (var choices in item.Choices)
                    {
                        var choice = await GetByIdChoice(choices.Id, cancellationToken);
  

                       
                        if (choice.Data is null)
                        {
                            Choice choiceAdd = new()
                            {
                                QuestionId = item.Id,
                                Value = choices.Value,
                            };
                            await context.AddAsync(choiceAdd);
                            await context.SaveChangesAsync();
                        }
                        if (choice.Data is not null)
                        {
                        
                            choice.Data.Value = choices.Value;
                            choice.Data.IsDeleted=choices.IsDeleted;
                            await context.SaveChangesAsync();
                            if (choice.Data.IsDeleted == true)
                            {
                                await DeleteChoice(choice.Data.Id, cancellationToken);
                            }
                        }

                    }
                }

                await context.SaveChangesAsync();
                if (question.Data.IsDeleted == true)
                {
                    await DeleteQuestion(question.Data.Id, cancellationToken);
                }
            }
            if (question.Data is null)
            {
                Question newQuestion = new()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type,
                    IsRequired = item.IsRequired,
                    SurveyId = surveyId
                };
                await context.AddAsync(newQuestion);
                await context.SaveChangesAsync();
                if (item.Choices is not null)
                {
                    foreach (var choiceValue in item.Choices)
                    {
                        var choice = new Choice
                        {
                            Value = choiceValue.Value,
                            QuestionId = newQuestion.Id
                        };

                        await context.Choices.AddAsync(choice);
                    }
                }
                await context.SaveChangesAsync();
            }
            await context.SaveChangesAsync();
        }

        return Result<string>.Succeed("Create success");

    }
}
