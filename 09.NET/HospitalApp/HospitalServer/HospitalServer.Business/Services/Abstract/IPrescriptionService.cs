using EntityFrameworkCorePagination.Nuget.Pagination;
using HospitalServer.Business.Result;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;

namespace HospitalServer.Business.Services.Abstract;
public interface IPrescriptionService
{
    IResult Create(CreatePrescriptionDto request);
    IResult Update(UpdatePrescriptionDto request);
    IResult DeleteById(Guid id);
    Task<PaginationResult<Prescription>> GetAll(PaginationRequestDto request);
}
