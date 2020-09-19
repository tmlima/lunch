using Lunch.Domain.Interfaces;
using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lunch.Domain.Services
{
    public class PoolService : IPoolService
    {
        private IRestaurantService restaurantAppService;
        private IUserService userAppService;

        List<Pool> pools = new List<Pool>();

        public PoolService(IRestaurantService restaurantAppService, IUserService userAppService)
        {
            this.restaurantAppService = restaurantAppService;
            this.userAppService = userAppService;
        }

        public int CreatePool(DateTime closingTime)
        {
            int poolId = pools.Count + 1;
            pools.Add(new Pool(poolId, closingTime));
            return poolId;
        }

        public Dictionary<Restaurant, int> GetResults(int poolId)
        {
            return pools.Single(x => x.Id == poolId).GetResults();
        }

        public void Vote(int poolId, int userId, int restaurantId)
        {
            Pool pool = pools.Single(x => x.Id == poolId);
            Restaurant restaurant = restaurantAppService.Get(restaurantId);
            User user = userAppService.GetUser(userId);
            pool.AddVote(new Vote(user, restaurant));
        }
    }
}
