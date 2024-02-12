using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services
{
    public interface IAppUserService
    {
        string Create(CreateAppUserDto request);
        string Update(UpdateAppUserDto request);
        string DeleteById(Guid id);
        List<AppUser> GetAll();
    }
}
