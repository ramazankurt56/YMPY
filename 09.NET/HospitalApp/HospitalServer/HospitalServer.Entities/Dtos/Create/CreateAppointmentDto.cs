namespace HospitalServer.Entities.Dtos.Create;
public sealed record CreateAppointmentDto(
    Guid PatientId ,
    Guid DoctorId,
    DateTime AppointmentDateTime,
    string Notes);
