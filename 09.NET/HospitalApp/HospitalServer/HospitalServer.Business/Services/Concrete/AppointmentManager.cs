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
public class AppointmentManager(IAppointmentRepository appointmentRepository, IMapper mapper) : IAppointmentService
{
    public IResult Create(CreateAppointmentDto request)
    {
        CreateAppointmentDtoValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            return new ErrorResult(string.Join(",", result.Errors));
        }
        Appointment appointment = mapper.Map<Appointment>(request);
        appointment.CreatedDate = DateTime.Now;
        appointment.CreatedBy = "Admin";

        appointmentRepository.Create(appointment);

        return new SuccessResult(MessageConstants.CreateIsSuccessfully);
    }

    public IResult DeleteById(Guid id)
    {
        appointmentRepository.DeleteById(id);
        return new SuccessResult(MessageConstants.DeleteIsSuccessfully);
    }

    public async Task<PaginationResult<Appointment>> GetAll(PaginationRequestDto request)
    {

        PaginationResult<Appointment> appointment = await appointmentRepository
                                                .GetAll()
                                                .Where(p => p.IsDeleted == false)
                                                .Where(search =>
                                                        search.Doctor.FirstName.ToLower().Contains(request.Search.ToLower()))
                                                .OrderBy(p => p.Doctor.FirstName)
                                                .ToPagedListAsync(request.PageNumber, request.PageSize);
        return appointment;
    }

    public IResult Update(UpdateAppointmentDto request)
    {
        UpdateAppointmentDtorValidator validator = new();
        ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            string errorMessage = string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList());
            return new ErrorResult(errorMessage);
        }

        Appointment? appointment = appointmentRepository.GetAppointmentById(request.Id);
            if (appointment is null)
        {
            return new ErrorResult(MessageConstants.DataNotFound);
        }

        mapper.Map(request, appointment);
        appointment.UpdatedDate = DateTime.Now;
        appointment.UpdatedBy = "Admin";

        appointmentRepository.Update(appointment);

        return new SuccessResult(MessageConstants.UpdateIsSuccessfully);
    }
}
