using Lunch.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lunch.Test.BDD
{
    class LunchDbContextInMemory : LunchDbContext
    {
        public LunchDbContextInMemory( DbContextOptions<LunchDbContext> dbContextOptions ) : base( dbContextOptions ) { }
    }
}
