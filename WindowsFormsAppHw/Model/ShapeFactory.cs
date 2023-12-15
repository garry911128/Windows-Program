using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class ShapeFactory
    {
        private Random _random;
        protected const int MAX_RANGE = 300;
        protected const int MIN_RANGE = 0;
        private const string RECTANGLE = "矩形";
        private const string LINE = "線";
        private const string ELLIPSE = "橢圓形";
        private const string INVALID_TYPE = "Invalid Shape Type";

        public ShapeFactory()
        {
            _random = new Random();
        }

        //create shape with factory by string name
        public virtual Shape CreateShapeRandom(string shapeType)
        {
            Point topLeftPoint = new Point(_random.Next(MIN_RANGE, MAX_RANGE), _random.Next(MIN_RANGE, MAX_RANGE));
            double width = _random.Next(MIN_RANGE, MAX_RANGE);
            double height = _random.Next(MIN_RANGE, MAX_RANGE);
            return CreateShape(shapeType, topLeftPoint, width, height);
        }

        //create shape with factory by string name
        public virtual Shape CreateShape(string shapeType, Point topLeftPoint, double width, double height)
        {
            switch (shapeType)
            {
                case RECTANGLE:
                    return new Rectangle(topLeftPoint.X, topLeftPoint.Y, width, height);
                case LINE:
                    return new Line(topLeftPoint.X, topLeftPoint.Y, topLeftPoint.X + width, topLeftPoint.Y + height);
                case ELLIPSE:
                    return new Ellipse(topLeftPoint.X, topLeftPoint.Y, width, height);
                default:
                    throw new ArgumentException(INVALID_TYPE);
            }
        }

    }
}
