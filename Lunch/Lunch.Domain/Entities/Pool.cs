using System;
using System.Collections.Generic;
using System.Linq;

namespace Lunch.Domain.Entities
{
    public class Pool
    {
        public int Id { get; private set; }
        public DateTime ClosingTime { get; private set; }
        public IEnumerable<Vote> Votes { get; private set; }

        public Pool(int id, DateTime closingTime)
        {
            this.Id = id;
            this.ClosingTime = closingTime;
            Votes = new List<Vote>();
        }

        public void AddVote(Vote vote)
        {
            if (Votes.Any(x => x.User == vote.User))
                throw new RuleBrokenException("Só é possível votar em um restaurante por dia");
            if (DateTime.Now > ClosingTime)
                throw new RuleBrokenException("Eleição já foi encerrada");

            Votes = Votes.Append(vote);
        }

        public Dictionary<Restaurant, int> GetResults()
        {
            return Votes.GroupBy(x => x.Restaurant).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}
