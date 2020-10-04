using Lunch.Domain.Entities;
using Lunch.Domain.Repositories;
using Lunch.Infra.Data.Data;
using System.Linq;

namespace Lunch.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(LunchDbContext dbContext) : base(dbContext) { }

        public int Add( string name )
        {
            Models.User user = new Models.User()
            {
                Name = name
            };
            dbContext.Users.Add( user );
            dbContext.SaveChanges();
            return user.Id;
        }

        public User Get( int id )
        {
            Models.User user = dbContext.Users.Single( x => x.Id == id );
            return new User( user.Id, user.Name );
        }
    }
}
