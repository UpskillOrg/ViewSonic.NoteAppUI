using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using ViewSonic.NoteApp.Toolbar;
using ViewSonic.NoteApp.UndoRedo;
using Brushes = System.Windows.Media.Brushes;
using Cursor = System.Windows.Input.Cursor;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Point = System.Windows.Point;

namespace ViewSonic.NoteApp
{
    /// <summary>
    /// Control for inking and text
    /// </summary>
    [TemplatePart(Name = "PART_Inking", Type = typeof(InkCanvas))]
    [TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
    public class AnnotationCanvas : ContentControl
    {
        private const string InkCanvasPartName = "PART_Inking";
        private const string ContentPartName = "PART_Content";
        private InkCanvas _canvas;
        private Cursor _brushCursor;
        private Cursor _textCursor;
        private Cursor _eraserCursor;
        private double _currentFontSize;
        private int _clearedStrokesCount;
        private int _clearedUIElementsCount;
        private UndoRedoManager _undoRedoManager;
        private CanvasTextItem _selectedTextItem;
        private CanvasMode _canvasMode;
        private readonly double _textItemVerticalOffset = 12;
        private ContentPresenter _content = null;

        static AnnotationCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnnotationCanvas), new FrameworkPropertyMetadata(typeof(AnnotationCanvas)));
        }

        /// <summary>
        /// Dependency property for PlaceholderText property
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(nameof(PlaceholderText),
            typeof(string), typeof(AnnotationCanvas), new PropertyMetadata(string.Empty, PlaceholderTextChanged));

        private static void PlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not AnnotationCanvas annotationCanvas || annotationCanvas._canvas == null)
            {
                return;
            }

            foreach (CanvasTextItem textItem in annotationCanvas._canvas.Children)
            {
                textItem.PlaceholderText = annotationCanvas.PlaceholderText;
            }
        }

        /// <summary>
        /// Gets or sets <see cref="CanvasTextItem"/> placeholder text
        /// </summary>
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        /// <summary>
        /// Dependency property for <see cref="AnnotationToolbar"/>
        /// </summary>
        public static readonly DependencyProperty ToolbarProperty = DependencyProperty.Register(nameof(Toolbar),
            typeof(AnnotationToolbar), typeof(AnnotationCanvas), new PropertyMetadata(PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not AnnotationCanvas surface)
            {
                return;
            }

            if (e.NewValue != null)
            {
                surface.SubscribeOnToolbarEvents();
            }
            else
            {
                surface.UnsubscribeFromToolbarEvents();
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Toolbar"/>
        /// </summary>
        public AnnotationToolbar Toolbar
        {
            get { return (AnnotationToolbar)GetValue(ToolbarProperty); }
            set { SetValue(ToolbarProperty, value); }
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _canvas = GetTemplateChild(InkCanvasPartName) as InkCanvas;
            if (_canvas != null)
            {
                _canvas.UseCustomCursor = true;
                _canvas.PreviewMouseLeftButtonDown += Canvas_MouseLeftButtonDown;
                _canvas.StrokeCollected += Canvas_StrokeCollected;
                _canvas.StrokeErasing += CanvasOnStrokeErasing;

                _undoRedoManager = new UndoRedoManager(_canvas);
                _undoRedoManager.UndoRedoAvailabilityChanged += UndoRedoManager_UndoRedoAvailabilityChanged;
            }

            if (_canvas == null)
            {
                throw new NullReferenceException($"Template part {InkCanvasPartName} not available");
            }

            _content = GetTemplateChild(ContentPartName) as ContentPresenter;

            if (_content == null)
            {
                throw new NullReferenceException($"Template part {ContentPartName} not available");
            }

            _brushCursor = ((FrameworkElement)TryFindResource("BrushCursor")).Cursor;
            _textCursor = ((FrameworkElement)TryFindResource("TextCursor")).Cursor;
            _eraserCursor = ((FrameworkElement)TryFindResource("EraserCursor")).Cursor;
            PreviewKeyDown += AnnotationCanvas_KeyDown;
            SetCanvasMode(CanvasMode.Pen);
        }

        /// <summary>
        /// Clear the strokes in the ink canvas 
        /// </summary>
        public void Clear()
        {
            _canvas?.Strokes.Clear();
        }

        private void AnnotationCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (_canvasMode == CanvasMode.Text &&
                _selectedTextItem != null &&
                !_selectedTextItem.IsEditing &&
                e.Key == Key.Delete)
            {
                _undoRedoManager.PerformUndoForElement(_selectedTextItem);
            }
            else if (e.Key == Key.Z && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                _undoRedoManager.PerformUndoRedo(1, true);
            }
            else if (e.Key == Key.Y && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                _undoRedoManager.PerformUndoRedo(1, false);
            }
        }

        private void Toolbar_Loaded(object sender, RoutedEventArgs e)
        {
            ApplySettings(Toolbar.GetActualSettings());
            Toolbar.SetUndoRedo(_undoRedoManager.IsUndoPossible(), _undoRedoManager.IsRedoPossible());
        }

        private void UndoRedoManager_UndoRedoAvailabilityChanged(object sender, UndoRedoEventArgs e)
        {
            Toolbar.SetUndoRedo(e.IsUndoAvailable, e.IsRedoAvailable);
        }

        private void CanvasOnStrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
        {
            ResetClearedElementCount();
            _undoRedoManager.Insert(e.Stroke, UndoRedoCommandType.Add);
        }

        private void Canvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            ResetClearedElementCount();
            _undoRedoManager.Insert(e.Stroke, UndoRedoCommandType.Delete);
        }

        private void ResetClearedElementCount()
        {
            _clearedStrokesCount = 0;
            _clearedUIElementsCount = 0;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var previousSelectedTextItem = _selectedTextItem;
            _selectedTextItem = null;
            var currentMousePosition = Mouse.GetPosition(this);
            currentMousePosition.X -= 6;

            foreach (CanvasTextItem textItem in _canvas.Children)
            {
                if (IsPointInTextBox(currentMousePosition, textItem))
                {
                    _selectedTextItem = textItem;
                    break;
                }
            }

            if (previousSelectedTextItem != null && !Equals(previousSelectedTextItem, _selectedTextItem) && !previousSelectedTextItem.IsRemoved)
            {
                previousSelectedTextItem.IsEditing = false;
                if (string.IsNullOrEmpty(previousSelectedTextItem.Text))
                {
                    _undoRedoManager.PerformUndoForElement(previousSelectedTextItem);
                }

                return;
            }

            if (_canvasMode != CanvasMode.Text)
            {
                return;
            }

            _selectedTextItem ??= CreateTextItem(currentMousePosition);

            if (e.ClickCount == 2 && _selectedTextItem != null)
            {
                _selectedTextItem.IsEditing = true;
            }
        }

        private bool IsPointInTextBox(Point p, CanvasTextItem item)
        {
            var left = InkCanvas.GetLeft(item);
            var top = InkCanvas.GetTop(item);
            var itemWidth = item.ActualWidth;
            var itemHeight = item.ActualHeight;
            if ((p.X > left) && (p.X < (left + itemWidth)) && (p.Y > top) && (p.Y < (top + itemHeight)))
            {
                return true;
            }

            return false;
        }

        private CanvasTextItem CreateTextItem(Point cursorPosition)
        {
            var textItem = new CanvasTextItem
            {
                IsEditing = true,
                Width = 200,
                PlaceholderText = PlaceholderText,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(2),
                FontSize = _currentFontSize,
                Foreground = new SolidColorBrush(_canvas.DefaultDrawingAttributes.Color)
            };
            InkCanvas.SetLeft(textItem, cursorPosition.X);
            InkCanvas.SetTop(textItem, cursorPosition.Y - _textItemVerticalOffset);
            textItem.DragDelta += TextItem_DragDelta;
            textItem.RemoveItem += TextItem_RemoveItem;
            _canvas.Children.Add(textItem);
            textItem.Focus();
            _undoRedoManager.Insert(textItem, UndoRedoCommandType.Delete);
            return textItem;
        }

        private void TextItem_RemoveItem(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.PerformUndoForElement((CanvasTextItem)sender);
            _selectedTextItem = null;
        }

        private void TextItem_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var item = (UIElement)sender;
            var left = InkCanvas.GetLeft(item);
            var top = InkCanvas.GetTop(item);
            InkCanvas.SetLeft(item, left + e.HorizontalChange);
            InkCanvas.SetTop(item, top + e.VerticalChange);
        }

        private void SubscribeOnToolbarEvents()
        {
            Toolbar.Loaded += Toolbar_Loaded;

            Toolbar.ColorChanged += Toolbar_ColorChanged;
            Toolbar.FontSizeChanged += Toolbar_FontSizeChanged;
            Toolbar.PenChanged += Toolbar_PenChanged;
            Toolbar.CanvasModeChanged += Toolbar_CanvasModeChanged;
            Toolbar.ClearBoard += Toolbar_ClearBoard;
            Toolbar.Undo += Toolbar_Undo;
            Toolbar.Redo += Toolbar_Redo;
        }

        private void UnsubscribeFromToolbarEvents()
        {
            Toolbar.ColorChanged -= Toolbar_ColorChanged;
            Toolbar.FontSizeChanged -= Toolbar_FontSizeChanged;
            Toolbar.PenChanged -= Toolbar_PenChanged;
            Toolbar.CanvasModeChanged -= Toolbar_CanvasModeChanged;
            Toolbar.ClearBoard -= Toolbar_ClearBoard;
            Toolbar.Undo -= Toolbar_Undo;
            Toolbar.Redo -= Toolbar_Redo;
        }

        private void ApplySettings(AnnotationSettings settings)
        {
            _canvas.DefaultDrawingAttributes = new DrawingAttributes
            {
                Color = settings.Color,
                IsHighlighter = settings.IsHighlighter,
                Width = settings.PenWidth,
                Height = settings.PenHeight,
                FitToCurve = true
            };
            _currentFontSize = settings.FontSize;

            SetCanvasMode(settings.CanvasMode);
        }

        private void SetCanvasMode(CanvasMode mode)
        {
            _canvasMode = mode;
            switch (mode)
            {
                case CanvasMode.Pen:
                    _canvas.EditingMode = InkCanvasEditingMode.Ink;
                    _canvas.Cursor = _brushCursor;
                    break;
                case CanvasMode.Text:
                    _canvas.EditingMode = InkCanvasEditingMode.None;
                    _canvas.Cursor = _textCursor;
                    break;
                case CanvasMode.Eraser:
                    _canvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    _canvas.Cursor = _eraserCursor;
                    break;
            }
        }


        private void Toolbar_Undo(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.PerformUndoRedo(1, true, _clearedStrokesCount, _clearedUIElementsCount);
        }

        private void Toolbar_Redo(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.PerformUndoRedo(1, false, _clearedStrokesCount, _clearedUIElementsCount);
        }

        private void Toolbar_ClearBoard(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.ClearStore();

            foreach (var stroke in _canvas.Strokes)
            {
                _undoRedoManager.Insert(stroke, UndoRedoCommandType.Add);
            }

            foreach (UIElement child in _canvas.Children)
            {
                _undoRedoManager.Insert(child, UndoRedoCommandType.Add);
            }

            _clearedStrokesCount = _canvas.Strokes.Count;
            _clearedUIElementsCount = _canvas.Children.Count;

            _canvas.Strokes.Clear();
            _canvas.Children.Clear();
        }

        private void Toolbar_CanvasModeChanged(object sender, Toolbar.Events.CanvasModeChangedEventArgs e)
        {
            SetCanvasMode(e.CanvasMode);
        }

        private void Toolbar_PenChanged(object sender, Toolbar.Events.PenChangedEventArgs e)
        {
            _canvas.DefaultDrawingAttributes.Width = e.PenWidth;
            _canvas.DefaultDrawingAttributes.Height = e.PenHeight;
            _canvas.DefaultDrawingAttributes.IsHighlighter = e.IsHighlighter;
        }

        private void Toolbar_FontSizeChanged(object sender, Toolbar.Events.FontSizeChangedEventArgs e)
        {
            _currentFontSize = e.FontSize;
        }

        private void Toolbar_ColorChanged(object sender, Toolbar.Events.ColorChangedEventArgs e)
        {
            _canvas.DefaultDrawingAttributes.Color = e.Color;
        }
    }
}

