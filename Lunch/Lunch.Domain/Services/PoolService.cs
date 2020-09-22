using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Lunch.Domain.Services
{
    public class PoolService : IPoolService
    {
        private IRestaurantService restaurantAppService;
        private IUserService userAppService;
        private IPoolRepository poolRepository;
        private IVoteRepository voteRepository;

        public PoolService( IPoolRepository poolRepository, IVoteRepository voteRepository, IRestaurantService restaurantAppService, IUserService userAppService)
        {
            this.poolRepository = poolRepository;
            this.voteRepository = voteRepository;
            this.restaurantAppService = restaurantAppService;
            this.userAppService = userAppService;
        }

        public int CreatePool(DateTime closingTime)
        {
            int poolId = poolRepository.Add( closingTime );
            return poolId;
        }

        public Dictionary<Restaurant, int> GetResults(int poolId)
        {
            return poolRepository.Get( poolId ).GetResults();
        }

        public void Vote(int poolId, int userId, int restaurantId)
        {
            Pool pool = poolRepository.Get( poolId );
            Restaurant restaurant = restaurantAppService.Get(restaurantId);
            User user = userAppService.GetUser(userId);
            pool.AddVote( new Entities.Vote( user, restaurant ) );
            voteRepository.Add( user, pool, restaurant );
        }
    }
}
