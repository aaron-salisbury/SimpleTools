using SimpleTools.Core.Domains.ToDoListManager;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Base.TemplateSelectors
{
    public class ToDoListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ListCategoryTemplate { get; set; }

        public DataTemplate ToDoItemTemplate { get; set; }

        public DataTemplate ToDoItemStepTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return GetTemplate(item) ?? base.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return GetTemplate(item) ?? base.SelectTemplateCore(item, container);
        }

        private DataTemplate GetTemplate(object item)
        {
            switch (item)
            {
                case ListCategory listCategory:
                    return ListCategoryTemplate;
                case ToDoItem toDoItem:
                    return ToDoItemTemplate;
                case ToDoItemStep toDoItemStep:
                    return ToDoItemStepTemplate;
            }

            return null;
        }
    }
}
