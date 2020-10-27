using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Changelog.Abstraction.Abstractions;
using Changelog.Abstraction.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace ChangelogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRegistrationService _userService;
        private readonly IConfiguration _config;

        public RegistrationController(IUserRegistrationService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDTO user)
        {
            user.UserName = user.UserName.ToLower();
            if (await _userService.UserExists(user.UserName))
                return BadRequest("User Already Exists");

            await _userService.Register(user);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRegistrationDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = await _userService.Login(user.UserName, user.Password,user.Provider);
            if (token == null)
            {
                return BadRequest("Not Authenticated");
            }

            return Ok(new { token = tokenHandler.WriteToken(token),username=user.UserName });
        }
    }
}
