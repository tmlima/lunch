using Lunch.Domain.Entities;
using Lunch.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lunch.Test.UnitTest
{
    class LunchDbContextInMemory : LunchDbContext
    {
        public LunchDbContextInMemory( DbContextOptions<LunchDbContext> dbContextOptions ) : base( dbContextOptions ) { }
    }
}
