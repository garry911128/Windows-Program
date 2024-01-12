using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class DeletePageCommandTests
    {
        [TestMethod]
        public void Execute_DeletePageCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapesMock = new Mock<Shapes>();
            int slideIndex = 1;

            var deletePageCommand = new DeletePageCommand(modelMock.Object, shapesMock.Object, slideIndex);

            // Act
            deletePageCommand.DoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.DeletePage(slideIndex, shapesMock.Object), Times.Once);
        }

        [TestMethod]
        public void UnExecute_InsertPageCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapesMock = new Mock<Shapes>();
            int slideIndex = 1;

            var deletePageCommand = new DeletePageCommand(modelMock.Object, shapesMock.Object, slideIndex);

            // Act
            deletePageCommand.UndoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.InsertPage(slideIndex, shapesMock.Object), Times.Once);
        }

        [TestMethod]
        public void GetSlideIndex_ShouldReturnCorrectSlideIndex()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapesMock = new Mock<Shapes>();
            int slideIndex = 2;
            var deletePageCommand = new DeletePageCommand(modelMock.Object, shapesMock.Object, slideIndex);
            int result = deletePageCommand.GetSlideIndex();
            Assert.AreEqual(slideIndex, result, "GetSlideIndex should return the correct slide index");
        }
    }
}
