using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Movies.Logic.Database;
using Movies.Logic.Extensions;
using Movies.Logic.Repositories;

namespace Movies.Logic.Queries
{
    public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery,MovieDetails>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRatingRepository _ratingRepository;

        public GetMovieDetailsQueryHandler(IMovieRepository movieRepository, IRatingRepository ratingRepository)
        {
            _movieRepository = movieRepository;
            _ratingRepository = ratingRepository;
        }
        
        public async Task<MovieDetails> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetMovie(request.MovieId);
            var ratings = await _ratingRepository.GetByMovieId(request.MovieId);
            return new MovieDetails
            {
                Title = movie.Title,
                Id = movie.Url.GetMovieIdFromUrl(),
                EpisodeId = movie.EpisodeId,
                Director = movie.Director,
                OpeningCrawl = movie.OpeningCrawl,
                Producer = movie.Producer,
                ReleaseDate = movie.ReleaseDate,
                Rating = CalculateRating(ratings),
                VotesCount = ratings.Count
            };
        }

        private static double CalculateRating(List<MovieRating> ratings)
        {
            if(ratings.Any())
                return Math.Round((double)ratings.Sum(r=>r.Rating) / ratings.Count,2);
            return 0;
        }
    }
}