using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class TextToolbarItem : AnnotationToolbarItem
    {
        static TextToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextToolbarItem), new FrameworkPropertyMetadata(typeof(TextToolbarItem)));
        }

        public TextToolbarItem()
        {        
            DataContext = new TextToolbarItemViewModel();         
        }
    }

}
