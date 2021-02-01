using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsNav.ViewModel;

namespace XamarinFormsNav.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabsPage : TabbedPage
    {
        public TabsPage(TabsViewModel tabsViewModel)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            BindingContext = tabsViewModel;
        }
    }
}