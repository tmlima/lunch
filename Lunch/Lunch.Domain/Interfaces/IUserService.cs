using Lunch.Domain.Entities;

namespace Lunch.Domain.Interfaces
{
    public interface IUserService
    {
        int CreateUser(string name);
        User GetUser(int userId); 
    }
}
