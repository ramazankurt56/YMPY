using FluentValidation;
using HospitalServer.Entities.Dtos.Update;

namespace HospitalServer.Business.Validator.Update;
public class UpdatePrescriptionDtoValidator : AbstractValidator<UpdatePrescriptionDto>
{
    public UpdatePrescriptionDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.UsageInstructions).NotEmpty().MinimumLength(3);
    }
}