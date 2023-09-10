namespace ViewSonic.NoteApp.Toolbar.ToolbarSubItems
{
    /// <summary>
    /// Represents toolbar text subitem for <see cref="TextToolbarItem"/>
    /// </summary>
    public class TextToolbarSubItem : ToolbarSubItem
    {
        /// <inheritdoc />
        public override AnnotationSubItemType ItemType
        {
            get { return AnnotationSubItemType.Text; }
        }

        /// <summary>
        /// Gets or sets a font size value for current <see cref="TextToolbarSubItem"/>
        /// </summary>
        public double FontSize { get; set; }
    }
}
