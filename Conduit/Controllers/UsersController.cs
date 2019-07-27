﻿using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conduit.ApplicationCore.DTOs.User;
using Conduit.ApplicationCore.Errors;
using Conduit.ApplicationCore.Interfaces.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("working");
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequestDtoRoot userRegistrationDtoRoot)
        {           
            var identityResult = await _userService.RegisterAsync(userRegistrationDtoRoot.User);
            if (identityResult.Succeeded)
            {
                var user = await _userService.GetUserAsync(userRegistrationDtoRoot.User.Email, userRegistrationDtoRoot.User.Password);
                return Ok(new UserDtoRoot { User = user });
            }

            return BadRequest(ToErrorsList(identityResult.Errors));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginRequestDtoRoot userLoginDtoRoot)
        {
            var user = await _userService.GetUserAsync(userLoginDtoRoot.User.Email, userLoginDtoRoot.User.Password);
            if (user == null)
            {
                var error = new Errors
                {
                    Body = new List<string> { "Invalid credentials" }
                };

                return BadRequest(new ErrorsDtoRoot { Errors = error });
            }

            return Ok(new UserDtoRoot { User = user });
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] UserSettingsUpdateRequestDtoRoot userSettingsUpdateRequestDtoRoot)
        {
            try
            {
                string jwtToken = Request.Headers["Authorization"].ToString().Split(" ").ElementAt(1);
                string email = new JwtSecurityToken(jwtToken).Claims
                    .Single(c => c.Type.Equals(JwtRegisteredClaimNames.Email))
                    .Value;

                var userUpdated = await _userService.UpdateUserAsync(userSettingsUpdateRequestDtoRoot.UserSettingsUpdateRequestDto, email);
                if (userUpdated.Succeeded)
                {
                    return Ok(_userService.GetUserByEmailAsync(email));
                }

                return BadRequest(ToErrorsList(userUpdated.Errors));
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}