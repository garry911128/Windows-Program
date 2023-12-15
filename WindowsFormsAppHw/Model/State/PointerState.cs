using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppHomework
{
    public class PointerState : IState
    {
        private Model _model;
        private bool _isShapeClicked;
        private Point _lastPoint;
        public bool IsShapeClicked
        {
            get
            {
                return _isShapeClicked;
            }
        }

        public PointerState(Model model)
        {
            _model = model;
            _isShapeClicked = false;
        }

        // Press Mouse and search what shape is selected
        public void PressedMouse(string shapeType, Point mousePoint, bool isPressed)
        {
            _isShapeClicked = false;
            _isShapeClicked = _model.SelectShapeByPoint(mousePoint);
            _lastPoint = mousePoint;
        }

        // Move Mouse and Move shape to another place(no resize)
        public void MovedMouse(Point firstPoint, Point mousePoint)
        {
            if (_isShapeClicked)
            {
                _model.MoveShape(mousePoint.X - _lastPoint.X, mousePoint.Y - _lastPoint.Y);
                _lastPoint = mousePoint;
            }
        }

        // Releasee Mouse and Know that shape stop move
        public void ReleaseMouse(Point firstPoint, Point nowPoint, bool isPressed)
        {
            if (_isShapeClicked)
            {
                _model.ExecuteMoveCommand(firstPoint, nowPoint);
            }
            _isShapeClicked = false;
        }

    }
}

