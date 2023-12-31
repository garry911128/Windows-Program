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
    public class DrawCommandTests
    {
        [TestMethod]
        public void Execute_AddShapeToListCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int indexOfStack = 1;

            var drawCommand = new DrawCommand(modelMock.Object, shapeMock.Object, 0, indexOfStack);

            // Act
            drawCommand.DoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.AddShapeToList(shapeMock.Object, 0, indexOfStack), Times.Once);
        }

        [TestMethod]
        public void UnExecute_DeleteShapeOfStackCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int indexOfStack = 1;

            var drawCommand = new DrawCommand(modelMock.Object, shapeMock.Object, 0, indexOfStack);

            // Act
            drawCommand.UndoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.DeleteShapeOfStack(0, indexOfStack), Times.Once);
        }
    }
}