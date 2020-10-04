using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public IReadOnlyCollection<string> CanGetPoolResults(int poolId)
        {
            Collection<string> errors = new Collection<string>();
            Pool pool = poolRepository.Get( poolId );
            if ( pool.ClosingTime > DateTime.Now )
                errors.Add( "Pool has not been closed yet" );

            return errors;
        }

        public int CanGetRestaurantElected( int poolId )
        {
            throw new NotImplementedException();
        }

        public Dictionary<Restaurant, int> GetPoolResults(int poolId)
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

        public int GetRestaurantElected( int poolId )
        {
            Pool pool = poolRepository.Get( poolId );
            Dictionary<Restaurant, int> results = pool.GetResults();
            IEnumerable<Restaurant> restaurantsElected = results.OrderByDescending( x => x.Value ).Select( x => x.Key );
            ICollection<Pool> sameWeekPools = GetWeekPools(pool.ClosingTime);

            foreach (Restaurant r in restaurantsElected)
            {
                if ( RestaurantNotVotedSameWeek( r, sameWeekPools ) )
                    return r.Id;
            }

            return restaurantsElected.First().Id;
        }

        private ICollection<Pool> GetWeekPools(DateTime week)
        {
            throw new NotImplementedException();
        }

        private bool RestaurantNotVotedSameWeek( Restaurant restaurant, ICollection<Pool> sameWeekPools)
        {
            throw new NotImplementedException();

            //ICollection<Pool> closedWeekPools;
            //foreach ( Pool p in closedWeekPools )
            //{
            //    Dictionary<Restaurant, int> closedPoolResults = p.GetResults();
            //    Restaurant mostVoted = closedPoolResults.OrderByDescending( x => x.Value ).FirstOrDefault().Key;

            //}
        }
    }
}
