using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lunch.Domain.Entities
{
    public class Pool : EntityBase
    {
        public DateTime ClosingTime { get; private set; }
        public IEnumerable<Vote> Votes { get; private set; }
        public Restaurant RestaurantElected { get; private set; }

        public Pool(int id, DateTime closingTime)
        {
            Id = id;
            ClosingTime = closingTime;
            Votes = new List<Vote>();
        }

        public Pool(int id, DateTime closingTime, IEnumerable<Vote> votes, Restaurant restaurantElected) : this(id, closingTime)
        {
            Votes = votes;
            RestaurantElected = restaurantElected;
        }

        public IReadOnlyCollection<string> CanVote(int userId)
        {
            Collection<string> errors = new Collection<string>();

            if ( Votes.Any( x => x.User.Id == userId ) )
                errors.Add( "Só é possível votar em um restaurante por dia" );
            if ( DateTime.Now > ClosingTime )
                errors.Add( "Eleição já foi encerrada" );

            return errors;
        }

        public void AddVote(Vote vote)
        {
            if ( CanVote(vote.User.Id).Any() )
                throw new InvalidOperationException();

            Votes = Votes.Append(vote);
        }

        public Dictionary<Restaurant, int> GetResults()
        {
            return Votes.GroupBy(x => x.Restaurant).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}
