using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class Ellipse : Shape
    {        

        public Ellipse(double newX, double newY, double width, double height) : base()
        {
            _shapeName = Constants.ELLIPSE;
            _width = width;
            _height = height;
            Point topLeftPoint = new Point(newX, newY);
            _position.Add(topLeftPoint);
            _position.Add(GetRightBottomPoint());
            base.InitializeRelativePoint();
        }

        // return line loc
        public override string GetInfo()
        {
            string message = Constants.LEFT_SMALL_BRACKET + _position[TOP_LEFT_POINT_INDEX].X + Constants.DOT + _position[TOP_LEFT_POINT_INDEX].Y + Constants.RIGHT_SMALL_BRACKET + Constants.DOT + Constants.LEFT_SMALL_BRACKET + _position[BOTTOM_RIGHT_POINT_INDEX].X + Constants.DOT + _position[BOTTOM_RIGHT_POINT_INDEX].Y + Constants.RIGHT_SMALL_BRACKET;
            return message;
        }

        // Draw Ellipse use windowform adpator 
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(_position[TOP_LEFT_POINT_INDEX].X, _position[TOP_LEFT_POINT_INDEX].Y, _width, _height);
            if (isSelected)
            {
                DrawHandles(graphics);
                DrawHandleRectangle(graphics);
            }
        }
    }
}
