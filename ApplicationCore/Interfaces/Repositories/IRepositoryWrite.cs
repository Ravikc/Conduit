using Conduit.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace Conduit.ApplicationCore.Interfaces.Repositories
{
    public interface IRepositoryWrite<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
