using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Logic.Database;

namespace Movies.Logic.Repositories
{
    public interface IRatingRepository
    {
        Task<List<MovieRating>> GetByMovieId(int movieId);
        Task Save(MovieRating rating);
    }

    public class RatingRepository : IRatingRepository
    {
        private readonly MovieContext _context;

        public RatingRepository(MovieContext context)
        {
            _context = context;
        }
        public async Task<List<MovieRating>> GetByMovieId(int movieId)
        {
            return await _context.MovieRatings.Where(m=>m.MovieId == movieId).ToListAsync();
        }

        public async Task Save(MovieRating rating)
        {
            _context.MovieRatings.Add(rating);
            await _context.SaveChangesAsync();
        }
    }
}