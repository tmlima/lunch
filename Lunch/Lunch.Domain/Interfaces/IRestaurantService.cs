using Lunch.Domain.Entities;
using System.Threading.Tasks;

namespace Lunch.Domain.Interfaces
{
    public interface IRestaurantService
    {
        Task<int> Add(string name);
        Task<Restaurant> Get(int id);
        Task<Restaurant> GetByName( string name );
    }
}
