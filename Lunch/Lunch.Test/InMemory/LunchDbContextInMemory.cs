using Lunch.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lunch.Test.InMemory
{
    class LunchDbContextInMemory : LunchDbContext
    {
        public LunchDbContextInMemory(DbContextOptions<LunchDbContext> dbContextOptions) : base( dbContextOptions ) { }
    }
}
