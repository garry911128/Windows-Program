using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsAppHomework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass()]
    public class LineTests
    {
        double x1;
        double y1;
        double x2;
        double y2;

        // Test Init
        [TestInitialize]
        public void Setup()
        {
            x1 = 10.0;
            y1 = 20.0;
            x2 = 30.0;
            y2 = 40.0;
        }

        // TestConstructor
        [TestMethod]
        public void Line_Constructor_SetsPropertiesCorrectly()
        {
            Setup();
            Line line = new Line(x1, y1, x2, y2);
            Assert.AreEqual(Constants.LINE, line.ShapeName);
            Assert.AreEqual(new Point(x1, y1), line.GetPoint(0));
            Assert.AreEqual(new Point(x2, y2), line.GetPoint(1));
        }

        // Test string
        [TestMethod]
        public void Line_GetInfo_ReturnsCorrectString()
        {
            Setup();
            Line line = new Line(x1, y1, x2, y2);
            string info = line.Location;
            string expectedInfo = "(10,20),(30,40)";
            Assert.AreEqual(expectedInfo, info);
        }

        // Test StartPoint smaller than endPoint should adjust list index in Position
        [TestMethod]
        public void Line_Resize_StartPointSmallerThanEndPoint_ShouldAdjustEndPoint()
        {
            Setup();
            Line line = new Line(x1, y1, x2, y2);
            Point newEndPoint = new Point(30, 10);//x bigger than x1
            line.Resize(new Point(x1, y1), new Point(newEndPoint.X, newEndPoint.Y));
            Assert.AreEqual(new Point(x1, y1), line.GetPoint(0));
            Assert.AreEqual(newEndPoint, line.GetPoint(1));
        }

        // Test StartPoint smaller than endPoint should replace list index in Position
        [TestMethod]
        public void Line_Resize_StartPointLargerThanEndPoint_ShouldSwapPoints()
        {
            Setup();
            Line line = new Line(x1, y1, x2, y2);
            Point newEndPoint = new Point(5, 10);//x smaller than x1
            line.Resize(new Point(x1, y1), new Point(newEndPoint.X, newEndPoint.Y));
            Assert.AreEqual(newEndPoint, line.GetPoint(0));
            Assert.AreEqual(new Point(x1, y1), line.GetPoint(1));
        }

        // test Ispoint Inside
        [TestMethod()]
        public void IsPointInsideLineTest()
        {
            Point startPoint = new Point(5, 5);
            Point endPoint = new Point(10, 10);
            Point endPoint1 = new Point(15, 15);
            Point endPoint2 = new Point(3, 3);
            Line line = new Line(startPoint.X, startPoint.Y, endPoint1.X, endPoint1.Y);
            Assert.IsTrue(line.IsPointInsideShape(endPoint));
            Assert.IsFalse(line.IsPointInsideShape(endPoint2));
        }

        // test Draw
        [TestMethod()]
        public void DrawLineTest()
        {
            Setup();
            Line rectangle = new Line(x1, y1, x2, y2);
            MockGraphics mockGraphics = new MockGraphics();
            rectangle.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.CountLine);
            mockGraphics.ResetAllCount();
            rectangle.isSelected = true;
            rectangle.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.CountLine);
            Assert.AreEqual(2, mockGraphics.CountHandle);
        }

        // test Draw
        [TestMethod()]
        public void InPointInsideHandleLineTest()
        {
            // Arrange
            Setup();
            Line line = new Line(x1, y1, x2, y2);
            Point insideHandlePoint = new Point(x1 + Constants.EIGHT / 2, y1 + Constants.EIGHT / 2);
            Point outsideHandlePoint = new Point(x2 + Constants.EIGHT, y2 + Constants.EIGHT);
            bool insideResult = line.IsPointInsideHandle(insideHandlePoint);
            bool outsideResult = line.IsPointInsideHandle(outsideHandlePoint);
            Assert.IsTrue(insideResult, "Inside point should be inside the handle");
            //Assert.IsFalse(outsideResult, "Outside point should be outside the handle");
            Assert.IsFalse(line.IsPointInsideHandle(new Point(0,0)));
        }
    }
}