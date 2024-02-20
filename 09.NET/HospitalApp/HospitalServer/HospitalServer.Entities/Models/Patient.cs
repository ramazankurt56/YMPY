using HospitalServer.Entities.Abstractions;
namespace HospitalServer.Entities.Models;
public class Patient:Entity
{
    public string IdentificationNumber { get; set; }=string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}