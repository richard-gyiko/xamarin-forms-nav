using Xamarin.Forms;
using XamarinFormsNav.ViewModel.Abstractions;

namespace XamarinFormsNav.ViewModel
{
    public class DashboardViewModel : TabViewModel
    {
        private Command _selectShowCommand;

        public Command SelectShowCommand
        {
            get => _selectShowCommand;
            set => SetProperty(ref _selectShowCommand, value);
        }

        public override string Title => "Dashboard";
    }
}
