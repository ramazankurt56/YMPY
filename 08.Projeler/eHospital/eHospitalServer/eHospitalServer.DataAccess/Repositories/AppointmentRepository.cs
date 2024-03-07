using eHospitalServer.DataAccess.Context;
using eHospitalServer.Entities.Models;
using eHospitalServer.Entities.Repositories;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.DataAccess.Repositories;
internal sealed class AppointmentRepository : Repository<Appointment, ApplicationDbContext>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}