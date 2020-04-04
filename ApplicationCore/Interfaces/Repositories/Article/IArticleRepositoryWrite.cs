using Conduit.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace Conduit.ApplicationCore.Interfaces.Repositories
{
    public interface IArticleRepositoryWrite : IRepositoryWrite<Article, int>
    {
        Task<Article> CreateArticleAsync(Article article);
    }
}
