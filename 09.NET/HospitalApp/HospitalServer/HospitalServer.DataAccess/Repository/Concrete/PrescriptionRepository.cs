using HospitalServer.DataAccess.Context;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;
namespace HospitalServer.DataAccess.Repository.Concrete;
public class PrescriptionRepository : EntityRepository<Prescription>, IPrescriptionRepository
{
    private readonly ApplicationDbContext _context;
    public PrescriptionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void DeleteById(Guid Id)
    {
        Prescription? prescription = _context.Prescriptions.Find(Id);
        if (prescription is not null)
        {
            prescription.IsDeleted = true;
            _context.SaveChanges();
        }
    }

    public Prescription? GetPrescriptionById(Guid Id)
    {
        return _context.Prescriptions.Where(p => p.Id == Id).FirstOrDefault();
    }
}