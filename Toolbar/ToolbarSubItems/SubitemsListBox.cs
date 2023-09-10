using System.Windows;
using ListBox = System.Windows.Controls.ListBox;

namespace ViewSonic.NoteApp.Toolbar.ToolbarSubItems
{
    /// <summary>
    /// List box control with SubitemsListBox style
    /// </summary>
    public class SubitemsListBox : ListBox
    {
        static SubitemsListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubitemsListBox), new FrameworkPropertyMetadata(typeof(SubitemsListBox)));
        }
    }
}
