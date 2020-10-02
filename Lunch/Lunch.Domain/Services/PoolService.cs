using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IReadOnlyCollection<string> CanVote(int poolId, int userId)
        {
            Pool pool = poolRepository.Get( poolId );
            return pool.CanVote( userId );
        }

        public void Vote(int poolId, int userId, int restaurantId)
        {
            if ( CanVote( poolId, userId ).Any() )
                throw new InvalidOperationException();

            Pool pool = poolRepository.Get( poolId );
            User user = userAppService.GetUser( userId );
            Restaurant restaurant = restaurantAppService.Get( restaurantId );
            pool.AddVote( new Entities.Vote( user, restaurant ) );
            voteRepository.Add( user, pool, restaurant );
        }
    }
}
