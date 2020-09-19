using Lunch.Domain.Entities;

namespace Lunch.Domain.Interfaces
{
    public interface IRestaurantService
    {
        int Add(string name);
        Restaurant Get(int id);
    }
}
