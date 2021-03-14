using MediatR;

namespace Movies.Logic.Commands
{
    public class RateMovieCommand : IRequest
    {
        public RateMovieCommand(int movieId, int rating)
        {
            MovieId = movieId;
            Rating = rating;
        }

        public int MovieId { get; }
        public int Rating { get; }
    }
}