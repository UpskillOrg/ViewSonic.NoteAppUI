using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ViewSonic.NoteApp.Toolbar.ViewModels
{
    public class RedoToolbarItemViewModel : AnnotationToolbarItemViewModel
    {
        private bool _isEnabled;
        private ICommand _redoCommand;

        public RedoToolbarItemViewModel()
        {
            ItemType = AnnotationItemType.Redo;
            IsEnabled = true; // Set the initial state of the button
            RedoCommand = new RelayCommand(Redo);
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

        public ICommand RedoCommand
        {
            get { return _redoCommand; }
            set
            {
                if (_redoCommand != value)
                {
                    _redoCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Redo()
        {
            // Implement the Redo logic here
            // This can involve undoing the last action or redoing a previously undone action
            // Update the IsEnabled property accordingly
        }
    }

}
