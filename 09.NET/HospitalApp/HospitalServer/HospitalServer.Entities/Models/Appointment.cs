﻿using HospitalServer.Entities.Abstractions;

namespace HospitalServer.Entities.Models;
public class Appointment:Entity
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
    public Guid DoctorId { get; set; }
   public Doctor Doctor { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string Notes { get; set; } = string.Empty;
}