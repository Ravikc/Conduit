using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
       
    }
}