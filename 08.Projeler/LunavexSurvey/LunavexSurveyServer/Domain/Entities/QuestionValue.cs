using LunavexSurveyServer.Domain.Common;

namespace LunavexSurveyServer.Domain.Entities;

public class QuestionValue : Entity
{
    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
    public Guid SurveySubmissionId { get; set; }
   // public SurveySubmission? SurveySubmission { get; set; }
    public string Value { get; set; } = string.Empty;
}

// Veritabanı gösterimi
// Id - QuestionId - SurveySubmissionId - Value
// 1  - 1          - 1                  - Recep
// 2  - 2          - 1                  - Obut
// 3  - 3          - 1                  - 25
// 4  - 4          - 1                  - Backend Developer
