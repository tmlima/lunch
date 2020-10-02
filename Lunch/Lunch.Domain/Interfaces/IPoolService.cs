using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Lunch.Domain.Interfaces
{
    public interface IPoolService
    {
        int CreatePool(DateTime closingTime);
        IReadOnlyCollection<string> CanVote( int poolId, int userId );
        void Vote(int poolId, int userId, int restaurantId);
        Dictionary<Restaurant, int> GetResults(int poolId);
    }
}
