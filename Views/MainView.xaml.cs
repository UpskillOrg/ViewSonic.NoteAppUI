using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using ViewSonic.NoteApp.Toolbar;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace ViewSonic.NoteApp.Views;

/// <summary>
/// Interaction logic for DrawingWindow.xaml
/// </summary>
public partial class MainView
{
    private const int ToolbarMargin = 30;

    private float _xDisplacementRatio = 0;
    private float _yDisplacementRatio = 0;
    private AnnotationToolbarWindow _toolbarWindow;
    private AnnotationToolbar _toolbar;
    private const string DefaultHeader = "Participants can see this whiteboard.";
    private const string DefaultSubHeader = "You are sharing this whiteboard.";
    private const string DefaultEndToolbarButtonText = "END";
    private const string DefaultCanvasTextItemPlaceholderText = "Type your text here...";
    private Window _fakeWnd;

    /// <summary>
    /// Constructs <see cref="MainView"/>
    /// </summary>
    public MainView()
    {

        InitFakeWindow();
        InitializeWindow();
        InitializeComponent();
        SourceInitialized += DrawingWindow_SourceInitialized;
        Closed += DrawingWindow_Closed;
        LocationChanged += DrawingWindow_LocationChanged;
        WeakEventManager<Window, RoutedEventArgs>.AddHandler(this, "Unloaded", OnDrawingWindowUnloaded);
        WeakEventManager<Window, EventArgs>.AddHandler(this, "ContentRendered", OnDrawingWindowContentRendered);
    }

    private void OnDrawingWindowUnloaded(object sender, RoutedEventArgs e)
    {
        WeakEventManager<Window, EventArgs>.RemoveHandler(this, "ContentRendered", OnDrawingWindowContentRendered);
        WeakEventManager<Window, RoutedEventArgs>.RemoveHandler(this, "Unloaded", OnDrawingWindowUnloaded);
        SourceInitialized -= DrawingWindow_SourceInitialized;
        Closed -= DrawingWindow_Closed;
        LocationChanged -= DrawingWindow_LocationChanged;
    }

    private void OnDrawingWindowContentRendered(object sender, EventArgs e)
    {
        SendKeys.SendWait("^t");
    }

    private void Wnd_StateChanged(object sender, EventArgs e)
    {
        Left = _fakeWnd.Left;
        Top = _fakeWnd.Top;
        _fakeWnd.Close();
    }

    private void Wnd_SourceInitialized(object sender, EventArgs e)
    {
        _fakeWnd.WindowState = WindowState.Maximized;
    }

    private void InitFakeWindow()
    {
        _fakeWnd = new Window
        {
            AllowsTransparency = true,
            ResizeMode = ResizeMode.NoResize,
            Background = Brushes.Transparent,
            WindowStyle = WindowStyle.None
        };
        _fakeWnd.SourceInitialized += Wnd_SourceInitialized;
        _fakeWnd.StateChanged += Wnd_StateChanged;
        _fakeWnd.Show();
    }

    /// <summary>
    /// Dependency property for PlaceholderText property
    /// </summary>
    public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(nameof(PlaceholderText),
        typeof(string), typeof(MainView), new PropertyMetadata(DefaultCanvasTextItemPlaceholderText, PlaceholderTextChanged));

