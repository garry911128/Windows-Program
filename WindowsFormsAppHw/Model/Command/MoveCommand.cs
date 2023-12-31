using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class MoveCommand : ICommand
    {
        Shape _shape;
        Point _delta;
        int _slideIndex;
        Model _model;
        Size _canvasSize;

        public MoveCommand(Model model, Shape shape, int slideIndex)
        {
            _shape = shape;
            _delta = new Point(0, 0);
            _model = model;
            _slideIndex = slideIndex;
        }

        //Set delta after no more move shape
        public virtual void SetDelta(Point firstPoint, Point endPoint)
        {
            _delta.X = endPoint.X - firstPoint.X;
            _delta.Y = endPoint.Y - firstPoint.Y;
        }

        // remember move delta
        public void DoExecute(Size nowSize)
        {
            _delta.X = _delta.X * nowSize.Width / _canvasSize.Width;
            _delta.Y = _delta.Y * nowSize.Height / _canvasSize.Height;
            _shape.Move(_delta.X, _delta.Y);
            SetSize(nowSize);
        }

        // move back
        public void UndoExecute(Size nowSize)
        {
            _delta.X = _delta.X * nowSize.Width / _canvasSize.Width;
            _delta.Y = _delta.Y * nowSize.Height / _canvasSize.Height;    
            _shape.Move(-_delta.X, -_delta.Y);
            SetSize(nowSize);
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
