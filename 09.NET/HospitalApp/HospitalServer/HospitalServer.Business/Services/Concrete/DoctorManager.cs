using AutoMapper;
using FluentValidation.Results;
using HospitalServer.Business.Constants;
using HospitalServer.Business.Result;
using HospitalServer.Business.Services.Abstract;
using HospitalServer.Business.Validator.Create;
using HospitalServer.Business.Validator.Update;
using HospitalServer.DataAccess.Repository.Abstract;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;

namespace HospitalServer.Business.Services.Concrete;
public class DoctorManager(IDoctorRepository doctorRepository, IMapper mapper) : IDoctorService
{
    public IResult Create(CreateDoctorDto request)
    {

        CreateDoctorDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            return new ErrorResult(string.Join(",", result.Errors));
        }
        Doctor doctor = mapper.Map<Doctor>(request);
        doctor.CreatedDate = DateTime.Now;
        doctor.CreatedBy = "Admin";

        doctorRepository.Create(doctor);

        return new SuccessResult(MessageConstants.CreateIsSuccessfully);
    }

    public IResult DeleteById(Guid id)
    {
        doctorRepository.DeleteById(id);
        return new SuccessResult(MessageConstants.DeleteIsSuccessfully);
    }

    public IDataResult<IQueryable<Doctor>> GetAll()
    {
        SuccessDataResult <IQueryable<Doctor>> doctors = new(doctorRepository.GetAll().OrderBy(p => p.FirstName).AsQueryable());
        return doctors;
    }

    public IResult Update(UpdateDoctorDto request)
    {
        UpdateDoctorDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            string errorMessage = string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList());
            return new ErrorResult(errorMessage);
        }

        Doctor? doctor = doctorRepository.GetDoctorById(request.Id);
            if (doctor is null)
        {
            return new ErrorResult(MessageConstants.DataNotFound);
        }

        mapper.Map(request, doctor);
        doctor.UpdatedDate = DateTime.Now;
        doctor.UpdatedBy = "Admin";

        doctorRepository.Update(doctor);

        return new SuccessResult(MessageConstants.UpdateIsSuccessfully);
    }
}
