using Lunch.Domain.Entities;
using Lunch.Domain.Repositories;
using Lunch.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Lunch.Infra.Data.Repositories
{
    public class RestaurantRepository : RepositoryBase, IRestaurantRepository
    {
        public RestaurantRepository(LunchDbContext dbContext) : base(dbContext) { }

        public async Task<int> Add( string name )
        {
            Models.Restaurant restaurant = new Models.Restaurant()
            {
                Name = name
            };
            dbContext.Restaurants.Add( restaurant );
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<Restaurant> Get( int id )
        {
            Models.Restaurant restaurant = await dbContext.Restaurants.SingleAsync( x => x.Id == id );
            return new Restaurant( restaurant.Id, restaurant.Name );
        }

        public async Task<Restaurant> GetByName( string name )
        {
            Models.Restaurant restaurant = await dbContext.Restaurants.SingleAsync( x => x.Name == name );
            return new Restaurant( restaurant.Id, restaurant.Name );
        }
    }
}
