using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class AddPageCommandTests
    {
        [TestMethod]
        public void Execute_InsertPageCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapesMock = new Mock<Shapes>();
            int slideIndex = 1;

            var addPageCommand = new AddPageCommand(modelMock.Object, shapesMock.Object, slideIndex);

            // Act
            addPageCommand.DoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.InsertPage(slideIndex, shapesMock.Object), Times.Once);
        }

        [TestMethod]
        public void UnExecute_DeletePageCalledWithCorrectParameters()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapesMock = new Mock<Shapes>();
            int slideIndex = 1;

            var addPageCommand = new AddPageCommand(modelMock.Object, shapesMock.Object, slideIndex);

            // Act
            addPageCommand.UndoExecute(It.IsAny<Size>());

            // Assert
            modelMock.Verify(m => m.DeletePage(slideIndex, shapesMock.Object), Times.Once);
        }

        [TestMethod]
        public void GetSlideIndex_ShouldReturnCorrectSlideIndex()
        {
            // Arrange
            var modelMock = new Mock<Model>();
            var shapesMock = new Mock<Shapes>();
            int slideIndex = 2;
            var addPageCommand = new AddPageCommand(modelMock.Object, shapesMock.Object, slideIndex);
            int result = addPageCommand.GetSlideIndex();
            Assert.AreEqual(slideIndex, result, "GetSlideIndex should return the correct slide index");
        }
    }
}
