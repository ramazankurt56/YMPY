using LunavexSurveyServer.Business.Services;
using LunavexSurveyServer.DataAccess.Context;
using LunavexSurveyServer.DataAccess.Services;
using LunavexSurveyServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(p => p.UseSqlServer("Data Source=KURT\\SQLEXPRESS;Initial Catalog=LunavexSurveyDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IQuestionService,QuestionService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var personelAnketi = new Survey
{
    Id = 1,
    Name = "Personel Anketi",
    Questions = new List<Question>
    {
        new Question
        {
            Id = 1,
            SurveyId =  1,
            Name = "Ad"
        },
        new Question
        {
            Id = 2,
            SurveyId =  1,
            Name = "Soyad"
        }
    }
};


var surveySubmission = new SurveySubmission
{
    SurveyId = 1,
    QuestionSubmissions = new List<QuestionValue>
    {
        new QuestionValue
        {
            QuestionId = 1,
            Value = "Recep"
        },
        new QuestionValue
        {
            QuestionId = 2,
            Value = "Obut"
        }
    }
};








app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
