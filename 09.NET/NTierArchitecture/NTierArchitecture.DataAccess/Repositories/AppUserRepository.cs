using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DataAccess.Repositories
{
    public sealed class AppUserRepository(ApplicationDbContext context):IAppUserRepository
    {
        public bool Any(Expression<Func<AppUser, bool>> predicate)
        {
            return context.Users.AsNoTracking().Any(predicate);
        }

        public void Create(AppUser appUser)
        {
            context.Add(appUser);
            context.SaveChanges();
        }

        public void DeleteById(Guid Id)
        {
            AppUser? appUser = GetAppUserById(Id);
            if (appUser is not null)
            {
                context.Remove(appUser);
                context.SaveChanges();
            }
        }

        public IQueryable<AppUser> GetAll()
        {
            return context.Users.AsNoTracking().AsQueryable();
        }
        public AppUser? GetAppUserById(Guid appUserId)
        {
            return context.Users.Where(p => p.Id == appUserId).FirstOrDefault();
        }

        public void Update(AppUser appUser)
        {
            context.Update(appUser);
            context.SaveChanges();
        }
    }
}
