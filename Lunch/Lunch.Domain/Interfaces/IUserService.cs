using Lunch.Domain.Entities;
using System.Threading.Tasks;

namespace Lunch.Domain.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUser(string name);
        Task<User> GetUser(int userId); 
    }
}
