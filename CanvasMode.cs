namespace ViewSonic.NoteApp
{
    /// <summary>
    /// Possible variants of <see cref="AnnotationCanvas"/>
    /// </summary>
    public enum CanvasMode
    {
        /// <summary>
        /// No annotation
        /// </summary>
        None,
        /// <summary>
        /// Drawing with Pen
        /// </summary>
        Pen,
        /// <summary>
        /// Creating text items
        /// </summary>
        Text,
        /// <summary>
        /// Eraser for pen strokes
        /// </summary>
        Eraser
    }
}
