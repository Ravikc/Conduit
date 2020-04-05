using Conduit.ApplicationCore.DTOs;
using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Conduit.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginRequestDtoRoot userLoginDtoRoot)
        {
            var user = await _userService.GetUserAsync(userLoginDtoRoot.User.Email, userLoginDtoRoot.User.Password);
            if (user == null)
            {
                var error = new Errors
                {
                    Body = new List<string> { "Invalid credentials" }
                };

                return BadRequest(new ErrorsDtoRoot { Errors = error });
            }

            return Ok(new UserDtoRoot { User = user });
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] UserSettingsUpdateRequestDtoRoot userSettingsUpdateRequestDtoRoot)
        {
            string jwtToken = Request.Headers["Authorization"].ToString().Split(" ").ElementAt(1);
            string email = new JwtSecurityToken(jwtToken).Claims
                .Single(c => c.Type.Equals(JwtRegisteredClaimNames.Email))
                .Value;

            var userUpdated = await _userService.UpdateUserAsync(userSettingsUpdateRequestDtoRoot.UserSettingsUpdateRequestDto, email);
            if (userUpdated.Succeeded)
            {
                var user = await _userService.GetUserByEmailAsync(email);
                user.Token = jwtToken;
                return Ok(new UserDtoRoot { User = user });
            }

            return BadRequest(ToErrorsList(userUpdated.Errors));
        }
    }
}