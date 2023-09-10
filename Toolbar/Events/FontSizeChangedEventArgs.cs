using System.Windows;

namespace ViewSonic.NoteApp.Toolbar.Events
{
    /// <summary>
    /// Routed event args for FontSize event
    /// </summary>
    public class FontSizeChangedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Constructs instance of <see cref="FontSizeChangedEventArgs"/>
        /// </summary>
        /// <param name="fontSize">Changed font size</param>
        public FontSizeChangedEventArgs(double fontSize)
        {
            FontSize = fontSize;
        }

        /// <summary>
        /// Gets a changed font size
        /// </summary>
        public double FontSize { get; }
    }
}
