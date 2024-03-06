namespace LunavexSurveyServer.Domain.DTOs;

public sealed record CreateSurveySubmissionDto(Guid SurveyId,List<CreateQuestionValueDto> CreateQuestionValueDtos);