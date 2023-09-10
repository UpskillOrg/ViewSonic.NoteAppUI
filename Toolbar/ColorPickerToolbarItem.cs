using System.Windows;
using System.Windows.Media;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Color picker toolbar item
    /// </summary>
    public class ColorPickerToolbarItem : AnnotationToolbarItem
    {
        static ColorPickerToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerToolbarItem), new FrameworkPropertyMetadata(typeof(ColorPickerToolbarItem)));
        }

        /// <summary>
        /// Constructs <see cref="ColorPickerToolbarItem"/>
        /// </summary>
        public ColorPickerToolbarItem()
        {
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#CBD2DA") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#00A7F4") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#1478C7") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FE3A68") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC43D") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#00CB79") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#4D6780") });
            SubItems.Add(new ColorPickerToolbarSubItem() { Brush = (SolidColorBrush)new BrushConverter().ConvertFrom("#292929") });

            BrushSubItem = (ColorPickerToolbarSubItem)SubItems[2];
            BrushSubItem.IsSelected = true;
        }

        /// <inheritdoc />
        public override AnnotationItemType ItemType
        {
            get { return AnnotationItemType.ColorPicker; }
        }

        /// <summary>
        /// Dependency property for <see cref="BrushSubItem"/>
        /// </summary>
        public static readonly DependencyProperty BrushSubItemProperty = DependencyProperty.Register(nameof(BrushSubItem), typeof(ColorPickerToolbarSubItem), typeof(ColorPickerToolbarItem));

        /// <summary>
        /// Gets or sets a value indicating current selected <see cref="ColorPickerToolbarSubItem"/> from subitems collection
        /// </summary>
        public ColorPickerToolbarSubItem BrushSubItem
        {
            get { return (ColorPickerToolbarSubItem)GetValue(BrushSubItemProperty); }
            set { SetValue(BrushSubItemProperty, value); }
        }
    }
}
