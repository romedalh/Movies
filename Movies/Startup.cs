using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Logic.Database;
using Movies.Logic.Queries;
using Movies.Logic.Repositories;

namespace Movies
{
    public class Startup  
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();

            services.AddMediatR(typeof(GetMovieDetailsQuery).Assembly);
            services.AddDbContext<MovieContext>(builder =>
                builder.UseSqlite("Data Source=.\\Movie.db"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            SetupDatabase(app);
        }

        private static void SetupDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<MovieContext>();
            context.Database.EnsureCreated();
        }
    }
}
