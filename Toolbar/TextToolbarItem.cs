using System.Windows;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Represents Text item for <see cref="AnnotationToolbar"/>
    /// </summary>
    public class TextToolbarItem : AnnotationToolbarItem
    {
        static TextToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextToolbarItem), new FrameworkPropertyMetadata(typeof(TextToolbarItem)));
        }

        /// <summary>
        /// Constructs <see cref="TextToolbarItem"/>
        /// </summary>
        public TextToolbarItem()
        {
            SubItems.Add(new TextToolbarSubItem() { FontSize = 12 });
            SubItems.Add(new TextToolbarSubItem() { FontSize = 18 });
            SubItems.Add(new TextToolbarSubItem() { FontSize = 24 });
            SubItems.Add(new TextToolbarSubItem() { FontSize = 36 });
            SubItems.Add(new TextToolbarSubItem() { FontSize = 48 });
            SubItems.Add(new TextToolbarSubItem() { FontSize = 60 });
            SubItems.Add(new TextToolbarSubItem() { FontSize = 72 });


            TextSubItem = (TextToolbarSubItem)SubItems[0];

            TextSubItem.IsSelected = true;
        }

        /// <inheritdoc />
        public override bool IsTogglable
        {
            get { return true; }
        }

        /// <inheritdoc />
        public override AnnotationItemType ItemType
        {
            get { return AnnotationItemType.Text; }
        }

        /// <summary>
        /// Dependency property for <see cref="TextSubItem"/>
        /// </summary>
        public static readonly DependencyProperty TextSubItemProperty = DependencyProperty.Register(nameof(TextSubItem), typeof(TextToolbarSubItem), typeof(TextToolbarItem));

        /// <summary>
        /// Gets or sets a selected <see cref="TextToolbarSubItem"/>
        /// </summary>
        public TextToolbarSubItem TextSubItem
        {
            get { return (TextToolbarSubItem)GetValue(TextSubItemProperty); }
            set { SetValue(TextSubItemProperty, value); }
        }
    }
}
