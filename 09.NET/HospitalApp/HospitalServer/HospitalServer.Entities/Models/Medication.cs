using HospitalServer.Entities.Abstractions;

namespace HospitalServer.Entities.Models;
public class Medication:Entity
{
    public string MedicationName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime ExpiryDate { get; set; }
}