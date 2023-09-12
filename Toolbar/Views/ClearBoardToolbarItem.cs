using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    /// <summary>
    /// Clear board item
    /// </summary>
    public class ClearBoardToolbarItem : System.Windows.Controls.Control
    {
        public ClearBoardToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClearBoardToolbarItem), new FrameworkPropertyMetadata(typeof(ClearBoardToolbarItem)));
            DataContext = new ClearBoardToolbarItemViewModel();
        }
    }
}
