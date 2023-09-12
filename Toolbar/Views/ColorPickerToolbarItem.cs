using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views;

public class ColorPickerToolbarItem : System.Windows.Controls.Control
{
    static ColorPickerToolbarItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerToolbarItem), new FrameworkPropertyMetadata(typeof(ColorPickerToolbarItem)));
    }

    public ColorPickerToolbarItem()
    {        
        DataContext = new ColorPickerToolbarItemViewModel();
    }
}
