using System.Threading.Tasks;

namespace XamarinFormsNav.Navigation
{
    public interface INavigationHandler
    {
        Task HandleArriveAsync(NavigationKind navigationKind);

        Task HandleLeaveAsync();
    }
}
