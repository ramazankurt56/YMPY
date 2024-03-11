using LunavexSurveyServer.Domain.Entities;
using LunavexSurveyServer.Domain.Enum;

namespace LunavexSurveyServer.Domain.DTOs;

public sealed record UpdateQuestionDto(Guid Id,string Name, string Description,bool IsDeleted, QuestionTypes Type, bool IsRequired,List<UpdateChoiceDto>? Choices);

