using HospitalServer.DataAccess.Context;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Concrete;
public class DoctorRepository : EntityRepository<Doctor>, IDoctorRepository
{
    private readonly ApplicationDbContext _context;
    public DoctorRepository(ApplicationDbContext context) : base(context)
    {
       _context = context;
    }

    public void DeleteById(Guid Id)
    {
        Doctor? doctor = _context.Doctors.Find(Id);
        if (doctor is not null)
        {
            doctor.IsDeleted = true;
            _context.SaveChanges();
        }
    }

    public Doctor? GetDoctorById(Guid Id)
    {
        return _context.Doctors.Where(p => p.Id == Id).FirstOrDefault();
    }

}
