using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsAppHomework;
using System;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass()]
    public class ShapeFactoryTests
    {
        // Test random Shape constructor
        [TestMethod()]
        public void CreateShapeRandomTest()
        {
            // Arrange
            ShapeFactory shapeFactory = new ShapeFactory();

            // Act
            Shape randomShape = shapeFactory.CreateShapeRandom("矩形");

            // Assert
            Assert.IsNotNull(randomShape);
            Assert.IsInstanceOfType(randomShape, typeof(Rectangle));
        }

        // Test given point create test
        [TestMethod()]
        public void CreateShapeTest()
        {
            // Arrange
            ShapeFactory shapeFactory = new ShapeFactory();
            Point topLeftPoint = new Point(50, 50);
            double width = 100;
            double height = 150;

            // Act
            Shape rectangle = shapeFactory.CreateShape("矩形", topLeftPoint, width, height);
            Shape ellipse = shapeFactory.CreateShape("橢圓形", topLeftPoint, width, height);
            Shape line = shapeFactory.CreateShape("線", topLeftPoint, width, height);
            // Assert
            Assert.IsNotNull(rectangle);
            Assert.IsInstanceOfType(rectangle, typeof(Rectangle));

        }

        // Test Create Shape
        [TestMethod()]
        public void CreateShapeInvalidTypeTest()
        {
            // Arrange
            ShapeFactory shapeFactory = new ShapeFactory();
            Point topLeftPoint = new Point(50, 50);
            double width = 100;
            double height = 150;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => shapeFactory.CreateShape("InvalidType", topLeftPoint, width, height));
        }
    }
}
