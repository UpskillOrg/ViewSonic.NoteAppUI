using System;

namespace ViewSonic.NoteApp;
internal class AnnotationWindow
{
    private DrawingWindow _drawingWindow;

    public event EventHandler WindowOpened;
    public event EventHandler WindowClosed;

    public AnnotationWindow()
    {
    }

    public void Hide() => System.Windows.Application.Current.Dispatcher.Invoke(() => _drawingWindow?.Close());

    public bool Show()
    {
        if (_drawingWindow != null)
        {
            return true;
        }

        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            _drawingWindow = new DrawingWindow { WindowState = System.Windows.WindowState.Maximized };

            _drawingWindow.Loaded += _drawingWindow_Loaded;
            _drawingWindow.Closed += _drawingWindow_Closed;

            _drawingWindow.Show();
        });

        return true;
    }

    private void _drawingWindow_Loaded(object _1, System.Windows.RoutedEventArgs _2) => WindowOpened?.Invoke(this, null!);

    private void _drawingWindow_Closed(object _1, EventArgs _2)
    {
        WindowClosed?.Invoke(this, null);
        if (_drawingWindow != null)
        {
            _drawingWindow.Closed -= _drawingWindow_Closed;
            _drawingWindow = null;
        }
    }
}
