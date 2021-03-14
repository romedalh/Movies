using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Movies.Logic.Database;
using Movies.Logic.Repositories;

namespace Movies.Logic.Commands
{
    public class RateMovieCommandHandler : IRequestHandler<RateMovieCommand>
    {
        private readonly IRatingRepository _ratingRepository;

        public RateMovieCommandHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<Unit> Handle(RateMovieCommand request, CancellationToken cancellationToken)
        {
            await _ratingRepository.Save(new MovieRating
            {
                MovieId = request.MovieId,
                Rating = request.Rating
            });
            return Unit.Value;
        }

    }
}