using EntityFrameworkCorePagination.Nuget.Pagination;
using HospitalServer.Business.Result;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;
namespace HospitalServer.Business.Services.Abstract;
public interface IDoctorService
{
    IResult Create(CreateDoctorDto request);
    IResult Update(UpdateDoctorDto request);
    IResult DeleteById(Guid id);
    Task<PaginationResult<Doctor>> GetAll(PaginationRequestDto request);
    IDataResult<Doctor> GetDoctorById(Guid id);

}
