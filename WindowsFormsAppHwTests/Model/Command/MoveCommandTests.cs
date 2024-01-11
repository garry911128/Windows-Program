using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WindowsFormsAppHomework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass()]
    public class MoveCommandTests
    {
        [TestMethod]
        public void Execute_ShapeMoveCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            var moveCommand = new MoveCommand(modelMock.Object, shapeMock.Object, 0);
            moveCommand.DoExecute(It.IsAny<Size>());
            shapeMock.Verify(s => s.Move(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
        }

        [TestMethod]
        public void UnExecute_ShapeMoveBackCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            var moveCommand = new MoveCommand(modelMock.Object, shapeMock.Object, 0);

            // Act
            moveCommand.UndoExecute(It.IsAny<Size>());

            // Assert
            shapeMock.Verify(s => s.Move(It.IsAny<double>(), It.IsAny<double>()), Times.Once); // Verify with the initial delta (0, 0)
        }

        [TestMethod]
        public void Setdelta_CorrectDeltaValues()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            var moveCommand = new MoveCommand(modelMock.Object, shapeMock.Object, 0);
            PrivateObject privateMoveCommand = new PrivateObject(moveCommand);
            moveCommand.SetDelta(new Point(10, 20), new Point(30, 40));
            var deltaField = (Point)privateMoveCommand.GetField("_delta");
            Assert.AreEqual(20, deltaField.X);
            Assert.AreEqual(20, deltaField.Y);
        }

        [TestMethod]
        public void AdjustNowSize_ShouldSetPanelSizeAndModifyCanvasSize()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            var moveCommand = new MoveCommand(modelMock.Object, shapeMock.Object, 0);
            var originalCanvasSize = new Size(100, 100);
            var newCanvasSize = new Size(200, 200);

            // Act
            moveCommand.SetSize(originalCanvasSize);
            moveCommand.AdjustNowSize(newCanvasSize);

            // Assert
            shapeMock.Verify(s => s.SetPanelSize(newCanvasSize, originalCanvasSize), Times.Once);
            PrivateObject privateMoveCommand = new PrivateObject(moveCommand);
            Size canvasSize = (Size)privateMoveCommand.GetField("_canvasSize");
            Assert.AreEqual(newCanvasSize, canvasSize);
        }
    }
}