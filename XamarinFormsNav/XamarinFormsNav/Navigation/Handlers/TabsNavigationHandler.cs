using Microsoft.Extensions.DependencyInjection;
using Stateless;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsNav.Pages;
using XamarinFormsNav.ViewModel;

namespace XamarinFormsNav.Navigation.Handlers
{
    public class TabsNavigationHandler : NavigationHandlerBase
    {
        private TabsViewModel _viewModel;

        public TabsNavigationHandler(IServiceProvider serviceProvider, INavigation navigation, StateMachine<NavigationState, NavigationTrigger> stateMachine)
            : base(serviceProvider, navigation, stateMachine)
        {
        }

        public override async Task HandleArriveAsync(NavigationKind navigationKind)
        {
            if (navigationKind == NavigationKind.New)
            {
                _viewModel = ServiceProvider.GetRequiredService<TabsViewModel>();
            }

            _viewModel.SelectionRequested += ViewModel_SelectionRequested;

            if (navigationKind == NavigationKind.New)
            {
                await Navigation.PushAsync(new TabsPage(_viewModel));
            }
        }

        public override Task HandleLeaveAsync()
        {
            _viewModel.SelectionRequested -= ViewModel_SelectionRequested;

            return Task.CompletedTask;
        }

        private async void ViewModel_SelectionRequested(object sender, EventArgs e)
        {
            await StateMachine.FireAsync(NavigationTrigger.SelectionRequested);
        }
    }
}
