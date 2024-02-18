namespace HospitalServer.Entities.Dtos.Update;
public sealed record UpdateMedicationDto(
    Guid Id,
    string MedicationName,
    int Quantity,
    DateTime ExpiryDate);