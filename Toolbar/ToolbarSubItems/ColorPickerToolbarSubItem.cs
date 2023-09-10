using Brush = System.Windows.Media.Brush;

namespace ViewSonic.NoteApp.Toolbar.ToolbarSubItems
{
    /// <summary>
    /// Represents Color picker toolbar subitem
    /// </summary>
    public class ColorPickerToolbarSubItem : ToolbarSubItem
    {
        /// <inheritdoc />
        public override AnnotationSubItemType ItemType
        {
            get { return AnnotationSubItemType.ColorPicker; }
        }

        /// <summary>
        /// Gets or sets brush for current <see cref="ColorPickerToolbarSubItem"/>
        /// </summary>
        public Brush Brush { get; set; }
    }
}
