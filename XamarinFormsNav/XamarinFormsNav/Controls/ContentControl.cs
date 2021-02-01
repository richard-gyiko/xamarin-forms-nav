using Xamarin.Forms;
using XamarinFormsNav.Utility;

namespace XamarinFormsNav.Controls
{
    public class ContentControl : ContentView
    {
        public static readonly BindableProperty DataContextProperty = BindableProperty.Create(
            nameof(DataContext),
            typeof(object),
            typeof(ContentControl),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnDataContentChanged);

        public object DataContext
        {
            get => GetValue(DataContextProperty);
            set => SetValue(DataContextProperty, value);
        }

        private static void OnDataContentChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var cp = (ContentControl)bindable;

            var dataContext = cp.DataContext;
            if (dataContext != null)
            {
                var dataTemplate = SelectTemplate(dataContext, bindable);
                if (dataTemplate != null)
                {
                    var contentView = (View)dataTemplate.CreateContent();
                    contentView.BindingContext = dataContext;
                    cp.Content = contentView;
                }
                else
                {
                    cp.Content = null;
                }
            }
            else
            {
                cp.Content = null;
            }
        }

        private static DataTemplate SelectTemplate(object item, BindableObject container)
        {
            var visualElement = container as VisualElement;
            if (visualElement != null && item != null)
            {
                var foundDataTemplate = FindDataTemplate(visualElement, item);
                return foundDataTemplate;
            }

            return null;
        }

        private static DataTemplate FindDataTemplate(Element element, object item)
        {
            var visualElement = element as VisualElement;
            if (visualElement?.Resources != null && item != null)
            {
                foreach (var resource in visualElement.Resources)
                {
                    var dataTemplate = resource.Value as TypedDataTemplate;

                    if (dataTemplate != null && dataTemplate.TargetType == item.GetType())
                    {
                        return dataTemplate.Template;
                    }
                }

                return FindDataTemplate(visualElement.Parent, item);
            }
            else
            {
                return null;
            }
        }
    }
}
