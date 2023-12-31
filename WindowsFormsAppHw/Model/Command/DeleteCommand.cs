using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class DeleteCommand : ICommand
    {

        Shape _shape;
        Model _model;
        int _indexOfStack;
        int _slideIndex;
        Size _canvasSize;
        public DeleteCommand(Model model, Shape shape, int slideIndex, int indexOfStack)
        {
            _shape = shape;
            _model = model;
            _indexOfStack = indexOfStack;
            _slideIndex = slideIndex;
        }

        // execute redo
        public void DoExecute(Size nowSize)
        {
            SetSize(nowSize);
            _model.DeleteShapeOfStack(_slideIndex, _indexOfStack);
        }

        // unexecute undo
        public void UndoExecute(Size nowSize)
        {
            AdjustNowSize(nowSize);
            _model.AddShapeToList(_shape, _slideIndex, _indexOfStack);
        }

        // Set Size
        public void SetSize(Size canvasSize)
        {
            _canvasSize = canvasSize;
        }

        // new Size
        public void AdjustNowSize(Size newCanvasSize)
        {
            _shape.SetPanelSize(newCanvasSize, _canvasSize);
            _canvasSize = newCanvasSize;
        }

        //get slideIndex
        public int GetSlideIndex()
        {
            return _slideIndex;
        }
    }
}
