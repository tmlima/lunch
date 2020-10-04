namespace Lunch.Domain.Entities
{
    public class Vote : EntityBase
    {
        public User User { get; private set; }
        public Restaurant Restaurant { get; private set; }

        public Vote(int id, User user, Restaurant restaurant) : this(user, restaurant)
        {
            this.Id = id;
        }

        public Vote(User user, Restaurant restaurant)
        {
            this.User = user;
            this.Restaurant = restaurant;
        }
    }
}
