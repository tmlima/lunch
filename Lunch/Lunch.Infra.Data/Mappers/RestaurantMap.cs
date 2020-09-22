using Lunch.Domain.Entities;

namespace Lunch.Infra.Data.Mappers
{
    class RestaurantMap
    {
        public Restaurant Map(Models.Restaurant restaurant)
        {
            return new Restaurant( restaurant.Id, restaurant.Name );
        }
    }
}
