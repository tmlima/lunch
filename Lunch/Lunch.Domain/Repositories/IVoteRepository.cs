using Lunch.Domain.Entities;
using System.Threading.Tasks;

namespace Lunch.Domain.Repositories
{
    public interface IVoteRepository : IRepositoryBase<Vote>
    {
        Task Add(User user, Pool pool, Restaurant restaurant);
    }
}
