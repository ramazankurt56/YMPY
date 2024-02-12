﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Entities.DTOs
{
    public sealed record CreateAppUserDto(
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string Password
        );
}