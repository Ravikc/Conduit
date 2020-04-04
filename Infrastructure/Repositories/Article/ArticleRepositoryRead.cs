using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Repositories;
using Conduit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
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

        public Task<List<Article>> GetByAuthorIdAsync(string authorId)
        {
            return dbContext
                .Articles
                .AsNoTracking()
                .Where(a => a.AuthorId.Equals(authorId))
                .ToListAsync();
        }
    }
}
