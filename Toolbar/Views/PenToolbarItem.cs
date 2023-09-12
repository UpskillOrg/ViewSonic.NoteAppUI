using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class PenToolbarItem : System.Windows.Controls.Control
    {
        public PenToolbarItem()
        {
            DataContext = new PenToolbarItemViewModel();
        }
    }

}
