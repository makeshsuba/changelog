using Changelog.Abstraction.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Changelog.Abstraction.Abstractions
{
   public interface IUserRegistrationService
    {
        Task<string> Register(UserRegistrationDTO user);
        Task<SecurityToken> Login(string username, string password,string provider);
        Task<bool> UserExists(string username);
    }
}
