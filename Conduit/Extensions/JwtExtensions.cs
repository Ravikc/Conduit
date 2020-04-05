using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Conduit.Web.Extensions
{
    public static class JwtExtensions
    {
        public static string GetEmail(this HttpRequest request)
        {
            var jwtToken = request.Headers["Authorization"].ToString().Split(" ").ElementAt(1);
            return new JwtSecurityToken(jwtToken)
                .Claims
                .Single(c => c.Type.Equals(JwtRegisteredClaimNames.Email))
                .Value;
        }
    }
}
