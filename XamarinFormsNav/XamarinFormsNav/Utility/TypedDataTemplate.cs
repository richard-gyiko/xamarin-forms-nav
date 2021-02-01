using System;
using Xamarin.Forms;

namespace XamarinFormsNav.Utility
{
    [ContentProperty(nameof(Template))]
    public class TypedDataTemplate
    {
        public Type TargetType { get; set; }

        public DataTemplate Template { get; set; }
    }
}
