using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsNav.ViewModel;

namespace XamarinFormsNav.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel loginViewModel)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            BindingContext = loginViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}