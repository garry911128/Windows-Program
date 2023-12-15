using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class DrawingStateTests
    {
        // Test Drawing state PressMouse
        [TestMethod]
        public void PressedMouseShouldCreateHintShapeAndShowHint()
        {
            var modelMock = new Mock<Model>();
            var drawingState = new DrawingState(modelMock.Object);
            string shapeType = Constants.RECTANGLE;
            Point topLeftPoint = new Point(10, 10);
            bool isPressed = true;
            modelMock.Setup(m => m.CreateHintShape(shapeType, topLeftPoint, 0, 0)).Verifiable();
            modelMock.SetupSet(m => m.ShowHint = true).Verifiable();
            drawingState.PressedMouse(shapeType, topLeftPoint, isPressed);
            modelMock.Verify();
        }

        // Test Moved Mouse
        [TestMethod()]
        public void MovedMouseTest()
        {
            var modelMock = new Mock<Model>();
            var drawingState = new DrawingState(modelMock.Object);
            Point firstPoint = new Point(10, 10);
            Point nowPoint = new Point(20, 20);
            modelMock.Setup(m => m.ResizeShape(firstPoint, nowPoint)).Verifiable();
            drawingState.MovedMouse(firstPoint, nowPoint);
            modelMock.Verify();
        }

        // Test Release Mouse
        [TestMethod()]
        public void ReleaseMouseTest()
        {
            var modelMock = new Mock<Model>();
            var drawingState = new DrawingState(modelMock.Object);
            Point firstPoint = new Point(10, 10);
            Point nowPoint = new Point(20, 20);
            bool isPressed = true;
            modelMock.Setup(m => m.ResizeShape(firstPoint, nowPoint)).Verifiable();
            modelMock.Setup(m => m.ExecuteDrawCommand()).Verifiable();
            modelMock.SetupSet(m => m.ShowHint = false).Verifiable();
            drawingState.ReleaseMouse(firstPoint, nowPoint, isPressed);
            modelMock.Verify();
        }
    }
}
