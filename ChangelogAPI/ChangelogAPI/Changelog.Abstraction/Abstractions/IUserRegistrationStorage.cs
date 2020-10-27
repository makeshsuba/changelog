using Changelog.Abstraction.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Changelog.Abstraction.Abstractions
{
   public interface IUserRegistrationStorage
    {
        Task<string> Register(UserRegistrationDTO user);
        Task<string> Login(string username, string password, string provider);
        Task<bool> UserExists(string username);
    }
}
