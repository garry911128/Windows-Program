using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAppHomework
{
    public enum HandlePosition
    {
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left
    }

    public class Shape
    {
        public bool isSelected 
        { 
            get; 
            set; 
        }

        // ShapeName Getter  DataBinding
        public string ShapeName
        {
            get
            {
                return GetShapeName();
            }
        }

        // Location Getter DataBinding
        public string Location
        {
            get
            {
                return GetInfo();
            }
        }
        public double Width
        {
            get 
            { 
                return _width; 
            }
        }
        public double Height
        {
            get 
            { 
                return _height; 
            }
        }

        protected string _shapeName;
        protected const string NOT_IMPLEMENT = "NotImplementedException";
        protected List<Point> _position;
        protected List<Point> _relativePoints;
        protected double _width;
        protected double _height;
        protected const int TOP_LEFT_POINT_INDEX = 0;
        protected const int BOTTOM_RIGHT_POINT_INDEX = 1;
        
        public Shape()
        {
            isSelected = false;
            _position = new List<Point>();
            _relativePoints = new List<Point>();
            for (int i = 0; i < Constants.EIGHT ; i++)
            {
                _relativePoints.Add(new Point(0, 0));
            }
        }

        // Init handle position, topleft point wont change
        public virtual void InitializeRelativePoint()
        {
            _relativePoints[(int)(HandlePosition.TopRight)].X = _width;// Top-Right
            _relativePoints[(int)(HandlePosition.BottomLeft)].Y = _height;// Bottom-Left
            _relativePoints[(int)(HandlePosition.BottomRight)].X = _width;// Bottom-Right
            _relativePoints[(int)(HandlePosition.BottomRight)].Y = _height;// Bottom-Right
            _relativePoints[(int)(HandlePosition.Top)].X = _width / Constants.TWO;// Top-Center
            _relativePoints[(int)(HandlePosition.Bottom)].X = _width / Constants.TWO;// Bottom-Center
            _relativePoints[(int)(HandlePosition.Bottom)].Y = _height;// Bottom-Center
            _relativePoints[(int)(HandlePosition.Left)].Y = _height / Constants.TWO;// Left-Center
            _relativePoints[(int)(HandlePosition.Right)].X = _width;// Right-Center
            _relativePoints[(int)(HandlePosition.Right)].Y = _height / Constants.TWO;// Right-Center
        }

        //getShapeName
        public virtual string GetShapeName() 
        {
            return _shapeName;
        }

        //get Shape's position
        public virtual string GetInfo()
        {
            throw new NotImplementedException(NOT_IMPLEMENT);
        }

        // Getter position
        public virtual Point GetPoint(int index)
        {
            return _position[index];
        }

        //cal the right button point
        public Point GetRightBottomPoint()
        {
            return new Point(_position[TOP_LEFT_POINT_INDEX].X + _width, _position[TOP_LEFT_POINT_INDEX].Y + _height);
        }

        // assert is a Point in the Shape
        public virtual bool IsPointInsideShape(Point mousePoint)
        {
            return IsPointInsideBoundingBox(mousePoint, _position[TOP_LEFT_POINT_INDEX], _position[BOTTOM_RIGHT_POINT_INDEX]);
        }

        // assert is a point in the Shape "Handle"
        public virtual bool IsPointInsideHandle(Point mousePoint)
        {
            foreach (Point relativePoint in _relativePoints)
            {
                Point handlePoint = new Point(_position[TOP_LEFT_POINT_INDEX].X + relativePoint.X, _position[TOP_LEFT_POINT_INDEX].Y + relativePoint.Y);
                if (IsPointInsideBoundingBox(mousePoint, new Point(handlePoint.X - Constants.EIGHT, handlePoint.Y - Constants.EIGHT), new Point(handlePoint.X + Constants.EIGHT, handlePoint.Y + Constants.EIGHT)))
                {
                    return true; 
                }
            }
            return false; 
        }

        // Helper method to check if a point is inside a bounding box
        protected bool IsPointInsideBoundingBox(Point pointToCheck, Point boundingBoxTopLeft, Point boundingBoxBottomRight)
        {
            bool checkX = (pointToCheck.X >= boundingBoxTopLeft.X) && (pointToCheck.X <= boundingBoxBottomRight.X);
            bool checkY = (pointToCheck.Y >= boundingBoxTopLeft.Y) && (pointToCheck.Y <= boundingBoxBottomRight.Y);
            return (checkX && checkY);
        }

        // resize Shape
        public virtual void Resize(Point startPoint, Point endPoint)
        {
            double left = Math.Min(startPoint.X, endPoint.X);
            double top = Math.Min(startPoint.Y, endPoint.Y);
            double right = Math.Max(startPoint.X, endPoint.X);
            double bottom = Math.Max(startPoint.Y, endPoint.Y);
            _position[TOP_LEFT_POINT_INDEX].X = left;
            _position[TOP_LEFT_POINT_INDEX].Y = top;
            _position[BOTTOM_RIGHT_POINT_INDEX].X = right;
            _position[BOTTOM_RIGHT_POINT_INDEX].Y = bottom;
            _width = right - left;
            _height = bottom - top;
            InitializeRelativePoint();
        }

        // Set panel(canvas) size, null size condition is prevented in shapes
        public virtual void SetPanelSize(Size newSize, Size oldSize)
        {
            _position[TOP_LEFT_POINT_INDEX].X = _position[TOP_LEFT_POINT_INDEX].X * newSize.Width / oldSize.Width;
            _position[TOP_LEFT_POINT_INDEX].Y = _position[TOP_LEFT_POINT_INDEX].Y * newSize.Height / oldSize.Height;
            _position[BOTTOM_RIGHT_POINT_INDEX].X = _position[BOTTOM_RIGHT_POINT_INDEX].X * newSize.Width / oldSize.Width;
            _position[BOTTOM_RIGHT_POINT_INDEX].Y = _position[BOTTOM_RIGHT_POINT_INDEX].Y * newSize.Height / oldSize.Height;
            _width = _position[BOTTOM_RIGHT_POINT_INDEX].X - _position[TOP_LEFT_POINT_INDEX].X;
            _height = _position[BOTTOM_RIGHT_POINT_INDEX].Y - _position[TOP_LEFT_POINT_INDEX].Y;
            InitializeRelativePoint();
        }

        //Move Shape
        public virtual void Move(double deltaX, double deltaY)
        {
            _position[TOP_LEFT_POINT_INDEX].X += deltaX;
            _position[TOP_LEFT_POINT_INDEX].Y += deltaY;
            _position[BOTTOM_RIGHT_POINT_INDEX].X += deltaX;
            _position[BOTTOM_RIGHT_POINT_INDEX].Y += deltaY;
        }

        // virtual Draw the graphic
        public virtual void Draw(IGraphics graphics)
        {
        }

        // Draw Handle
        public virtual void DrawHandles(IGraphics graphics)
        {
            foreach ( Point relativePoint in _relativePoints)
            {
                Point handlePoint = new Point(_position[TOP_LEFT_POINT_INDEX].X + relativePoint.X, _position[TOP_LEFT_POINT_INDEX].Y + relativePoint.Y);
                DrawHandleAt(handlePoint, graphics);
            }
        }

        // Draw Ellipse Rectangle
        protected void DrawHandleRectangle(IGraphics graphics)
        {
            graphics.DrawRectangle(_position[TOP_LEFT_POINT_INDEX].X, _position[TOP_LEFT_POINT_INDEX].Y, _width, _height);
        }

        // Draw a hanlde at a specific place
        protected void DrawHandleAt(Point position, IGraphics graphics)
        {
            graphics.DrawHandle(position.X, position.Y);
        }

    }
}
