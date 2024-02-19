using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace HospitalServer.Entities.Models;
public class DoctorPatient
{
    [ForeignKey("Doctor")]
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    [ForeignKey("Patient")]
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
}
