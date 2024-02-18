using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Abstract;
public interface IAppointmentRepository : IEntityRepository<Appointment>
{
    public void DeleteById(Guid Id);
    Appointment? GetAppointmentById(Guid Id);
}
