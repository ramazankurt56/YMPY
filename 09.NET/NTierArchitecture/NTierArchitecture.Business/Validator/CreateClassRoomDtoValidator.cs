using FluentValidation;
using NTierArchitecture.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Validator
{
    public sealed class CreateClassRoomDtoValidator: AbstractValidator<CreateClassRoomDto>
    {
        public CreateClassRoomDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MinimumLength(3);
        }
    }
}
