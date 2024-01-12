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
    [TestClass()]
    public class DeleteCommandTests
    {
        [TestMethod]
        public void Execute_DeleteShapeOfStackCalledWithCorrectParameters()
        {
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int indexOfStack = 1;
            modelMock.Setup(m => m.DeleteShapeOfStack(0, indexOfStack));
            var deleteCommand = new DeleteCommand(modelMock.Object, shapeMock.Object, 0, indexOfStack);
            deleteCommand.DoExecute(It.IsAny<Size>());
            modelMock.Verify(m => m.DeleteShapeOfStack(0, indexOfStack), Times.Once);
        }

        [TestMethod]
        public void UnExecute_AddShapeToListCalledWithCorrectParameters()
        {
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int indexOfStack = 1;
            modelMock.Setup(m => m.AddShapeToList(shapeMock.Object, 0, indexOfStack));
            var deleteCommand = new DeleteCommand(modelMock.Object, shapeMock.Object, 0, indexOfStack);
            deleteCommand.UndoExecute(It.IsAny<Size>());
            modelMock.Verify(m => m.AddShapeToList(shapeMock.Object, 0, indexOfStack), Times.Once);
        }

        [TestMethod]
        public void GetSlideIndex_ShouldReturnCorrectSlideIndex()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int slideIndex = 2;
            int indexOfStack = 1;
            var drawCommand = new DeleteCommand(modelMock.Object, shapeMock.Object, slideIndex, indexOfStack);
            int result = drawCommand.GetSlideIndex();
            Assert.AreEqual(slideIndex, result, "GetSlideIndex should return the correct slide index");
        }
    }
}