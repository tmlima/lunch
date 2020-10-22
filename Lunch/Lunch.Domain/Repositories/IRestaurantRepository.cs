using Lunch.Domain.Entities;
using System.Threading.Tasks;

namespace Lunch.Domain.Repositories
{
    public interface IRestaurantRepository : IRepositoryBase<Restaurant>
    {
        Task<int> Add( string name );
        Task<Restaurant> GetByName( string name );
    }
}
