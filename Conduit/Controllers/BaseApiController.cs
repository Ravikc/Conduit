using Conduit.ApplicationCore.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Conduit.Web.Controllers
{
    [Route("/api/v1/[controller]")]
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