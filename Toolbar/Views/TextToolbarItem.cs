using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class TextToolbarItem : System.Windows.Controls.Control
    {
        public TextToolbarItem()
        {
            // Set the ViewModel as the DataContext
            DataContext = new TextToolbarItemViewModel();

            // Other initialization code
        }

        // Other methods and properties
    }

}
