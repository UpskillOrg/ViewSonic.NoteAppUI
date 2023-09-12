using ViewSonic.NoteApp.Toolbar.Views;

namespace ViewSonic.NoteApp.Toolbar
{
    /// <summary>
    /// Annotation toolbar item types
    /// </summary>
    public enum AnnotationItemType
    {
        /// <summary>
        /// Undefined item
        /// </summary>
        Undefined,

        /// <summary>
        /// <see cref="ColorPickerToolbarItem"/>
        /// </summary>
        ColorPicker,

        /// <summary>
        /// <see cref="CanvasTextItem"/>
        /// </summary>
        Text,

        /// <summary>
        /// <see cref="PenToolbarItem"/>
        /// </summary>
        Pen,

        /// <summary>
        /// <see cref="EraserToolbarItem"/>
        /// </summary>
        Eraser,

        /// <summary>
        /// <see cref="ClearBoardToolbarItem"/>
        /// </summary>
        ClearBoard,

        /// <summary>
        /// <see cref="UndoToolbarItem"/>
        /// </summary>
        Undo,

        /// <summary>
        /// <see cref="RedoToolbarItem"/>
        /// </summary>
        Redo
    }
}
