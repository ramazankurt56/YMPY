using LunavexSurveyServer.Domain.Common;
using LunavexSurveyServer.Domain.Enum;

namespace LunavexSurveyServer.Domain.Entities;

public class Question : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public QuestionTypes Type { get; set; }
    public bool IsRequired { get; set; } = false;
    public Guid SurveyId { get; set; }
    public Survey? Survey { get; set; }
    public List<Choice>? Choices { get; set; }

}

// Veritabanı gösterimi

// Name - Description- Type- IsRequired- SurveyId
// Çalışanın adı - Çalışanın adını giriniz. - Text - True - 1
// Çalışanın soyadı - Çalışanın soyadını giriniz. - Text - True - 1
// Çalışanın yaşı - Çalışanın yaşını giriniz. - Number - True - 1
// Çalışanın cinsiyeti - Çalışanın cinsiyetini seçiniz. - SingleChoice - True - 1
// Çalışanın departmanı - Çalışanın departmanını seçiniz. - SingleChoice - True - 1




// Aşağıdakilerden doğru olan şıkkı işaretleyiniz.

// A) Yukarıdaki kodlarda SOLID prensiplerinden hangilerine uyulmamıştır?

// B) Yukarıdaki kodlarda SOLID prensiplerine uyulmuştur.

// C) Yukarıdaki kodlarda SOLID prensiplerinden hangilerine uyulmuştur?

// D) Yukarıdaki kodlarda SOLID prensiplerinden hangilerine uyulmamıştır?

// E) Yukarıdaki kodlarda SOLID prensiplerine uyulmamıştır.


// Aşağıdaklerden doğru olanları işaretleyiniz.

// A) Yukarıdaki kodlarda SOLID prensiplerinden hangilerine uyulmamıştır?

// B) Yukarıdaki kodlarda SOLID prensiplerine uyulmuştur.

// C) Yukarıdaki kodlarda SOLID prensiplerinden hangilerine uyulmuştur?

// D) Yukarıdaki kodlarda SOLID prensiplerinden hangilerine uyulmamıştır?

// E) Yukarıdaki kodlarda SOLID prensiplerine uyulmamıştır.

