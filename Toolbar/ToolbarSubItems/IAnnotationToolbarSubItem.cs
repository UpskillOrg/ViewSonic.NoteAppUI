namespace ViewSonic.NoteApp.Toolbar.ToolbarSubItems
{
    /// <summary>
    /// Interface for toolbar subitems
    /// </summary>
    public interface IAnnotationToolbarSubItem
    {
        /// <summary>
        /// Gets or sets value indicating is current <see cref="ToolbarSubItem"/> is selected
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Gets <see cref="ToolbarSubItem"/> type
        /// </summary>
        AnnotationSubItemType ItemType { get; }
    }
}
