using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Logic.Commands;
using Movies.Logic.Queries;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _mediator.Send(new GetMoviesQuery());
            return View("Index", movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _mediator.Send(new GetMovieDetailsQuery(id));
            return View("Details", movieDetails);
        }

        public async Task<ActionResult> Rate(int movieId, int rating)
        {
            await _mediator.Send(new RateMovieCommand(movieId, rating));
            return RedirectToAction("Details", new {id = movieId});
        }
    }
}