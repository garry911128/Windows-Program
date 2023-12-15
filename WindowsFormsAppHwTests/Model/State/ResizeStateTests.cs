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
            shapeMock.Setup(s => s.GetPoint(0)).Returns(mousePoint);
            modelMock.Setup(m => m.GetSelectedShape()).Returns(shapeMock.Object);
            resizeState.PressedMouse(Constants.RECTANGLE, mousePoint, true);
            Assert.AreEqual(mousePoint, resizeState.GetType().GetField("_firstPoint", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(resizeState));
        }

        [TestMethod]
        public void MovedMouse_ResizeShapeCalledWithCorrectParameters()
        {
            var modelMock = new Mock<Model>();
            var resizeState = new ResizeState(modelMock.Object);
            PrivateObject _privateState = new PrivateObject(resizeState);
            Point firstPoint = new Point(10, 20);
            _privateState.SetField("_firstPoint", firstPoint);
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
