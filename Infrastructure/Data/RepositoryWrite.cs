using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infrastructure.Data
{
    public class RepositoryWrite<TEntity, TKey> : IRepositoryWrite<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly ConduitDbContext dbContext;
        public RepositoryWrite(ConduitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var added = await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return added.Entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
