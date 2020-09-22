using System;
using System.Collections.Generic;
using System.Text;

namespace Lunch.Infra.Data.Models
{
    public class Vote
    {
        public User User { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
