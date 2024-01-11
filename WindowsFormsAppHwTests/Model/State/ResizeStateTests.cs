using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class ResizeStateTests
    {
        [TestMethod]
        public void PressedMouse_ShouldSetFirstPoint()
        {
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            var resizeState = new ResizeState(modelMock.Object);
            var mousePoint = new Point(10, 10);
            var originBottomRightPoint = new Point(20, 20);
            shapeMock.Setup(s => s.GetPoint(0)).Returns(mousePoint);
            shapeMock.Setup(s => s.GetPoint(1)).Returns(originBottomRightPoint);
            modelMock.Setup(m => m.GetSelectedShape()).Returns(shapeMock.Object);
            resizeState.PressedMouse(Constants.RECTANGLE, mousePoint, true);
            Assert.AreEqual(mousePoint, resizeState.GetType().GetField("_originTopLeftPoint", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(resizeState));
        }

        [TestMethod]
        public void MovedMouse_ResizeShapeCalledWithCorrectParameters()
        {
            var modelMock = new Mock<Model>();
            var resizeState = new ResizeState(modelMock.Object);
            PrivateObject _privateState = new PrivateObject(resizeState);
            Point firstPoint = new Point(10, 20);
            _privateState.SetField("_originTopLeftPoint", firstPoint);
            modelMock.Object.SlideIndex = 0;
            Point mousePoint = new Point(30, 40);
            resizeState.MovedMouse(firstPoint, mousePoint);
            modelMock.Verify(m => m.ResizeShape(It.Is<Point>(p => p.X == firstPoint.X && p.Y == firstPoint.Y), mousePoint), Times.Once);
        }

        [TestMethod]
        public void ReleaseMouse_ShouldNotThrowException()
        {
            var modelMock = new Mock<Model>();
            var resizeState = new ResizeState(modelMock.Object);
            var firstPoint = new Point(5, 5);
            var mousePoint = new Point(15, 15);
            resizeState.ReleaseMouse(firstPoint, mousePoint, true);
        }
    }
}
