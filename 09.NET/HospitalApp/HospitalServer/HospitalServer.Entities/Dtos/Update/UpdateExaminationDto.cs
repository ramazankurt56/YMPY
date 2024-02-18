namespace HospitalServer.Entities.Dtos.Update;
public sealed record UpdateExaminationDto(
    Guid Id,
    Guid AppointmentId,
    string Symptoms,
    string Diagnosis,
    string Treatment);
