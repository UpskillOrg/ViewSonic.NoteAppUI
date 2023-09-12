using CommunityToolkit.Mvvm.ComponentModel;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;

namespace ViewSonic.NoteApp.Toolbar.ViewModels
{
    public class PenToolbarItemViewModel : AnnotationToolbarItemViewModel
    {
        private PenToolbarSubItem _selectedPenSubItem;

        public PenToolbarItemViewModel()
        {
            ItemType = AnnotationItemType.Pen;
            SelectedPenSubItem = new PenToolbarSubItem() { PenWidth = 4, PenHeight = 4, IsHighlighter = false };
        }

        public PenToolbarSubItem SelectedPenSubItem
        {
            get { return _selectedPenSubItem; }
            set
            {
                if (_selectedPenSubItem != value)
                {
                    _selectedPenSubItem = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
