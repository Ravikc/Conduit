using System;
using System.Collections.Generic;
using System.Text;

namespace Conduit.ApplicationCore.DTOs
{
    public class UserDtoRoot
    {
        public UserDto User { get; set; }
    }

    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
    }
}
