using Lunch.Application.Interfaces;
using Lunch.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lunch.Application.Services
{
    public class RestaurantAppService : IRestaurantAppService
    {
        List<Restaurant> restaurants = new List<Restaurant>();

        public int Add(string name)
        {
            int id = restaurants.Count + 1;
            restaurants.Add(new Restaurant(id, name));
            return id;
        }

        public Restaurant Get(int id)
        {
            return restaurants.Single(x => x.Id == id);
        }
    }
}
