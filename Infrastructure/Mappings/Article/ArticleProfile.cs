using Conduit.ApplicationCore.DTOs.Articles;
using Conduit.ApplicationCore.Entities;

namespace Conduit.Infrastructure.Mappings
{
    public class ArticleProfile : BaseMapper
    {
        public ArticleProfile()
        {
            CreateMap<Article, UpsertArticleRequestDto>()
               .ReverseMap();

            CreateMap<Article, ArticleResponseDto>()
                .ReverseMap();
        }
    }
}
