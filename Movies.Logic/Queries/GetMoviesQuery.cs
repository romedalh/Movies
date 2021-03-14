using System.Collections.Generic;
using MediatR;

namespace Movies.Logic.Queries
{
    public class GetMoviesQuery : IRequest<IList<MovieHeader>>
    {
    }

    public class MovieHeader
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
