using Conduit.ApplicationCore.DTOs.Article;
using Conduit.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conduit.ApplicationCore.Interfaces.Services
{
    public interface IArticleService : IServiceBase<Article>
    {
        Task<ArticleResponseDto> CreateArticleAsync(UpsertArticleRequestDto article, string authorEmail);
        Task<IEnumerable<ArticleResponseDto>> GetAllArticlesAsync();
        Task<IEnumerable<ArticleResponseDto>> GetArticlesForAuthorAsync(string authorEmail);
    }
}
