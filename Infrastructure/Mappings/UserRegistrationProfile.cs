using System;
using System.Collections.Generic;
using System.Text;
using Conduit.ApplicationCore.DTOs;
using Conduit.ApplicationCore.Entities;

namespace Conduit.Infrastructure.Mappings
{
    public class UserRegistrationProfile : BaseMapper
    {
        public UserRegistrationProfile()
        {
            CreateMap<UserRegistrationDto, ApplicationUser>()
                .ReverseMap();
        }
    }
}
