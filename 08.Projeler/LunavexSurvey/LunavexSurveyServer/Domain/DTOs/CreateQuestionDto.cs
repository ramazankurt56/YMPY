using LunavexSurveyServer.Domain.Entities;
using LunavexSurveyServer.Domain.Enum;

namespace LunavexSurveyServer.Domain.DTOs;

public sealed record CreateQuestionDto(string Name,string Description, QuestionTypes Type,bool IsRequired,
    List<CreateChoiceDto>? Choices);
