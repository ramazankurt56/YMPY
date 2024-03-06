namespace LunavexSurveyServer.Domain.DTOs;

public sealed record UpdateSurveySubmissionDto(Guid Id,Guid SurveyId,List<UpdateQuestionValueDto> UpdateQuestionValueDtos);
