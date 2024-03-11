namespace LunavexSurveyServer.Domain.DTOs;

public sealed record UpdateSurveyDto(Guid Id,string Name,string Description,List<UpdateQuestionDto> UpdateQuestionDto);