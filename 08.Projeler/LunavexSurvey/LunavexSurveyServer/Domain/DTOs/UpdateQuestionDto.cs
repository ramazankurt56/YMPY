using LunavexSurveyServer.Domain.Entities;
using LunavexSurveyServer.Domain.Enum;

namespace LunavexSurveyServer.Domain.DTOs;

public sealed record UpdateQuestionDto(Guid Id, Guid SurveyId,string Name, string Description, QuestionTypes Type, bool IsRequired,List<UpdateChoiceDto> Choice);

