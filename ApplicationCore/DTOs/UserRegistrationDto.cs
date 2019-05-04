using System.ComponentModel.DataAnnotations;

namespace Conduit.ApplicationCore.DTOs
{
    public class UserRegistrationDtoRoot
    {
        [Required]
        public UserRegistrationDto User { get; set; }
    }

    public class UserRegistrationDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
