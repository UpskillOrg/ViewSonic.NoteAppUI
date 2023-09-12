using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views;

public class ColorPickerToolbarItem : System.Windows.Controls.Control
{
    public ColorPickerToolbarItem()
    {
        // Set the ViewModel as the DataContext
        DataContext = new ColorPickerToolbarItemViewModel();
    }
}
