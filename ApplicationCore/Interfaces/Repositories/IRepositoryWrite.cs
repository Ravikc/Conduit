using System.Threading.Tasks;
using Conduit.ApplicationCore.Entities;

namespace Conduit.ApplicationCore.Interfaces.Repositories
{
    public interface IRepositoryWrite<T> where T : BaseEntity<T>
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
