using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DataAccess.Repositories
{
    public interface IAppUserRepository
    {
        void Create(AppUser appUser);
        void Update(AppUser appUser);
        void DeleteById(Guid Id);
        bool Any(Expression<Func<AppUser, bool>> predicate);
        IQueryable<AppUser> GetAll();
        AppUser? GetAppUserById(Guid appUserId);
    }
}
