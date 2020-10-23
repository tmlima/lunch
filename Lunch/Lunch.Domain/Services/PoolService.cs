using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> CreatePool(DateTime closingTime)
        {
            int poolId = await poolRepository.Add( closingTime );
            return poolId;
        }

        public async Task<IReadOnlyCollection<string>> CanGetPoolResults(int poolId)
        {
            Collection<string> errors = new Collection<string>();
            Pool pool = await poolRepository.Get( poolId );
            if ( pool.ClosingTime > DateTime.Now )
                errors.Add( "Pool has not been closed yet" );

            return errors;
        }

        public async Task<int> CanGetRestaurantElected( int poolId )
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<Restaurant, int>> GetPoolResults(int poolId)
        {
            return (await poolRepository.Get( poolId )).GetResults();
        }

        public async Task<IReadOnlyCollection<string>> CanVote(int poolId, int userId)
        {
            return (await poolRepository.Get( poolId )).CanVote( userId );
        }

        public async Task Vote(int poolId, int userId, int restaurantId)
        {
            if ( (await CanVote( poolId, userId )).Any() )
                throw new InvalidOperationException();

            Pool pool = await poolRepository.Get( poolId );
            User user = await userAppService.GetUser( userId );
            Restaurant restaurant = await restaurantAppService.Get( restaurantId );
            pool.AddVote( new Vote( user, restaurant ) );
            await voteRepository.Add( user, pool, restaurant );
        }

        public async Task<int> GetRestaurantElected( int poolId )
        {
            Pool pool = await poolRepository.Get( poolId );
            if ( pool.RestaurantElected == null )
                await UpdateAllClosedPoolsResult();

            return (await poolRepository.Get( poolId )).RestaurantElected.Id;
        }

        public async Task<IEnumerable<Pool>> GetAllAsync()
        {
            return await poolRepository.All();
        }

        private async Task UpdateAllClosedPoolsResult()
        {
            IEnumerable<Pool> closedPoolsNotUpdated = (await poolRepository.All()).Where( x => x.RestaurantElected == null && x.ClosingTime < DateTime.Now );
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
