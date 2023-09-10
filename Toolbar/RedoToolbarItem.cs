using System.Windows;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Represents Redo toolbar item
    /// </summary>
    public class RedoToolbarItem : AnnotationToolbarItem
    {
        static RedoToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RedoToolbarItem), new FrameworkPropertyMetadata(typeof(RedoToolbarItem)));
        }

        /// <inheritdoc />
        public override AnnotationItemType ItemType
        {
            get { return AnnotationItemType.Redo; }
        }
    }
}
