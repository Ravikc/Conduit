using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Repositories;
using Conduit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infrastructure.Repositories
{
    public class RepositoryRead<TEntity, TKey> : IRepositoryRead<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly ConduitDbContext dbContext;

        public RepositoryRead(ConduitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Predicate<TEntity> condition)
        {            
            return await dbContext.Set<TEntity>().Where(e => condition(e)).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }
    }
}
