namespace ViewSonic.NoteApp.Toolbar.ViewModels
{
    public class EraserToolbarItemViewModel : AnnotationToolbarItemViewModel
    {
        public EraserToolbarItemViewModel()
        {
            ItemType = AnnotationItemType.Eraser;
            IsTogglable = true;
        }
    }
}
