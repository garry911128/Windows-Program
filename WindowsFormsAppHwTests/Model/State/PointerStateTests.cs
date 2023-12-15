using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsAppHomework;
using System.Drawing;
using Moq;
namespace WindowsFormsAppHomework.Tests
{
    [TestClass()]
    public class PointerStateTests
    {
        // Test Selected
        [TestMethod]
        public void PressedMouse_ShouldSelectShapeByPoint()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var pointerState = new PointerState(modelMock.Object);
            var mousePoint = new Point(10, 10);

            // Set up the mock to return true when SelectShapeByPoint is called with mousePoint
            modelMock.Setup(m => m.SelectShapeByPoint(mousePoint)).Returns(true);

            // Act
            pointerState.PressedMouse("Circle", mousePoint, true);

            // Assert
            modelMock.Verify(m => m.SelectShapeByPoint(mousePoint), Times.Once);
            Assert.IsTrue(pointerState.IsShapeClicked); // Corrected
        }

        // Test Move Shape
        [TestMethod]
        public void MovedMouse_WhenShapeClicked_ShouldMoveShape()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shape = new Mock<Shape>();
            var pointerState = new PointerState(modelMock.Object);
            var mousePoint = new Point(10, 10);
            modelMock.Setup(m => m.SelectShapeByPoint(mousePoint)).Returns(true);
            pointerState.PressedMouse("Circle", new Point(10, 10), true); // Simulate a shape being clicked
            var firstPoint = new Point(20, 20);
            mousePoint = new Point(30, 30);

            // Act
            pointerState.MovedMouse(firstPoint, mousePoint);

            // Assert
            modelMock.Verify(m => m.MoveShape(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
        }

        // Cant Move Shape
        [TestMethod]
        public void MovedMouse_WhenShapeNotClicked_ShouldNotMoveShape()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var pointerState = new PointerState(modelMock.Object);
            var firstPoint = new Point(20, 20);
            var mousePoint = new Point(30, 30);

            // Act
            pointerState.MovedMouse(firstPoint, mousePoint);

            // Assert
            modelMock.Verify(m => m.MoveShape(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        // Release Shoud not throw Exception
        [TestMethod]
        public void ReleaseMouse_ShouldNotThrowException()
        {
            var modelMock = new Mock<Model>();
            modelMock.Setup(m => m.ExecuteMoveCommand(It.IsAny<Point>(), It.IsAny<Point>()));
            var pointerState = new PointerState(modelMock.Object);
            PrivateObject privateStatelMock = new PrivateObject(pointerState);
            privateStatelMock.SetField("_isShapeClicked", true);
            var firstPoint = new Point(20, 20);
            var nowPoint = new Point(30, 30);
            pointerState.ReleaseMouse(firstPoint, nowPoint, true);
            modelMock.Verify(m => m.ExecuteMoveCommand(It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
        }

    }
}
