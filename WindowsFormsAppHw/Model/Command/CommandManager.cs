using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class CommandManager
    {
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();

        // execute
        public virtual void Execute(ICommand command, Size nowSize)
        {
            command.SetSize(nowSize);
            command.DoExecute(nowSize);
            _undo.Push(command);// push command 進 undo stack
            _redo.Clear();// 清除redo stack
        }

        // undo
        public virtual void Undo(Size nowSize)
        {
            if (_undo.Count <= 0)
                throw new Exception(Constants.CAN_NOT_UNDO);
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.UndoExecute(nowSize);
        }

        // redo
        public virtual void Redo(Size nowSize)
        {
            if (_redo.Count <= 0)
                throw new Exception(Constants.CAN_NOT_REDO);
            ICommand command = _redo.Pop();
            _undo.Push(command);
            command.DoExecute(nowSize);
        }

        public virtual bool IsRedoEnabled
        {
            get
            {
                return _redo.Count > 0;
            }
        }

        public virtual bool IsUndoEnabled
        {
            get
            {
                return _undo.Count > 0;
            }
        }

    }
}
