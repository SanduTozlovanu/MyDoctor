using AutoMapper;
using System.Linq.Expressions;

namespace MyDoctorApp.Infrastructure.Generics
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task<T?> GetAsync(Guid id);
        Task<IEnumerable<T>> AllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
        IMapper GetMapper();
    }
}
