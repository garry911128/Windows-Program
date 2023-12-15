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
    public class RectangleTests
    {
        double startX;
        double startY;
        double width;
        double height;

        // Test Init
        [TestInitialize]
        public void Setup()
        {
            startX = 10.0;
            startY = 20.0;
            width = 30.0;
            height = 40.0;
        }

        // TestConstructor
        [TestMethod]
        public void Rectangle_Constructor_SetsPropertiesCorrectly()
        {
            Setup();
            Rectangle rectangle = new Rectangle(startX, startY, width, height);
            Assert.AreEqual(Constants.RECTANGLE, rectangle.ShapeName);
            Assert.AreEqual(startX, rectangle.GetPoint(0).X);
            Assert.AreEqual(startY, rectangle.GetPoint(0).Y);
            Assert.AreEqual(startX + width, rectangle.GetPoint(1).X);
            Assert.AreEqual(startY + height, rectangle.GetPoint(1).Y);
        }

        // Test StartPoint smaller than endPoint should adjust list index in Position
        [TestMethod]
        public void Rect_Resize_StartPointSmallerThanEndPoint_ShouldAdjustEndPoint()
        {
            Setup();
            Rectangle rectangle = new Rectangle(startX, startY, width, height);
            Point newEndPoint = new Point(30, 10);//x bigger than x1
            rectangle.Resize(new Point(startX, startY), new Point(newEndPoint.X, newEndPoint.Y));
            Assert.AreEqual(new Point(10, 10), rectangle.GetPoint(0));
            Assert.AreEqual(new Point(30, 20), rectangle.GetPoint(1));
        }

        // Test string
        [TestMethod]
        public void Rectangle_GetInfo_ReturnsCorrectString()
        {
            Setup();
            Rectangle rectangle = new Rectangle(startX, startY, width, height);
            string info = rectangle.Location;
            string expectedInfo = "(10,20),(40,60)";
            Assert.AreEqual(expectedInfo, info);
        }
        
        // Test Draw call
        [TestMethod]
        public void Rectangle_Draw_CallsDrawRectangleOnGraphics()
        {
            Setup();
            Rectangle rectangle = new Rectangle(startX, startY, width, height);
            MockGraphics mockGraphics = new MockGraphics();
            rectangle.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.CountRectangle);
            mockGraphics.ResetAllCount();
            rectangle.isSelected = true;
            rectangle.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.CountRectangle);
            Assert.AreEqual(8, mockGraphics.CountHandle);
        }
    }
}