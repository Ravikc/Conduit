using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Entities;

namespace Conduit.Infrastructure.Mappings.User
{
    public class UserLoginProfile : BaseMapper
    {
        public UserLoginProfile()
        {
            CreateMap<UserLoginRequestDto, ApplicationUser>()
                .ReverseMap();
        }
    }
}
