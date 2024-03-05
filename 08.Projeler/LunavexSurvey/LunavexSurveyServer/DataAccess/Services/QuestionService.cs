using Azure.Core;
using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Context;
using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace LunavexSurveyServer.DataAccess.Services;

public class QuestionService(AppDbContext context) : IQuestionService
{
    public async Task<Result<string>> CreateQuestion(CreateQuestionDto request, CancellationToken cancellationToken)
    {
        try
        {
            await context.Database.BeginTransactionAsync();
            if (request.Name is not null)
            {
                bool isNameExists = await context.Question.AnyAsync(p => p.Name == request.Name);
                if (isNameExists)
                {
                    return Result<string>.Failure(StatusCodes.Status409Conflict, "Question Name already has taken");
                }
            }
            Question question = new()
            {
                Name = request.Name,
                Description = request.Description,
                Type = request.Type,
                IsRequired = request.IsRequired,
                SurveyId = request.SurveyId
            };
            context.Add(question);
            await context.SaveChangesAsync();
            foreach (var choiceValue in request.Choices)
            {
                var choice = new Choice
                {
                    Value = choiceValue,
                    QuestionId = question.Id // Soruya ait olduğunu belirtmek için soru kimliğini ayarlayın
                };

                context.Choices.Add(choice);
            }
            await context.SaveChangesAsync();

            await context.Database.CommitTransactionAsync();
            return Result<string>.Succeed("Survey create is successful");
        }
        catch (Exception)
        {
            await context.Database.RollbackTransactionAsync();
            return (500, "Could not create record");
        }


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
        Choice? choice = await context.Choices.FirstOrDefaultAsync(p=>p.Id==id);
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
            if (question is not null)
            {

                question.Data.Name = request.Name;
                question.Data.Description = request.Description;
                question.Data.IsRequired = request.IsRequired;
                question.Data.Type = request.Type;
                question.Data.SurveyId = request.SurveyId;

                foreach (var item in request.Choice)
                {
                    var choice = await GetByIdChoice(item.Id, cancellationToken);
                    if(choice.Data is null)
                    {
                        Choice choiceAdd = new() 
                        { 
                            QuestionId=request.Id,
                            Value=item.Value,
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
