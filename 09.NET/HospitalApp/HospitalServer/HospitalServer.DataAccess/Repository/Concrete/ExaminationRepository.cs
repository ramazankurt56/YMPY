using HospitalServer.DataAccess.Context;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;namespace HospitalServer.DataAccess.Repository.Concrete;
public class ExaminationRepository : EntityRepository<Examination>, IExaminationRepository
{
    private readonly ApplicationDbContext _context;
    public ExaminationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public void DeleteById(Guid Id)
    {

        Examination? examination = _context.Examination.Find(Id);
        if (examination is not null)
        {
            examination.IsDeleted = true;
            _context.SaveChanges();
        }
    }

    public Examination? GetExaminationById(Guid Id)
    {
        return _context.Examination.Where(p => p.Id == Id).FirstOrDefault();
    }
}
