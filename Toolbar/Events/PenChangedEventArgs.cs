using System.Windows;

namespace ViewSonic.NoteApp.Toolbar.Events
{
    /// <summary>
    /// Describes Pen event args for PenChaged event
    /// </summary>
    public class PenChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Constructs <see cref="PenChangedEventArgs"/>
        /// </summary>
        /// <param name="penWidth">pen width</param>
        /// <param name="penHeight">pen height</param>
        /// <param name="isHighlighter">is pen highlighter</param>
        public PenChangedEventArgs(double penWidth, double penHeight, bool isHighlighter)
        {
            PenWidth = penWidth;
            PenHeight = penHeight;
            IsHighlighter = isHighlighter;
        }

        /// <summary>
        /// Gets a Pen width value
        /// </summary>
        public double PenWidth { get; }

        /// <summary>
        /// Gets a Pen height value
        /// </summary>
        public double PenHeight { get; }

        /// <summary>
        /// Gets a value is Pen highligter
        /// </summary>
        public bool IsHighlighter { get; }
    }
}
