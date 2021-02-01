using Microsoft.Extensions.DependencyInjection;
using Stateless;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsNav.Pages;
using XamarinFormsNav.ViewModel;

namespace XamarinFormsNav.Navigation.Handlers
{
    public class SelectorNavigationHandler : NavigationHandlerBase
    {
        private SelectorViewModel _selectorViewModel;

        public SelectorNavigationHandler(IServiceProvider serviceProvider, INavigation navigation, StateMachine<NavigationState, NavigationTrigger> stateMachine) : base(serviceProvider, navigation, stateMachine)
        {
        }

        public async override Task HandleArriveAsync(NavigationKind navigationKind)
        {
            if (navigationKind == NavigationKind.New)
            {
                _selectorViewModel = ServiceProvider.GetRequiredService<SelectorViewModel>();
            }

            _selectorViewModel.ItemSelected += ItemSelectorViewModel_ItemSelected;

            if (navigationKind == NavigationKind.New)
            {
                await Navigation.PushModalAsync(new SelectorPage(_selectorViewModel));
            }
        }

        private async void ItemSelectorViewModel_ItemSelected(object sender, EventArgs e)
        {
            await StateMachine.FireAsync(NavigationTrigger.ItemSelected);
        }

        public override async Task HandleLeaveAsync()
        {
            _selectorViewModel.ItemSelected -= ItemSelectorViewModel_ItemSelected;

            await Navigation.PopModalAsync();
        }
    }
}
