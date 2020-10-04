using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Lunch.Domain.Services
{
    public class PoolService : IPoolService
    {
        private IRestaurantService restaurantAppService;
        private IUserService userAppService;
        private IPoolRepository poolRepository;
        private IVoteRepository voteRepository;
        private const CalendarWeekRule DefaultCalendarWeekRule = CalendarWeekRule.FirstDay;
        private const DayOfWeek DefaultFirstDayOfWeek = DayOfWeek.Monday;

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
            pool.AddVote( new Vote( user, restaurant ) );
            voteRepository.Add( user, pool, restaurant );
        }

        public int GetRestaurantElected( int poolId )
        {
            Pool pool = poolRepository.Get( poolId );
            if ( pool.RestaurantElected == null )
                UpdateAllClosedPoolsResult();

            return poolRepository.Get( poolId ).RestaurantElected.Id;
        }

        private void UpdateAllClosedPoolsResult()
        {
            IEnumerable<Pool> closedPoolsNotUpdated = poolRepository.All().Where( x => x.RestaurantElected == null && x.ClosingTime < DateTime.Now );
            IEnumerable<IGrouping<int, Pool>> weeks = closedPoolsNotUpdated.GroupBy( x => DateTimeFormatInfo.InvariantInfo.Calendar.GetWeekOfYear( x.ClosingTime, DefaultCalendarWeekRule, DefaultFirstDayOfWeek ) );
            foreach ( IGrouping<int, Pool> w in weeks)
            {
                ICollection<int> electedRestaurantsId = new List<int>();
                foreach (Pool p in w.OrderBy(x => x.ClosingTime))
                    electedRestaurantsId.Add( UpdatePoolResult( p, electedRestaurantsId ) );
            }
        }

        private int UpdatePoolResult(Pool pool, ICollection<int> electedRestaurantsId)
        {
            IList<Restaurant> results = pool.GetResults().OrderByDescending( x => x.Value ).Select( x => x.Key ).ToList();
            int? restaurantElectedId = null;
            foreach ( Restaurant r in results )
            {
                if ( !electedRestaurantsId.Contains( r.Id ) )
                {
                    restaurantElectedId = r.Id;
                    break;
                }
            }
            if ( !restaurantElectedId.HasValue )
                restaurantElectedId = results.First().Id;

            poolRepository.UpdateElectedRestaurant( pool.Id, restaurantElectedId.Value );
            return restaurantElectedId.Value;
        }
    }
}
