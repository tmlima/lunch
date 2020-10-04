using Lunch.Domain.Entities;

namespace Lunch.Infra.Data.Mappers
{
    class VoteMap
    {
        public Vote Map( Models.Vote vote )
        {
            return new Vote(
                vote.Id,
                new UserMap().Map( vote.User ),
                new RestaurantMap().Map( vote.Restaurant )
            );
        }
    }
}
