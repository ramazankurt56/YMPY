using FluentValidation;
using HospitalServer.Entities.Dtos.Update;
namespace HospitalServer.Business.Validator.Update;
public class UpdateExaminationDtoValidator : AbstractValidator<UpdateExaminationDto>
{
    public UpdateExaminationDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Diagnosis).NotEmpty().MinimumLength(3);
        RuleFor(p => p.Symptoms).NotEmpty().MinimumLength(3);
    }
}