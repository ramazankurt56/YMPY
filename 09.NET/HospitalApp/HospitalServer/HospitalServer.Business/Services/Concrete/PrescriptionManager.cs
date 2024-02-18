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
public class PrescriptionManager(IPrescriptionRepository prescriptionRepository, IMapper mapper) : IPrescriptionService
{
    public IResult Create(CreatePrescriptionDto request)
    {
        CreatePrescriptionDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            return new ErrorResult(string.Join(",", result.Errors));
        }
        Prescription prescription = mapper.Map<Prescription>(request);
        prescription.CreatedDate = DateTime.Now;
        prescription.CreatedBy = "Admin";

        prescriptionRepository.Create(prescription);

        return new SuccessResult(MessageConstants.CreateIsSuccessfully);
    }

    public IResult DeleteById(Guid id)
    {
        prescriptionRepository.DeleteById(id);
        return new SuccessResult(MessageConstants.DeleteIsSuccessfully);
    }

    public IDataResult<IQueryable<Prescription>> GetAll()
    {
        SuccessDataResult<IQueryable<Prescription>> prescription = new(prescriptionRepository.GetAll().OrderBy(p => p.CreatedDate).AsQueryable());
        return prescription;
    }

    public IResult Update(UpdatePrescriptionDto request)
    {
        UpdatePrescriptionDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            string errorMessage = string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList());
            return new ErrorResult(errorMessage);
        }

        Prescription? prescription = prescriptionRepository.GetPrescriptionById(request.Id);
        if (prescription is null)
        {
            return new ErrorResult(MessageConstants.DataNotFound);
        }

        mapper.Map(request, prescription);
        prescription.UpdatedDate = DateTime.Now;
        prescription.UpdatedBy = "Admin";

        prescriptionRepository.Update(prescription);

        return new SuccessResult(MessageConstants.UpdateIsSuccessfully);
    }
}
