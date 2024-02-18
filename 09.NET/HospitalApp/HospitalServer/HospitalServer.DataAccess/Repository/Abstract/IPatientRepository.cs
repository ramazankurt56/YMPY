using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Abstract;
public interface IPatientRepository : IEntityRepository<Patient>
{
    public void DeleteById(Guid Id);
    Patient? GetPatientById(Guid Id);
}
