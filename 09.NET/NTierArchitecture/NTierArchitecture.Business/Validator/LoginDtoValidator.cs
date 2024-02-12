using FluentValidation;
using NTierArchitecture.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Validator
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {

        public LoginDtoValidator()
        {
            RuleFor(p => p.UserNameOrEmail).NotEmpty().MinimumLength(3);
            RuleFor(p => p.Password).NotEmpty().MinimumLength(3);
        }
    }
}
