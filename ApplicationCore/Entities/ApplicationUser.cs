using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Conduit.ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(1024)]
        public string Bio { get; set; }

        [MaxLength(1024)]
        public string Image { get; set; }

    }
}
