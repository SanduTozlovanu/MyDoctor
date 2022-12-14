using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyDoctorApp.Infrastructure.Generics
{
    public abstract class Repository<T>
        : IRepository<T> where T : class
    {
        protected DatabaseContext context;
        private IMapper mapper;

        protected Repository(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Guid id)
        {
            return await context.FindAsync<T>(id);
        }

        public virtual async Task<IEnumerable<T>> AllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual T Update(T entity)
        {
            return context.Update(entity)
                .Entity;
        }

        public virtual T Delete(T entity)
        {
            return context.Remove(entity)
                .Entity;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
