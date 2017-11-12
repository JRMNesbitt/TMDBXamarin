using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.Mocks;
using TMDBXamarin.ViewModels;
using TMDBXamarin.Services;
using TMDBXamarin.Interfaces;

namespace UnitTest
{
    [TestClass]
    public class TestUpComingMoviesViewModel
    {
        
        private PrismApplicationMock prismApplicationMock;
        private MainPageViewModel upcomingMoviesViewModel;

        private ITmdbService tmdbService;

        public TestUpComingMoviesViewModel()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            prismApplicationMock = new PrismApplicationMock();
            tmdbService = new TmdbService(); // we should mock this too.
            //upcomingMoviesViewModel = new MainPageViewModel(tmdbService, prismApplicationMock.NavigationService);
        }


        //[TestMethod]
        //public void GetUpComingMovies()
        //{

        //    //upcomingMoviesViewModel.GetUpcomingMoviesCommand.Execute();
        //    Assert.AreNotEqual(0, upcomingMoviesViewModel.UpcomingMovieItems.Count);

        //}
    }
}
