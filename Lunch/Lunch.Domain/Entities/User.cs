using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public User(int id)
        {
            this.Id = id;
        }
    }
}
