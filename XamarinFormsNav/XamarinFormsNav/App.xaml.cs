using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using XamarinFormsNav.ViewModel;

namespace XamarinFormsNav
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private ViewPresenter _viewPresenter;

        public App()
        {
            InitializeComponent();

            _serviceProvider = ConfigureServices().BuildServiceProvider();

            MainPage = new NavigationPage(new ContentPage());

            _viewPresenter = new ViewPresenter(MainPage.Navigation, _serviceProvider);
        }

        protected override async void OnStart()
        {
            await _viewPresenter.StartAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private IServiceCollection ConfigureServices()
        {
            return new ServiceCollection()
                .AddTransient<LoginViewModel>()
                .AddTransient<TabsViewModel>()
                .AddTransient<SelectorViewModel>()
                .AddTransient<DashboardViewModel>()
                .AddTransient<SellViewModel>();
        }
    }
}
