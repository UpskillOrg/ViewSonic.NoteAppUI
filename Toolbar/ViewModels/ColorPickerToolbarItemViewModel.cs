using System.Windows.Media;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;
using System.Collections.ObjectModel;

namespace ViewSonic.NoteApp.Toolbar.ViewModels;

public class ColorPickerToolbarItemViewModel : AnnotationToolbarItemViewModel
{
    private IAnnotationToolbarSubItem _selectedSubItem;

    public ColorPickerToolbarItemViewModel()
    {
        ItemType = AnnotationItemType.ColorPicker;
        SubItems  = new ObservableCollection<IAnnotationToolbarSubItem>
        {
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#CBD2DA") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#00A7F4") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#1478C7") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FE3A68") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC43D") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#00CB79") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#4D6780") },
            new ColorPickerToolbarSubItem { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#292929") }
        };
        SelectedSubItem = SubItems[2];
        SelectedSubItem.IsSelected = true;
    }

    public IAnnotationToolbarSubItem SelectedSubItem
    {
        get { return _selectedSubItem; }
        set
        {
            if (_selectedSubItem != value)
            {
                _selectedSubItem = value;
                OnPropertyChanged();
            }
        }
    }
}


