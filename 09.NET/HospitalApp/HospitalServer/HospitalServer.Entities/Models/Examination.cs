using HospitalServer.Entities.Abstractions;

namespace HospitalServer.Entities.Models;
public class Examination:Entity
{
    public Guid AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
    public string Symptoms { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
}