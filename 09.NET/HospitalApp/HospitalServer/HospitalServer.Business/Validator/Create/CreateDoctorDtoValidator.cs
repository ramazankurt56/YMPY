using FluentValidation;
using HospitalServer.Entities.Dtos.Create;

namespace HospitalServer.Business.Validator.Create;
public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
{
    public CreateDoctorDtoValidator()
    {

        RuleFor(p => p.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(p => p.LastName).NotEmpty().MinimumLength(3);
    }
}