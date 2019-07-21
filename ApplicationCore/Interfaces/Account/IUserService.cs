using System.Threading.Tasks;
using Conduit.ApplicationCore.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Conduit.ApplicationCore.Interfaces.Account
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(UserRegistrationRequestDto userRegistrationDto);
        Task<UserDto> GetUserAsync(string email, string password);
    }
}
