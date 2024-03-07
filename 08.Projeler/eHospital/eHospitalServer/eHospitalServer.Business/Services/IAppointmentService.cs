using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eHospitalServer.Business.Services;
public interface IAppointmentService
{
    Task<Result<string>> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken);
    Task<Result<string>> CompleteAsync(CompleteAppointmentDto request, CancellationToken cancellationToken);
    Task<Result<List<Appointment>>> GetAllByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken);
}