using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Infra.Data.Models
{
    public class Pool
    {
        public DateTime ClosingTime { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
    }
}
