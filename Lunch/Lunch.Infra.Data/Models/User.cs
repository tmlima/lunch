using System.Collections.Generic;

namespace Lunch.Infra.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
