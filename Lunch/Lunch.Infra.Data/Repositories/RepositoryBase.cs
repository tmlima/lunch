using Lunch.Infra.Data.Data;

namespace Lunch.Infra.Data.Repositories
{
    public abstract class RepositoryBase
    {
        protected LunchDbContext dbContext;

        public RepositoryBase( LunchDbContext dbContext )
        {
            this.dbContext = dbContext;
        }
    }
}
