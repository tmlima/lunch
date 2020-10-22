using Lunch.Domain.Entities;
using System.Threading.Tasks;

namespace Lunch.Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<int> Add( string name );
    }
}
