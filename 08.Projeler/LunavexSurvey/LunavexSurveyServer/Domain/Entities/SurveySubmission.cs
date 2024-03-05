using LunavexSurveyServer.Domain.Common;

namespace LunavexSurveyServer.Domain.Entities;

public class SurveySubmission : Entity
{
    public Guid SurveyId { get; set; }
    public Survey? Survey { get; set; }
    public List<QuestionValue>? QuestionSubmissions { get; set; }
}
// Veritabanı gösterimi
// Id - SurveyId 
// 1  - 1
// 3  - 1
// 4  - 2
// 5  - 2
// 6  - 2
// 7  - 2
//   2 - 3
