using Conduit.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conduit.ApplicationCore.Interfaces.Repositories
{
    public interface IArticleRepositoryRead : IRepositoryRead<Article, int>
    {
        Task<List<Article>> GetByAuthorIdAsync(string authorId);
    }
}
