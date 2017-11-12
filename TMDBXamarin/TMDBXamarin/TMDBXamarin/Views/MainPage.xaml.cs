using Xamarin.Forms;

namespace TMDBXamarin.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpComingMovieItemsListView.ItemSelected += (sender, e) =>
            {
                // Manually deselect item
                ((ListView)sender).SelectedItem = null;
            };

            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android bug where the search bar would be hidden
                SearchBar.HeightRequest = 50;
            }
        }
    }
}
