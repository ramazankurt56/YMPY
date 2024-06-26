﻿using EntityFrameworkCorePagination.Nuget.Pagination;
using Lunavex.EFCore.Pagination;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;

namespace NTierArchitecture.Business;

public interface IStudentService
{
    string Create(CreateStudentDto request);
    string Update(UpdateStudentDto request);
    string DeleteById(Guid id);
    List<Student> GetAll();
    Task<PageResult<Student>> GetAllByClassRoomIdAsync(PaginationRequestDto request);
}
