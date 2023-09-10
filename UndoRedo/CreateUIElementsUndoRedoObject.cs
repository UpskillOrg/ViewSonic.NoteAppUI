using System.Windows;
using System.Windows.Controls;

namespace ViewSonic.NoteApp.UndoRedo
{
    internal class CreateUIElementsUndoRedoObject : UndoRedoObject
    {
        private readonly InkCanvas _container;
        private readonly UIElement _control;

        public CreateUIElementsUndoRedoObject(InkCanvas container, UIElement control) : base(control)
        {
            _container = container;
            _control = control;
        }
        public override void Undo()
        {
            _container.Children.Add(_control);

            if (_control is CanvasTextItem item)
            {
                item.IsRemoved = false;
            }
        }

        public override void Redo()
        {
            _container.Children.Remove(_control);

            if (_control is CanvasTextItem item)
            {
                item.IsRemoved = true;
            }
        }
    }
}
