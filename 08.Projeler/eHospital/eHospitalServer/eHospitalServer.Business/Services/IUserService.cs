﻿using eHospitalServer.Entities.DTOs;
using TS.Result;

namespace eHospitalServer.Business.Services;
public interface IUserService
{
    Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken);
    Task<Result<Guid>> CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken);
}
