using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Abstract;
public interface IPrescriptionRepository : IEntityRepository<Prescription>
{
    public void DeleteById(Guid Id);
    Prescription? GetPrescriptionById(Guid Id);
}
