using MediatR;

namespace Movies.Logic.Queries
{
    public class GetMovieDetailsQuery : IRequest<MovieDetails>
    {
        public GetMovieDetailsQuery(int movieId)
        {
            MovieId = movieId;
        }
        public int MovieId { get; private set; }
    }

    public class MovieDetails
    {
        public int Id { get; set; }
        public int EpisodeId { get; set; }
        public string Title { get; set; }
        public string OpeningCrawl { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public string ReleaseDate { get; set; }
        public double Rating { get; set; }
        public int VotesCount { get; set; }

    }
}
