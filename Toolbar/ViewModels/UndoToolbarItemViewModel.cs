using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ViewSonic.NoteApp.Toolbar.ViewModels
{
    public class UndoToolbarItemViewModel : AnnotationToolbarItemViewModel
    {
        private bool _isEnabled;

        public UndoToolbarItemViewModel()
        {
            ItemType = AnnotationItemType.Undo;
            IsEnabled = true;         
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
