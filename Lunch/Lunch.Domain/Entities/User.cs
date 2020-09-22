namespace Lunch.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; private set; }

        public User(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
