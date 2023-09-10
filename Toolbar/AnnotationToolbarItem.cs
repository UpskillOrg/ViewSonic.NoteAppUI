using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Automation;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Base class for toolbar items
    /// </summary>
    public abstract class AnnotationToolbarItem : System.Windows.Controls.Control
    {
        /// <summary>
        /// Constructs <see cref="AnnotationToolbarItem"/>
        /// </summary>
        protected AnnotationToolbarItem()
        {
            SubItems = new ObservableCollection<object>();
        }

        /// <summary>
        /// Gets a value indicating is current item could be toggled
        /// </summary>
        public virtual bool IsTogglable
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating current item type
        /// </summary>
        public virtual AnnotationItemType ItemType
        {
            get { return AnnotationItemType.Undefined; }
        }

        /// <summary>
        /// Dependency property for <see cref="IsToggled"/> property
        /// </summary>
        public static readonly DependencyProperty IsToggledProperty = DependencyProperty.Register(nameof(IsToggled), typeof(bool), typeof(AnnotationToolbarItem));

        /// <summary>
        /// Gets or sets value indicating is current item toggled
        /// </summary>
        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        /// <summary>
        /// Dependency property for <see cref="IsSelected"/> property
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(AnnotationToolbarItem));

        /// <summary>
        /// Gets or sets value indicating is current item selected
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Gets a collection of subitems for current item
        /// </summary>
        public ObservableCollection<object> SubItems { get; }
    }
}
