using FluentValidation;
using HospitalServer.Entities.Dtos.Create;

namespace HospitalServer.Business.Validator.Create;
public class CreateExaminationDtoValidator : AbstractValidator<CreateExaminationDto>
{
    public CreateExaminationDtoValidator()
    {

        RuleFor(p => p.Diagnosis).NotEmpty().MinimumLength(3);
        RuleFor(p => p.Symptoms).NotEmpty().MinimumLength(3);
    }
}