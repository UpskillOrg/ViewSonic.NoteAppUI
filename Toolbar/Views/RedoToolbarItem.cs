using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class RedoToolbarItem : System.Windows.Controls.Control
    {
        public RedoToolbarItem()
        {
            // Set the ViewModel as the DataContext
            DataContext = new RedoToolbarItemViewModel();

        }
    }
}
