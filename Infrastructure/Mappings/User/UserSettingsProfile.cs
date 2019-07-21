using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Entities;

namespace Conduit.Infrastructure.Mappings.User
{
    public class UserSettingsProfile : BaseMapper
    {
        public UserSettingsProfile()
        {
            CreateMap<UserSettingsUpdateRequestDto, ApplicationUser>()
                .ReverseMap();
        }
    }
}
