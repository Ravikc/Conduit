using System.Threading.Tasks;
using AutoMapper;
using Conduit.ApplicationCore.DTOs;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers
{
    [AllowAnonymous]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("")]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequestDtoRoot userRegistrationDtoRoot)
        {           
            var identityResult = await _userService.RegisterAsync(userRegistrationDtoRoot.User);
            if (identityResult.Succeeded)
            {
                var user = await _userService.GetUserAsync(userRegistrationDtoRoot.User.Email, userRegistrationDtoRoot.User.Password);
                return Ok(new UserDtoRoot { User = user });
            }

            return BadRequest(ToErrorsList(identityResult.Errors));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequestDtoRoot userLoginDtoRoot)
        {
            var user = await _userService.GetUserAsync(userLoginDtoRoot.User.Email, userLoginDtoRoot.User.Password);
            return Ok(new UserDtoRoot { User = user });
        }
    }
}