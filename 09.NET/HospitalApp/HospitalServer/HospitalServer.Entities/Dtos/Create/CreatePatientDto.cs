namespace HospitalServer.Entities.Dtos.Create;
public sealed record CreatePatientDto(
    string FirstName,
    string LastName,
    string Gender,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Address
    );