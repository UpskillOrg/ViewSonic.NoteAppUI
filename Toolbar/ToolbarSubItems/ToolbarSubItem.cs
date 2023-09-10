using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewSonic.NoteApp.Toolbar.ToolbarSubItems
{
    /// <summary>
    /// Represent base class for toolbar subitems
    /// </summary>
    public abstract class ToolbarSubItem : IAnnotationToolbarSubItem, INotifyPropertyChanged
    {
        private bool _isSelected;

        /// <inheritdoc />
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        /// <inheritdoc />
        public virtual AnnotationSubItemType ItemType { get; }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises property changed event for selected property
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
