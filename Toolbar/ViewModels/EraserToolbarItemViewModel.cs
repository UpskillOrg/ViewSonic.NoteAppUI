namespace ViewSonic.NoteApp.Toolbar.ViewModels
{
    public class EraserToolbarItemViewModel : AnnotationToolbarItemViewModel
    {
        public EraserToolbarItemViewModel()
        {
            ItemType = AnnotationItemType.Eraser;
        }
        
        public override bool IsTogglable
        {
            get { return true; }
        }
    }
}
