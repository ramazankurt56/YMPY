using HospitalServer.Entities.Abstractions;

namespace HospitalServer.Entities.Models;
public class Doctor:Entity
{
    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}