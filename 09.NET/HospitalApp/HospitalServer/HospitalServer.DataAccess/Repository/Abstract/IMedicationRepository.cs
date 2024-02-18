using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;


namespace HospitalServer.DataAccess.Repository.Abstract;
public interface IMedicationRepository : IEntityRepository<Medication>
{
    public void DeleteById(Guid Id);
    Medication? GetMedicationById(Guid Id);
}
