using FluentValidation;
using HospitalServer.Entities.Dtos.Update;
namespace HospitalServer.Business.Validator.Update;
public class UpdatePatientDtoValidator : AbstractValidator<UpdatePatientDto>
{
    public UpdatePatientDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(p => p.LastName).NotEmpty().MinimumLength(3);
    }
}