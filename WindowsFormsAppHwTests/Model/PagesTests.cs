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
    }
}
