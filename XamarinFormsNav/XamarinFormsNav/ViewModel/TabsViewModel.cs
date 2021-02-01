using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using XamarinFormsNav.ViewModel.Abstractions;

namespace XamarinFormsNav.ViewModel
{
    public class TabsViewModel : ViewModelBase
    {
        public event EventHandler SelectionRequested;

        private TabViewModel _selectedTab;

        public TabViewModel SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        public List<TabViewModel> Tabs { get; }

        public TabsViewModel(IServiceProvider serviceProvider)
        {
            var dashboardViewModel = serviceProvider.GetRequiredService<DashboardViewModel>();
            dashboardViewModel.SelectShowCommand = new Xamarin.Forms.Command(() => SelectionRequested?.Invoke(this, EventArgs.Empty));

            var sellViewModel = serviceProvider.GetRequiredService<SellViewModel>();

            Tabs = new List<TabViewModel> { dashboardViewModel, sellViewModel };
            SelectedTab = Tabs[0];
        }
    }
}
