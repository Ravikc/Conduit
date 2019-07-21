using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Entities;

namespace Conduit.Infrastructure.Mappings.User
{
    public class UserRegistrationProfile : BaseMapper
    {
        public UserRegistrationProfile()
        {
            CreateMap<UserRegistrationRequestDto, ApplicationUser>()
                .ReverseMap();

            CreateMap<UserRegistrationRequestDto, UserLoginRequestDto>()
                .ReverseMap();


        }
    }
}
