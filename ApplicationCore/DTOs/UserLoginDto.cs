using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conduit.ApplicationCore.DTOs
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

    #region Response

    public class UserLoginResponseDtoRoot
    {
        public UserLoginResponseDto User { get; set; }
    }

    public class UserLoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
    }

    #endregion
}
