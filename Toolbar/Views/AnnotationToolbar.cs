using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using ViewSonic.NoteApp.Toolbar.Events;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;
using ViewSonic.NoteApp.Toolbar.ViewModels;
using Binding = System.Windows.Data.Binding;
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using Control = System.Windows.Controls.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using ListBox = System.Windows.Controls.ListBox;
using Size = System.Windows.Size;

namespace ViewSonic.NoteApp.Toolbar.Views
{
    /// <summary>
    /// Represents a toolbar control
    /// </summary>
    public class AnnotationToolbar : Control
    {
        private DraggableToolbarItem _draggableZone;
        private Button _sessionEnd;
        private ListBox _subItemsListBox;
        private ListBox _toolsListBox;
        private Button _clearBoardButton;
        private Button _undoToolbarButton;
        private Button _redoToolbarButton;
        private AnnotationToolbarItemViewModel _selectedItem;
        private CanvasMode _lastCanvasMode;

        private Window _subItemsPopup;
        private const double MaxSubItemWidth = 36 * 9;
        private const double TopMargin = 21;

        private double _currentTop;
        private double _currentLeft;
        private bool _isPopupOpen;

        static AnnotationToolbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnnotationToolbar), new FrameworkPropertyMetadata(typeof(AnnotationToolbar)));
        }

        /// <summary>
        /// DragDelta routed event
        /// </summary>
        public static readonly RoutedEvent DragDeltaEvent = EventManager.RegisterRoutedEvent("DragDelta",
            RoutingStrategy.Direct, typeof(DragDeltaEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// DragDelta event
        /// </summary>
        public event DragDeltaEventHandler DragDelta
        {
            add => AddHandler(DragDeltaEvent, value);
            remove => RemoveHandler(DragDeltaEvent, value);
        }

        /// <summary>
        /// DragCompleted routed event
        /// </summary>
        public static readonly RoutedEvent DragCompletedEvent = EventManager.RegisterRoutedEvent("DragCompleted",
            RoutingStrategy.Direct, typeof(DragCompletedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// DragCompleted event
        /// </summary>
        public event DragCompletedEventHandler DragCompleted
        {
            add => AddHandler(DragCompletedEvent, value);
            remove => RemoveHandler(DragCompletedEvent, value);
        }

        /// <summary>
        /// SessionEnded routed event
        /// </summary>
        public static readonly RoutedEvent SessionEndedEvent = EventManager.RegisterRoutedEvent("SessionEnded",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// SessionEnded event
        /// </summary>
        public event RoutedEventHandler SessionEnded
        {
            add => AddHandler(SessionEndedEvent, value);
            remove => RemoveHandler(SessionEndedEvent, value);
        }

        /// <summary>
        /// ColorChanged routed event
        /// </summary>
        public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged",
            RoutingStrategy.Direct, typeof(ColorChangedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// ColorChanged event
        /// </summary>
        public event ColorChangedEventHandler ColorChanged
        {
            add => AddHandler(ColorChangedEvent, value);
            remove => RemoveHandler(ColorChangedEvent, value);
        }

        /// <summary>
        /// FontSizeChanged routed event
        /// </summary>
        public static readonly RoutedEvent FontSizeChangedEvent = EventManager.RegisterRoutedEvent("FontSizeChanged",
            RoutingStrategy.Direct, typeof(FontSizeChangedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// FontSizeChanged  event
        /// </summary>
        public event FontSizeChangedEventHandler FontSizeChanged
        {
            add => AddHandler(FontSizeChangedEvent, value);
            remove => RemoveHandler(FontSizeChangedEvent, value);
        }

        /// <summary>
        /// PenCHanged routed event
        /// </summary>
        public static readonly RoutedEvent PenChangedEvent = EventManager.RegisterRoutedEvent("PenChanged",
            RoutingStrategy.Direct, typeof(PenChangedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// PenChanged event
        /// </summary>
        public event PenChangedEventHandler PenChanged
        {
            add => AddHandler(PenChangedEvent, value);
            remove => RemoveHandler(PenChangedEvent, value);
        }

        /// <summary>
        /// CanvasModeChanged routed event
        /// </summary>
        public static readonly RoutedEvent CanvasModeChangedEvent = EventManager.RegisterRoutedEvent("CanvasModeChanged",
            RoutingStrategy.Direct, typeof(CanvasModeChangedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// CanvasModeChanged event
        /// </summary>
        public event CanvasModeChangedEventHandler CanvasModeChanged
        {
            add => AddHandler(CanvasModeChangedEvent, value);
            remove => RemoveHandler(CanvasModeChangedEvent, value);
        }

        /// <summary>
        /// ClearBoard routed event
        /// </summary>
        public static readonly RoutedEvent ClearBoardEvent = EventManager.RegisterRoutedEvent("ClearBoard",
            RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// ClearBoard event
        /// </summary>
        public event RoutedEventHandler ClearBoard
        {
            add => AddHandler(ClearBoardEvent, value);
            remove => RemoveHandler(ClearBoardEvent, value);
        }

        /// <summary>
        /// Undo routed event
        /// </summary>
        public static readonly RoutedEvent UndoEvent = EventManager.RegisterRoutedEvent("Undo",
            RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// Undo event
        /// </summary>
        public event RoutedEventHandler Undo
        {
            add => AddHandler(UndoEvent, value);
            remove => RemoveHandler(UndoEvent, value);
        }

        /// <summary>
        /// Redo routed event
        /// </summary>
        public static readonly RoutedEvent RedoEvent = EventManager.RegisterRoutedEvent("Redo",
            RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(AnnotationToolbar));

        /// <summary>
        /// Redo event
        /// </summary>
        public event RoutedEventHandler Redo
        {
            add => AddHandler(RedoEvent, value);
            remove => RemoveHandler(RedoEvent, value);
        }

        /// <summary>
        /// Dependency property for <see cref="SubItems"/>
        /// </summary>
        public static readonly DependencyProperty SubItemsProperty = DependencyProperty.Register(nameof(SubItems), typeof(ObservableCollection<IAnnotationToolbarSubItem>), typeof(AnnotationToolbar));

        /// <summary>
        /// Gets or sets a SubItems collection
        /// </summary>
        public ObservableCollection<IAnnotationToolbarSubItem> SubItems
        {
            get => (ObservableCollection<IAnnotationToolbarSubItem>)GetValue(SubItemsProperty);
            set => SetValue(SubItemsProperty, value);
        }

        /// <summary>
        /// DependencyProperty for SharingIndicatorHeader
        /// </summary>
        public static readonly DependencyProperty EndToolbarButtonTextProperty =
            DependencyProperty.Register(nameof(EndToolbarButtonText), typeof(string), typeof(AnnotationToolbar));

        /// <summary>
        /// Gets or sets text for sharing indicator header
        /// </summary>
        public string EndToolbarButtonText
        {
            get => (string)GetValue(EndToolbarButtonTextProperty);
            set => SetValue(EndToolbarButtonTextProperty, value);
        }

        /// <summary>
        /// Gets or sets a SubItems collection
        /// </summary>
        public bool IsSubItemsOnLeftSide { get; private set; }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _draggableZone = GetTemplateChild("DraggableZone") as DraggableToolbarItem;


            if (_draggableZone != null)
            {
                _draggableZone.DragDelta += DraggableZone_DragDelta;
                _draggableZone.DragCompleted += DraggableZone_DragCompleted;
                _draggableZone.PreviewMouseDown += DraggableZone_PreviewMouseDown;
            }

            //Setting the end button
            _sessionEnd = GetTemplateChild("EndSession") as Button;
            if (_sessionEnd != null)
            {
                _sessionEnd.Click += SessionEnd_Click;
            }

            //Setting clear board button
            _clearBoardButton = GetTemplateChild("ClearBoardButton") as Button;
            if (_clearBoardButton != null)
            {
                _clearBoardButton.Click += ClearBoardButton_Click;
            }

            //Setting UndoToolbarButton
            _undoToolbarButton = GetTemplateChild("UndoToolbarButton") as Button;
            if (_undoToolbarButton != null)
            {
                _undoToolbarButton.Click += UndoToolbarButton_Click;
            }

            //Setting RedoToolbarButton
            _redoToolbarButton = GetTemplateChild("RedoToolbarButton") as Button;
            if (_redoToolbarButton != null)
            {
                _redoToolbarButton.Click += RedoToolbarButton_Click;
            }

            //Setting the main items list box
            _toolsListBox = GetTemplateChild("ToolsListBox") as ListBox;
            if (_toolsListBox != null)
            {
                _toolsListBox.SelectionChanged += ToolsListBox_SelectionChanged;
                _toolsListBox.MouseLeftButtonUp += ToolsListBox_MouseLeftButtonUp;
                _toolsListBox.KeyDown += ToolsListBox_KeyDown;
            }

            _subItemsListBox = new SubitemsListBox();
            if (_subItemsListBox != null)
            {
                _subItemsListBox.SelectionChanged += SubItemsListBox_SelectionChanged;
                _subItemsListBox.MouseLeftButtonUp += SubItemsListBox_MouseLeftButtonUp;
            }

            var b = new Binding("SubItems")
            {
                Source = this,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            _subItemsListBox.SetBinding(ItemsControl.ItemsSourceProperty, b);

            _subItemsPopup = new Window
            {
                ResizeMode = ResizeMode.NoResize,
                AllowsTransparency = false,
                Background = Brushes.Transparent,
                WindowStyle = WindowStyle.None,
                SizeToContent = SizeToContent.WidthAndHeight,
                Content = _subItemsListBox,
                ShowInTaskbar = false,
                Title = "BJNPalette",
                Width = 0,
                Height = 0
            };

            WindowChrome.SetWindowChrome(_subItemsPopup, new WindowChrome()
            {
                CaptionHeight = 0
            });

            _subItemsPopup.Show();

            _isPopupOpen = true;

            IsSubItemsOnLeftSide = true;

            _lastCanvasMode = CanvasMode.Pen;
            SetIsToggled(AnnotationItemType.Pen);
            var wnd = Window.GetWindow(this);
            if (wnd != null)
            {
                wnd.LocationChanged += Wnd_LocationChanged;
                _subItemsPopup.Deactivated += Wnd_Deactivated;
                _subItemsPopup.Left = wnd.Left;
                _subItemsPopup.Top = wnd.Top;
                _subItemsPopup.Owner = wnd;
            }
        }

        /// <summary>
        /// This method is used to handle arrow keys navigation inside sub items like text/color/size
        /// </summary>
        private void ToolsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isPopupOpen && _subItemsListBox != null && _subItemsListBox.Items.Count != 0)
            {
                if (e.Key == Key.Left)
                {
                    var selectedIndex = _subItemsListBox.SelectedIndex;
                    if (selectedIndex > 0)
                    {
                        _subItemsListBox.SelectedIndex--;
                    }
                }
                else if (e.Key == Key.Right)
                {
                    var selectedIndex = _subItemsListBox.SelectedIndex;
                    if (selectedIndex < _subItemsListBox.Items.Count - 1)
                    {
                        _subItemsListBox.SelectedIndex++;
                    }
                }
            }
        }

        private void Wnd_LocationChanged(object sender, EventArgs e)
        {
            if (sender is Window wnd)
            {
                IsSubItemsOnLeftSide = wnd.Owner.Left + wnd.Owner.Width - (wnd.Left + 42) < MaxSubItemWidth;

                _subItemsPopup.Left = wnd.Left;
                _subItemsPopup.Top = wnd.Top;
                _currentLeft = wnd.Left;
                _currentTop = wnd.Top;
            }
        }

        private void Wnd_Deactivated(object sender, EventArgs e) => CloseSubItems();

        private void CloseSubItems()
        {
            SubItems = null;
            if (_selectedItem != null)
            {
                _selectedItem.IsSelected = false;
            }

            _isPopupOpen = false;
        }

        private void ToolsListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_selectedItem != null)
            {
                _toolsListBox.SelectedItem = null;
            }

            if (_selectedItem != null && _selectedItem.SubItems.Count == 0)
            {
                _selectedItem.IsSelected = false;
            }
        }

        private void ToolsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            if (e.AddedItems[0] is not Control item)
            {
                return;
            }

            SubItems = null;
            _selectedItem = (AnnotationToolbarItemViewModel)item.DataContext;
            _selectedItem.IsSelected = !_selectedItem.IsSelected;

            DeselectAllExceptCurrent();

            if (_selectedItem.IsSelected && !_selectedItem.IsTogglable || _selectedItem.IsTogglable && _selectedItem.IsToggled)
            {
                SubItems = _selectedItem.SubItems;
            }

            if (_selectedItem.IsTogglable && _selectedItem.IsToggled)
            {
                switch (_selectedItem.ItemType)
                {
                    case AnnotationItemType.Text:
                    case AnnotationItemType.Pen:
                        PlaceSubItemsPopup(_selectedItem.ItemType);
                        break;
                }
            }

            if (_selectedItem.IsTogglable && !_selectedItem.IsToggled)
            {
                SetIsToggled(_selectedItem.ItemType);
            }

            if (_selectedItem.ItemType is AnnotationItemType.Eraser or
                AnnotationItemType.Pen or
                AnnotationItemType.Text)
            {
                var mode = GetCanvasModeFromItemType(_selectedItem.ItemType);
                if (mode == _lastCanvasMode)
                {
                    if (_selectedItem.ItemType == _lastOpenedItemType)
                    {
                        _isPopupOpen = !_isPopupOpen;
                        if (!_isPopupOpen)
                        {
                            SubItems = null;
                        }
                    }
                    else
                    {
                        _isPopupOpen = SubItems != null;
                    }

                    _lastOpenedItemType = _selectedItem.ItemType;
                    _toolsListBox.SelectedItem = null;
                    return;
                }

                _lastCanvasMode = mode;
                var modeChangedArgs = new CanvasModeChangedEventArgs(mode)
                {
                    RoutedEvent = CanvasModeChangedEvent
                };
                RaiseEvent(modeChangedArgs);
            }
            else if (!_selectedItem.IsTogglable)
            {
                switch (_selectedItem.ItemType)
                {
                    case AnnotationItemType.ColorPicker:
                        PlaceSubItemsPopup(AnnotationItemType.ColorPicker);
                        break;
                    case AnnotationItemType.Eraser:
                        break;
                }
            }

            if (_selectedItem.ItemType == _lastOpenedItemType)
            {
                _isPopupOpen = !_isPopupOpen;
                if (!_isPopupOpen)
                {
                    SubItems = null;
                }
            }
            else
            {
                _isPopupOpen = SubItems != null;
            }

            _lastOpenedItemType = _selectedItem.ItemType;
            _toolsListBox.SelectedItem = null;
        }

        private void PlaceSubItemsPopup(AnnotationItemType itemType)
        {
            var i = 0;
            foreach (Control item in _toolsListBox.Items)
            {
                var itemViewModel = (AnnotationToolbarItemViewModel)item.DataContext;
                if (itemViewModel.ItemType == itemType)
                {
                    _subItemsPopup.Top = _currentTop + TopMargin + i * item.ActualHeight;
                    if (IsSubItemsOnLeftSide)
                    {
                        _subItemsListBox.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        _subItemsPopup.Left = _currentLeft - 2 - _subItemsListBox.DesiredSize.Width;
                    }
                    else
                    {
                        _subItemsPopup.Left = _currentLeft + item.ActualWidth + 2;
                    }

                    if (IsSubItemsOnLeftSide && SubItems != null)
                    {
                        SubItems = new ObservableCollection<IAnnotationToolbarSubItem>(SubItems.Reverse());
                    }

                    break;
                }

                i++;
            }
        }

        private AnnotationItemType _lastOpenedItemType;

        private CanvasMode GetCanvasModeFromItemType(AnnotationItemType type)
        {
            switch (type)
            {
                case AnnotationItemType.Pen:
                    return CanvasMode.Pen;
                case AnnotationItemType.Text:
                    return CanvasMode.Text;
                case AnnotationItemType.Eraser:
                    return CanvasMode.Eraser;
            }

            return CanvasMode.None;
        }

        private void SetIsToggled(AnnotationItemType itemType)
        {
            foreach (Control toolbarItem in _toolsListBox.Items)
            {
                var itemViewModel = (AnnotationToolbarItemViewModel)toolbarItem.DataContext;
                if (itemViewModel.ItemType == itemType)
                {
                    itemViewModel.IsToggled = true;
                    continue;
                }

                itemViewModel.IsToggled = false;
            }
        }

        private void DeselectAllExceptCurrent()
        {
            foreach (Control toolbarItem in _toolsListBox.Items)
            {
                var itemViewModel = (AnnotationToolbarItemViewModel)toolbarItem.DataContext;
                if (itemViewModel == _selectedItem)
                {
                    continue;
                }

                itemViewModel.IsSelected = false;
            }
        }

        private void SubItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            if (e.AddedItems[0] is ToolbarSubItem selectedItem)
            {
                switch (selectedItem.ItemType)
                {
                    case AnnotationSubItemType.ColorPicker:
                        var colorPicker = (ColorPickerToolbarSubItem)selectedItem;
                        ((ColorPickerToolbarItemViewModel)_selectedItem).SelectedSubItem = colorPicker;
                        var args = new ColorChangedEventArgs(((SolidColorBrush)colorPicker.Brush).Color)
                        {
                            RoutedEvent = ColorChangedEvent
                        };
                        RaiseEvent(args);
                        break;

                    case AnnotationSubItemType.Text:
                        var textSubItem = (TextToolbarSubItem)selectedItem;
                        ((TextToolbarItemViewModel)_selectedItem).SelectedSubItem = textSubItem;
                        var fontSizeArgs = new FontSizeChangedEventArgs(textSubItem.FontSize)
                        {
                            RoutedEvent = FontSizeChangedEvent
                        };
                        RaiseEvent(fontSizeArgs);
                        break;
                    case AnnotationSubItemType.Pen:
                        var penSubItem = (PenToolbarSubItem)selectedItem;
                        ((PenToolbarItemViewModel)_selectedItem).SelectedPenSubItem = penSubItem;
                        var penChangedArgs =
                            new PenChangedEventArgs(
                                penSubItem.PenWidth,
                                penSubItem.PenHeight,
                                penSubItem.IsHighlighter
                            )
                            {
                                RoutedEvent = PenChangedEvent
                            };
                        RaiseEvent(penChangedArgs);
                        break;
                }
            }
        }

        private void SubItemsListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SubItems = null;
            _toolsListBox.SelectedItem = null;
            if (_selectedItem != null)
            {
                _selectedItem.IsSelected = false;
            }

            _isPopupOpen = false;
        }

        private void SessionEnd_Click(object sender, RoutedEventArgs e)
        {
            var args = new RoutedEventArgs(SessionEndedEvent);
            RaiseEvent(args);
        }

        private void ClearBoardButton_Click(object sender, RoutedEventArgs e) => RaiseEvent(new RoutedEventArgs { RoutedEvent = ClearBoardEvent });

        private void RedoToolbarButton_Click(object sender, RoutedEventArgs e) => RaiseEvent(new RoutedEventArgs() { RoutedEvent = RedoEvent });

        private void UndoToolbarButton_Click(object sender, RoutedEventArgs e) => RaiseEvent(new RoutedEventArgs() { RoutedEvent = UndoEvent });

        private void DraggableZone_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var args = new DragDeltaEventArgs(e.HorizontalChange, e.VerticalChange)
            {
                RoutedEvent = DragDeltaEvent
            };
            RaiseEvent(args);
            _subItemsPopup.Left += e.HorizontalChange;
            _subItemsPopup.Top += e.VerticalChange;
        }

        private void DraggableZone_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            var args = new DragCompletedEventArgs(e.HorizontalChange, e.VerticalChange, e.Canceled)
            {
                RoutedEvent = DragCompletedEvent
            };
            RaiseEvent(args);
        }

        private void DraggableZone_PreviewMouseDown(object sender, MouseButtonEventArgs e) => CloseSubItems();

        internal void SetUndoRedo(bool isUndoAvailable, bool isRedoAvailable)
        {
            if (_undoToolbarButton != null)
            {
                _undoToolbarButton.IsEnabled = isUndoAvailable;
            }

            if (_redoToolbarButton != null)
            {
                _redoToolbarButton.IsEnabled = isRedoAvailable;
            }
        }

        /// <summary>
        /// Gets an actual settings that <see cref="AnnotationToolbar"/> is currently holding
        /// </summary>
        /// <returns>an instance of <see cref="AnnotationSettings"/></returns>
        public AnnotationSettings GetActualSettings()
        {
            var settings = new AnnotationSettings
            {
                CanvasMode = _lastCanvasMode
            };

            foreach (Control toolbarItem in _toolsListBox.Items)
            {
                var toolBarItemViewModel = toolbarItem.DataContext as AnnotationToolbarItemViewModel;
                switch (toolBarItemViewModel.ItemType)
                {
                    case AnnotationItemType.Pen:
                        {
                            if (toolbarItem is PenToolbarItem pen)
                            {
                                var penToolbarItemViewModel = toolbarItem.DataContext as PenToolbarItemViewModel;
                                settings.PenWidth = penToolbarItemViewModel.SelectedPenSubItem.PenWidth;
                                settings.PenHeight = penToolbarItemViewModel.SelectedPenSubItem.PenHeight;
                                settings.IsHighlighter = penToolbarItemViewModel.SelectedPenSubItem.IsHighlighter;
                            }

                            break;
                        }
                    case AnnotationItemType.ColorPicker:
                        {
                            if (toolbarItem is ColorPickerToolbarItem colorPicker)
                            {
                                settings.Color = (((ColorPickerToolbarSubItem)(colorPicker.DataContext as ColorPickerToolbarItemViewModel).SelectedSubItem).Brush as SolidColorBrush).Color;
                            }

                            break;
                        }
                    case AnnotationItemType.Text:
                        {
                            if (toolbarItem is TextToolbarItem fontSize)
                            {
                                settings.FontSize = (fontSize.DataContext as TextToolbarItemViewModel).SelectedSubItem.FontSize;
                            }

                            break;
                        }
                }
            }

            return settings;
        }

        public new bool Focus() => _toolsListBox?.Focus() ?? base.Focus();
    }
}
