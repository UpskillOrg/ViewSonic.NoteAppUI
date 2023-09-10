using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;

namespace ViewSonic.NoteApp.UndoRedo
{
    internal class UndoRedoManager
    {
        private readonly InkCanvas _container;

        private readonly Stack<UndoRedoObject> _undoCommands;
        private readonly Stack<UndoRedoObject> _redoCommands;

        public UndoRedoManager(InkCanvas canvas)
        {
            _container = canvas;
            _undoCommands = new Stack<UndoRedoObject>();
            _redoCommands = new Stack<UndoRedoObject>();
        }

        public void ClearStore()
        {
            _undoCommands.Clear();
            _redoCommands.Clear();
        }

        public bool IsUndoPossible()
        {
            return _undoCommands.Count > 0;
        }
        public bool IsRedoPossible()
        {
            return _redoCommands.Count > 0;
        }

        public void Insert(UIElement element, UndoRedoCommandType cmdType)
        {
            UndoRedoObject obj;
            if (cmdType == UndoRedoCommandType.Add)
            {
                obj = new CreateUIElementsUndoRedoObject(_container, element);
            }
            else
            {
                obj = new RemoveUIElementsUndoRedoObject(_container, element);
            }

            _undoCommands.Push(obj);
            RaiseUndoRedoAvailabilityChanged();
        }

        public void Insert(Stroke stroke, UndoRedoCommandType cmdType)
        {
            UndoRedoObject obj;
            if (cmdType == UndoRedoCommandType.Add)
            {
                obj = new CreatePathUndoRedoObject(_container, stroke);
            }
            else
            {
                obj = new RemovePathUndoRedoObject(_container, stroke);
            }

            _undoCommands.Push(obj);
            RaiseUndoRedoAvailabilityChanged();
        }

        public void PerformUndoForElement(CanvasTextItem textItem)
        {
            var localStack = new Stack<UndoRedoObject>();
            while (_undoCommands.Count > 0)
            {
                var command = _undoCommands.Pop();
                if (command.ContainsElement(textItem))
                {
                    command.Undo();
                    _redoCommands.Push(command);
                    break;
                }

                localStack.Push(command);
            }

            while (localStack.Count > 0)
            {
                var command = localStack.Pop();
                _undoCommands.Push(command);
            }

            RaiseUndoRedoAvailabilityChanged();
        }

        public void PerformUndoRedo(int levels, bool isUndo, int clearedStrokesCount = 0, int clearedUIElementsCount = 0)
        {
            if (clearedStrokesCount != 0 || clearedUIElementsCount != 0)
            {
                ProcessCommands(clearedStrokesCount, isUndo);
                ProcessCommands(clearedUIElementsCount, isUndo);
            }
            else
            {
                ProcessCommands(levels, isUndo);
            }
        }

        private void ProcessCommands(int levels, bool isUndo)
        {
            for (var i = 1; i <= levels; i++)
            {
                if (isUndo)
                {
                    if (_undoCommands.Count > 0)
                    {
                        var command = _undoCommands.Pop();
                        command.Undo();
                        _redoCommands.Push(command);
                    }
                }
                else
                {
                    if (_redoCommands.Count > 0)
                    {
                        var command = _redoCommands.Pop();
                        command.Redo();
                        _undoCommands.Push(command);
                    }
                }
            }

            RaiseUndoRedoAvailabilityChanged();
        }

        private void RaiseUndoRedoAvailabilityChanged()
        {
            var isUndoAvailable = _undoCommands.Count > 0;
            var isRedoAvailable = _redoCommands.Count > 0;
            var args = new UndoRedoEventArgs(isUndoAvailable, isRedoAvailable);
            UndoRedoAvailabilityChanged?.Invoke(this, args);
        }

        public event EventHandler<UndoRedoEventArgs> UndoRedoAvailabilityChanged;
    }
}
