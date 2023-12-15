using WindowsFormsAppHomework.PresentationModel;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Forms;
using WindowsFormsAppHomework;
using System;

namespace WindowsFormsAppHomework.PresentationModel.Tests
{
    [TestClass]
    public class PresentationModelTests
    {
        // Mocks
        private Mock<Model> modelMock;
        PrivateObject _presentationModelPrivate;
        private PresentationModel _presentationModel;
        int eventCalled;
        // set up
        [TestInitialize]
        public void SetUp()
        {
            modelMock = new Mock<Model>();
            _presentationModel = new PresentationModel(modelMock.Object);
            _presentationModelPrivate = new PrivateObject(_presentationModel);
            eventCalled = 0;
            _presentationModel._cursorChanged += (Cursors) => eventCalled += 1;
        }

        // set mode test
        [TestMethod]
        public void SetMode_ShouldSetStateAndNotifyObserver()
        {
            SetUp();
            var shapeType = Constants.POINTER_MODE_TYPE;
            _presentationModel.SetMode(shapeType);
            modelMock.Verify(m => m.SetState(It.IsAny<PointerState>()), Times.Once);
            Assert.AreEqual(Constants.POINTER_MODE_TYPE, _presentationModelPrivate.GetField("_hintShapeType")); // Corrected variable name
            shapeType = Constants.RECTANGLE;
            _presentationModel.SetMode(shapeType);
            modelMock.Verify(m => m.SetState(It.IsAny<PointerState>()), Times.Once);
            Assert.AreEqual(Constants.RECTANGLE, _presentationModelPrivate.GetField("_hintShapeType")); // Corrected variable name
        }

        // Press Mouse
        [TestMethod]
        public void PressedPointerTest()
        {
            SetUp();
            var point = new Point(1, 1);
            _presentationModel.PressedPointer(point.X, point.Y);
            modelMock.Verify(m => m.PressedMouse(It.IsAny<string>(), point, true), Times.Once);
        }

        [TestMethod]
        public void PressedPointer_ShouldSetStateWhenPointInHandle()
        {
            SetUp();
            var mouseX = 10.0;
            var mouseY = 20.0;
            modelMock.Setup(m => m.IsPointInSelectedShapeHandle(It.IsAny<Point>())).Returns(true);
            _presentationModel.PressedPointer(mouseX, mouseY);
            modelMock.Verify(m => m.IsPointInSelectedShapeHandle(new Point(mouseX, mouseY)), Times.Once);
            modelMock.Verify(m => m.SetState(It.IsAny<ResizeState>()), Times.Once);
        }

        [TestMethod]
        public void PressedPointer_ShouldNotSetStateWhenPointNotInHandle()
        {
            SetUp();
            var mouseX = 10.0;
            var mouseY = 20.0;
            modelMock.Setup(m => m.IsPointInSelectedShapeHandle(It.IsAny<Point>())).Returns(false);
            _presentationModel.PressedPointer(mouseX, mouseY);
            modelMock.Verify(m => m.IsPointInSelectedShapeHandle(new Point(mouseX, mouseY)), Times.Once);
            modelMock.Verify(m => m.SetState(It.IsAny<ResizeState>()), Times.Never);
        }

        // Test Moved Mouse
        [TestMethod]
        public void MovedPointerTest()
        {
            SetUp();
            var point = new Point(1, 1);
            _presentationModelPrivate.SetField("_isPressed", true);
            _presentationModel.MovedPointer(point.X, point.Y);
            modelMock.Verify(m => m.MovedMouse(It.IsAny<Point>(), point), Times.Once);
        }

        [TestMethod]
        public void MovedPointer_ShouldCallModelMethodsWhenPressed()
        {
            SetUp();
            var mouseX = 10.0;
            var mouseY = 20.0;
            modelMock.Setup(m => m.IsPointInSelectedShapeHandle(It.IsAny<Point>())).Returns(true);
            _presentationModel.MovedPointer(mouseX, mouseY);
            modelMock.Verify(m => m.IsPointInSelectedShapeHandle(new Point(mouseX, mouseY)), Times.Once);

        }

