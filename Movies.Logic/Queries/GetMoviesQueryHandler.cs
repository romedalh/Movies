using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Movies.Logic.Extensions;
using Movies.Logic.Repositories;

namespace Movies.Logic.Queries
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery,IList<MovieHeader>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IList<MovieHeader>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies =  await _movieRepository.GetMovies();
            return movies.Select(m => new MovieHeader
            {
                Id = m.Url.GetMovieIdFromUrl(),
                Title = m.Title
            }).OrderBy(m=>m.Id).ToList();
        }
    }
}