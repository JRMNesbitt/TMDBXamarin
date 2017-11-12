using System.Threading.Tasks;
using TMDBXamarin.Interfaces;
using TMDBXamarin.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMDBXamarin.Models;
namespace UnitTest
{
    [TestClass]
    public class TestTMDBService
    {


        private ITmdbService tmdbService;

        public TestTMDBService()
        {
            tmdbService = new TmdbService();
        }

        // lets see if our api returns any data.
        [TestMethod]
        public async Task FetchUpcomingMovies()
        {
            int page = 1;
            var result = await tmdbService.GetUpComingMovies(page);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.ListOfUpComingMovies.Count);
        }


        // lets see if our api returns any data.
        [TestMethod]
        public async Task FetchMovieGere()
        {
            var result = await tmdbService.GetMovieGenres();
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
        }
    }
}
