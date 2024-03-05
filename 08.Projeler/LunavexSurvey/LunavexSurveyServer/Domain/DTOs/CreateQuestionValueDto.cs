namespace LunavexSurveyServer.Domain.DTOs;

public sealed record CreateQuestionValueDto(Guid QuestionId,Guid SurveySubmissionId, string Value);
