﻿using Lunch.Domain.Entities;
using Lunch.Domain.Repositories;
using Lunch.Infra.Data.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lunch.Infra.Data.Repositories
{
    public class VoteRepository : RepositoryBase, IVoteRepository
    {
        public VoteRepository(LunchDbContext dbContext) : base(dbContext) { }

        public async Task Add( User user, Pool pool, Restaurant restaurant )
        {
            Models.User userModel = dbContext.Users.Single( x => x.Id == user.Id );
            Models.Pool poolModel = dbContext.Pools.Single( x => x.Id == pool.Id );
            Models.Restaurant restaurantModel = dbContext.Restaurants.Single( x => x.Id == restaurant.Id );

            dbContext.Votes.Add( new Models.Vote()
            {
                User = userModel,
                Pool = poolModel,
                Restaurant = restaurantModel
            } );
            await dbContext.SaveChangesAsync();
        }

        public async Task<Vote> Get( int id )
        {
            throw new NotImplementedException();
        }
    }
}
