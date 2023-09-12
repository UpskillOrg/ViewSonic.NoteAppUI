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

        /// <summary>
        /// Gets a value indicating is current item could be toggled
        /// </summary>
        public virtual bool IsTogglable
        {
            get { return false; }
        }

        public AnnotationItemType ItemType { get; set; }

        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;                    
                    OnPropertyChanged(nameof(IsToggled));
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
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public ObservableCollection<IAnnotationToolbarSubItem> SubItems { get; set; }     
    }
}
