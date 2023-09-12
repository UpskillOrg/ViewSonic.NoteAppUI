using ViewSonic.NoteApp.Toolbar.Views;
using Color = System.Windows.Media.Color;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Annotation settings which passes from <see cref="AnnotationToolbar"/> to <see cref="AnnotationCanvas"/>
    /// </summary>
    public class AnnotationSettings
    {
        /// <summary>
        /// Gets or sets <see cref="AnnotationCanvas"/> mode
        /// </summary>
        public CanvasMode CanvasMode { get; set; }

        /// <summary>
        /// Gets or sets color for pen
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets font size for text items
        /// </summary>
        public double FontSize { get; set; }

        /// <summary>
        /// Gets or sets pen width in pixels
        /// </summary>
        public double PenWidth { get; set; }

        /// <summary>
        /// Gets or sets pen height in pixels
        /// </summary>
        public double PenHeight { get; set; }

        /// <summary>
        /// Gets or sets is selected pen is highlighter
        /// </summary>
        public bool IsHighlighter { get; set; }
    }
}
