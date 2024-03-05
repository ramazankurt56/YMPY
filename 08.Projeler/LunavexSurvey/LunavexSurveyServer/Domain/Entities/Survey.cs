using LunavexSurveyServer.Domain.Common;

namespace LunavexSurveyServer.Domain.Entities;

/// <summary>
/// Anket bilgilerini tutar.
/// </summary>
public class Survey : Entity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? ModifiedDate { get; set; }

    public List<Question> Questions { get; set; } = new();

}

// Veritabanı gösterimi

// Name Description CreatedDate ModifiedDate
// Çalışan Değerlendirme Anketi Survey1Description 2021-01-01 00:00:00.000 NULL
// Seçim Anketi Survey2Description 2021-01-01 00:00:00.000 NULL
// Survey3 Survey3Description 2021-01-01 00:00:00.000 NULL
