namespace HospitalServer.Entities.Dtos.Create;
public sealed record CreateDoctorDto(
    string FirstName,
    string LastName,
    string Specialization, 
    string PhoneNumber,
    string Email);
