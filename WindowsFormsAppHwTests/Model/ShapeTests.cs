using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using WindowsFormsAppHomework;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass()]
    public class ShapeTests
    {

        [TestMethod]
        public void WidthAndHeightProperties_ShouldReturnCorrectValues()
        {
            // Arrange
            double expectedWidth = 20.0;
            double expectedHeight = 30.0;
            Shape shape = new Rectangle(10,20, expectedWidth, expectedHeight); // Assuming your Shape class has a parameterless constructor

            // Act
            double actualWidth = shape.Width;
            double actualHeight = shape.Height;

            // Assert
            Assert.AreEqual(expectedWidth, shape.Width, "Width property should return the correct value");
            Assert.AreEqual(expectedHeight, shape.Height, "Height property should return the correct value");
        }

        //Move Shape
        [TestMethod]
        public void MoveTest()
        {
            // Arrange
            double expectedWidth = 20.0;
            double expectedHeight = 30.0;
            Shape shape = new Rectangle(10, 20, expectedWidth, expectedHeight); // Assuming your Shape class has a parameterless constructor
            shape.Move(5, 5);
            Assert.AreEqual(15, shape.GetPoint(0).X);
            Assert.AreEqual(25, shape.GetPoint(0).Y);
        }

        // test Draw
        [TestMethod()]
        public void InPointInsideHandleLineTest()
        {
            double x1 = 10.0;
            double y1 = 20.0;
            double x2 = 30.0;
            double y2 = 40.0;
            Shape line = new Rectangle(x1, y1, x2, y2);
            Point insideHandlePoint = new Point(x1 + Constants.EIGHT / 2, y1 + Constants.EIGHT / 2);
            Point outsideHandlePoint = new Point(x2 + x1 + Constants.EIGHT, y2 + y1 + Constants.EIGHT);
            bool insideResult = line.IsPointInsideHandle(insideHandlePoint);
            bool outsideResult = line.IsPointInsideHandle(outsideHandlePoint);
            Assert.IsTrue(insideResult, "Inside point should be inside the handle");
            //Assert.IsFalse(outsideResult, "Outside point should be outside the handle");
            Assert.IsFalse(line.IsPointInsideHandle(new Point(0, 0)));
        }

        // Test get shape name
        [TestMethod()]
        public void GetShapeName_ReturnsNull()
        {
            Shape shape = new Shape();
            var result = shape.GetShapeName();
            Assert.IsNull(result);
        }

        // Test get shape info
        [TestMethod()]
        public void GetShapeInfoTest()
        {
            Shape shape = new Shape();
            string expectedInfo = "NotImplementedException";
            Assert.ThrowsException<NotImplementedException>(() => shape.GetInfo(), expectedInfo);
        }

        // Test Draw shape
        [TestMethod()]
        public void DrawShapeTest()
        {
            Shape shape = new Shape();
            MockGraphics graphics = new MockGraphics();
            shape.Draw(graphics);
            Assert.AreEqual(0, graphics.CountRectangle);
        }

        [TestMethod]
        public void SetPanelSize_ShouldAdjustPositionWidthHeightAndCallInitializeRelativePoint()
        {
            // Arrange
            var shape = new Shape(); // 這是測試用的自訂形狀類別，後面會定義
            shape = new Rectangle(10, 20, 20, 20);
            var newSize = new Size(100, 200);
            var oldSize = new Size(50, 100);

            // Act
            shape.SetPanelSize(newSize, oldSize);

            // Assert
            Assert.AreEqual(20, shape.GetPoint(0).X); // (10 * 100) / 50
            Assert.AreEqual(40, shape.GetPoint(0).Y); // (20 * 200) / 100
            Assert.AreEqual(60, shape.GetPoint(1).X); // (30 * 100) / 50
            Assert.AreEqual(80, shape.GetPoint(1).Y); // (40 * 200) / 100
            Assert.AreEqual(40, shape.Width);
            Assert.AreEqual(40, shape.Height);
        }

    }
}
