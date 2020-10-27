using Changelog.Abstraction.Abstractions;
using Changelog.Abstraction.DTOs;
using Changelog.EFCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Changelog.EFCore
{
    public class UserRegistrationRepository : IUserRegistrationStorage
    {
        private readonly ChangelogContext _context;

        public UserRegistrationRepository(ChangelogContext context)
        {
            _context = context;
        }
        public async Task<string> Login(string username, string password, string provider)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null && !string.IsNullOrEmpty(provider)) // register new users who is logs through social accounts
            {
                UserRegistrationDTO userData = new UserRegistrationDTO
                {
                    UserName = username,
                    Password = password
                };
                var userName = await Register(userData);
                user = await _context.User.FirstOrDefaultAsync(x => x.Username == username);
            }

            if (user == null || !VerfiyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return username;
        }

        private bool VerfiyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (passwordHash[i] != computedHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<string> Register(UserRegistrationDTO user)
        {
            CreatingPasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            UserRegistrationModel userModel = new UserRegistrationModel
            {
                Username = user.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _context.User.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel.Username;
        }

        private void CreatingPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public Task<bool> UserExists(string username)
        {
            return _context.User.AnyAsync(x => x.Username == username);
        }
    }
}
