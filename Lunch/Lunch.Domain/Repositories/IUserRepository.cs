using Lunch.Domain.Entities;

namespace Lunch.Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        int Add( string name );
    }
}
