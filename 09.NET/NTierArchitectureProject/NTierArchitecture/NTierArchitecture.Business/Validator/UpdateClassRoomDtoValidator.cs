using FluentValidation;
using NTierArchitecture.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Validator
{
    public sealed class UpdateClassRoomDtoValidator : AbstractValidator<UpdateClassRoomDto>
    {
        public UpdateClassRoomDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty().MinimumLength(3);
        }
    }
}
