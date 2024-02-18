using HospitalServer.DataAccess.Context;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Concrete;
public class MedicationRepository : EntityRepository<Medication>, IMedicationRepository
{
    private readonly ApplicationDbContext _context;
    public MedicationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void DeleteById(Guid Id)
    {
        Medication? medication = _context.Medication.Find(Id);
        if (medication is not null)
        {
            medication.IsDeleted = true;
            _context.SaveChanges();
        }
    }

    public Medication? GetMedicationById(Guid Id)
    {
        return _context.Medication.Where(p => p.Id == Id).FirstOrDefault();
    }
}