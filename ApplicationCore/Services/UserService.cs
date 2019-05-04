using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Conduit.ApplicationCore.DTOs;
using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Identity;

namespace Conduit.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            var user = _mapper.Map<ApplicationUser>(userRegistrationDto);
            var result = await _userManager.CreateAsync(new ApplicationUser
            { Email = userRegistrationDto.Email, UserName = userRegistrationDto.UserName },
                userRegistrationDto.Password);

            return result;
        }
    }
}
