using System.Threading.Tasks;
using AutoMapper;
using Conduit.ApplicationCore.DTOs;
using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequestDtoRoot userRegistrationDtoRoot)
        {
            var identityResult = await _userService.RegisterAsync(userRegistrationDtoRoot.User);
            if (identityResult.Succeeded)
            {
                var userLoginRequestDto = _mapper.Map<UserLoginRequestDto>(userRegistrationDtoRoot.User);
                return await Login(new UserLoginRequestDtoRoot { User = userLoginRequestDto });
            }

            return BadRequest(identityResult.Errors);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequestDtoRoot userLoginDtoRoot)
        {
            string token = await _userService.LoginAsync(userLoginDtoRoot.User);
            return Ok(token);
        }

        [HttpGet("test")]
        public string Test()
        {
            return "test successful.";
        }
    }
}