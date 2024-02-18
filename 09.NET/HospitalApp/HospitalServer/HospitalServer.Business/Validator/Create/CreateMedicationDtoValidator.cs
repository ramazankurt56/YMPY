using FluentValidation;
using HospitalServer.Entities.Dtos.Create;

namespace HospitalServer.Business.Validator.Create;
public class CreateMedicationDtoValidator : AbstractValidator<CreateMedicationDto>
{
    public CreateMedicationDtoValidator()
    {
        RuleFor(p => p.MedicationName).NotEmpty().MinimumLength(3);
    }
}