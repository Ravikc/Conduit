using System.ComponentModel.DataAnnotations;

namespace Conduit.ApplicationCore.DTOs.User
{
    public class UserRegistrationRequestDtoRoot
    {
        [Required]
        public UserRegistrationRequestDto User { get; set; }
    }

    public class UserRegistrationRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]        
        public string Password { get; set; }
    }
}
