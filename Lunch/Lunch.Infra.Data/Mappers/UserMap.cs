using Lunch.Domain.Entities;

namespace Lunch.Infra.Data.Mappers
{
    class UserMap
    {
        public User Map(Models.User user)
        {
            return new User( user.Id, user.Name );
        }
    }
}
