using Business.Services.Interfaces;
using Data.Data;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly InstagramContextDB db;

        public UserService(InstagramContextDB db)
        {
            this.db = db;
        }

        public bool IsEmailUnique(string email)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == email);

            return user is null;
        }

        public bool IsUserNameUnique(string userName)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == userName);

            return user is null;
        }
    }
}
