using System.Windows;
using ViewSonic.NoteApp.Toolbar.ToolbarSubItems;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Represents Pen item on toolbar
    /// </summary>
    public class PenToolbarItem : AnnotationToolbarItem
    {
        static PenToolbarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PenToolbarItem), new FrameworkPropertyMetadata(typeof(PenToolbarItem)));
        }

        /// <summary>
        /// Constructs <see cref="PenToolbarItem"/>
        /// </summary>
        public PenToolbarItem()
        {
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 4, PenHeight = 4, IsHighlighter = false });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 10, PenHeight = 10 });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 16, PenHeight = 16 });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 20, PenHeight = 20 });
            SubItems.Add(new PenToolbarSubItem() { PenWidth = 24, PenHeight = 24 });

            PenSubItem = (PenToolbarSubItem)SubItems[0];
            PenSubItem.IsSelected = true;
        }

        /// <inheritdoc />
        public override bool IsTogglable
        {
            get { return true; }
        }

        /// <inheritdoc />
        public override AnnotationItemType ItemType
        {
            get { return AnnotationItemType.Pen; }
        }

        /// <summary>
        /// Dependency property for <see cref="PenSubItem"/>
        /// </summary>
        public static readonly DependencyProperty PenSubItemProperty = DependencyProperty.Register(nameof(PenSubItem), typeof(PenToolbarSubItem), typeof(PenToolbarItem));

        /// <summary>
        /// Gets or sets current selected PenSubItem from subitems collection
        /// </summary>
        public PenToolbarSubItem PenSubItem
        {
            get { return (PenToolbarSubItem)GetValue(PenSubItemProperty); }
            set { SetValue(PenSubItemProperty, value); }
        }
    }
}
