using Afforestation.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Afforestation.Domain.Infrastructure
{
    public interface IUserManager
    {
        Task<Users> Register(Users user, string password);
        Task<Users> Login(string userName, string phone, string password);
        Task<bool> UserExist(string userName, string phone);

        TResult GetUserById<TResult>(int id, Func<Users, TResult> selector);
    }
}
