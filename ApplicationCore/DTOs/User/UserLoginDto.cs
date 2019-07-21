using System.ComponentModel.DataAnnotations;

namespace Conduit.ApplicationCore.DTOs.User
{
    #region Request

    public class UserLoginRequestDtoRoot
    {
        public UserLoginRequestDto User { get; set; }
    }

    public class UserLoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    #endregion     
}
