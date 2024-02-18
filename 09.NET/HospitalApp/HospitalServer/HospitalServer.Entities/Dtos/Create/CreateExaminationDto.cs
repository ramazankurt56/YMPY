namespace HospitalServer.Entities.Dtos.Create;
public sealed record CreateExaminationDto(
    Guid AppointmentId,
    string Symptoms,
    string Diagnosis,
    string Treatment);
