using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Conduit.ApplicationCore.Entities;

namespace Conduit.ApplicationCore.Interfaces.Repositories
{
    public interface IRepositoryRead<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Predicate<TEntity> condition);
    }
}
