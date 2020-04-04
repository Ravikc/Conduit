using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conduit.ApplicationCore.DTOs.Article;
using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Errors;
using Conduit.ApplicationCore.Interfaces.Account;
using Conduit.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleService.GetAllArticles();
            return Ok(new { Articles = articles });
        }

    }
}