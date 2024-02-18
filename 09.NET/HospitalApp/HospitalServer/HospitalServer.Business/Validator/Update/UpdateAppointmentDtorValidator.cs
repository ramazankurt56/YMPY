using FluentValidation;
using HospitalServer.Entities.Dtos.Update;
namespace HospitalServer.Business.Validator.Update;
public class UpdateAppointmentDtorValidator : AbstractValidator<UpdateAppointmentDto>
{
    public UpdateAppointmentDtorValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Notes).NotEmpty().MinimumLength(3);
    }
}