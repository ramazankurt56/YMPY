using AutoMapper;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eHospitalServer.DataAccess.Mapping;
internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}