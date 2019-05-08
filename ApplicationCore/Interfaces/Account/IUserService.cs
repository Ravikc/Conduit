using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Conduit.ApplicationCore.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Conduit.ApplicationCore.Interfaces.Account
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(UserRegistrationRequestDto userRegistrationDto);
        Task<string> LoginAsync(UserLoginRequestDto userLoginDto);
    }
}
