using System.Windows.Controls;
using System.Windows.Ink;

namespace ViewSonic.NoteApp.UndoRedo
{
    internal class RemovePathUndoRedoObject : UndoRedoObject
    {
        private readonly InkCanvas _container;
        private readonly Stroke _stroke;

        public RemovePathUndoRedoObject(InkCanvas container, Stroke stroke) : base(stroke)
        {
            _container = container;
            _stroke = stroke;
        }
        public override void Undo()
        {
            _container.Strokes.Remove(_stroke);
        }

        public override void Redo()
        {
            _container.Strokes.Add(_stroke);
        }
    }
}
