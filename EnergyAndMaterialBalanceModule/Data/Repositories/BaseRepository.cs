using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{

    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        protected readonly TContext Context;

        protected BaseRepository(TContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task Create(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetById(id);
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }


    }
}
