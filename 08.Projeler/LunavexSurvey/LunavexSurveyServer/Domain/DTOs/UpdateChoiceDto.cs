namespace LunavexSurveyServer.Domain.DTOs;

public sealed record UpdateChoiceDto(Guid Id,Guid QuestionId,string Value,bool IsDeleted);
