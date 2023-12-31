using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class ResizeCommand : ICommand
    {
        Shape _shape;
        Model _model;
        Point _originTopLeftPoint;
        Point _originBottomRightPoint;
        Point _mouseReleasePoint;
        int _slideIndex;
        Size _canvasSize;

        public ResizeCommand(Model model, Shape shape, Point originTopLeftPoint, Point originBottomRightPoint, Point mouseReleasePoint)
        {
            _shape = shape;
            _model = model;
            _originTopLeftPoint = originTopLeftPoint;
            _originBottomRightPoint = originBottomRightPoint;
            _mouseReleasePoint = mouseReleasePoint;
        }

        // remember move delta
        public void DoExecute(Size nowSize)
        {
            _shape.Resize(_originTopLeftPoint, _mouseReleasePoint);
            SetSize(nowSize);
        }

        // move back
        public void UndoExecute(Size nowSize)
        {
            _shape.Resize(_originTopLeftPoint, _originBottomRightPoint);
            SetSize(nowSize);
        }

        //SetSlideIndex
        public void SetSlideIndex(int slideIndex)
        {
            _slideIndex = slideIndex;
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
