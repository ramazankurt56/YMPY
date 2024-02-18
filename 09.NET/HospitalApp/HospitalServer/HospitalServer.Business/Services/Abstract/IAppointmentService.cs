using HospitalServer.Business.Result;
using HospitalServer.Entities.Dtos.Create;
using HospitalServer.Entities.Dtos.Update;
using HospitalServer.Entities.Models;

namespace HospitalServer.Business.Services.Abstract;
public interface IAppointmentService
{
    IResult Create(CreateAppointmentDto request);
    IResult Update(UpdateAppointmentDto request);
    IResult DeleteById(Guid id);
    IDataResult<IQueryable<Appointment>> GetAll();
}
