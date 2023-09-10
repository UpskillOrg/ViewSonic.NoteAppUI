using System.Windows.Controls;
using System.Windows.Ink;

namespace ViewSonic.NoteApp.UndoRedo
{
    internal class CreatePathUndoRedoObject : UndoRedoObject
    {
        private readonly InkCanvas _container;
        private readonly Stroke _stroke;

        public CreatePathUndoRedoObject(InkCanvas container, Stroke stroke) : base(stroke)
        {
            _container = container;
            _stroke = stroke;
        }
        public override void Undo()
        {
            if (!_container.Strokes.Contains(_stroke))
            {
                _container.Strokes.Add(_stroke);
            }
        }

        public override void Redo()
        {
            _container.Strokes.Remove(_stroke);
        }
    }
}
