using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class ResizeState : IState
    {
        private Model _model;
        private Point _originTopLeftPoint;
        private Point _originBottomRightPoint;

        public ResizeState(Model model)
        {
            _model = model;
        }

        // Press Mouse and search what shape is selected
        public void PressedMouse(string shapeType, Point mousePoint, bool isPressed)
        {
            Shape selectedShape = _model.GetSelectedShape();
            Point selectedShapeBasePoint = selectedShape.GetPoint(0);
            _originTopLeftPoint = new Point(selectedShapeBasePoint.X, selectedShapeBasePoint.Y);
            _originBottomRightPoint = new Point(selectedShape.GetPoint(1).X, selectedShape.GetPoint(1).Y);
        }

        // Move Mouse and Move shape to another place(no resize)
        public void MovedMouse(Point firstPoint, Point mousePoint)
        {
            _model.ResizeShape(new Point(_originTopLeftPoint.X, _originTopLeftPoint.Y), mousePoint);
        }

        // Releasee Mouse and Know that shape stop move
        public void ReleaseMouse(Point firstPoint, Point mousePoint, bool isPressed)
        {
            _model.ExecuteResizeCommand(_originTopLeftPoint, _originBottomRightPoint, mousePoint);
        }

    }
}
