using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Entities;

namespace Conduit.Infrastructure.Mappings.User
{
    public class UserProfile : BaseMapper
    {
        public UserProfile()
        {
            CreateMap<UserDto, ApplicationUser>()
                .ReverseMap();

            CreateMap<UserDto, UserRegistrationRequestDto>()
                .ReverseMap();

            CreateMap<UserDto, UserLoginRequestDto>()
                .ReverseMap();
        }
    }
}
