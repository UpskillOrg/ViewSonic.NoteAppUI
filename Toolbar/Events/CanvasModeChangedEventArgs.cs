using System.Windows;

namespace ViewSonic.NoteApp.Toolbar.Events
{
    /// <summary>
    /// Routed event args for CanvasModeChanged event
    /// </summary>
    public class CanvasModeChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Constructs <see cref="CanvasModeChangedEventArgs"/>
        /// </summary>
        /// <param name="mode">New canvas mode</param>
        public CanvasModeChangedEventArgs(CanvasMode mode)
        {
            CanvasMode = mode;
        }

        /// <summary>
        /// Gets <see cref="CanvasMode"/>
        /// </summary>
        public CanvasMode CanvasMode { get; }
    }
}
