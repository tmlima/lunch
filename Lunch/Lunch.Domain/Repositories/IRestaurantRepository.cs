using Lunch.Domain.Entities;

namespace Lunch.Domain.Repositories
{
    public interface IRestaurantRepository : IRepositoryBase<Restaurant>
    {
        int Add( string name );
        Restaurant GetByName( string name );
    }
}
