using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class Point
    {
        // construct x
        public double X 
        {
            get; 
            set; 
        }

        // construct y
        public double Y 
        { 
            get; 
            set; 
        }

        // construct point
        public Point(double newX, double newY)
        {
            X = newX;
            Y = newY;
        }

        // equal for test
        public override bool Equals(object instance)
        {
            Point other = (Point)instance;
            return X == other.X && Y == other.Y;
        }
    }

}
