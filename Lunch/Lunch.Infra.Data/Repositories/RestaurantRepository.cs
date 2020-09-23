using Lunch.Domain.Entities;
using Lunch.Domain.Repositories;
using Lunch.Infra.Data.Data;
using System.Linq;

namespace Lunch.Infra.Data.Repositories
{
    public class RestaurantRepository : RepositoryBase, IRestaurantRepository
    {
        public RestaurantRepository(LunchDbContext dbContext) : base(dbContext) { }

        public int Add( string name )
        {
            dbContext.Restaurants.Add( new Models.Restaurant()
            {
                Name = name
            } );
            int id = dbContext.SaveChanges();
            return id;
        }

        public Restaurant Get( int id )
        {
            Models.Restaurant restaurant = dbContext.Restaurants.Single( x => x.Id == id );
            return new Restaurant( restaurant.Id, restaurant.Name );
        }

        public Restaurant GetByName( string name )
        {
            Models.Restaurant restaurant = dbContext.Restaurants.Single( x => x.Name == name );
            return new Restaurant( restaurant.Id, restaurant.Name );
        }
    }
}
