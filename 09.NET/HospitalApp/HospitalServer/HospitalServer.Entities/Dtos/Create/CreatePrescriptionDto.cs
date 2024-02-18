namespace HospitalServer.Entities.Dtos.Create;
public sealed record  CreatePrescriptionDto(
    Guid ExaminationId,
    Guid MedicationId,
    int Quantity,
    string UsageInstructions
    );
