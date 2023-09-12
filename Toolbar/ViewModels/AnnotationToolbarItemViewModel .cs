using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;

namespace ViewSonic.NoteApp.Toolbar.ViewModels
{
    public class AnnotationToolbarItemViewModel : ObservableObject
    {
        private bool _isToggled;
        private bool _isSelected;

        public AnnotationToolbarItemViewModel()
        {
            SubItems = new ObservableCollection<IAnnotationToolbarSubItem>();
        }

        public bool IsTogglable { get; set; }

        public AnnotationItemType ItemType { get; set; }

        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<IAnnotationToolbarSubItem> SubItems { get; set; }     
    }
}
