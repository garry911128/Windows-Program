using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WindowsFormsAppHomework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    [TestClass()]
    public class CommandManagerTests
    {
        [TestMethod]
        public void Execute_AddCommandToUndoStack()
        {
            var commandManager = new CommandManager();
            PrivateObject privateCommandManager = new PrivateObject(commandManager);
            var commandMock = new Mock<ICommand>();
            commandManager.Execute(commandMock.Object, new Size(10, 10)); // 這裡加入一個 Size 參數
            Stack<ICommand> undo = (Stack<ICommand>)privateCommandManager.GetField("_undo");
            Assert.AreEqual(1, undo.Count);
        }

        [TestMethod]
        public void Undo_PopCommandFromUndoStackAndCallUnExecute()
        {
            var commandManager = new CommandManager();
            PrivateObject privateCommandManager = new PrivateObject(commandManager);
            var commandMock = new Mock<ICommand>();
            commandManager.Execute(commandMock.Object, new Size(10,10));
            commandManager.Undo(new Size(10, 10));
            Stack<ICommand> undo = (Stack<ICommand>)privateCommandManager.GetField("_undo");
            commandMock.Verify(c => c.UndoExecute(It.IsAny<Size>()), Times.Once);
            Stack<ICommand> redo = (Stack<ICommand>)privateCommandManager.GetField("_redo");
            Assert.AreEqual(0, undo.Count);
            Assert.AreEqual(1, redo.Count);
        }

        [TestMethod]
        public void Redo_PopCommandFromRedoStackAndCallExecute()
        {
            var commandManager = new CommandManager();
            PrivateObject privateCommandManager = new PrivateObject(commandManager);
            var commandMock = new Mock<ICommand>();
            commandManager.Execute(commandMock.Object, new Size(10,10));
            commandManager.Undo(new Size(10, 10));
            commandManager.Redo(new Size(10, 10));
            Stack<ICommand> undo = (Stack<ICommand>)privateCommandManager.GetField("_undo");
            Assert.AreEqual(1, undo.Count);
            commandMock.Verify(c => c.DoExecute(It.IsAny<Size>()), Times.Exactly(2));
        }

        [TestMethod]
        public void IsUndoEnabled_ReturnsTrueWhenUndoStackNotEmpty()
        {
            var commandManager = new CommandManager();
            PrivateObject privateCommandManager = new PrivateObject(commandManager);
            var commandMock = new Mock<ICommand>();
            commandManager.Execute(commandMock.Object, new Size(0, 0));
            bool isUndoEnabled = (bool)privateCommandManager.GetProperty("IsUndoEnabled");
            Assert.IsTrue(isUndoEnabled);
        }

        [TestMethod]
        public void IsRedoEnabled_ReturnsTrueWhenRedoStackNotEmpty()
        {
            var commandManager = new CommandManager();
            PrivateObject privateCommandManager = new PrivateObject(commandManager);
            var commandMock = new Mock<ICommand>();

            // Act
            commandManager.Execute(commandMock.Object, new Size(0, 0));
            commandManager.Undo(new Size(0, 0));

            // Assert
            bool isRedoEnabled = (bool)privateCommandManager.GetProperty("IsRedoEnabled");
            Assert.IsTrue(isRedoEnabled);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Cannot Redo exception\n")]
        public void Redo_ThrowsExceptionWhenRedoStackEmpty()
        {
            var commandManager = new CommandManager();
            commandManager.Redo(new Size(0,0));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Cannot Undo exception\n")]
        public void Undo_ThrowsExceptionWhenUndoStackEmpty()
        {
            var commandManager = new CommandManager();
            commandManager.Undo(new Size(0, 0));
        }

    }
}