using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Conduit.ApplicationCore.DTOs.User;

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

        public async Task<UserDto> GetUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!signInResult.Succeeded)
            {
                return null;
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = GetToken(email);
            return userDto;
        }


        private string GetToken(string email)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
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

        public async Task<IdentityResult> UpdateUserAsync(UserSettingsUpdateRequestDto userSettingsUpdateRequestDto, string email)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            //ToDO: check if the password satisfies all password requirements

            string password = string.IsNullOrWhiteSpace(userSettingsUpdateRequestDto.Password)
                    ? applicationUser.PasswordHash
                    : _userManager.PasswordHasher.HashPassword(applicationUser, userSettingsUpdateRequestDto.Password);

            var updatedApplicationUser = new ApplicationUser
            {
                UserName = userSettingsUpdateRequestDto.UserName ?? applicationUser.UserName,
                Bio = userSettingsUpdateRequestDto.Bio ?? applicationUser.Bio,
                Image = userSettingsUpdateRequestDto.Image ?? applicationUser.Email,
                PasswordHash = password
            };

            return await _userManager.UpdateAsync(updatedApplicationUser);
            
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<UserDto>(applicationUser);
        }
    }
}
