using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Lunch.Domain.Interfaces
{
    public interface IPoolService
    {
        int CreatePool(DateTime closingTime);
        void Vote(int poolId, int userId, int restaurantId);
        Dictionary<Restaurant, int> GetResults(int poolId);
    }
}
