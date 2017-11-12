using Plugin.Connectivity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TMDBXamarin.Helpers;
using TMDBXamarin.Interfaces;
using TMDBXamarin.Models;
using Xamarin.Forms;

namespace TMDBXamarin.ViewModels
{
    public class MainPageViewModel : BindableBase
    {

        #region View Related
        string headerTitle = string.Empty;
        public string HeaderTitle
        {
            get { return headerTitle; }
            set { SetProperty(ref headerTitle, value); }
        }
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        bool internetConnection = true;
        public bool InternetConnection
        {
            get { return internetConnection; }
            set { SetProperty(ref internetConnection, value); }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                SetProperty(ref searchTerm, value);
                if(value =="") GetUpcomingMoviesCommand.Execute();
            }
        }


        private int currentPage = 1;
        private int totalPages = 0;
        #endregion

        private ITmdbService _tmdbService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;

        public DelegateCommand GetUpcomingMoviesCommand { get; }
        public DelegateCommand<MovieInfo> LoadNextPageCommand { get; }
        public DelegateCommand<MovieInfo> LoadMovieDetailsPageCommand { get; }
        public DelegateCommand SearchCommand { get; }

        public ObservableRangeCollection<MovieInfo> MovieItems { get; set; }
        private List<MovieGenre> movieGenres;


        public MainPageViewModel(ITmdbService tmdbService, INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this._tmdbService = tmdbService;
            this._navigationService = navigationService;
            this._pageDialogService = pageDialogService;

            MovieItems = new ObservableRangeCollection<MovieInfo>();

            GetUpcomingMoviesCommand = new DelegateCommand(async () => await ExecuteLoadUpcomingMovieItemsCommand().ConfigureAwait(false));
            LoadNextPageCommand = new DelegateCommand<MovieInfo>(async (MovieInfo movieItem) => await ExecuteLoadNextPageCommand(movieItem).ConfigureAwait(false));
            LoadMovieDetailsPageCommand = new DelegateCommand<MovieInfo>(async (MovieInfo movieItem) => await ExecuteLoadMovieDetailsPageCommand(movieItem).ConfigureAwait(false));
            SearchCommand = new DelegateCommand(async () => await ExecuteSearchCommand().ConfigureAwait(false));

            GetUpcomingMoviesCommand.Execute();
        }

        private async Task ExecuteLoadMovieDetailsPageCommand(MovieInfo movieItem)
        {
            var parameters = new NavigationParameters();
            parameters.Add(nameof(movieItem), movieItem);
            await _navigationService.NavigateAsync("MovieDetailsPage", parameters).ConfigureAwait(false);
        }

        private async Task ExecuteSearchCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                MovieItems.Clear();
                currentPage = 1;
                await DoSearching(currentPage).ConfigureAwait(true);
            }
            finally
            {
                IsBusy = false;
            }

            if (MovieItems.Count == 0)
            {
                await _pageDialogService.DisplayAlertAsync("No results found", "No results found matching your search criteria..", "Ok").ConfigureAwait(false);
            }
        }


        private async Task DoSearching(int page)
        {
            try
            {
                HeaderTitle = String.Format("{0} '{1}'", "Search Results for", searchTerm);
                var searchMoviesResult = await _tmdbService.MovieSearchAsync(searchTerm, page).ConfigureAwait(false);
                if (searchMoviesResult != null)
                {
                    
                        var searchMovies = new List<MovieInfo>();
                        totalPages = searchMoviesResult.TotalPages;
                        foreach (var movie in searchMoviesResult.ListOfUpComingMovies)
                        {
                            searchMovies.Add(MapMovieGenreIdToName(movie));
                        }
                        MovieItems.AddRange(searchMovies);
                   
                }
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task ExecuteLoadNextPageCommand(MovieInfo movieItem)
        {
            int itemLoadNextItem = 5;
            int viewCellIndex = MovieItems.IndexOf(movieItem);
            if (MovieItems.Count - itemLoadNextItem <= viewCellIndex)
            {
                currentPage++;
                if (currentPage <= totalPages)
                {
                    // Make sure we are connected
                    InternetConnection = CrossConnectivity.Current.IsConnected;
                    if (IsBusy || !InternetConnection) return;
                    //We are doing something
                    IsBusy = true;
                    try
                    {
                        if(String.IsNullOrEmpty(searchTerm))
                            await GetUpComingMoviesAsync(currentPage).ConfigureAwait(false);
                        else
                            await DoSearching(currentPage).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        await _pageDialogService.DisplayAlertAsync("Error", "Unable to load next page upcoming movies.", "Ok").ConfigureAwait(false);
                    }
                    finally
                    {
                        IsBusy = false;
                    }


                }
            }
        }


        private async Task ExecuteLoadUpcomingMovieItemsCommand()
        {
            // Make sure we are connected
            InternetConnection = CrossConnectivity.Current.IsConnected;
            if (IsBusy || !InternetConnection) return;
            //We are doing something
            IsBusy = true;
            try
            {
                MovieItems.Clear(); // rem clear list
                currentPage = 1;
                await GetUpComingMoviesAsync(currentPage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await _pageDialogService.DisplayAlertAsync("Error", "Unable to load upcoming movies.", "Ok").ConfigureAwait(false);
            }
            finally
            {
                IsBusy = false;
            }
        }


        private async Task GetUpComingMoviesAsync(int page)
        {
            try
            {
                HeaderTitle = "TMDB Upcoming Movies";
                SearchTerm = String.Empty;
                // get the genre list, if we don't already have it
                movieGenres = movieGenres ?? await _tmdbService.GetMovieGenres().ConfigureAwait(false);
                var upcomingMoviesResult = await _tmdbService.GetUpComingMovies(page).ConfigureAwait(false); ;
                if (upcomingMoviesResult != null)
                {
                    var upcomingMovies = new List<MovieInfo>();
                    totalPages = upcomingMoviesResult.TotalPages;
                    foreach (var movie in upcomingMoviesResult.ListOfUpComingMovies)
                    {
                        upcomingMovies.Add(MapMovieGenreIdToName(movie));
                    }
                    MovieItems.AddRange(upcomingMovies);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private MovieInfo MapMovieGenreIdToName(MovieInfo upComingMovie)
        {
            upComingMovie.GenresNames = String.Join(", ", movieGenres.Where(s => upComingMovie.GenreIds.Contains(s.Id)).Select(x => x.Name)); ;
            return upComingMovie;
        }
    }
}


