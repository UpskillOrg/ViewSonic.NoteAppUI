using System.Windows;
using Color = System.Windows.Media.Color;

namespace ViewSonic.NoteApp.Toolbar.Events
{
    /// <summary>
    /// Routed event args for ColorChanged event
    /// </summary>
    public class ColorChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Constructs <see cref="ColorChangedEventArgs"/>
        /// </summary>
        /// <param name="color">Changed color</param>
        public ColorChangedEventArgs(Color color)
        {
            Color = color;
        }

        /// <summary>
        /// Gets a changed color
        /// </summary>
        public Color Color { get; }
    }
}
