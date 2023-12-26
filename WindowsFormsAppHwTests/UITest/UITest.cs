using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Windows;
using System.Globalization;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass]
    public class UITest
    {
        public string targetAppPath;
        public const string MENU_FORM = "Form1";
        public Robot _robot;
        private WindowsElement _canvas;

        // init
        [TestInitialize()]
        public void Initialize()
        {
            var projectName = "WindowsFormsAppHw";
            string solutionPath = Path.GetFullPath("..\\..\\..\\");
            var targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", "WindowsFormsAppHomework.exe");
            _robot = new Robot(targetAppPath, MENU_FORM);
            _robot.Sleep(2);
            _canvas = _robot.FindElementByName("_panel1");
        }

        [TestCleanup()]
        public void Teardown()
        {
           //_robot.ClickButton("關閉");
        }

        // Move pointer to point
        public Interaction MovePointerTo(PointerInputDevice device, Point point)
        {
            var size = _canvas.Size;
            //Console.WriteLine("In Move Pointer To");
            //Console.WriteLine("canvasSize" + size);
            //Console.WriteLine("X:" + ((int)point.X - size.Width / 2) + "Y" + ((int)point.Y - size.Height / 2));
            return device.CreatePointerMove(_canvas, (int)point.X - size.Width / 2, (int)point.Y - size.Height / 2, TimeSpan.Zero);
        }

        // draw
        public void DrawShape(string shapeType, Point topLeftPoint, Point bottomRightPoint)
        {
            _robot.ClickButton(shapeType);
            Console.WriteLine("topLeftPoint.X" + topLeftPoint.X + "topLeftPoint.Y" + topLeftPoint.Y);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerTo(pointer, topLeftPoint))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerTo(pointer, bottomRightPoint))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
        }

        //Test
        public void ResizeShape(Point bottomRightHandlePoint, Point resizeEndPoint)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice pointer = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
                .AddAction(MovePointerTo(pointer, bottomRightHandlePoint))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left))
                .AddAction(pointer.CreatePointerDown(MouseButton.Left))
                .AddAction(MovePointerTo(pointer, resizeEndPoint))
                .AddAction(pointer.CreatePointerUp(MouseButton.Left));
            _robot.PerformAction(actionBuilder.ToActionSequenceList());
        }

        [TestMethod]
        public void VerifyToolStripButtonCheckedAndDrawLine()
        {
            _robot.ClickButton("線");
            _robot.AssertEnable("線", true);
            DrawShape("線", new Point(1, 1), new Point(200, 200));
            string[] expectedData = { "刪除", "線", "(1,1),(200,200)" };
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, expectedData);
        }

        [TestMethod]
        public void VerifyToolStripButtonCheckedAndDrawRectangle()
        {
            _robot.ClickButton("矩形");
            _robot.AssertEnable("矩形", true);
            DrawShape("矩形", new Point(300, 100), new Point(350, 150));
            string[] expectedData = { "刪除", "矩形", "(300,100),(350,150)" };
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, expectedData);
        }


        [TestMethod]
        public void VerifyToolStripButtonCheckedAndDrawEllipse()
        {
            _robot.ClickButton("橢圓形");
            _robot.AssertEnable("橢圓形", true);
            DrawShape("橢圓形", new Point(300, 100), new Point(350, 150));
            string[] expectedData = { "刪除", "橢圓形", "(300,100),(350,150)" };
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, expectedData);
        }

        [TestMethod]
        public void AssertLineDrawCommandUndoRedoAndDataGridView()
        {
            _robot.ClickButton("線");
            DrawShape("線", new Point(300, 100), new Point(350, 150));
            string[] expectedData = { "刪除", "線", "(300,100),(350,150)" };
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, expectedData);
            _robot.ClickButton("toolStripButton1");
            _robot.AssertDataGridViewRowCountBy("_dataGridViewRight", 0);
            _robot.ClickButton("toolStripButton2");
            _robot.AssertDataGridViewRowCountBy("_dataGridViewRight", 1);
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, expectedData);
        }

        [TestMethod]
        public void AssertRectResizeCommandUndoRedoAndDataGridView()
        {
            _robot.ClickButton("矩形");
            DrawShape("矩形", new Point(300, 100), new Point(350, 150));
            string[] UndoexpectedData = { "刪除", "矩形", "(300,100),(350,150)" };
            string[] RedoexpectedData = { "刪除", "矩形", "(300,100),(400,200)" };
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, UndoexpectedData);
            ResizeShape(new Point(350, 150), new Point(400, 200));
            _robot.ClickButton("toolStripButton1");
            _robot.AssertDataGridViewRowCountBy("_dataGridViewRight", 1);
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, UndoexpectedData);
            _robot.ClickButton("toolStripButton2");
            _robot.AssertDataGridViewRowCountBy("_dataGridViewRight", 1);
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, RedoexpectedData);
        }

        [TestMethod]
        public void AssertEllipseMoveCommandUndoRedoAndDataGridView()
        {
            _robot.ClickButton("橢圓形");
            DrawShape("橢圓形", new Point(300, 100), new Point(350, 150));
            string[] UndoexpectedData = { "刪除", "橢圓形", "(300,100),(350,150)" };
            string[] RedoexpectedData = { "刪除", "橢圓形", "(0,0),(50,50)" };
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, UndoexpectedData);
            ResizeShape(new Point(325, 125), new Point(25, 25));
            _robot.ClickButton("toolStripButton1");
            _robot.AssertDataGridViewRowCountBy("_dataGridViewRight", 1);
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, UndoexpectedData);
            _robot.ClickButton("toolStripButton2");
            _robot.AssertDataGridViewRowCountBy("_dataGridViewRight", 1);
            _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", 0, RedoexpectedData);
        }


        // only can do in us type
        [TestMethod]
        public void AssertShapeAddCommandBySecondForm()
        {
            var size = _canvas.Size;
            var random = new Random();
            string[] shapeTypes = { "矩形", "橢圓形", "線" };
            for (int i = 0; i < shapeTypes.Length; i++)
            {
                string shapeType = shapeTypes[i];
                _robot.SelectComboBoxItem("_comboBoxShapeType", shapeType);
                _robot.ClickButton("新增");
                var dialogForm = _robot.FindElementByName("DialogForm");
                var textBox1 = dialogForm.FindElementByAccessibilityId("_textBox1");
                var textBox2 = dialogForm.FindElementByAccessibilityId("_textBox2");
                var textBox3 = dialogForm.FindElementByAccessibilityId("_textBox3");
                var textBox4 = dialogForm.FindElementByAccessibilityId("_textBox4");
                var buttonOK = dialogForm.FindElementByAccessibilityId("_buttonOk");
                int left = random.Next(0, size.Width);
                int top = random.Next(0, size.Height);
                int right = random.Next(left + 1, size.Width);
                int bottom = random.Next(top + 1, size.Height);
                textBox1.SendKeys(left.ToString());
                textBox2.SendKeys(top.ToString());
                textBox3.SendKeys(right.ToString());
                textBox4.SendKeys(bottom.ToString());
                buttonOK.Click();
                string formattedString = $"({left},{top}),({right},{bottom})";
                string[] expectedData = { "刪除", shapeType, formattedString};
                _robot.AssertDataGridViewRowDataBy("_dataGridViewRight", i, expectedData);
            }
        }


    }
}
