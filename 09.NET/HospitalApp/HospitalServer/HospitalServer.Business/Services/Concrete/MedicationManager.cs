using AutoMapper;
using EntityFrameworkCorePagination.Nuget.Pagination;
using FluentValidation.Results;
using HospitalServer.Business.Constants;
using HospitalServer.Business.Result;
using HospitalServer.Business.Services.Abstract;
using HospitalServer.Business.Validator.Create;
using HospitalServer.Business.Validator.Update;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.DataAccess.Repository.Concrete;
using HospitalServer.Entities.Dtos;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;

namespace HospitalServer.Business.Services.Concrete;
public class MedicationManager(IMedicationRepository medicationRepository, IMapper mapper) : IMedicationService
{
    public IResult Create(CreateMedicationDto request)
    {
        CreateMedicationDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            return new ErrorResult(string.Join(",", result.Errors));
        }
        Medication medication = mapper.Map<Medication>(request);
        medication.CreatedDate = DateTime.Now;
        medication.CreatedBy = "Admin";

        medicationRepository.Create(medication);

        return new SuccessResult(MessageConstants.CreateIsSuccessfully);
    }

    public IResult DeleteById(Guid id)
    {
        medicationRepository.DeleteById(id);
        return new SuccessResult(MessageConstants.DeleteIsSuccessfully);
    }
    public async Task<PaginationResult<Medication>> GetAll(PaginationRequestDto request)
    {

        PaginationResult<Medication> medication = await medicationRepository
                                                .GetAll()
                                                .Where(p => p.IsDeleted == false)
                                                .Where(search =>
                                                        search.MedicationName.ToLower().Contains(request.Search.ToLower()))
                                                .OrderBy(p => p.CreatedDate)
                                                .ToPagedListAsync(request.PageNumber, request.PageSize);
        return medication;
    }

    public IResult Update(UpdateMedicationDto request)
    {
        UpdateMedicationDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            string errorMessage = string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList());
            return new ErrorResult(errorMessage);
        }

        Medication? medication = medicationRepository.GetMedicationById(request.Id);
        if (medication is null)
        {
            return new ErrorResult(MessageConstants.DataNotFound);
        }

        mapper.Map(request, medication);
        medication.UpdatedDate = DateTime.Now;
        medication.UpdatedBy = "Admin";

        medicationRepository.Update(medication);

        return new SuccessResult(MessageConstants.UpdateIsSuccessfully);
    }
}
