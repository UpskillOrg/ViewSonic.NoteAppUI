using System;

namespace ViewSonic.NoteApp.UndoRedo
{
    /// <summary>
    /// Notifying is undo/redo available
    /// </summary>
    public class UndoRedoEventArgs : EventArgs
    {
        /// <summary>
        /// Constructs <see cref="UndoRedoObject"/>
        /// </summary>
        /// <param name="isUndoAvailable"></param>
        /// <param name="isRedoAvailable"></param>
        public UndoRedoEventArgs(bool isUndoAvailable, bool isRedoAvailable)
        {
            IsUndoAvailable = isUndoAvailable;
            IsRedoAvailable = isRedoAvailable;
        }

        /// <summary>
        /// Gets or sets value is undo available
        /// </summary>
        public bool IsUndoAvailable { get; set; }

        /// <summary>
        /// Gets or sets value is redo available
        /// </summary>
        public bool IsRedoAvailable { get; set; }
    }
}
