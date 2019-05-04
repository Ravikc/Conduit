using System.Threading.Tasks;
using Conduit.ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers
{
    public class UsersController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult RegisterUser(UserRegistrationDtoRoot userRegistrationDtoRoot)
        {
            return Ok();
        }
    }
}