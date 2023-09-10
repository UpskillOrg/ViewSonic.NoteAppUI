using System.Windows;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Represents Undo toolbar item
    /// </summary>
    public class UndoToolbarItem : AnnotationToolbarItem
    {
        static UndoToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UndoToolbarItem), new FrameworkPropertyMetadata(typeof(UndoToolbarItem)));
        }

        /// <inheritdoc />
        public override AnnotationItemType ItemType
        {
            get { return AnnotationItemType.Undo; }
        }
    }
}
