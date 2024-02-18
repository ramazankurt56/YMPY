namespace HospitalServer.Entities.Dtos.Update;
public sealed record UpdateDoctorDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Specialization,
    string PhoneNumber,
    string Email);

