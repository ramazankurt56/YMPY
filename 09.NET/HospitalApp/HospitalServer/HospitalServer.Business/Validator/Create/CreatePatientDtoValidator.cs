using FluentValidation;
using HospitalServer.Entities.Dtos.Create;
namespace HospitalServer.Business.Validator.Create;
public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientDtoValidator()
    {

        RuleFor(p => p.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(p => p.LastName).NotEmpty().MinimumLength(3);
    }
}