namespace LunavexSurveyServer.Domain.DTOs;

public sealed record UpdateQuestionValueDto(Guid Id,Guid QuestionId, Guid SurveySubmissionId, string Value);