using HospitalServer.Business.Result;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;
namespace HospitalServer.Business.Services.Abstract;
public interface IDoctorService
{
    IResult Create(CreateDoctorDto request);
    IResult Update(UpdateDoctorDto request);
    IResult DeleteById(Guid id);
    IDataResult<IQueryable<Doctor>> GetAll();
}
