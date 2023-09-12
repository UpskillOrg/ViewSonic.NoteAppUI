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
            UndoCommand = new RelayCommand(Undo);
        }

        public ICommand UndoCommand { get; }

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

        private void Undo()
        {
            // Implement the undo logic here
            // For example, you can raise an event or execute a command to perform the undo action
        }
    }
}
