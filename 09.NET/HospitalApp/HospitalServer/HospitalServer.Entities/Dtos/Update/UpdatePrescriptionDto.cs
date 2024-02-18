namespace HospitalServer.Entities.Dtos.Update;
public sealed record  UpdatePrescriptionDto(
    Guid Id,
    Guid ExaminationId,
    Guid MedicationId,
    int Quantity,
    string UsageInstructions
    );