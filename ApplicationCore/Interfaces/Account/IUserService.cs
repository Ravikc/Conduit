using Conduit.ApplicationCore.DTOs.User;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Conduit.ApplicationCore.Interfaces.Account
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(UserRegistrationRequestDto userRegistrationDto);
        Task<UserDto> GetUserAsync(string email, string password);
        Task<IdentityResult> UpdateUserAsync(UserUpdateRequestDto userSettingsUpdateRequestDto, string email);
        Task<UserDto> GetUserByEmailAsync(string email);
    }
}
