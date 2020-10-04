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
        private IRestaurantRepository restaurantRepository;
        private LunchDbContextInMemory dbContext;
        private IList<Infra.Data.Models.User> users = new List<Infra.Data.Models.User>();
        private IList<Infra.Data.Models.Restaurant> restaurants = new List<Infra.Data.Models.Restaurant>();

        public PoolServiceTest()
        {
            string dbName = Guid.NewGuid().ToString();
            DbContextOptionsBuilder<LunchDbContext> contextOptionsBuilder = new DbContextOptionsBuilder<LunchDbContext>().UseInMemoryDatabase( dbName );
            dbContext = new LunchDbContextInMemory( contextOptionsBuilder.Options );

            restaurantRepository = new RestaurantRepository( dbContext );
            userRepository = new UserRepository( dbContext );
            voteRepository = new VoteRepository( dbContext );
            poolRepository = new PoolRepository( dbContext );

            restaurantService = new RestaurantService( restaurantRepository );
            userService = new UserService( userRepository );
            poolService = new PoolService( poolRepository, voteRepository, restaurantService, userService );
            CreateUsersAndRestaurants();
        }

        [Fact]
        public void SameRestaurantCannotBeChosenTwiceInAWeek()
        {
            CreateClosedPool( new Collection<Infra.Data.Models.Vote>()
            {
                new Infra.Data.Models.Vote()
                {
                    User = users[0],
                    Restaurant = restaurants[0]
                }
            } );

            int poolId = CreateClosedPool( new Collection<Infra.Data.Models.Vote>()
            {
                new Infra.Data.Models.Vote()
                {
                    User = users[0],
                    Restaurant = restaurants[0]
                },
                new Infra.Data.Models.Vote()
                {
                    User = users[1],
                    Restaurant = restaurants[0]
                },
                new Infra.Data.Models.Vote()
                {
                    User = users[2],
                    Restaurant = restaurants[1]
                }
            } );

            int electedRestaurantId = poolService.GetRestaurantElected( poolId );
            Assert.Equal( restaurants[ 1 ].Id, electedRestaurantId );
        }

        [Fact]
        public void GetMostVotedRestaurantWhenAllWereAlreadyElectedInSameWeek()
        {
            CreateClosedPool( new Collection<Infra.Data.Models.Vote>()
            {
                new Infra.Data.Models.Vote()
                {
                    User = users[0],
                    Restaurant = restaurants[0]
                }
            } );

            CreateClosedPool( new Collection<Infra.Data.Models.Vote>()
            {
                new Infra.Data.Models.Vote()
                {
                    User = users[0],
                    Restaurant = restaurants[0]
                },
                new Infra.Data.Models.Vote()
                {
                    User = users[1],
                    Restaurant = restaurants[0]
                },
                new Infra.Data.Models.Vote()
                {
                    User = users[2],
                    Restaurant = restaurants[1]
                }
            } );

            int poolId = CreateClosedPool( new Collection<Infra.Data.Models.Vote>()
            {
                new Infra.Data.Models.Vote()
                {
                    User = users[0],
                    Restaurant = restaurants[0]
                },
                new Infra.Data.Models.Vote()
                {
                    User = users[1],
                    Restaurant = restaurants[0]
                },
                new Infra.Data.Models.Vote()
                {
                    User = users[2],
                    Restaurant = restaurants[1]
                }
            } );

            int electedRestaurantId = poolService.GetRestaurantElected( poolId );
            Assert.Equal( restaurants[ 0 ].Id, electedRestaurantId );
        }

        private void CreateUsersAndRestaurants()
        {
            users.Add( dbContext.Users.Single( x => x.Id == userService.CreateUser( "user1" ) ) );
            users.Add( dbContext.Users.Single( x => x.Id == userService.CreateUser( "user2" ) ) );
            users.Add( dbContext.Users.Single( x => x.Id == userService.CreateUser( "user3" ) ) );

            restaurants.Add( dbContext.Restaurants.Single( x => x.Id == restaurantService.Add( "restaurant1" ) ) );
            restaurants.Add( dbContext.Restaurants.Single( x => x.Id == restaurantService.Add( "restaurant2" ) ) );
        }

        private int CreateClosedPool(ICollection<Infra.Data.Models.Vote> votes)
        {
            Infra.Data.Models.Pool pool = new Infra.Data.Models.Pool()
            {
                ClosingTime = DateTime.Now,
                Votes = votes
            };
            dbContext.Pools.Add( pool );
            dbContext.SaveChanges();
            return pool.Id;
        }
    }
}
