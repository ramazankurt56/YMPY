using EntityFrameworkCorePagination.Nuget.Pagination;
using HospitalServer.Business.Result;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;

namespace HospitalServer.Business.Services.Abstract;
public interface IPatientService
{
    IResult Create(CreatePatientDto request);
    IResult Update(UpdatePatientDto request);
    IResult DeleteById(Guid id);
    Task<PaginationResult<Patient>> GetAll(PaginationRequestDto request);
    IDataResult<Patient> GetPatientById(Guid id);
}
