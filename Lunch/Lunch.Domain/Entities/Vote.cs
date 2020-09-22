using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Entities
{
    public class Vote : EntityBase
    {
        public User User { get; private set; }
        public Restaurant Restaurant { get; private set; }

        public Vote(User user, Restaurant restaurant)
        {
            this.User = user;
            this.Restaurant = restaurant;
        }
    }
}
