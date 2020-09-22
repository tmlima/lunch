using Lunch.Domain.Entities;

namespace Lunch.Domain.Repositories
{
    public interface IVoteRepository : IRepositoryBase<Vote>
    {
        void Add(User user, Pool pool, Restaurant restaurant);
    }
}
