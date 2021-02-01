using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsNav.ViewModel;

namespace XamarinFormsNav.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectorPage : ContentPage
    {
        public SelectorPage(SelectorViewModel selectorViewModel)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            BindingContext = selectorViewModel;
        }
    }
}