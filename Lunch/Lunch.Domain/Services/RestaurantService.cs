using Lunch.Domain.Entities;
using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;

namespace Lunch.Domain.Services
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public int Add(string name)
        {
            int restaurantId = restaurantRepository.Add( name );
            return restaurantId;
        }

        public Restaurant Get(int id)
        {
            return restaurantRepository.Get( id );
        }

        public Restaurant GetByName( string name )
        {
            return restaurantRepository.GetByName( name );
        }
    }
}
