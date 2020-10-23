using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lunch.Domain.Interfaces
{
    public interface IPoolService
    {
        Task<int> CreatePool(DateTime closingTime);
        Task<IReadOnlyCollection<string>> CanVote( int poolId, int userId );
        Task Vote(int poolId, int userId, int restaurantId);
        Task<int> GetRestaurantElected( int poolId );
        Task<Dictionary<Restaurant, int>> GetPoolResults( int poolId);
        Task<IEnumerable<Pool>> GetAllAsync();
    }
}
