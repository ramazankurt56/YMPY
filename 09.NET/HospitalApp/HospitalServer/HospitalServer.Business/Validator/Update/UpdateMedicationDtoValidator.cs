using FluentValidation;
using HospitalServer.Entities.Dtos.Update;

namespace HospitalServer.Business.Validator.Update;
public class UpdateMedicationDtoValidator : AbstractValidator<UpdateMedicationDto>
{
    public UpdateMedicationDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.MedicationName).NotEmpty().MinimumLength(3);
    }
}