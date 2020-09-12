using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Restaurant(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
