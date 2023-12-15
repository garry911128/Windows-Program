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
    public class EllipseTests
    {
        double newX;
        double newY;
        double width;
        double height;

        // Test Init
        [TestInitialize]
        public void Setup()
        {
            newX = 10.0;
            newY = 20.0;
            width = 30.0;
            height = 40.0;
        }

        // TestConstructor
        [TestMethod]
        public void Ellipse_Constructor_SetsPropertiesCorrectly()
        {
            Setup();
            Ellipse ellipse = new Ellipse(newX, newY, width, height);
            Assert.AreEqual(Constants.ELLIPSE, ellipse.ShapeName);
            Assert.AreEqual(new Point(newX, newY), ellipse.GetPoint(0));
            Assert.AreEqual(new Point(newX + width, newY + height), ellipse.GetPoint(1));
        }

        // Test string
        [TestMethod]
        public void Ellipse_GetInfo_ReturnsCorrectString()
        {
            Setup();
            Ellipse ellipse = new Ellipse(newX, newY, width, height);
            string info = ellipse.Location;
            string expectedInfo = "(10,20),(40,60)";
            Assert.AreEqual(expectedInfo, info);
        }

        // Test Draw call
        [TestMethod]
        public void Ellipse_Draw_CallsDrawEllipseOnGraphics()
        {
            Setup();
            Ellipse ellipse = new Ellipse(newX, newY, width, height);
            MockGraphics mockGraphics = new MockGraphics();
            ellipse.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.CountEllipse);
            mockGraphics.ResetAllCount();
            ellipse.isSelected = true;
            ellipse.Draw(mockGraphics);
            Assert.AreEqual(1, mockGraphics.CountEllipse);
            Assert.AreEqual(8, mockGraphics.CountHandle);
            Assert.AreEqual(1, mockGraphics.CountRectangle);
        }
    }

}