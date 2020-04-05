using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Repositories;
using Conduit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conduit.Infrastructure.Repositories
{
    public class ArticleRepositoryRead : RepositoryRead<Article, int>, IArticleRepositoryRead
    {
        public ArticleRepositoryRead(ConduitDbContext dbContext) : base(dbContext)
        {

        }

        public Task<List<Article>> GetArticlesForAuthorAsync(string authorEmail)
        {
            return dbContext
                .Articles
                .AsNoTracking()
                .Where(a => a.Author.Email.Equals(authorEmail))
                .ToListAsync();
        }
    }
}
