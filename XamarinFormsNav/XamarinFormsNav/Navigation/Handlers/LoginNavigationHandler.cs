using Microsoft.Extensions.DependencyInjection;
using Stateless;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsNav.Pages;
using XamarinFormsNav.ViewModel;

namespace XamarinFormsNav.Navigation.Handlers
{
    public class LoginNavigationHandler : NavigationHandlerBase
    {
        private LoginViewModel _loginViewModel;

        public LoginNavigationHandler(IServiceProvider serviceProvider, INavigation navigation, StateMachine<NavigationState, NavigationTrigger> stateMachine)
            : base(serviceProvider, navigation, stateMachine)
        {
        }

        public override async Task HandleArriveAsync(NavigationKind navigationKind)
        {
            if (navigationKind == NavigationKind.New)
            {
                _loginViewModel = ServiceProvider.GetRequiredService<LoginViewModel>();
            }

            _loginViewModel.LoginSucceeded += LoginViewModel_LoginSucceeded;

            if (navigationKind == NavigationKind.New)
            {
                await Navigation.PushAsync(new LoginPage(_loginViewModel));
            }
        }

        public override Task HandleLeaveAsync()
        {
            _loginViewModel.LoginSucceeded -= LoginViewModel_LoginSucceeded;

            return Task.CompletedTask;
        }

        private async void LoginViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            await StateMachine.FireAsync(NavigationTrigger.LoginSucceeded);
        }
    }
}
