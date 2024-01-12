using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WindowsFormsAppHomework;
using System;
using System.ComponentModel;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class ShapesTests
    {
        private Mock<Shape> _shape;
        PrivateObject _privateShapes;
        private Shapes _shapes;

        // set up
        [TestInitialize]
        public void SetUp()
        {
            _shape = new Mock<Shape>();
            _shapes = new Shapes();
            _privateShapes = new PrivateObject(_shapes);
        }

        // Add Shape to list
        [TestMethod]
        public void IncreaseShapeToList_ShouldAddShapeToList()
        {
            var shape = new Mock<Shape>();
            var shapes = new Shapes();
            shapes.IncreaseShapeToList(shape.Object, -1);
            Assert.AreEqual(1, shapes.GetShapeListSize());
        }

        [TestMethod]
        public void IncreaseShapeToList_WhenIndexIsValid_InsertsShapeIntoList()
        {
            var shapes = new Shapes();
            var shapeMock = new Mock<Shape>();
            PrivateObject privateShapes = new PrivateObject(shapes);
            shapes.IncreaseShapeToList(shapeMock.Object, -1);
            int index = 0;
            shapes.IncreaseShapeToList(shapeMock.Object, index);
            Assert.AreEqual(2, shapes.GetShapeList.Count); // Accessing the actual state of the shapes object
        }

        [TestMethod]
        public void IncreaseShapeToList_InvalidIndex_ThrowsIndexOutOfRangeException()
        {
            SetUp();
            Assert.ThrowsException<IndexOutOfRangeException>(() => _shapes.IncreaseShapeToList(new Shape(), 10));
        }

        // delete shape in stack use index
        [TestMethod]
        public void DeleteShape_ShouldRemoveShapeFromList()
        {
            var shape = new Mock<Shape>();
            var shapes = new Shapes();
            shapes.IncreaseShapeToList(shape.Object, -1);
            shapes.DeleteShape(0);
            Assert.AreEqual(0, shapes.GetShapeListSize());
        }

        // delete shape in stack use index
        [TestMethod]
        public void DeleteShape_ShouldRemoveSelectedShape()
        {
            SetUp();
            var shape = new Mock<Shape>();
            _shapes.IncreaseShapeToList(shape.Object, -1);
            _privateShapes.SetField("_selectedShape", _shapes.GetShapeList[0]);
            _shapes.DeleteShape();
            Assert.AreEqual(0, _shapes.GetShapeListSize());
        }

        // get shape type
        [TestMethod]
        public void GetShapeTypeName_ShouldReturnShapeName()
        {
            var shape = new Mock<Shape>();
            shape.Setup(s => s.GetShapeName()).Returns("MockShape");
            var shapes = new Shapes();
            shapes.IncreaseShapeToList(shape.Object, -1);
            var result = shapes.GetShapeTypeName(0);
            Assert.AreEqual("MockShape", result);
        }

        // get shape location
        [TestMethod]
        public void GetShapeLocation_ShouldReturnShapeLocation()
        {
            // Arrange
            var shape = new Mock<Shape>();
            shape.Setup(s => s.GetInfo()).Returns("MockShapeLocation");
            var shapes = new Shapes();
            shapes.IncreaseShapeToList(shape.Object, -1);
            var result = shapes.GetShapeLocation(0);
            Assert.AreEqual("MockShapeLocation", result);
        }

        [TestMethod]
        public void IsPointInSelectedShapeHandle_ShouldReturnTrueWhenInsideHandle()
        {
            var selectedShape = new Mock<Shape>();
            selectedShape.Setup(s => s.IsPointInsideHandle(It.IsAny<Point>())).Returns(true);
            selectedShape.Setup(s => s.IsPointInsideShape(It.IsAny<Point>())).Returns(true);
            var shapes = new Shapes();
            shapes.IncreaseShapeToList(selectedShape.Object, -1);
            shapes.SelectedShapeByPoint(new Point(1, 1)); // Set _selectedShape
            var result = shapes.IsPointInSelectedShapeHandle(new Point(2, 2));
            Assert.IsTrue(result);
        }

        // return false
        [TestMethod]
        public void IsPointInSelectedShapeHandle_ShouldReturnFalseWhenNotInsideHandle()
        {
            var selectedShape = new Mock<Shape>();
            selectedShape.Setup(s => s.IsPointInsideHandle(It.IsAny<Point>())).Returns(false);
            var shapes = new Shapes();
            shapes.IncreaseShapeToList(selectedShape.Object, -1);
            shapes.SelectedShapeByPoint(new Point(1, 1)); // Set _selectedShape
            var result = shapes.IsPointInSelectedShapeHandle(new Point(2, 2));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPointInSelectedShapeHandle_ShouldReturnFalseWhenSelectedShapeIsNull()
        {
            var result = _shapes.IsPointInSelectedShapeHandle(new Point(2, 2));
            Assert.IsFalse(result);
            var mockShape = new Mock<Shape>();
            Point mousePoint = new Point(20, 20);
            _privateShapes.SetField("_selectedShape", mockShape.Object);
            mockShape.Setup(s => s.IsPointInsideHandle(mousePoint)).Returns(false);
            _shapes.IsPointInSelectedShapeHandle(mousePoint);
            mockShape.Verify(s => s.IsPointInsideHandle(mousePoint), Times.Once);
        }

        [TestMethod]
        public void MovedSelectedShapeByMouse_ShouldCallMoveMethod()
        {
            // Arrange
            var mockShape = new Mock<Shape>();
            double deltaX = 10;
            double deltaY = 1;
            mockShape.Setup(s => s.Move(deltaX, deltaY));
            _privateShapes.SetField("_selectedShape", mockShape.Object);
            _shapes.MovedSelectedShapeByMouse(10, 1);
            mockShape.Verify(s => s.Move(10, 1), Times.Once);
        }

        // Test have selected but not in shapelist
        [TestMethod]
        public void SelectedShapeByPoint_ShouldDoNothing()
        {
            // Arrange
            var mockShape = new Mock<Shape>();
            Point mousePoint = new Point(5, 5);
            mockShape.Setup(s => s.IsPointInsideShape(mousePoint)).Returns(true);
            _privateShapes.SetField("_selectedShape", mockShape.Object);
            _shapes.SelectedShapeByPoint(mousePoint);
            mockShape.Verify(s => s.IsPointInsideShape(mousePoint), Times.Never);
        }

        [TestMethod]
        public void TestGetSelectedShape()
        {
            // Arrange
            var shapes = new Shapes();
            var shape1 = new Mock<Shape>();
            var shape2 = new Mock<Shape>();

            // Add shapes to the list
            shapes.IncreaseShapeToList(shape1.Object, -1);
            shapes.IncreaseShapeToList(shape2.Object, -1);

            // Simulate selecting shape2
            shape2.Setup(s => s.IsPointInsideShape(It.IsAny<Point>())).Returns(true);
            shapes.SelectedShapeByPoint(new Point(0, 0));

            // Act
            var selectedShape = shapes.GetSelectedShape();

            // Assert
            Assert.AreEqual(shape2.Object, selectedShape);
        }

        [TestMethod]
        public void SetPanelSize_ShouldCallSetPanelSizeForEachShape()
        {
            // Arrange
            var shape1Mock = new Mock<Shape>();
            var shape2Mock = new Mock<Shape>();
            var shape3Mock = new Mock<Shape>();

            var shapes = new Shapes();
            shapes.IncreaseShapeToList(shape1Mock.Object, -1);
            shapes.IncreaseShapeToList(shape2Mock.Object, -1);
            shapes.IncreaseShapeToList(shape3Mock.Object, -1);

            var newCanvasSize = new Size(200, 300);
            var oldSize = new Size(100, 150);

            // Act
            shapes.SetPanelSize(newCanvasSize, oldSize);

            // Assert
            shape1Mock.Verify(s => s.SetPanelSize(newCanvasSize, oldSize), Times.Once);
            shape2Mock.Verify(s => s.SetPanelSize(newCanvasSize, oldSize), Times.Once);
            shape3Mock.Verify(s => s.SetPanelSize(newCanvasSize, oldSize), Times.Once);
        }

        [TestMethod]
        public void ClearSelectedShape_ShouldSetSelectedShapeToNull()
        {
            // Arrange
            var shapes = new Shapes();

            // Set a mock selected shape to simulate an existing selected shape
            var mockSelectedShape = new Mock<Shape>();
            shapes.GetType().GetField("_selectedShape", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(shapes, mockSelectedShape.Object);

            // Act
            shapes.ClearSelectedShape();

            // Assert
            var selectedShape = shapes.GetType().GetField("_selectedShape", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(shapes);
            Assert.IsNull(selectedShape, "SelectedShape should be null after ClearSelectedShape");
        }

        [TestMethod]
        public void Clear_ShouldClearShapeList()
        {
            // Arrange
            var shapes = new Shapes();

            // Add some mock shapes to the shape list
            var mockShape1 = new Mock<Shape>();
            var mockShape2 = new Mock<Shape>();
            shapes.GetType().GetField("_shapeList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(shapes, new System.ComponentModel.BindingList<Shape> { mockShape1.Object, mockShape2.Object });

            // Act
            shapes.Clear();

            // Assert
            var shapeList = (System.ComponentModel.BindingList<Shape>)shapes.GetType().GetField("_shapeList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(shapes);
            Assert.AreEqual(0, shapeList.Count, "ShapeList should be empty after Clear");
        }

        [TestMethod]
        public void ConvertToFile_ShouldReturnFormattedString()
        {
            var shapes = new Shapes();
            var mockShape1 = new Mock<Shape>();
            var mockShape2 = new Mock<Shape>();
            shapes.GetType().GetField("_shapeList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(shapes, new System.ComponentModel.BindingList<Shape> { mockShape1.Object, mockShape2.Object });
            mockShape1.Setup(s => s.GetConvert(It.IsAny<Size>())).Returns("MockShape1Convert");
            mockShape2.Setup(s => s.GetConvert(It.IsAny<Size>())).Returns("MockShape2Convert");
            Size newCanvasSize = new Size(100, 200);
            var result = shapes.ConvertToFile(newCanvasSize);
            var expected = "{MockShape1Convert}, {MockShape2Convert}";
            Assert.AreEqual(expected, result, "ConvertToFile should return the expected formatted string");
        }

        [TestMethod]
        public void LoadShapes_ShouldAddShapesToList()
        {
            // Arrange
            var shapes = new Shapes();

            // Create another instance of Shapes with mock shapes
            var mockShape1 = new Mock<Shape>();
            var mockShape2 = new Mock<Shape>();
            var shapesToLoad = new Shapes();
            shapesToLoad.GetType().GetField("_shapeList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(shapesToLoad, new System.ComponentModel.BindingList<Shape> { mockShape1.Object, mockShape2.Object });

            // Act
            shapes.LoadShapes(shapesToLoad);

            // Assert
            var shapeList = (System.ComponentModel.BindingList<Shape>)shapes.GetType().GetField("_shapeList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(shapes);
            Assert.AreEqual(2, shapeList.Count, "LoadShapes should add shapes to the list");
        }
    }
}
