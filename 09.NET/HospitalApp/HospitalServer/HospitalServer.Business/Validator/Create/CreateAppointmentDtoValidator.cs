using FluentValidation;
using HospitalServer.Entities.Dtos.Create;

namespace HospitalServer.Business.Validator.Create;
public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
{
    public CreateAppointmentDtoValidator()
    {
        RuleFor(p => p.Notes).NotEmpty().MinimumLength(3);
    }
}