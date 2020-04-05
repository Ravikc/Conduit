using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Conduit.ApplicationCore.DTOs.Article;
using Conduit.ApplicationCore.Interfaces.Services;
using Conduit.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers
{
    [Authorize]
    public class ArticlesController : BaseApiController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleDtoRoot<UpsertArticleRequestDto> dto)
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Split(" ").ElementAt(1);
            string email = new JwtSecurityToken(jwtToken).Claims
                .Single(c => c.Type.Equals(JwtRegisteredClaimNames.Email))
                .Value;

            var article = await _articleService.CreateArticleAsync(dto.Article, email);
            return Ok(new { article });
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(new { articles });
        }

        [HttpGet("Feed")]
        public async Task<IActionResult> Feed()
        {
            string authorEmail = Request.GetEmail();
            var articles = await _articleService.GetArticlesForAuthorAsync(authorEmail);
            return Ok(new { articles });
        }
    }
}