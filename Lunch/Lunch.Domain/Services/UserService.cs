using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;

namespace Lunch.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public int CreateUser(string name)
        {
            int userId = userRepository.Add( name );
            return userId;
        }

        public User GetUser(int userId)
        {
            return userRepository.Get( userId );
        }
    }
}
