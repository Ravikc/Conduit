using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Conduit.ApplicationCore.DTOs;
using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace Conduit.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegistrationRequestDto userRegistrationDto)
        {
            var user = _mapper.Map<ApplicationUser>(userRegistrationDto);
            return await _userManager.CreateAsync(user, userRegistrationDto.Password);
        }

        public async Task<string> LoginAsync(UserLoginRequestDto userLoginDto)
        {
            //if (!await AuthenticateAsync(userLoginDto))
            //{
            //    return null;
            //}

            string token = GetToken(userLoginDto);
            return token;
        }

        private string GetToken(UserLoginRequestDto userLoginDto)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userLoginDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Secret"]));
            var token = new JwtSecurityToken(
                _configuration["Token:Issuer"],
                _configuration["Token:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<bool> AuthenticateAsync(UserLoginRequestDto userLoginDto)
        {
            var user = _mapper.Map<ApplicationUser>(userLoginDto);
            return (await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false)).Succeeded;
            //return await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
        }
    }
}
