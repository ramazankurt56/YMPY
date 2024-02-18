using FluentValidation;
using HospitalServer.Entities.Dtos.Create;
namespace HospitalServer.Business.Validator.Create;
public class CreatePrescriptionDtoValidator : AbstractValidator<CreatePrescriptionDto>
{
    public CreatePrescriptionDtoValidator()
    {
        RuleFor(p => p.UsageInstructions).NotEmpty().MinimumLength(3);
    }
}