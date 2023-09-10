namespace ViewSonic.NoteApp.Toolbar.ToolbarSubItems
{
    /// <summary>
    /// Represents Pen toolbar subitem for <see cref="PenToolbarItem"/>
    /// </summary>
    public class PenToolbarSubItem : ToolbarSubItem
    {
        /// <summary>
        /// Constructs <see cref="PenToolbarSubItem"/>
        /// </summary>
        public PenToolbarSubItem()
        {
            IsHighlighter = true;
        }

        /// <inheritdoc />
        public override AnnotationSubItemType ItemType
        {
            get { return AnnotationSubItemType.Pen; }
        }

        /// <summary>
        /// Gets or sets Pen width
        /// </summary>
        public double PenWidth { get; set; }

        /// <summary>
        /// Gets or sets Pen height
        /// </summary>
        public double PenHeight { get; set; }

        /// <summary>
        /// Gets or sets is current Pen a highligter
        /// </summary>
        public bool IsHighlighter { get; set; }
    }
}
