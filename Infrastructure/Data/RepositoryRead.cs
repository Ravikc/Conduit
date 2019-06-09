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
    public class RepositoryRead<T> : IRepositoryRead<T> where T : BaseEntity<T>
    {
        protected readonly ConduitDbContext _dbContext;

        public RepositoryRead(ConduitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Predicate<T> condition)
        {            
            return await _dbContext.Set<T>().Where(e => condition(e)).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync<TKey>(TKey id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
