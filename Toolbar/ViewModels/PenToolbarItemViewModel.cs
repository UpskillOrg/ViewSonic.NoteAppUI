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
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 4, PenHeight = 4, IsHighlighter = false });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 10, PenHeight = 10 });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 16, PenHeight = 16 });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 20, PenHeight = 20 });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 24, PenHeight = 24 });

            SelectedPenSubItem = (PenToolbarSubItem)SubItems[0];
            SelectedPenSubItem.IsSelected = true;
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

        public override bool IsTogglable
        {
            get { return true; }
        }
    }
}
