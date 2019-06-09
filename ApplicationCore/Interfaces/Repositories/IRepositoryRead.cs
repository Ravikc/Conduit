using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Conduit.ApplicationCore.Entities;

namespace Conduit.ApplicationCore.Interfaces.Repositories
{
    public interface IRepositoryRead<T> where T : BaseEntity<T>
    {
        Task<T> GetByIdAsync<TKey>(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Predicate<T> condition);
    }
}
