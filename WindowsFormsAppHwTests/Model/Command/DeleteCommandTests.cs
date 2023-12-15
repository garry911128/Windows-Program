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
            modelMock.Setup(m => m.DeleteShapeOfStack(indexOfStack));
            var deleteCommand = new DeleteCommand(modelMock.Object, shapeMock.Object, indexOfStack);
            deleteCommand.DoExecute(It.IsAny<Size>());
            modelMock.Verify(m => m.DeleteShapeOfStack(indexOfStack), Times.Once);
        }

        [TestMethod]
        public void UnExecute_AddShapeToListCalledWithCorrectParameters()
        {
            var modelMock = new Mock<Model>();
            var shapeMock = new Mock<Shape>();
            int indexOfStack = 1;
            modelMock.Setup(m => m.AddShapeToList(shapeMock.Object, indexOfStack));
            var deleteCommand = new DeleteCommand(modelMock.Object, shapeMock.Object, indexOfStack);
            deleteCommand.UndoExecute(It.IsAny<Size>());
            modelMock.Verify(m => m.AddShapeToList(shapeMock.Object, indexOfStack), Times.Once);
        }
    }
}