        // Test Release Mouse
        [TestMethod]
        public void ReleasePointerTest()
        {
            SetUp();
            bool isCalled = false;
            _presentationModel._cursorChanged += (cursor) => { isCalled = true; };
            var point = new Point(1, 1);
            _presentationModelPrivate.SetField("_isPressed", true);
            _presentationModel.ReleasedPointer(point.X, point.Y);
            modelMock.Verify(m => m.ReleaseMouse(It.IsAny<Point>(), point, false), Times.Once);
            Assert.IsFalse((bool)_presentationModelPrivate.GetField("_isPressed"));
            Assert.AreEqual("", _presentationModelPrivate.GetField("_hintShapeType"));
            modelMock.Verify(m => m.SetState(It.IsAny<PointerState>()), Times.Once);
            _presentationModel.ReleasedPointer(point.X, point.Y);
            Assert.IsFalse((bool)_presentationModelPrivate.GetField("_isPressed"));
        }

        // Test On Keydown
        [TestMethod]
        public void ProcessKeyDownTest()
        {
            SetUp();
            var keyCode = Keys.Delete;
            _presentationModel.ProcessKeyDown(keyCode);
            modelMock.Verify(m => m.HandleKeyDown(keyCode), Times.Once);
            keyCode = Keys.Enter;
            _presentationModel.ProcessKeyDown(keyCode);
            modelMock.Verify(m => m.HandleKeyDown(keyCode), Times.Once);
        }

        // Test Draw
        [TestMethod]
        public void Presentation_DrawTest()
        {
            SetUp();
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(100, 100));
            _presentationModel.Draw(graphics);
            WindowsFormsGraphicsAdaptor _graphic = _presentationModelPrivate.GetField("_graphics") as WindowsFormsGraphicsAdaptor;
            modelMock.Verify(m => m.Draw(_graphic), Times.Once);
        }

        // Draw on Button(small Screen)
        [TestMethod]
        public void DrawOnButtonTest()
        {
            SetUp();
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(100, 100));
            Size buttonSize = new Size(50, 50);
            Size canvasSize = new Size(100, 100);
            _presentationModel.DrawOnButton(graphics, buttonSize, canvasSize);
            var privatePresentationModel = new PrivateObject(_presentationModel);
            var graphicsAdaptor = privatePresentationModel.GetField("_graphics") as WindowsFormsGraphicsAdaptor;
            Assert.IsNotNull(graphicsAdaptor, "_graphics should not be null");
        }

        // TestButton Checked
        [TestMethod]
        [DataRow(Constants.ELLIPSE, true, false, false, false)]
        [DataRow(Constants.POINTER_MODE_TYPE, false, false, false, true)]
        [DataRow(Constants.RECTANGLE, false, false, true, false)]
        [DataRow(Constants.LINE, false, true, false, false)]
        // Add more DataRow attributes as needed for other scenarios
        public void ButtonCheckedPropertiesTest(string mode, bool expectedEllipse, bool expectedLine, bool expectedRectangle, bool expectedCursor)
        {
            SetUp();
            _presentationModel.SetMode(mode);
            Assert.AreEqual(expectedEllipse, _presentationModel.IsEllipseButtonChecked, "IsEllipseButtonChecked does not match expected value");
            Assert.AreEqual(expectedLine, _presentationModel.IsLineButtonChecked, "IsLineButtonChecked does not match expected value");
            Assert.AreEqual(expectedRectangle, _presentationModel.IsRectangleButtonChecked, "IsRectangleButtonChecked does not match expected value");
            Assert.AreEqual(expectedCursor, _presentationModel.IsCursorButtonChecked, "IsCursorButtonChecked does not match expected value");
        }

        // Test Notify Observer
        [TestMethod]
        public void NotifyObserverTest()
        {
            SetUp();
            bool presentationModelChangedNotified = false;
            bool propertyChangedNotified = false;
            _presentationModel._presentationModelChanged += () => presentationModelChangedNotified = true;
            _presentationModel.PropertyChanged += (sender, args) =>
            {
                propertyChangedNotified = true;
                switch (args.PropertyName)
                {
                    case Constants.IS_LINE_CHECKED:
                        // Add assertions or verifications for IS_LINE_CHECKED
                        break;
                    case Constants.IS_CIRCLE_CHECKED:
                        // Add assertions or verifications for IS_CIRCLE_CHECKED
                        break;
                    case Constants.IS_RECTANGLE_CHECKED:
                        // Add assertions or verifications for IS_RECTANGLE_CHECKED
                        break;
                    case Constants.IS_CURSOR_CHECKED:
                        // Add assertions or verifications for IS_CURSOR_CHECKED
                        break;
                }
            };
            _presentationModel.NotifyObserver();
            Assert.IsTrue(presentationModelChangedNotified, "PresentationModelChanged event should be notified");
            Assert.IsTrue(propertyChangedNotified, "PropertyChanged event should be notified");
        }

    }
}
