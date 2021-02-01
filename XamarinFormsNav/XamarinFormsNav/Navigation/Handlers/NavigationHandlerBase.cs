using Stateless;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsNav.Navigation.Handlers
{
    public abstract class NavigationHandlerBase : INavigationHandler
    {
        public NavigationHandlerBase(IServiceProvider serviceProvider, INavigation navigation, StateMachine<NavigationState, NavigationTrigger> stateMachine)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            Navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            StateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        }

        protected IServiceProvider ServiceProvider { get; }

        protected INavigation Navigation { get; }

        protected StateMachine<NavigationState, NavigationTrigger> StateMachine { get; }

        public abstract Task HandleArriveAsync(NavigationKind navigationKind);

        public abstract Task HandleLeaveAsync();
    }
}
