using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class Rectangle : Shape
    {

        // constructor
        public Rectangle(double startX, double startY, double width, double height) : base()
        {
            _shapeName = Constants.RECTANGLE;
            _width = width;
            _height = height;
            Point topLeftPoint = new Point(startX, startY);
            _position.Add(topLeftPoint);
            _position.Add(GetRightBottomPoint());
            base.InitializeRelativePoint();
        }

        // return the rect loc
        public override string GetInfo()
        {
            string message = Constants.LEFT_SMALL_BRACKET + _position[TOP_LEFT_POINT_INDEX].X + Constants.DOT + _position[TOP_LEFT_POINT_INDEX].Y + Constants.RIGHT_SMALL_BRACKET + Constants.DOT + Constants.LEFT_SMALL_BRACKET + _position[BOTTOM_RIGHT_POINT_INDEX].X + Constants.DOT + _position[BOTTOM_RIGHT_POINT_INDEX].Y + Constants.RIGHT_SMALL_BRACKET;
            return message;
        }

        //Draw Rectangle use windowform adpator
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(_position[TOP_LEFT_POINT_INDEX].X, _position[TOP_LEFT_POINT_INDEX].Y, _width, _height);
            if (isSelected)
            {
                DrawHandles(graphics);
            }
        }
    }
}
