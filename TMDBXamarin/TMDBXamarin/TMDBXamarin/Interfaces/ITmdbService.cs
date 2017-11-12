using System.Collections.Generic;
using System.Threading.Tasks;
using TMDBXamarin.Models;

namespace TMDBXamarin.Interfaces
{
    public interface ITmdbService
    {
        
        Task<MovieResult> GetUpComingMovies(int page);

        Task<List<MovieGenre>> GetMovieGenres();

        Task<MovieResult> MovieSearchAsync(string searchTerm, int page);

    }
}
