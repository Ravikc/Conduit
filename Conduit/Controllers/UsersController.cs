using System.Threading.Tasks;
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

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDtoRoot userRegistrationDtoRoot)
        {
            var identityResult = await _userService.RegisterAsync(userRegistrationDtoRoot.User);
            if (identityResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(identityResult.Errors);
        }
    }
}