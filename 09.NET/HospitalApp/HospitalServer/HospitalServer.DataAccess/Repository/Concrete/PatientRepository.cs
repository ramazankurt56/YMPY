using HospitalServer.DataAccess.Context;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Concrete;
public class PatientRepository : EntityRepository<Patient>, IPatientRepository
{
    private readonly ApplicationDbContext _context;
    public PatientRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void DeleteById(Guid Id)
    {
        Patient? patient = _context.Patients.Find(Id);
        if (patient is not null)
        {
            patient.IsDeleted = true;
            _context.SaveChanges();
        }
    }

    public Patient? GetPatientById(Guid Id)
    {
        return _context.Patients.Where(p => p.Id == Id).FirstOrDefault();
    }
}