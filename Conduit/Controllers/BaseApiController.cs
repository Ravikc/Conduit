using Conduit.ApplicationCore.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Conduit.Web.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ErrorsDtoRoot ToErrorsList(IEnumerable<IdentityError> identityErrors)
        {
            return new ErrorsDtoRoot
            {
                Errors = new Errors
                {
                    Body = new List<string>(identityErrors.Select(e => e.Description))
                }
            };
        }

        
    }
}