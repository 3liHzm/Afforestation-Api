using Afforestation.Domain.Infrastructure;

namespace Afforestation.App.Users
{
    [Service]
    public class GerUser
    {
        private readonly IUserManager _userManager;

        public GerUser(IUserManager  userManager)
        {
            _userManager = userManager;
        }

        public Response Do(int id)
        {
            return _userManager.GetUserById(id, s => new Response
            {
                Id = s.Id,
                FullName = s.FullName,
                UserName = s.UserName,
                Phone = s.Phone
            });

        }

        public class Response
        {
            public int Id { get; set; }
            
            public string FullName { get; set; }
            
            public string UserName { get; set; }
          
            public string Phone { get; set; }

        }
    }
}
