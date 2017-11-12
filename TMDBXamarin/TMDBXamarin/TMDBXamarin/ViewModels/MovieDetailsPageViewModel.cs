using Prism.Mvvm;
using Prism.Navigation;
using TMDBXamarin.Interfaces;
using TMDBXamarin.Models;

namespace TMDBXamarin.ViewModels
{
    public class MovieDetailsPageViewModel : BindableBase, INavigationAware
    {
        #region View Related
        string pageTitle = string.Empty;
        public string PageTitle
        {
            get { return pageTitle; }
            set { SetProperty(ref pageTitle, value); }
        }
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        bool internetConnection = false;
        public bool InternetConnection
        {
            get { return internetConnection; }
            set { SetProperty(ref internetConnection, value); }
        }

        private int currentPage = 1;
        private int totalPages = 0;
        #endregion

        private MovieInfo movieDetail;
        public MovieInfo MovieDetail
        {
            get { return movieDetail; }
            set { SetProperty(ref movieDetail, value); }
        }
        //private ITmdbService _TmdbService;

        //public MovieDetailsPageViewModel(ITmdbService tmdbService)
        //{
        //    this._TmdbService = tmdbService;
        //}

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            MovieDetail = parameters.GetValue<MovieInfo>("movieItem");
            PageTitle = string.Format("{0}  ({1})", MovieDetail.Title, MovieDetail.ReleaseDate.Value.Year);
            //IsConnected = CrossConnectivity.Current.IsConnected;
            //await LoadMovieDetailAsync(Movie.Id).ConfigureAwait(false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // Do nothing, added because of implementation INavigationAware.
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // Do nothing, added because of implementation INavigationAware.
        }
    }
}
