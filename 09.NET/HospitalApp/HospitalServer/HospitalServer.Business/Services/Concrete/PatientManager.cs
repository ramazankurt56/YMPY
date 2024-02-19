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
public class PatientManager(IPatientRepository patientRepository, IMapper mapper) : IPatientService
{
    public IResult Create(CreatePatientDto request)
    {
        CreatePatientDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            return new ErrorResult(string.Join(",", result.Errors));
        }
        Patient patient = mapper.Map<Patient>(request);
        patient.CreatedDate = DateTime.Now;
        patient.CreatedBy = "Admin";

        patientRepository.Create(patient);

        return new SuccessResult(MessageConstants.CreateIsSuccessfully);
    }

    public IResult DeleteById(Guid id)
    {
        patientRepository.DeleteById(id);
        return new SuccessResult(MessageConstants.DeleteIsSuccessfully);
    }

    public async Task<PaginationResult<Patient>> GetAll(PaginationRequestDto request)
    {

        PaginationResult<Patient> patient = await patientRepository
                                                .GetAll()
                                                .Where(p => p.IsDeleted == false)
                                                .Where(search =>
                                                        search.FirstName
                                                .ToLower()
                                                                .Contains(request.Search.ToLower()) ||
                                                        search.LastName
                                                .ToLower()
                                                                .Contains(request.Search.ToLower()) ||
                                                        search.PhoneNumber
                                                                .Contains(request.Search))
                                                .OrderBy(p => p.FirstName)
                                                .ToPagedListAsync(request.PageNumber, request.PageSize);
        return patient;
    }
    public IDataResult<Patient> GetPatientById(Guid id)
    {
        Patient? patient = patientRepository.GetPatientById(id);
        if (patient is not null)
        {
            return new SuccessDataResult<Patient>(patient);
        }
        return new ErrorDataResult<Patient>(patient!, MessageConstants.DataNotFound);
    }

    public IResult Update(UpdatePatientDto request)
    {
        UpdatePatientDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            string errorMessage = string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList());
            return new ErrorResult(errorMessage);
        }

        Patient? patient = patientRepository.GetPatientById(request.Id);
        if (patient is null)
        {
            return new ErrorResult(MessageConstants.DataNotFound);
        }

        mapper.Map(request, patient);
        patient.UpdatedDate = DateTime.Now;
        patient.UpdatedBy = "Admin";

        patientRepository.Update(patient);

        return new SuccessResult(MessageConstants.UpdateIsSuccessfully);
    }
}
