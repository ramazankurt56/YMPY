using HospitalServer.DataAccess.Context;
using HospitalServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalServer.DataAccess.Repository.EntityFramework;
public class EntityRepository<TEntity>(ApplicationDbContext context): IEntityRepository<TEntity> where TEntity : class, new()
{
    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return context.Set<TEntity>().AsNoTracking().Any(predicate);
    }

    public void Create(TEntity entity)
    {
        context.Add(entity);
        context.SaveChanges();
    }
    public IQueryable<TEntity> GetAll()
    {
        return context.Set<TEntity>().AsNoTracking().AsQueryable();
    }
    public void Update(TEntity entity)
    {
        context.SaveChanges();
    }
}
