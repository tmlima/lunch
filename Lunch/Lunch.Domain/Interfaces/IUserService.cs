using Lunch.Domain.Entities;

namespace Lunch.Domain.Interfaces
{
    public interface IUserService
    {
        int CreateUser();
        User GetUser(int userId); 
    }
}
