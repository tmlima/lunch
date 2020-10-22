using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using System.Threading.Tasks;

namespace Lunch.Domain.Services
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public async Task<int> Add(string name)
        {
            int restaurantId = await restaurantRepository.Add( name );
            return restaurantId;
        }

        public async Task<Restaurant> Get(int id)
        {
            return await restaurantRepository.Get( id );
        }

        public async Task<Restaurant> GetByName( string name )
        {
            return await restaurantRepository.GetByName( name );
        }
    }
}
