using Prism.Modularity;
using Prism.Navigation;
using Xamarin.Forms;
using Prism.Unity;
using TMDBXamarin.Interfaces;
using TMDBXamarin.Services;
using Microsoft.Practices.Unity;

namespace UnitTest.Mocks
{
    public class PrismApplicationMock : PrismApplication
    {
        // From : https://github.com/PrismLibrary/Prism/blob/master/Source/Xamarin/Prism.Unity.Forms.Tests/Mocks/PrismApplicationMock.cs
        public PrismApplicationMock()
        {
        }

        public PrismApplicationMock(Page startPage) : this()
        {
            Current.MainPage = startPage;
        }

        public new INavigationService NavigationService => base.NavigationService;

        public bool Initialized { get; private set; }

        protected override void OnInitialized()
        {
            Initialized = true;
        }
        protected override void RegisterTypes()
        {
            Container.RegisterType<ITmdbService, TmdbService>(new ContainerControlledLifetimeManager());
            Container.RegisterTypeForNavigation<NavigationPage>();
            // needs a mock too Container.RegisterTypeForNavigation<MainPage>();
        }

        public INavigationService CreateNavigationServiceForPage(Page page)
        {
            return CreateNavigationService(page);
        }
    }
}