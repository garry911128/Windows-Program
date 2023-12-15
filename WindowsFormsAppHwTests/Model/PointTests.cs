using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsAppHomework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void PointConstructorTest()
        {
            // Arrange
            double newX = 1.0;
            double newY = 2.5;

            // Act
            Point point = new Point(newX, newY);

            // Assert
            Assert.AreEqual(newX, point.X);
            Assert.AreEqual(newY, point.Y);
        }

        [TestMethod]
        public void EqualsTest()
        {
            // Arrange
            double x1 = 1.0;
            double y1 = 2.5;

            double x2 = 1.0;
            double y2 = 2.5;

            double x3 = 3.0;
            double y3 = 4.5;

            Point point1 = new Point(x1, y1);
            Point point2 = new Point(x2, y2);
            Point point3 = new Point(x3, y3);

            // Act & Assert
            Assert.AreEqual(point1, point2); // Should be equal
            Assert.AreNotEqual(point1, point3); // Should not be equal
        }
    }
}