    private static void PlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MainView drawingWindow)
        {
            return;
        }

        drawingWindow.AnnotationCanvas.PlaceholderText = drawingWindow.PlaceholderText;
    }

    /// <summary>
    /// Gets or sets <see cref="CanvasTextItem"/> placeholder text
    /// </summary>
    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    /// <summary>
    /// DependencyProperty for SharingIndicatorHeader
    /// </summary>
    public static readonly DependencyProperty EndToolbarButtonTextProperty =
        DependencyProperty.Register(nameof(EndToolbarButtonText), typeof(string), typeof(MainView),
            new PropertyMetadata(DefaultEndToolbarButtonText, EndButtonTextChanged));

    private static void EndButtonTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MainView wnd || wnd._toolbar == null)
        {
            return;
        }

        wnd._toolbar.EndToolbarButtonText = wnd.EndToolbarButtonText;
    }

    /// <summary>
    /// Gets or sets text for sharing indicator header
    /// </summary>
    public string EndToolbarButtonText
    {
        get => (string)GetValue(EndToolbarButtonTextProperty);
        set => SetValue(EndToolbarButtonTextProperty, value);
    }

    /// <summary>
    /// DependencyProperty for SharingIndicatorHeader
    /// </summary>
    public static readonly DependencyProperty SharingIndicatorHeaderProperty =
        DependencyProperty.Register(nameof(SharingIndicatorHeader), typeof(string), typeof(MainView),
            new PropertyMetadata(DefaultHeader));

    /// <summary>
    /// Gets or sets text for sharing indicator header
    /// </summary>
    public string SharingIndicatorHeader
    {
        get => (string)GetValue(SharingIndicatorHeaderProperty);
        set => SetValue(SharingIndicatorHeaderProperty, value);
    }

    /// <summary>
    /// DependencyProperty for SharingIndicatorSubHeader
    /// </summary>
    public static readonly DependencyProperty SharingIndicatorSubHeaderProperty =
        DependencyProperty.Register(nameof(SharingIndicatorSubHeader), typeof(string), typeof(MainView),
            new PropertyMetadata(DefaultSubHeader));

    /// <summary>
    /// Gets or sets text for sharing indicator sub header
    /// </summary>
    public string SharingIndicatorSubHeader
    {
        get => (string)GetValue(SharingIndicatorSubHeaderProperty);
        set => SetValue(SharingIndicatorSubHeaderProperty, value);
    }

    /// <inheritdoc />
    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        if (_toolbarWindow != null)
        {
            AdjustToolbarPosition();
        }
    }

    private void InitializeWindow()
    {
        var canvasBackground = new SolidColorBrush
        {
            Color = Color.FromArgb(1, 0, 0, 0)
        };
        Background = canvasBackground;
        AllowsTransparency = true;
        WindowStyle = WindowStyle.None;
        ResizeMode = ResizeMode.NoResize;
        ShowInTaskbar = false;
    }

    private void DrawingWindow_LocationChanged(object sender, EventArgs e)
    {
        if (_toolbarWindow != null)
        {
            _toolbarWindow.Left = Left + (_xDisplacementRatio * Width);
            _toolbarWindow.Top = Top + (_yDisplacementRatio * Height);
        }

        WindowState = WindowState.Maximized;
    }

    private void InitializeToolbar()
    {
        _toolbarWindow = new AnnotationToolbarWindow();
        _toolbar = new AnnotationToolbar
        {
            EndToolbarButtonText = EndToolbarButtonText
        };
        _toolbarWindow.Content = _toolbar;
        AnnotationCanvas.Toolbar = _toolbar;
        AnnotationCanvas.PlaceholderText = PlaceholderText;
        _toolbarWindow.Owner = this;

        _toolbarWindow.Show();

        _toolbar.DragDelta += Toolbar_DragDelta;
        _toolbar.DragCompleted += Toolbar_DragCompleted;
        _toolbar.SessionEnded += Toolbar_SessionEnded;

        AnnotationCanvas.BorderThickness = new Thickness(0);

        _toolbarWindow.Top = Top + ((ActualHeight - _toolbarWindow.ActualHeight) / 2);
        _toolbarWindow.Left = Left + ToolbarMargin;
        SetToolbarWindowDisplacementFactors();
    }

    private void DrawingWindow_SourceInitialized(object sender, EventArgs e) => InitializeToolbar();

    private void Toolbar_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
    {
        if (_toolbarWindow != null)
        {
            AdjustToolbarPosition();
        }
    }

    private void SetToolbarWindowDisplacementFactors()
    {
        _xDisplacementRatio = ((float)_toolbarWindow.Left - (float)Left) / (float)Width;
        _yDisplacementRatio = ((float)_toolbarWindow.Top - (float)Top) / (float)Height;
    }

    private void AdjustToolbarPosition()
    {
        if (_toolbarWindow.Left < Left)
        {
            _toolbarWindow.Left = Left + ToolbarMargin;
        }

        if (_toolbarWindow.Left + _toolbarWindow.ActualWidth > Left + Width)
        {
            _toolbarWindow.Left = Left + Width - _toolbarWindow.ActualWidth;
        }

        if (_toolbarWindow.Top < Top)
        {
            _toolbarWindow.Top = Top;
        }

        if (_toolbarWindow.Top + _toolbar.ActualHeight > Top + Height)
        {
            _toolbarWindow.Top = Top + Height - _toolbar.ActualHeight;
        }

        SetToolbarWindowDisplacementFactors();
        Focus();
    }

    private void Toolbar_SessionEnded(object sender, RoutedEventArgs e) => Close();

    private void DrawingWindow_Closed(object sender, EventArgs e) => _toolbarWindow?.Close();

    private void Toolbar_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
    {
        _toolbarWindow.Left += e.HorizontalChange;
        _toolbarWindow.Top += e.VerticalChange;
    }

    private void DrawingWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.T && Keyboard.Modifiers == ModifierKeys.Control)
        {
            AnnotationCanvas.Toolbar.Focus();
        }
    }
}
