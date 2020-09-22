using System;
using System.Collections.Generic;
using System.Linq;

namespace Lunch.Domain.Entities
{
    public class Pool : EntityBase
    {
        public DateTime ClosingTime { get; private set; }
        public IEnumerable<Vote> Votes { get; private set; }

        public Pool(int id, DateTime closingTime)
        {
            Id = id;
            ClosingTime = closingTime;
            Votes = new List<Vote>();
        }

        public Pool(int id, DateTime closingTime, IList<Vote> votes) : this(id, closingTime)
        {
            Votes = votes;
        }

        public void AddVote(Vote vote)
        {
            if (Votes.Any(x => x.User.Id == vote.User.Id))
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
