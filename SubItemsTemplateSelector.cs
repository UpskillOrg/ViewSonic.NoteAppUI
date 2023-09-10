using System.Windows;
using System.Windows.Controls;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;

namespace ViewSonic.NoteApp
{
    /// <summary>
    /// Template selector for toolbar subitems
    /// </summary>
    public class SubItemsTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets template for color picker subitems
        /// </summary>
        public DataTemplate ColorPickerTemplate { get; set; }

        /// <summary>
        /// Gets or sets template for text subitems
        /// </summary>
        public DataTemplate TextTemplate { get; set; }

        /// <summary>
        /// Gets or sets template for Pen subitems
        /// </summary>
        public DataTemplate PenTemplate { get; set; }

        /// <inheritdoc />
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = item as IAnnotationToolbarSubItem;
            switch (element.ItemType)
            {
                case AnnotationSubItemType.ColorPicker:
                    return ColorPickerTemplate;
                case AnnotationSubItemType.Text:
                    return TextTemplate;
                case AnnotationSubItemType.Pen:
                    return PenTemplate;
                default:
                    return base.SelectTemplate(item, container);
            }
        }
    }
}
