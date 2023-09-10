using System.Windows;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Clear board item
    /// </summary>
    public class ClearBoardToolbarItem : AnnotationToolbarItem
    {
        static ClearBoardToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClearBoardToolbarItem), new FrameworkPropertyMetadata(typeof(ClearBoardToolbarItem)));
        }

        /// <inheritdoc />
        public override AnnotationItemType ItemType
        {
            get { return AnnotationItemType.ClearBoard; }
        }
    }
}
