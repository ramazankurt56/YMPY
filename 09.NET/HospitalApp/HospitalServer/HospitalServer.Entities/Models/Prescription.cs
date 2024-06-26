﻿using HospitalServer.Entities.Abstractions;
namespace HospitalServer.Entities.Models;
public class Prescription:Entity
{
    public Guid ExaminationId { get; set; }
    public Examination Examination { get; set; }
    public Guid MedicationId { get; set; }
    public Medication Medication { get; set; }
    public int Quantity { get; set; }
    public string UsageInstructions { get; set; } = string.Empty;
}