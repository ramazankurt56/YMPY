using LunavexSurveyServer.Domain.Common;

namespace LunavexSurveyServer.Domain.Entities;

public class Choice : Entity
{
    public string Value { get; set; } = string.Empty;
    public Guid QuestionId { get; set; }
    //public Question? Question { get; set; }
}

// Veritabanı gösterimi
// Value - QuestionId
// Çok iyi 1
// İyi 1
// Orta 1
// Kötü 1
// Çok kötü 1
// Evet 2
// Hayır 2
// Belki 2
// Backend Developer 3
// Frontend Developer 3
// Fullstack Developer 3
// Devops 3
