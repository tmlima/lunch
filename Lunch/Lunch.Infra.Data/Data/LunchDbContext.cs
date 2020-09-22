using Lunch.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lunch.Infra.Data.Data
{
    public class LunchDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Pool> Pools { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<User> Users { get; set; }

        public LunchDbContext( DbContextOptions<LunchDbContext> options ) : base(options) { }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );
        }
    }
}
