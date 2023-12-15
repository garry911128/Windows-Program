using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsAppHomework.PresentationModel;
using System;
using Moq;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework.PresentationModel.Tests
{
    [TestClass]
    public class WindowsFormsGraphicsAdaptorTests
    {
        private WindowsFormsGraphicsAdaptor _graphicsAdaptor;

        // Init
        [TestInitialize]
        public void SetUp()
        {
            // 在每個測試方法之前初始化 _graphicsAdaptor
            _graphicsAdaptor = new WindowsFormsGraphicsAdaptor(Graphics.FromImage(new Bitmap(100, 100)));
        }

        // Test Construct
        [TestMethod]
        public void WindowsFormsGraphicsAdaptor_ConstructorTest()
        {
            // Arrange & Act - 在 SetUp 中已經初始化 _graphicsAdaptor
            // Assert - 檢查 _graphicsAdaptor 是否為非空物件
            Assert.IsNotNull(_graphicsAdaptor);
        }

        // Test Clear All
        [TestMethod]
        public void WindowsFormsGraphicsAdaptor_ClearAllTest()
        {
            // Arrange & Act - 在 SetUp 中已經初始化 _graphicsAdaptor
            // 測試 ClearAll 是否成功運行，不易進行具體的檢查
            _graphicsAdaptor.ClearAll();
        }

        // Line Test
        [TestMethod]
        public void WindowsFormsGraphicsAdaptor_DrawLineTest()
        {
            // Arrange - 在 SetUp 中已經初始化 _graphicsAdaptor
            double startX = 0, startY = 0, endX = 10, endY = 10;

            // Act - 測試 DrawLine 是否成功運行，不易進行具體的檢查
            _graphicsAdaptor.DrawLine(startX, startY, endX, endY);
        }

        // Line Test
        [TestMethod]
        public void WindowsFormsGraphicsAdaptor_DrawRectangleTest()
        {
            // Arrange - 在 SetUp 中已經初始化 _graphicsAdaptor
            double topLeftX = 0, topLeftY = 0, width = 10, height = 10;

            // Act - 測試 DrawRectangle 是否成功運行，不易進行具體的檢查
            _graphicsAdaptor.DrawRectangle(topLeftX, topLeftY, width, height);
        }

        // Ellipse Test
        [TestMethod]
        public void WindowsFormsGraphicsAdaptor_DrawEllipseTest()
        {
            // Arrange - 在 SetUp 中已經初始化 _graphicsAdaptor
            double topLeftX = 0, topLeftY = 0, width = 10, height = 10;

            // Act - 測試 DrawEllipse 是否成功運行，不易進行具體的檢查
            _graphicsAdaptor.DrawEllipse(topLeftX, topLeftY, width, height);
        }

        // Test Handle
        [TestMethod]
        public void WindowsFormsGraphicsAdaptor_DrawHandleTest()
        {
            // Arrange - 在 SetUp 中已經初始化 _graphicsAdaptor
            double centerX = 0, centerY = 0;

            // Act - 測試 DrawHandle 是否成功運行，不易進行具體的檢查
            _graphicsAdaptor.DrawHandle(centerX, centerY);
        }
    }
}