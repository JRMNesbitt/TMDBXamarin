using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Practices.Unity;
using Prism.Unity;
using TMDBXamarin.Interfaces;
using TMDBXamarin.Services;
using TMDBXamarin.Views;
using Xamarin.Forms;

namespace TMDBXamarin
{
    public partial class App : PrismApplication
    {
        public App() : base(null)
        {
        }
     
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }

        protected override void OnStart()
        {
            base.OnStart();
            MobileCenter.Start(
               "android=27f45e90-722a-44bb-85c5-9fcc24d2655a;" +
               "ios=a504a015-38a7-43b0-9b7b-d37fce7bf25c;",
               typeof(Analytics),
               typeof(Crashes)
           );
        }
        protected override void RegisterTypes()
        {
            Container.RegisterType<ITmdbService, TmdbService>(new ContainerControlledLifetimeManager());
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MovieDetailsPage>();
        }
    }
}
