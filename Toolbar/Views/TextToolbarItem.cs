using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class TextToolbarItem : System.Windows.Controls.Control
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
