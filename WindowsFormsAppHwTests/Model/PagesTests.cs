using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class PagesTests
    {
        [TestMethod]
        public void Insert_ShouldAddShapeAtSpecifiedIndex()
        {
            // Arrange
            Pages pages = new Pages();
            var mockShape = new Mock<Shapes>();

            // Act
            pages.Insert(0, mockShape.Object);

            // Assert
            Assert.AreEqual(mockShape.Object, pages[0]);
        }

        [TestMethod]
        public void Remove_ShouldRemoveShapeFromPages()
        {
            // Arrange
            Pages pages = new Pages();
            var mockShape = new Mock<Shapes>();
            pages.Add(mockShape.Object);

            // Act
            pages.Remove(mockShape.Object);

            // Assert
            Assert.AreEqual(0, pages.Count);
        }

        [TestMethod]
        public void Encode_ShouldReturnNonEmptyString()
        {
            // Arrange
            Pages pages = new Pages();
            var mockShape = new Mock<Shapes>();
            pages.Add(mockShape.Object);

            // Act
            string encodedString = pages.Encode(new Size(100, 100));

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(encodedString));
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllShapes()
        {
            // Arrange
            Pages pages = new Pages();
            var mockShape = new Mock<Shapes>();
            pages.Add(mockShape.Object);
            pages.Clear();
            Assert.AreEqual(1, pages.Count);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllButOnePageAndClearTheRemainingPage()
        {
            Pages pages = new Pages();
            var mockShape1 = new Mock<Shapes>();
            var mockShape2 = new Mock<Shapes>();
            pages.Add(mockShape1.Object);
            pages.Add(mockShape2.Object);
            int removedCount = pages.Clear();
            Assert.AreEqual(1, removedCount);
            Assert.AreEqual(1, pages.Count);
            mockShape1.Verify(s => s.Clear(), Times.Once);
        }

        [TestMethod]
        public void IndexerSetter_ShouldSetShapeAtIndex()
        {
            Pages pages = new Pages();
            var mockShape1 = new Mock<Shapes>();
            var mockShape2 = new Mock<Shapes>();
            pages.Insert(0, mockShape1.Object);
            pages.Insert(1, mockShape2.Object);
            pages[1] = mockShape1.Object;
            Assert.AreEqual(mockShape1.Object, pages[0]);
            Assert.AreEqual(mockShape1.Object, pages[1]);
            
        }
    }
}
