using HospitalServer.DataAccess.Repository.EntityFramework;
using HospitalServer.Entities.Models;

namespace HospitalServer.DataAccess.Repository.Abstract;
public interface IDoctorRepository:IEntityRepository<Doctor>
{
    void DeleteById(Guid Id);
    Doctor? GetDoctorById(Guid Id);
}
