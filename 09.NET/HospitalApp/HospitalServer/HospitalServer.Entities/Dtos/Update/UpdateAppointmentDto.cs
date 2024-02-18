namespace HospitalServer.Entities.Dtos.Update;
public sealed record  UpdateAppointmentDto(
    Guid Id,
    Guid PatientId,
    Guid DoctorId,
    DateTime AppointmentDateTime,
    string Notes);