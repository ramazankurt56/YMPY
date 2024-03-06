namespace LunavexSurveyServer.Domain.DTOs;

public sealed record CreateSurveyDto(string Description,string Name,List<CreateQuestionDto> CreateQuestionDto);
