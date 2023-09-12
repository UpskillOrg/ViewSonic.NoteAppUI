using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class UndoToolbarItem : System.Windows.Controls.Control
    {
        public UndoToolbarItem()
        {
            // Set the ViewModel as the DataContext
            DataContext = new UndoToolbarItemViewModel();

            // Other initialization code
        }
    }
}
