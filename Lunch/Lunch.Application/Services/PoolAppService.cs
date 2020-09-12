using Lunch.Application.Interfaces;
using Lunch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lunch.Application.Services
{
    public class PoolAppService : IPoolAppService
    {
        private IRestaurantAppService restaurantAppService;
        private IUserAppService userAppService;

        List<Pool> pools = new List<Pool>();

        public PoolAppService(IRestaurantAppService restaurantAppService, IUserAppService userAppService)
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
