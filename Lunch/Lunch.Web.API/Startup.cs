using Lunch.Domain.Interfaces;
using Lunch.Domain.Repositories;
using Lunch.Domain.Services;
using Lunch.Infra.Data.Data;
using Lunch.Infra.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lunch.Web.API
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            string connectionString = Configuration[ "ConnectionStrings:DefaultConnection" ];
            services.AddTransient<LunchDbContext>();
            services.AddDbContext<DbContext, LunchDbContext>( options => options.UseSqlServer( connectionString ), ServiceLifetime.Transient );

            services.AddTransient<IVoteRepository, VoteRepository>();
            services.AddTransient<IPoolRepository, PoolRepository>();
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IPoolService, PoolService>();
            services.AddTransient<IUserService, UserService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}
