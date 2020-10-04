using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using Lunch.Domain.Services;
using Lunch.Infra.Data.Data;
using Lunch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Lunch.Test.UnitTest
{
    public class PoolServiceTest
    {
        private IPoolService poolService;
        private IRestaurantService restaurantService;
        private IUserService userService;
        private IUserRepository userRepository;
        private IVoteRepository voteRepository;
        private IPoolRepository poolRepository;
        private LunchDbContextInMemory dbContext;

        public PoolServiceTest()
        {
            string dbName = Guid.NewGuid().ToString();
            DbContextOptionsBuilder<LunchDbContext> contextOptionsBuilder = new DbContextOptionsBuilder<LunchDbContext>().UseInMemoryDatabase( dbName );
            dbContext = new LunchDbContextInMemory( contextOptionsBuilder.Options );

            IRestaurantRepository restaurantRepository = new RestaurantRepository( dbContext );
            userRepository = new UserRepository( dbContext );
            voteRepository = new VoteRepository( dbContext );
            poolRepository = new PoolRepository( dbContext );

            restaurantService = new RestaurantService( restaurantRepository );
            userService = new UserService( userRepository );
            poolService = new PoolService( poolRepository, voteRepository, restaurantService, userService );
        }

        [Fact]
        public void SameRestaurantCannotBeChosenTwiceInAWeek()
        {
            // criar 3 usuarios
            // na primeira eleicao dois votam no 1
            // na segunda dois votam no 1

            int user1Id = userService.CreateUser( "user1" );
            int user2Id = userService.CreateUser( "user2" );
            int user3Id = userService.CreateUser( "user3" );
            int restaurant1Id = restaurantService.Add( "restaurant1" );
            int restaurant2Id = restaurantService.Add( "restaurant2" );

            CreateClosedPoolWhereRestaurantWasElected( restaurant1Id, user1Id, DateTime.Now.AddHours( -1 ) );
            int poolId = poolService.CreatePool( DateTime.Now.AddHours( 1 ) );
            poolService.Vote( poolId, user1Id, restaurant1Id );
            poolService.Vote( poolId, user2Id, restaurant1Id );
            poolService.Vote( poolId, user3Id, restaurant2Id );

            int electedRestaurantId = poolService.GetRestaurantElected( poolId );
            Assert.Equal( restaurant2Id, electedRestaurantId );
        }

        private void CreateClosedPoolWhereRestaurantWasElected(int restaurantId, int userId, DateTime closingTime)
        {
            Infra.Data.Models.User user = dbContext.Users.Single( x => x.Id == userId );
            Infra.Data.Models.Restaurant restaurant = dbContext.Restaurants.Single( x => x.Id == restaurantId );

            dbContext.Pools.Add( new Infra.Data.Models.Pool()
            {
                ClosingTime = DateTime.Now,
                Votes = new Collection<Infra.Data.Models.Vote>()
                {
                    new Infra.Data.Models.Vote()
                    {
                        Restaurant = restaurant,
                        User = user                        
                    }
                }
            } );
        }
    }
}
