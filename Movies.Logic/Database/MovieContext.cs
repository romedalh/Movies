using Microsoft.EntityFrameworkCore;

namespace Movies.Logic.Database
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }

        public DbSet<MovieRating> MovieRatings { get; set; }
    }
}