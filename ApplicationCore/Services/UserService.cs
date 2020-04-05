using AutoMapper;
using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Entities;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
            return await _userManager.CreateAsync(user, userRegistrationDto?.Password).ConfigureAwait(false);
        }

        public async Task<UserDto> GetUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            if (user == null)
            {
                return null;
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false).ConfigureAwait(false);
            if (!signInResult.Succeeded)
            {
                return null;
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = GetToken(email);
            return userDto;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateRequestDto userSettingsUpdateRequestDto, string email)
        {
            if (userSettingsUpdateRequestDto == null)
            {
                return null;
            }

            var applicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            //ToDO: check if the password satisfies all the requirements.

            string passwordHash = string.IsNullOrWhiteSpace(userSettingsUpdateRequestDto.Password)
                ? applicationUser.PasswordHash
                : _userManager.PasswordHasher.HashPassword(applicationUser, userSettingsUpdateRequestDto.Password);

            applicationUser.UserName = userSettingsUpdateRequestDto.UserName ?? applicationUser.UserName;
            applicationUser.Bio = userSettingsUpdateRequestDto.Bio ?? applicationUser.Bio;
            applicationUser.Image = userSettingsUpdateRequestDto.ImageUrl ?? applicationUser.Image;
            applicationUser.Email = applicationUser.Email;
            applicationUser.PasswordHash = passwordHash;
            applicationUser.SecurityStamp = Guid.NewGuid().ToString();

            return await _userManager.UpdateAsync(applicationUser).ConfigureAwait(false);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            return _mapper.Map<UserDto>(applicationUser);
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
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
