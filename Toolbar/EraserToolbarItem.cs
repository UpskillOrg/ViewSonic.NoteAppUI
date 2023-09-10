using System.Windows;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Represents eraser toolbar item
    /// </summary>
    public class EraserToolbarItem : AnnotationToolbarItem
    {
        static EraserToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EraserToolbarItem), new FrameworkPropertyMetadata(typeof(EraserToolbarItem)));
        }

        /// <inheritdoc />
        public override AnnotationItemType ItemType
        {
            get { return AnnotationItemType.Eraser; }
        }

        /// <inheritdoc />
        public override bool IsTogglable
        {
            get { return true; }
        }
    }
}
