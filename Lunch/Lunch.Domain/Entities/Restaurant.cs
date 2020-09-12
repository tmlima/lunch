using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Domain.Entities
{
    public class Restaurant
    {
        public string Name { get; private set; }
        public Restaurant(string name)
        {
            this.Name = name;
        }
    }
}
