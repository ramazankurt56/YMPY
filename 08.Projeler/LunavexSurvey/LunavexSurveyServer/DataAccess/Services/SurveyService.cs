using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Context;
using LunavexSurveyServer.Domain.DTOs;
using LunavexSurveyServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace LunavexSurveyServer.DataAccess.Services;

public class SurveyService(AppDbContext context) : ISurveyService
{
    public async Task<Result<string>> CreateSurvey(CreateSurveyDto request, CancellationToken cancellationToken)
    {
        if (request.Name is not null)
        {
            bool isNameExists = await context.Surveys.AnyAsync(p => p.Name == request.Name);
            if (isNameExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Survey Name already has taken");
            }
        }
        Survey survey = new()
        {
            Name = request.Name,
            Description = request.Description
           
        };
        context.Add(survey);
        context.SaveChanges();
        return Result<string>.Succeed("Survey create is successful");
    }

    public async Task<Result<string>> DeleteSurvey(Guid request, CancellationToken cancellationToken)
    {
        var survey =await GetById(request,cancellationToken);
        if (survey.Data is not null)
        {
            context.Remove(survey.Data);
            context.SaveChanges();    
        }
        return Result<string>.Succeed("Survey delete is successful");

    }

    public async Task<Result<List<Survey>>> GetAll(CancellationToken cancellationToken)
    {
        List<Survey> surveyList = await context.Surveys.AsNoTracking().ToListAsync();
        return surveyList;
    }

    public async Task<Result<Survey>> GetById(Guid id, CancellationToken cancellationToken)
    {
        Survey? survey = await context.Surveys.FindAsync(id);
        if(survey is not null)
        {
            return survey;
        }
        return (500, "Survey not found");
    }

    public async Task<Result<string>> UpdateSurvey(UpdateSurveyDto request, CancellationToken cancellationToken)
    {
        var survey = await GetById(request.Id, cancellationToken);
        if (survey is not null)
        {
            survey.Data.Name = request.Name;
            survey.Data.Description = request.Description;
            survey.Data.ModifiedDate= DateTime.Now;
            await context.SaveChangesAsync();
            return Result<string>.Succeed("Survey update is successful");
        }
        return (500, "Could not update record");
    }
}
