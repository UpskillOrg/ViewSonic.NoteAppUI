using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewSonic.NoteApp.Toolbar.ViewModels
{
    public class TextToolbarItemViewModel : AnnotationToolbarItemViewModel
    {
        private TextToolbarSubItem _selectedSubItem;
        private ICommand _toggleCommand;

        public TextToolbarItemViewModel()
        {
            ItemType = AnnotationItemType.Text;            
            IsToggled = false;
            SubItems = new ObservableCollection<IAnnotationToolbarSubItem>
                {
                    new TextToolbarSubItem { FontSize = 12 },
                    new TextToolbarSubItem { FontSize = 18 },
                    new TextToolbarSubItem { FontSize = 24 },
                    new TextToolbarSubItem { FontSize = 36 },
                    new TextToolbarSubItem { FontSize = 48 },
                    new TextToolbarSubItem { FontSize = 60 },
                    new TextToolbarSubItem { FontSize = 72 }
                };
            SelectedSubItem = SubItems[0] as TextToolbarSubItem; // Set the initial selected subitem
            ToggleCommand = new RelayCommand(Toggle);
        }

        public TextToolbarSubItem SelectedSubItem
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

        public ICommand ToggleCommand
        {
            get { return _toggleCommand; }
            set
            {
                if (_toggleCommand != value)
                {
                    _toggleCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Toggle()
        {
            // Implement the Toggle logic here
            IsToggled = !IsToggled;
        }        
    }
}
