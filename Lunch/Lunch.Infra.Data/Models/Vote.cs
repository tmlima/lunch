namespace Lunch.Infra.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Pool Pool { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
