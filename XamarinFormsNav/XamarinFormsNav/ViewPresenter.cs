using Stateless;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsNav.Navigation;
using XamarinFormsNav.Navigation.Handlers;

namespace XamarinFormsNav
{
    public sealed class ViewPresenter
    {
        private readonly INavigation _navigation;
        private readonly IServiceProvider _serviceProvider;
        private readonly StateMachine<NavigationState, NavigationTrigger> _stateMachine;

        private INavigationHandler LoginNavigationHandler { get; }

        private INavigationHandler TabsNavigationHandler { get; }

        private INavigationHandler SelectorNavigationHandler { get; }

        public ViewPresenter(INavigation navigation, IServiceProvider serviceProvider)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            _stateMachine = CreateNavigationStateMachine();

            LoginNavigationHandler = new LoginNavigationHandler(_serviceProvider, _navigation, _stateMachine);
            TabsNavigationHandler = new TabsNavigationHandler(_serviceProvider, _navigation, _stateMachine);
            SelectorNavigationHandler = new SelectorNavigationHandler(_serviceProvider, _navigation, _stateMachine);
        }

        private StateMachine<NavigationState, NavigationTrigger> CreateNavigationStateMachine()
        {
            var stateMachine = new StateMachine<NavigationState, NavigationTrigger>(NavigationState.Init);

            stateMachine.Configure(NavigationState.Init)
                .Permit(NavigationTrigger.Unauthorized, NavigationState.Login)
                .OnExitAsync(() => _navigation.PopAsync());

            stateMachine.Configure(NavigationState.Login)
                .Permit(NavigationTrigger.LoginSucceeded, NavigationState.Tabs)
                .OnEntryAsync(() => LoginNavigationHandler.HandleArriveAsync(NavigationKind.New))
                .OnExitAsync(() => LoginNavigationHandler.HandleLeaveAsync());

            stateMachine.Configure(NavigationState.Tabs)
                .Permit(NavigationTrigger.SelectionRequested, NavigationState.Selector)
                .OnEntryFromAsync(NavigationTrigger.LoginSucceeded, () => TabsNavigationHandler.HandleArriveAsync(NavigationKind.New))
                .OnEntryFromAsync(NavigationTrigger.ItemSelected, () => TabsNavigationHandler.HandleArriveAsync(NavigationKind.Reentry))
                .OnExitAsync(() => TabsNavigationHandler.HandleLeaveAsync());

            stateMachine.Configure(NavigationState.Selector)
                .Permit(NavigationTrigger.ItemSelected, NavigationState.Tabs)
                .OnEntryAsync(() => SelectorNavigationHandler.HandleArriveAsync(NavigationKind.New))
                .OnExitAsync(() => SelectorNavigationHandler.HandleLeaveAsync());

            return stateMachine;
        }

        internal async Task StartAsync()
        {
            await _stateMachine.FireAsync(NavigationTrigger.Unauthorized);
        }
    }
}
