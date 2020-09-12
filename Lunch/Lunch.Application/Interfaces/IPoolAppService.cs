using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Application.Interfaces
{
    public interface IPoolAppService
    {
        int CreatePool(DateTime closingTime);
        void Vote(int poolId, int userId, int restaurantId);
        Dictionary<Restaurant, int> GetResults(int poolId);
    }
}
