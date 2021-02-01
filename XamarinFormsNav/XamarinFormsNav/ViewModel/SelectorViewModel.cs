using System;
using Xamarin.Forms;
using XamarinFormsNav.ViewModel.Abstractions;

namespace XamarinFormsNav.ViewModel
{
    public class SelectorViewModel : ViewModelBase
    {
        public event EventHandler ItemSelected;

        public Command _selectItemCommand;

        public Command SelectItemCommand
        {
            get
            {
                return _selectItemCommand ?? (_selectItemCommand = new Command(() =>
                {
                    ItemSelected?.Invoke(this, EventArgs.Empty);
                }));
            }
        }
    }
}
