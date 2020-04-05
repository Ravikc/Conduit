using AutoMapper;
using Conduit.ApplicationCore.DTOs.Articles;
using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Account;
using Conduit.ApplicationCore.Interfaces.Repositories;
using Conduit.ApplicationCore.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conduit.ApplicationCore.Services
{
    public class ArticleService : IArticleService

    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IArticleRepositoryRead _readRepository;
        private readonly IArticleRepositoryWrite _writeRepository;

        public ArticleService(IMapper mapper, IUserService userService, IArticleRepositoryRead readRepository, IArticleRepositoryWrite writeRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<ArticleResponseDto> CreateArticleAsync(UpsertArticleRequestDto articleDTO, string authorEmail)
        {
            var article = _mapper.Map<Article>(articleDTO);

            var author = await _userService.GetUserByEmailAsync(authorEmail).ConfigureAwait(false);
            article.Author = _mapper.Map<ApplicationUser>(author);

            article = await _writeRepository.CreateArticleAsync(article).ConfigureAwait(false);
            if (article == null)
            {
                return null;
            }
            
            article = await _readRepository.GetByIdAsync(article.Id).ConfigureAwait(false);

            article.UpdateSlugWithId();
            await _writeRepository.UpdateAsync(article).ConfigureAwait(false);

            return _mapper.Map<ArticleResponseDto>(article);
        }

        public async Task<IEnumerable<ArticleResponseDto>> GetAllArticlesAsync()
        {
            var articles = await _readRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ArticleResponseDto>>(articles);
        }

        public async Task<IEnumerable<ArticleResponseDto>> GetArticlesForAuthorAsync(string authorEmail)
        {
            var articlesForAuthor = await _readRepository.GetArticlesForAuthorAsync(authorEmail).ConfigureAwait(false);
            return _mapper.Map<List<Article>, List<ArticleResponseDto>>(articlesForAuthor);
        }
    }
}
