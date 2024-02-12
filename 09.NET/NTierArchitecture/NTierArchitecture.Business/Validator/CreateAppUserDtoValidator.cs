using FluentValidation;
using NTierArchitecture.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Validator
{
    public class CreateAppUserDtoValidator : AbstractValidator<CreateAppUserDto>
    {

        public CreateAppUserDtoValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(p => p.LastName).NotEmpty().MinimumLength(3);
            RuleFor(p => p.Email).NotEmpty().EmailAddress().MinimumLength(3);
            RuleFor(p => p.Password).NotEmpty().MinimumLength(3);
            RuleFor(p => p.UserName).NotEmpty().MinimumLength(3);        
        }
    }
}
