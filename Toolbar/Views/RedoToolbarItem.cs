using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class RedoToolbarItem : AnnotationToolbarItem
    {
        static RedoToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RedoToolbarItem), new FrameworkPropertyMetadata(typeof(RedoToolbarItem)));
        }

        public RedoToolbarItem()
        {        
            DataContext = new RedoToolbarItemViewModel();
        }
    }
}
