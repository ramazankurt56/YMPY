using HospitalServer.DataAccess.Context;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;
namespace HospitalServer.DataAccess.Repository.Concrete;
public class AppointmentRepository : EntityRepository<Appointment>, IAppointmentRepository
{
    private readonly ApplicationDbContext _context;
    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void DeleteById(Guid Id)
    {
        Appointment? appointment = _context.Appointments.Find(Id);
        if (appointment is not null)
        {
            appointment.IsDeleted = true;
            _context.SaveChanges();
        }
    }

    public Appointment? GetAppointmentById(Guid Id)
    {
        return _context.Appointments.Where(p => p.Id == Id).FirstOrDefault();
    }
}
