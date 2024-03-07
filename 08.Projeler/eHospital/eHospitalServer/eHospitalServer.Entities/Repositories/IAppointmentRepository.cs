using eHospitalServer.Entities.Models;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Entities.Repositories;
public interface IAppointmentRepository : IRepository<Appointment>
{
}