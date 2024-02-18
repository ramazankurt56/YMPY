namespace HospitalServer.Entities.Dtos.Create;
public sealed record CreateMedicationDto(
    string MedicationName,
    int Quantity,
    DateTime ExpiryDate);