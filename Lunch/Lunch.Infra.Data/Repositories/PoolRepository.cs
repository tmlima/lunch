using Lunch.Domain.Entities;
using Lunch.Domain.Repositories;
using Lunch.Infra.Data.Data;
using Lunch.Infra.Data.Mappers;
using System;
using System.Collections.Generic;
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
            return new Pool(pool.Id, pool.ClosingTime, votes);
        }

        public IList<Pool> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
