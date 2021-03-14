using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies.Logic.Repositories
{
    public interface IMovieRepository
    {
        Task<IList<Movie>> GetMovies();
        Task<Movie> GetMovie(int movieId);
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://swapi.dev/api/")
        };

        public async Task<IList<Movie>> GetMovies()
        {
            var response = await _client.GetAsync("films/");
            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<Movies>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return movies.Results;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            var response = await _client.GetAsync($"films/{movieId}/");
            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<Movie>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return movie;
        }
    }

    public class Movies
    {
        public IList<Movie> Results { get; set; }
    }

    public class Movie
    {
        public string Title { get; set; }
        [JsonPropertyName("episode_id")]
        public int EpisodeId { get; set; }
        [JsonPropertyName("opening_crawl")]
        public string OpeningCrawl { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }
        public string Url { get; set; }
    }
}
