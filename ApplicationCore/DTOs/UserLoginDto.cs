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
}
