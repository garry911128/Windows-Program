using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsAppHomework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class AddCommandTests
    {
        [TestMethod]
        public void Execute_AddShapeToListCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int indexOfStack = 1;

            var addCommand = new AddCommand(modelMock.Object, shapeMock.Object, indexOfStack);

            // Act
            addCommand.DoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.AddShapeToList(shapeMock.Object, indexOfStack), Times.Once);
        }

        [TestMethod]
        public void UnExecute_DeleteShapeOfStackCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int indexOfStack = 1;

            var addCommand = new AddCommand(modelMock.Object, shapeMock.Object, indexOfStack);

            // Act
            addCommand.UndoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.DeleteShapeOfStack(indexOfStack), Times.Once);
        }
    }
}