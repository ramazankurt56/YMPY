using EntityFrameworkCorePagination.Nuget.Pagination;
using HospitalServer.Business.Result;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;
namespace HospitalServer.Business.Services.Abstract;
public interface IExaminationService
{
    IResult Create(CreateExaminationDto request);
    IResult Update(UpdateExaminationDto request);
    IResult DeleteById(Guid id);
    Task<PaginationResult<Examination>> GetAll(PaginationRequestDto request);
}
