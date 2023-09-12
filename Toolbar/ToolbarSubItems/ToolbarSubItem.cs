using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace ViewSonic.NoteApp.Toolbar.ToolbarSubItems
{
    /// <summary>
    /// Represent base class for toolbar subitems
    /// </summary>
    public abstract class ToolbarSubItem : ObservableObject, IAnnotationToolbarSubItem, INotifyPropertyChanged
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public virtual AnnotationSubItemType ItemType { get; }
    }
}
