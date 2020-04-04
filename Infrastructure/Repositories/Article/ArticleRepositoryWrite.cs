using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Repositories;
using Conduit.Infrastructure.Data;
using System.Threading.Tasks;

namespace Conduit.Infrastructure.Repositories
{
    public class ArticleRepositoryWrite : RepositoryWrite<Article, int>, IArticleRepositoryWrite
    {
        public ArticleRepositoryWrite(ConduitDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            dbContext.Articles.Add(article);
            var numRowsAffected = await dbContext.SaveChangesAsync();
            return numRowsAffected == 1 ? article : null;
        }
    }
}
