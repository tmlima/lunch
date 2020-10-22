using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using System.Threading.Tasks;

namespace Lunch.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> CreateUser(string name)
        {
            int userId = await userRepository.Add( name );
            return userId;
        }

        public async Task<User> GetUser(int userId)
        {
            return await userRepository.Get( userId );
        }
    }
}
