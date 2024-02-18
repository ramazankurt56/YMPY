using System.Linq.Expressions;

namespace HospitalServer.DataAccess.Repository.EntityFramework;
public interface IEntityRepository<T> where T : class, new()
{
    void Create(T entity);
    void Update(T entity);
    bool Any(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAll();
}
