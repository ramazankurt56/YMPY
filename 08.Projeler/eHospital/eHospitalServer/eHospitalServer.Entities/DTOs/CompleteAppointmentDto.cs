﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Entities.DTOs;
public sealed record CompleteAppointmentDto(
    Guid AppointmentId,
    string EpicrisisReport);