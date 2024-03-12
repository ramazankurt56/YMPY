using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Entities.DTOs;
public sealed record CreateAppointmentDto(
    Guid DoctorId,
    string IdentityNumber,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price
    );