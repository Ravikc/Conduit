using Conduit.ApplicationCore.DTOs.Article;
using Conduit.ApplicationCore.Entities;
using System.Collections.Generic;

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

            CreateMap<IList<Article>, IList<ArticleResponseDto>>()
                .ReverseMap();
        }
    }
}
