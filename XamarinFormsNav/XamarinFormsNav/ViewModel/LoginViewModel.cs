using System;
using Xamarin.Forms;
using XamarinFormsNav.ViewModel.Abstractions;

namespace XamarinFormsNav.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public event EventHandler LoginSucceeded;

        private Command _loginCommand;

        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(() =>
                {
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                }));
            }
        }
    }
}
