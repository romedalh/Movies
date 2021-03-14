using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Movies.Logic.Database;
using Movies.Logic.Queries;
using Movies.Logic.Repositories;
using NUnit.Framework;

namespace Movies.Tests.Tests
{
    [TestFixture]
    public class GetMovieDetailsQueryHandlerTests
    {
        [Test]
        public async Task Can_Return_Movie_Details_Without_Ratings()
        {
            //Arrange
            var movieId = 2;
            var fixture = new GetMovieDetailsQueryHandlerFixture()
                .WithMovieDetailsForMovieId(movieId, new Movie
                {
                    Title = "test title",
                    EpisodeId = 4,
                    Url = "https://testendpoint.com/api/films/2/",
                    Director = "test director",
                    Producer = "test producer",
                    ReleaseDate = "1977-05-25",
                    OpeningCrawl = "Test text"
                })
                .WithoutAnyRatingsForMovie(movieId);
            var sut = fixture.CreateSut();

            //Act
            var details = await sut.Handle(new GetMovieDetailsQuery(movieId), CancellationToken.None);

            //Assert
            var expectedDetails = new MovieDetails
            {
                Title = "test title",
                Id = 2,
                Rating = 0,
                OpeningCrawl = "Test text",
                Producer = "test producer",
                ReleaseDate = "1977-05-25",
                Director = "test director",
                VotesCount = 0,
                EpisodeId = 4
            };
            details.Should().BeEquivalentTo(expectedDetails);
        }

        [Test]
        public async Task Can_Return_Movie_Details_With_Ratings()
        {
            //Arrange
            var movieId = 2;
            var fixture = new GetMovieDetailsQueryHandlerFixture()
                .WithMovieDetailsForMovieId(movieId, new Movie
                {
                    Title = "test title",
                    EpisodeId = 4,
                    Url = "https://testendpoint.com/api/films/2/",
                    Director = "test director",
                    Producer = "test producer",
                    ReleaseDate = "1977-05-25",
                    OpeningCrawl = "Test text"
                })
                .WithRatingsForMovie(movieId,new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
            var sut = fixture.CreateSut();

            //Act
            var details = await sut.Handle(new GetMovieDetailsQuery(movieId), CancellationToken.None);

            //Assert
            Assert.That(details.Rating,Is.EqualTo(5.5));
            Assert.That(details.VotesCount,Is.EqualTo(10));
        }
    }

    public class GetMovieDetailsQueryHandlerFixture
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock = new Mock<IMovieRepository>();
        private readonly Mock<IRatingRepository> _ratingRepositoryMock = new Mock<IRatingRepository>();
        
        public GetMovieDetailsQueryHandler CreateSut()
        {
            return new GetMovieDetailsQueryHandler(_movieRepositoryMock.Object, _ratingRepositoryMock.Object);
        }

        public GetMovieDetailsQueryHandlerFixture WithMovieDetailsForMovieId(int movieId, Movie expectedDetails)
        {
            _movieRepositoryMock.Setup(a => a.GetMovie(movieId)).Returns(Task.FromResult(expectedDetails));
            return this;
        }

        public GetMovieDetailsQueryHandlerFixture WithoutAnyRatingsForMovie(int movieId)
        {
            _ratingRepositoryMock.Setup(a => a.GetByMovieId(movieId)).Returns(Task.FromResult(new List<MovieRating>()));
            return this;
        }

        public GetMovieDetailsQueryHandlerFixture WithRatingsForMovie(int movieId, List<int> ratings)
        {
            var movieRatings = ratings.Select(r => new MovieRating
            {
                Rating = r,
                MovieId = movieId
            }).ToList();
            _ratingRepositoryMock.Setup(a => a.GetByMovieId(movieId))
                .Returns(Task.FromResult(movieRatings));
            return this;
        }
    }
}