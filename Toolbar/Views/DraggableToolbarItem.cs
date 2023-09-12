using System.Windows;
using System.Windows.Controls.Primitives;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    /// <summary>
    /// Toolbar item created for dragging <see cref="AnnotationToolbar"/>
    /// </summary>
    public class DraggableToolbarItem : Thumb
    {
        static DraggableToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DraggableToolbarItem), new FrameworkPropertyMetadata(typeof(DraggableToolbarItem)));
        }
    }
}
