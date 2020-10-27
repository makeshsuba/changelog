using Changelog.Abstraction.Abstractions;
using Changelog.Abstraction.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Changelog.Business
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRegistrationStorage _userStorage;
        private readonly IConfiguration _config;

        public UserRegistrationService(IUserRegistrationStorage userStorage, IConfiguration config)
        {
            _userStorage = userStorage;
            _config = config;
        }

        public async Task<SecurityToken> Login(string username, string password, string provider)
        {
            var userinfo = await _userStorage.Login(username, password, provider);
            if (userinfo == null)
                return null;

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim (ClaimTypes.Name, userinfo)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Token").Value));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = credential
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        public Task<string> Register(UserRegistrationDTO user)
        {
            return _userStorage.Register(user);
        }

        public Task<bool> UserExists(string username)
        {
            return _userStorage.UserExists(username);
        }
    }
}
