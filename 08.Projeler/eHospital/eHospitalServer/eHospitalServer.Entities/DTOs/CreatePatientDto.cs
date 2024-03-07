using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Entities.DTOs;
public sealed record CreatePatientDto(
    string FirstName,
    string LastName,
    string IdentityNumber = "11111111111",
    string FullAddress = "",
    string? Email = null,
    string? PhoneNumber = null,
    DateOnly? DateOfBirth = null,
    string? BloodType = null);