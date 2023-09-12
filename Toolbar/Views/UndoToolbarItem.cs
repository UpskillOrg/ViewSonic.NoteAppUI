using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class UndoToolbarItem : AnnotationToolbarItem
    {
        static UndoToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UndoToolbarItem), new FrameworkPropertyMetadata(typeof(UndoToolbarItem)));
        }

        public UndoToolbarItem()
        {        
            DataContext = new UndoToolbarItemViewModel();
        }
    }
}
