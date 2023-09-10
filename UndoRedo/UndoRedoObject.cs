namespace ViewSonic.NoteApp.UndoRedo
{
    internal abstract class UndoRedoObject
    {
        private readonly object _innerElement;

        protected UndoRedoObject(object element)
        {
            _innerElement = element;
        }

        public abstract void Undo();
        public abstract void Redo();

        /// <summary>
        /// Compares elements inside command
        /// </summary>
        /// <param name="element">Element to compare</param>
        /// <returns>True if elements the same</returns>
        public virtual bool ContainsElement(object element)
        {
            return Equals(element, _innerElement);
        }
    }
}
