using System.Windows;
using ViewSonic.NoteApp.Toolbar.ViewModels;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    public class PenToolbarItem : AnnotationToolbarItem
    {
        static PenToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PenToolbarItem), new FrameworkPropertyMetadata(typeof(PenToolbarItem)));
        }

        public PenToolbarItem()
        {            
            DataContext = new PenToolbarItemViewModel();
        }
    }

}
