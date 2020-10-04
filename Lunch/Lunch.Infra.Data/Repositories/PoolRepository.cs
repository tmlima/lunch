using Lunch.Domain.Entities;
using Lunch.Domain.Repositories;
using Lunch.Infra.Data.Data;
using Lunch.Infra.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lunch.Infra.Data.Repositories
{
    public class PoolRepository : RepositoryBase, IPoolRepository
    {
        public PoolRepository(LunchDbContext dbContext) : base(dbContext) { }

        public int Add( DateTime closingTime )
        {
            Models.Pool pool = new Models.Pool()
            {
                ClosingTime = closingTime
            };

            dbContext.Pools.Add( pool );
            dbContext.SaveChanges();
            return pool.Id;
        }

        public Pool Get( int id )
        {
            Models.Pool pool = dbContext.Pools.Single( x => x.Id == id );
            IList<Vote> votes = pool.Votes.Select( x => new Vote(
                new UserMap().Map( x.User ),
                new RestaurantMap().Map( x.Restaurant )
            ) ).ToList();
            Restaurant elected = pool.RestaurantElected == null ? null : new RestaurantMap().Map( pool.RestaurantElected );
            return new Pool(
                pool.Id, 
                pool.ClosingTime, 
                votes,
                elected
            );
        }

        public IEnumerable<Pool> GetWeekPools(int weekOfYear, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek)
        {
            return dbContext.Pools.Where(x => DateTimeFormatInfo.InvariantInfo.Calendar.GetWeekOfYear( x.ClosingTime, calendarWeekRule, firstDayOfWeek ) == weekOfYear).Select( x => new Pool(
                x.Id,
                x.ClosingTime,
                x.Votes.Select( y => new VoteMap().Map( y ) ),
                new RestaurantMap().Map(x.RestaurantElected))
            );
        }

        public IEnumerable<Pool> All()
        {
            return dbContext.Pools.Select( x => new Pool(
                x.Id,
                x.ClosingTime,
                x.Votes.Select( x => new Vote(
                    new UserMap().Map( x.User ),
                    new RestaurantMap().Map( x.Restaurant )
                ) ).ToList(),
                x.RestaurantElected == null ? null : new RestaurantMap().Map( x.RestaurantElected ))
            );
        }

        public void UpdateElectedRestaurant( int poolId, int restaurantId )
        {
            Models.Pool pool = dbContext.Pools.Single( x => x.Id == poolId );
            Models.Restaurant restaurant = dbContext.Restaurants.Single( x => x.Id == restaurantId );
            pool.RestaurantElected = restaurant;
            dbContext.Update( pool );
            dbContext.SaveChanges();
        }
    }
}
