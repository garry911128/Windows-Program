using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsAppHomework;
using System.Windows.Forms;
using System;
using System.Drawing;
using Moq;
using System.ComponentModel;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class ModelTests
    {
        PrivateObject _privateModel;
        private Model model;
        private MockState mockState;
        private int eventCalled;
        private Mock<CommandManager> commandManagerMock;

        // Test Setup
        [TestInitialize]
        public void Setup()
        {
            model = new Model();
            _privateModel = new PrivateObject(model);
            mockState = new MockState();
            commandManagerMock = new Mock<CommandManager>();
            eventCalled = 0;
            model._modelChanged += () => eventCalled += 1;
            model.SetState(mockState);
            commandManagerMock.Setup(cm => cm.Execute(It.IsAny<ICommand>(), It.IsAny<Size>()));
            _privateModel.SetField("_commandManager", commandManagerMock.Object);
            var actualCommandManager = _privateModel.GetField("_commandManager");
            Assert.AreEqual(commandManagerMock.Object, actualCommandManager);
        }


        [TestMethod]
        public void TestShowHintProperty()
        {
            Setup();
            const bool expectedValue = true;
            model.ShowHint = expectedValue;
            var actualValue = model.ShowHint;
            Assert.AreEqual(expectedValue, actualValue);
        }

        // Test State
        [TestMethod]
        public void SetState_CallsCorrectMethodsOnState()
        {
            TestStateMethodCall(() => model.PressedMouse(Constants.RECTANGLE, new Point(0, 0), true), true, false, false);
            TestStateMethodCall(() => model.MovedMouse(new Point(0, 0), new Point(10, 10)), false, true, false);
            TestStateMethodCall(() => model.ReleaseMouse(new Point(0, 0), new Point(10, 10), false), false, false, true);
        }

        // Test Notiobser
        [TestMethod]
        public void NotifyObserver_CallsModelChangedEvent()
        {
            model.NotifyObserver();
            Assert.AreEqual(1, eventCalled);
        }

        // Test Add Random Shape to List
        //[TestMethod]
        //public void TestAdd_Random_Shape_To_List()
        //{
        //    Setup();
        //    var shapeFactoryMock = new Mock<ShapeFactory>();
        //    var shapesMock = new Mock<Shapes>();
        //    var mockShape = new Mock<Shape>();
        //    var shapeList = new BindingList<Shape> { mockShape.Object };
        //    shapesMock.Setup(s => s.GetShapeList).Returns(shapeList);
        //    _privateModel.SetField("_shapeFactory", shapeFactoryMock.Object);
        //    _privateModel.SetField("_shapes", shapesMock.Object);
        //    shapeFactoryMock.Setup(sf => sf.CreateShapeRandom(Constants.RECTANGLE)).Returns(mockShape.Object);
        //    shapesMock.Setup(s => s.GetShapeListSize()).Returns(1);
        //    model.AddShapeListRandom(Constants.RECTANGLE);
        //    var finalShapeCount = model.GetShapeList().Count;
        //    Assert.AreEqual(1, finalShapeCount);
        //    Assert.AreEqual(1, eventCalled);
        //    commandManagerMock.Verify(cm => cm.Execute(It.IsAny<AddCommand>(), It.IsAny<Size>()), Times.Once);
        //}

        // Test State
        private void TestStateMethodCall(Action action, bool pressedMouseCalled, bool movedMouseCalled, bool releaseMouseCalled)
        {
            action.Invoke();
            Assert.AreEqual(pressedMouseCalled, mockState.PressedMouseCalled);
            Assert.AreEqual(movedMouseCalled, mockState.MovedMouseCalled);
            Assert.AreEqual(releaseMouseCalled, mockState.ReleaseMouseCalled);
            mockState.ResetCalls();
        }

        // Test OnKeyDown - Delete key pressed
        [TestMethod]
        public void TestOnKeyDown_DeleteKeyPressed()
        {
            Setup();
            // 1 testcase
            var keyCode = Keys.A;
            model.HandleKeyDown(keyCode);
            commandManagerMock.Verify(cm => cm.Execute(It.IsAny<ICommand>(), It.IsAny<Size>()), Times.Never);
            Assert.AreEqual(0, eventCalled);
        }

        // Test OnKeyDown - Delete key pressed
        [TestMethod]
        public void TestOnKeyDown_DeleteKeyPressedShouldDeleteSelectedShape()
        {
            Setup();
            var keyCode = Keys.Delete;
            var selectedShapeMock = new Mock<Shape>();
            selectedShapeMock.Setup(s => s.GetPoint(0));
            var shapesMock = new Mock<Shapes>();
            var shapeList = new BindingList<Shape> { selectedShapeMock.Object };
            shapesMock.Setup(s => s.GetShapeList).Returns(shapeList);
            shapesMock.Setup(s => s.GetSelectedShape()).Returns(selectedShapeMock.Object);
            _privateModel.SetField("_shapes", shapesMock.Object);
            model.HandleKeyDown(keyCode);
            commandManagerMock.Verify(cm => cm.Execute(It.IsAny<ICommand>(), It.IsAny<Size>()), Times.Once);
            Assert.AreEqual(0, shapesMock.Object.GetShapeList.IndexOf(selectedShapeMock.Object));
            shapesMock.Verify(s => s.SelectedShapeByPoint(It.IsAny<Point>()), Times.Never); // Ensure SelectShapeByPoint is not called in this case
            Assert.AreEqual(2, eventCalled);
        }

        // Test GetResizeBasePoint
        [TestMethod]
        public void Test_GetSelectedShape()
        {
            Setup();
            var shapesMock = new Mock<Shapes>();
            var selectedShape = new Mock<Shape>();
            _privateModel.SetField("_shapes", shapesMock.Object);
            shapesMock.Setup(s => s.GetSelectedShape()).Returns(selectedShape.Object);
            Assert.AreEqual(selectedShape.Object, model.GetSelectedShape());
        }

        // Test DeleteShapeOfStack - Notify observer when shape is deleted
        [TestMethod]
        public void TestDeleteShapeOfStack_NotifyObserver()
        {
            Setup();
            model.AddShapeToList(new Rectangle(0, 0, 10, 10), 0);
            model.DeleteShapeOfStack(0, 0);
            Assert.AreEqual(0, model.GetShapeList().Count);
            Assert.AreEqual(1, eventCalled);
        }

        [TestMethod]
        public void IsPointInSelectedShapeHandle_ShouldCallShapesMethod()
        {
            // Arrange
            var mousePoint = new Point(10, 20);
            var shapesMock = new Mock<Shapes>();
            _privateModel.SetField("_shapes", shapesMock.Object);
            shapesMock.Setup(s => s.IsPointInSelectedShapeHandle(mousePoint)).Returns(true);
            var result = model.IsPointInSelectedShapeHandle(mousePoint);
            Assert.IsTrue(result); // Depending on your actual logic
            shapesMock.Verify(s => s.IsPointInSelectedShapeHandle(mousePoint), Times.Once);
        }

        [TestMethod]
        public void TestCreateHintShape()
        {
            // Arrange
            var shapeFactoryMock = new Mock<ShapeFactory>();

            // Mock Shape
            var hintMock = new Mock<Shape>();

            // Set up the model with mocks
            _privateModel.SetField("_shapeFactory", shapeFactoryMock.Object);

            shapeFactoryMock.Setup(f => f.CreateShape(It.IsAny<string>(), It.IsAny<Point>(), It.IsAny<double>(), It.IsAny<double>()))
                           .Returns(hintMock.Object);

            // Act
            model.CreateHintShape("Rectangle", new Point(0, 0), 100, 50);

            // Assert
            shapeFactoryMock.Verify(f => f.CreateShape("Rectangle", new Point(0, 0), 100, 50), Times.Once);
            Assert.AreEqual(hintMock.Object, _privateModel.GetField("_hint"));
        }

        // Test ResizeHint
        [TestMethod]
        public void Test_ResizeHint()
        {
            Setup();
            var shapeMock = new Mock<Shape>();
            _privateModel.SetField("_hint", shapeMock.Object);
            model.ResizeShape(new Point(0, 0), new Point(25, 25));
            model.NotifyObserver();
            shapeMock.Verify(h => h.Resize(It.IsAny<Point>(), It.IsAny<Point>()), Times.Once);
            Assert.AreEqual(2, eventCalled);
        }

        // Test Resize
        [TestMethod]
        public void Test_ResizeShape()
        {
            Setup();
            Shape shape = new Rectangle(50, 50, 100, 100);
            model.AddShapeToList(shape, 0);
            model.SelectShapeByPoint(new Point(75, 75));
            Point initialMousePoint = new Point(150, 150);
            Point newMousePoint = new Point(175, 175);
            model.ResizeShape(new Point(50, 50), newMousePoint);
            Assert.AreEqual(new Point(175, 175), shape.GetPoint(1));
        }

        [TestMethod]
        public void Test_MoveShape()
        {
            Setup();
            var shapesMock = new Mock<Shapes>();
            _privateModel.SetField("_shapes", shapesMock.Object);
            model.MoveShape(10, 10);
            shapesMock.Verify(s => s.MovedSelectedShapeByMouse(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
        }

        // Test Draw
        [TestMethod]
        public void TestDraw()
        {
            var shapesMock = new Mock<Shapes>();
            var graphicsMock = new Mock<IGraphics>();
            var shape1Mock = new Mock<Shape>();
            var shape2Mock = new Mock<Shape>();
            var hintMock = new Mock<Shape>();
            _privateModel.SetField("_shapes", shapesMock.Object);
            _privateModel.SetField("_hint", hintMock.Object);

            shapesMock.Setup(s => s.GetShapeList).Returns(new BindingList<Shape> { shape1Mock.Object, shape2Mock.Object });
            model.Draw(graphicsMock.Object);
            graphicsMock.Verify(g => g.ClearAll(), Times.Once);
            hintMock.Verify(h => h.Draw(graphicsMock.Object), Times.Once);
            shape1Mock.Verify(s => s.Draw(graphicsMock.Object), Times.Once);
            shape2Mock.Verify(s => s.Draw(graphicsMock.Object), Times.Once);
        }

        [TestMethod]
        public void Undo_ShouldCallUndoOnCommandManagerAndNotifyObserver()
        {
            Setup();
            model.Undo();

            // Assert
            commandManagerMock.Verify(cm => cm.Undo(It.IsAny<Size>()), Times.Once);
            Assert.AreEqual(1, eventCalled);
        }

        [TestMethod]
        public void Redo_ShouldCallRedoOnCommandManagerAndNotifyObserver()
        {
            // Act
            model.Redo();

            // Assert
            commandManagerMock.Verify(cm => cm.Redo(It.IsAny<Size>()), Times.Once);
            Assert.AreEqual(1, eventCalled);
        }

        [TestMethod]
        public void ExecuteDrawCommand_ShouldExecuteDrawCommandAndNotifyObserver()
        {
            var shape = new Mock<Shape>().Object;
            _privateModel.SetField("_hint", shape);
            // Act
            model.ExecuteDrawCommand();

            // Assert
            commandManagerMock.Verify(cm => cm.Execute(It.IsAny<ICommand>(), It.IsAny<Size>()), Times.Once);
            Assert.AreEqual(1, eventCalled);
        }

        [TestMethod]
        public void TestExecuteMoveCommand()
        {
            Setup();
            // Arrange
            var commandManagerMock = new Mock<CommandManager>();
            var selectedShapeMock = new Mock<Shape>();
            var shapesMock = new Mock<Shapes>();
            var moveCommandMock = new Mock<MoveCommand>(model, selectedShapeMock.Object);
            _privateModel.SetField("_commandManager", commandManagerMock.Object);
            shapesMock.Setup(spm => spm.GetSelectedShape()).Returns(selectedShapeMock.Object);
            _privateModel.SetField("_shapes", shapesMock.Object);
            model.ExecuteMoveCommand(new Point(0, 0), new Point(50, 50));
            commandManagerMock.Verify(cm => cm.Execute(It.IsAny<MoveCommand>(), It.IsAny<Size>()), Times.Once);
        }

        [TestMethod]
        public void SetCanvasSize_ShouldSetCanvasSizeAndNotifyObserver()
        {
            Setup();
            var shapesMock = new Mock<Shapes>();
            _privateModel.SetField("_shapes", shapesMock.Object);
            var newSize = new Size(300, 400);
            _privateModel.SetField("_canvasSize", newSize);
            //shapesMock.Setup(sps => sps.SetPanelSize(It.IsAny<Size>(), newSize));
            model.SetCanvasSize(new Size(newSize.Width, newSize.Height));
            shapesMock.Verify(s => s.SetPanelSize(It.IsAny<Size>(), newSize), Times.Once);
            Assert.AreEqual(newSize.Width,((Size)_privateModel.GetField("_canvasSize")).Width);
            Assert.AreEqual(newSize.Height, ((Size)_privateModel.GetField("_canvasSize")).Height);
        }

        [TestMethod]
        public void IsRedoEnabled_ShouldReflectCommandManagerIsRedoEnabled()
        {
            Setup();
            bool isRedoEnabled = model.IsRedoEnabled;
            commandManagerMock.VerifyGet(cm => cm.IsRedoEnabled, Times.Once);
            Assert.AreEqual(commandManagerMock.Object.IsRedoEnabled, isRedoEnabled);
        }

        [TestMethod]
        public void IsUndoEnabled_ShouldReflectCommandManagerIsUndoEnabled()
        {
            Setup();
            // Act
            bool isUndoEnabled = model.IsUndoEnabled;

            // Assert
            commandManagerMock.VerifyGet(cm => cm.IsUndoEnabled, Times.Once);
            Assert.AreEqual(commandManagerMock.Object.IsUndoEnabled, isUndoEnabled);
        }


    }
}
