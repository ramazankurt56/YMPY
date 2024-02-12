using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using NTierArchitecture.Business.Constants;
using NTierArchitecture.Business.Validator;
using NTierArchitecture.DataAccess.Repositories;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services
{
    public class AppUserManager(IAppUserRepository appUserRepository, IMapper mapper) : IAppUserService
    {
        public string Create(CreateAppUserDto request)
        {
            CreateAppUserDtoValidator validator = new();
            ValidationResult result = validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList()));
            }
            bool isUserNameExists =
            appUserRepository.Any(p => p.UserName == request.UserName);
            bool isEmailExists =
           appUserRepository.Any(p => p.Email == request.Email);
            if (isUserNameExists)
            {
                throw new ArgumentException(MessageConstants.UserNamelAlreadyExists);
            }
            if (isEmailExists)
            {
                throw new ArgumentException(MessageConstants.EmailAlreadyExists);
            }

            AppUser appUser = mapper.Map<AppUser>(request);
            appUserRepository.Create(appUser);
            return MessageConstants.CreateIsSuccessfully;
        }

        public string DeleteById(Guid id)
        {
            appUserRepository.DeleteById(id);
            return MessageConstants.DeleteIsSuccessfully;
        }

        public List<AppUser> GetAll()
        {
            List<AppUser> appUser = appUserRepository
             .GetAll()
             .OrderBy(p => p.FirstName)
             .ToList();

            return appUser;
        }

        public string Update(UpdateAppUserDto request)
        {
            UpdateAppUserDtoValidator validator = new();
            ValidationResult result = validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(", ", result.Errors.Select(s => s.ErrorMessage).ToList()));
            }

            AppUser? appUser = appUserRepository.GetAppUserById(request.Id);
            if (appUser is null)
            {
                throw new ArgumentException(MessageConstants.DataNotFound);
            }

            if (request.UserName != appUser.UserName)
            {
                bool isUserNameExists = appUserRepository.Any(p => p.UserName == request.UserName);
                if (isUserNameExists)
                {
                    throw new ArgumentException(MessageConstants.UserNamelAlreadyExists);
                }
            }

            if (request.Email != appUser.Email)
            {
                bool isUserNameExists = appUserRepository.Any(p => p.Email == request.Email);
                if (isUserNameExists)
                {
                    throw new ArgumentException(MessageConstants.UserNamelAlreadyExists);
                }
            }

            mapper.Map(request, appUser);
            appUserRepository.Update(appUser);
            return MessageConstants.UpdateIsSuccessfully;
        }
    }
}
