
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
public class ExaminationManager(IExaminationRepository examinationRepository, IMapper mapper) : IExaminationService
{
    public IResult Create(CreateExaminationDto request)
    {
        CreateExaminationDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            return new ErrorResult(string.Join(",", result.Errors));
        }
        Examination examination = mapper.Map<Examination>(request);
        examination.CreatedDate = DateTime.Now;
        examination.CreatedBy = "Admin";

        examinationRepository.Create(examination);

        return new SuccessResult(MessageConstants.CreateIsSuccessfully);
    }

    public IResult DeleteById(Guid id)
    {
        examinationRepository.DeleteById(id);
        return new SuccessResult(MessageConstants.DeleteIsSuccessfully);
    }

    public IDataResult<IQueryable<Examination>> GetAll()
    {
        SuccessDataResult<IQueryable<Examination>> examination = new(examinationRepository.GetAll().OrderBy(p => p.CreatedDate).AsQueryable());
        return examination;
    }

    public IResult Update(UpdateExaminationDto request)
    {
        UpdateExaminationDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            string errorMessage = string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList());
            return new ErrorResult(errorMessage);
        }

        Examination? examination = examinationRepository.GetExaminationById(request.Id);
        if (examination is null)
        {
            return new ErrorResult(MessageConstants.DataNotFound);
        }

        mapper.Map(request, examination);
        examination.UpdatedDate = DateTime.Now;
        examination.UpdatedBy = "Admin";

        examinationRepository.Update(examination);

        return new SuccessResult(MessageConstants.UpdateIsSuccessfully);
    }
}
