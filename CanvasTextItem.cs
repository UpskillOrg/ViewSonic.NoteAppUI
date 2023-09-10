
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Windows.Point;
using TextBox = System.Windows.Controls.TextBox;

namespace ViewSonic.NoteApp
{
    /// <summary>
    /// Canvas text element
    /// </summary>
    public class CanvasTextItem : TextBox
    {
        private Button _removeButton;
        private TextBlock _placeholder;

        private Point _lastCoords;
        private bool _isDragging;

        static CanvasTextItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CanvasTextItem), new FrameworkPropertyMetadata(typeof(CanvasTextItem)));
        }

        /// <summary>
        /// DragDelta event definition
        /// </summary>
        public static readonly RoutedEvent DragDeltaEvent = EventManager.RegisterRoutedEvent("DragDelta",
            RoutingStrategy.Direct, typeof(DragDeltaEventHandler), typeof(CanvasTextItem));

        /// <summary>
        /// Fires when test item is dragging
        /// </summary>
        public event DragDeltaEventHandler DragDelta
        {
            add { AddHandler(DragDeltaEvent, value); }
            remove { RemoveHandler(DragDeltaEvent, value); }
        }

        /// <summary>
        /// RemoveItem event definition
        /// </summary>
        public static readonly RoutedEvent RemoveItemEvent = EventManager.RegisterRoutedEvent("RemoveItem",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CanvasTextItem));

        /// <summary>
        /// Fires when remove button is pressed
        /// </summary>
        public event RoutedEventHandler RemoveItem
        {
            add { AddHandler(RemoveItemEvent, value); }
            remove { RemoveHandler(RemoveItemEvent, value); }
        }

        /// <summary>
        /// Dependency property for IsEditing property
        /// </summary>
        public static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(nameof(IsEditing),
            typeof(bool), typeof(CanvasTextItem), new PropertyMetadata(true, IsEditingCallback));

        private static void IsEditingCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not CanvasTextItem item)
            {
                return;
            }

            if (item.IsEditing)
            {
                item.Cursor = Cursors.IBeam;
            }
            else
            {
                item.Cursor = Cursors.SizeAll;
            }
        }

        /// <summary>
        /// Gets or sets value indicating is <see cref="CanvasTextItem"/> is in editing mode
        /// </summary>
        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        /// <summary>
        /// Dependency property for PlaceholderText property
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(nameof(PlaceholderText),
            typeof(string), typeof(CanvasTextItem));

        /// <summary>
        /// Gets or sets <see cref="CanvasTextItem"/> placeholder text
        /// </summary>
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        /// <summary>
        /// Dependency property for IsEditing property
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextColorProperty = DependencyProperty.Register(nameof(PlaceholderTextColor),
            typeof(Brush), typeof(CanvasTextItem), new PropertyMetadata(Brushes.DarkGray));

        /// <summary>
        /// Gets or sets value indicating is <see cref="CanvasTextItem"/> is in editing mode
        /// </summary>
        public Brush PlaceholderTextColor
        {
            get { return (Brush)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or set is current element was removed from Parent
        /// </summary>
        public bool IsRemoved { get; set; }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_removeButton != null)
            {
                _removeButton.PreviewMouseLeftButtonDown -= RemoveButton_PreviewMouseLeftButtonDown;
            }

            _removeButton = GetTemplateChild("RemoveButton") as Button;
            _placeholder = GetTemplateChild("Placeholder") as TextBlock;

            if (_removeButton != null)
            {
                _removeButton.PreviewMouseLeftButtonDown += RemoveButton_PreviewMouseLeftButtonDown;
            }

            UpdatePlaceholderVisibility();
        }

        private void RemoveButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RaiseRemoveEvent();
        }

        /// <inheritdoc />
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            UpdatePlaceholderVisibility();
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!_isDragging)
            {
                return;
            }

            e.Handled = true;

            var currentCoords = PointToScreen(e.GetPosition(this));

            var diff = currentCoords - _lastCoords;

            if (diff.X == 0 && diff.Y == 0)
            {
                return;
            }

            RaiseDragDeltaEvent(diff);

            _lastCoords = currentCoords;
        }

        /// <inheritdoc />
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            if (e.ChangedButton != MouseButton.Left || IsEditing)
            {
                return;
            }

            var source = PresentationSource.FromVisual(this); //Avoid Visual is not connected to a PresentationSource exception.
            if (source == null)
            {
                return;
            }

            _lastCoords = PointToScreen(e.GetPosition(this));
            _isDragging = true;
            CaptureMouse();

            e.Handled = true;
        }

        /// <inheritdoc />
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            _isDragging = false;
            ReleaseMouseCapture();
        }

        private void UpdatePlaceholderVisibility()
        {
            if (_placeholder == null)
            {
                return;
            }

            _placeholder.Visibility = string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RaiseDragDeltaEvent(Vector change)
        {
            var args = new DragDeltaEventArgs(change.X, change.Y)
            {
                RoutedEvent = DragDeltaEvent
            };
            RaiseEvent(args);
        }

        private void RaiseRemoveEvent()
        {
            IsRemoved = true;
            var args = new RoutedEventArgs
            {
                RoutedEvent = RemoveItemEvent
            };
            RaiseEvent(args);
        }
    }
}
