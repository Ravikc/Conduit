using System.Threading.Tasks;
using Conduit.ApplicationCore.Entities;

namespace Conduit.ApplicationCore.Interfaces.Repositories
{
    public interface IRepositoryWrite<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
