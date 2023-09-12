using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class EraserToolbarItem : System.Windows.Controls.Control
    {
        public EraserToolbarItem()
        {
            DataContext = new EraserToolbarItemViewModel();
        }
    }
}
