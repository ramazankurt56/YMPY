using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Abstract;
public interface IExaminationRepository : IEntityRepository<Examination>
{
    public void DeleteById(Guid Id);
    Examination? GetExaminationById(Guid Id);
}
