using Afforestation.Domain.Infrastructure;
using Afforestation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Afforestation.Database.Managers
{
    public class UsersManager : IUserManager
    {
        private readonly AppDbContext _ctx;

        public UsersManager(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Users> Login(string userName, string phone, string password)
        {
            var user = new Users();
            if (userName == null)
            {
                user = await _ctx.Users.FirstOrDefaultAsync(s => s.Phone == phone);
            }
            else
            {

                 user = await _ctx.Users.FirstOrDefaultAsync(s => s.UserName == userName);
            }
                if (user == null)
                {
                    return null;
                }
                if (!Verify(password, user.PasswordSalt, user.PasswordHash))
                {
                    return null;
                }
                return user;
            }
            private bool Verify(string password, byte[] passwordSalt, byte[] passwordHash)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
                {
                    var unHashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));//System.Text.Encoding.UTF8.GetBytes(password) conveting the string to array of bytes
                    for (int i = 0; i < unHashPassword.Length; i++)
                    {
                        if (unHashPassword[i] != passwordHash[i])
                        {
                            return false;
                        }
                    }
                    return true;

                }
            
            }

        public async Task<Users> Register(Users user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatPasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        private void CreatPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));//System.Text.Encoding.UTF8.GetBytes(password) conveting the string to array of bytes
            }
        }


        public async Task<bool> UserExist(string userName, string phone)
        {
            if (await _ctx.Users.AnyAsync(s => s.UserName == userName || s.Phone == phone))
            {
                return true;
            }
            return false;
        }

        public TResult GetUserById<TResult>(int id, Func<Users, TResult> selector)
        {
            return _ctx.Users.Where(s => s.Id == id).Select(selector)
                .FirstOrDefault();
              
            
        }
    }
}
