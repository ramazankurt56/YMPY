namespace HospitalServer.Entities.Dtos.Update;
public sealed record UpdatePatientDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Gender,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Address
    );
