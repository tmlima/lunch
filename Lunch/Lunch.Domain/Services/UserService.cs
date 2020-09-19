using Lunch.Domain.Interfaces;
using Lunch.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lunch.Domain.Services
{
    public class UserService : IUserService
    {
        private List<User> users = new List<User>();

        public int CreateUser()
        {
            int userId = users.Count + 1;
            users.Add(new User(userId));
            return userId;
        }

        public User GetUser(int userId)
        {
            return users.Single(x => x.Id == userId);
        }
    }
}
