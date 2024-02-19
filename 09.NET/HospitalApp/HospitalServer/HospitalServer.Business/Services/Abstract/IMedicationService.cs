using EntityFrameworkCorePagination.Nuget.Pagination;
using HospitalServer.Business.Result;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;

namespace HospitalServer.Business.Services.Abstract;
public interface IMedicationService
{
    IResult Create(CreateMedicationDto request);
    IResult Update(UpdateMedicationDto request);
    IResult DeleteById(Guid id);
    Task<PaginationResult<Medication>> GetAll(PaginationRequestDto request);
}
