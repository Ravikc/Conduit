using System;
using System.Collections.Generic;
using System.Text;
using Conduit.ApplicationCore.DTOs;
using Conduit.ApplicationCore.Entities;

namespace Conduit.Infrastructure.Mappings
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
