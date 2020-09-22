using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lunch.Infra.Data.Models
{
    public class Pool
    {
        public int Id { get; set; }
        public DateTime ClosingTime { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public Pool()
        {
            Votes = new Collection<Vote>();
        }
    }
}
