using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class Line : Shape
    {
        private const int LEFT_POINT_INDEX = 0;
        private const int RIGHT_POINT_INDEX = 1;

        public Line(double x1, double y1, double x2, double y2) : base()
        {
            _shapeName = Constants.LINE;
            Point leftPoint = new Point(x1,y1);
            Point rightPoint = new Point(x2, y2);
            _position.Add(leftPoint);
            _position.Add(rightPoint);
            InitializeRelativePoint();
        }

        // return line loc
        public override string GetInfo()
        {
            string message = Constants.LEFT_SMALL_BRACKET + _position[LEFT_POINT_INDEX].X + Constants.DOT + _position[LEFT_POINT_INDEX].Y + Constants.RIGHT_SMALL_BRACKET + Constants.DOT + Constants.LEFT_SMALL_BRACKET + _position[RIGHT_POINT_INDEX].X + Constants.DOT + _position[RIGHT_POINT_INDEX].Y + Constants.RIGHT_SMALL_BRACKET;
            return message;
        }

        //resize Line
        public override void Resize(Point startPoint, Point endPoint)
        {
            if (startPoint.X <= endPoint.X)
            {
                _position[LEFT_POINT_INDEX] = startPoint;
                _position[RIGHT_POINT_INDEX] = endPoint;
            }
            else
            {
                _position[LEFT_POINT_INDEX] = endPoint;
                _position[RIGHT_POINT_INDEX] = startPoint;
            }
            _width = Math.Abs(_position[RIGHT_POINT_INDEX].X - _position[LEFT_POINT_INDEX].X);
            _height = Math.Abs(_position[RIGHT_POINT_INDEX].Y - _position[LEFT_POINT_INDEX].Y);
            InitializeRelativePoint();
        }

        // Is point in Line
        public override bool IsPointInsideShape(Point mousePoint)
        {
            double nowX = mousePoint.X;
            double nowY = mousePoint.Y;
            bool checkX = (nowX >= _position[LEFT_POINT_INDEX].X) && (nowX <= _position[RIGHT_POINT_INDEX].X);
            bool checkY = (nowY >= Math.Min(_position[TOP_LEFT_POINT_INDEX].Y, _position[RIGHT_POINT_INDEX].Y)) && (nowY <= Math.Max(_position[TOP_LEFT_POINT_INDEX].Y, _position[RIGHT_POINT_INDEX].Y));
            return (checkX && checkY);
        }

        // assert is a point in the Shape "Handle"
        public override bool IsPointInsideHandle(Point mousePoint)
        {
            Point handlePoint = new Point(_position[LEFT_POINT_INDEX].X, _position[LEFT_POINT_INDEX].Y);
            if (IsPointInsideBoundingBox(mousePoint, new Point(handlePoint.X - Constants.EIGHT, handlePoint.Y - Constants.EIGHT), new Point(handlePoint.X + Constants.EIGHT, handlePoint.Y + Constants.EIGHT)))
            {
                return true;
            }
            handlePoint = new Point(_position[RIGHT_POINT_INDEX].X, _position[RIGHT_POINT_INDEX].Y);
            if (IsPointInsideBoundingBox(mousePoint, new Point(handlePoint.X - Constants.EIGHT, handlePoint.Y - Constants.EIGHT), new Point(handlePoint.X + Constants.EIGHT, handlePoint.Y + Constants.EIGHT)))
            {
                return true;
            }
            return false;
        }

        //Draw Line
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_position[LEFT_POINT_INDEX].X, _position[LEFT_POINT_INDEX].Y, _position[RIGHT_POINT_INDEX].X, _position[RIGHT_POINT_INDEX].Y);
            if (isSelected)
            {
                DrawHandles(graphics);
            }
        }

        // Draw handles
        public override void DrawHandles(IGraphics graphics)
        {
            DrawHandleAt(_position[LEFT_POINT_INDEX], graphics);// left               
            DrawHandleAt(_position[RIGHT_POINT_INDEX], graphics);// right
        }

    }
}
