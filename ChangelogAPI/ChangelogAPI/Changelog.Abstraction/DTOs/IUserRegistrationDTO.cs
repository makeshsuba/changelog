using System;

namespace Changelog.Abstraction.DTOs
{
    public class UserRegistrationDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
    }
}
