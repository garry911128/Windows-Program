using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using WindowsFormsAppHomework.PresentationModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace WindowsFormsAppHomework
{
    public partial class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        public event Action<int, Shapes.Action> _pageChanged;
        private ShapeFactory _shapeFactory;
        private Pages _pages;
        private Shape _hint;
        private CommandManager _commandManager;
        GoogleDriveService _service;
        private string _fileId = "";
        private string _solutionPath;
        private string _filePath;
        Size _canvasSize;
        IState _state;
        // SetShowHint
        public virtual bool ShowHint
        {
            get;
            set;
        }

        public virtual bool IsRedoEnabled
        {
            get
            {
                return _commandManager.IsRedoEnabled;
            }
        }

        public virtual bool IsUndoEnabled
        {
            get
            {
                return _commandManager.IsUndoEnabled;
            }
        }

        public virtual int SlideIndex
        {
            get; set;
        }

        public Model()
        {
            Shapes shapes = new Shapes();
            _pages = new Pages();
            _pages.Add(shapes);
            SlideIndex = 0;
            _shapeFactory = new ShapeFactory(); //Point topLeftPoint,double width,double height
            _state = new PointerState(this);
            _commandManager = new CommandManager();
            const string APPLICATION_NAME = "DrawAnyWhere";
            const string CLIENT_SECRET_FILE_NAME = "clientSecret.json";
            _service = new GoogleDriveService(APPLICATION_NAME, CLIENT_SECRET_FILE_NAME);
            _solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            _filePath = Path.Combine(_solutionPath, "WindowsFormsAppHw", "bin", "Debug", "Garry's.txt");
        }

        // Set State
        public virtual void SetState(IState state)
        {
            _state = state;
        }

        // press new slide button or old slide will base index to change
        public virtual void SwitchSlide(int slideIndex)
        {
            Debug.Assert(slideIndex < _pages.Count);
            SlideIndex = slideIndex;
            NotifyObserver();
        }

        // insert a shapes 
        public virtual void InsertPage(int slideIndex, Shapes shapes)
        {
            SlideIndex = slideIndex;
            _pages.Insert(slideIndex, shapes);
            _pageChanged(SlideIndex, Shapes.Action.Add);
            NotifyObserver();
        }

        // delete a shapes
        public virtual void DeletePage(int slideIndex, Shapes shapes)
        {
            Console.WriteLine("In Model , now slide Index:" + SlideIndex);
            Console.WriteLine("In Model , now pages Count:" + _pages.Count);
            if (slideIndex == _pages.Count - 1)
            {
                SlideIndex = slideIndex - 1 ;
            }
            _pages.Remove(shapes);
            _pageChanged(SlideIndex, Shapes.Action.Remove);
            Console.WriteLine("In Model , now slide Index:" +  SlideIndex);
            NotifyObserver();
        }

        //undo
        public virtual void Undo()
        {
            Console.WriteLine("In Model Undo , slide Index:" + SlideIndex);
            Console.WriteLine("In Model Undo , pages count" +  _pages.Count);
            _commandManager.Undo(_canvasSize);
            //_pageChanged(_commandManager.GetCommandSlideIndex());
            NotifyObserver();
        }

        // redo
        public virtual void Redo()
        {
            _commandManager.Redo(_canvasSize);
            //_pageChanged(_commandManager.GetCommandSlideIndex());
            NotifyObserver();
        }

        // execute DrawCommand
        public virtual void ExecuteAddPageCommand(int newSlideIndex)
        {
            Console.WriteLine("Add page command Execute");
            AddPageCommand addPageCommand = new AddPageCommand(this, new Shapes(), newSlideIndex);
            _commandManager.Execute(addPageCommand, _canvasSize);
            NotifyObserver();
        }

        // execute DrawCommand
        public virtual void ExecuteDeletePageCommand(int newSlideIndex)
        {
            Console.WriteLine("Delete page command Execute");
            DeletePageCommand addPageCommand = new DeletePageCommand(this, _pages[SlideIndex], newSlideIndex);
            _commandManager.Execute(addPageCommand, _canvasSize);
            NotifyObserver();
        }

        // execute DrawCommand
        public virtual void ExecuteDrawCommand()
        {
            DrawCommand drawCommand = new DrawCommand(this, _hint, SlideIndex, _pages[SlideIndex].GetShapeListSize());
            _commandManager.Execute(drawCommand, _canvasSize);
            NotifyObserver();
        }

        // execute DrawCommand
        public virtual void ExecuteAddCommand(string shapeType, Point topLeftPoint, Point bottomRightPoint)
        {
            if (shapeType != null && shapeType != "" && shapeType != Constants.POINTER_MODE_TYPE)
            {
                _state = new PointerState(this);
                Shape newShape = _shapeFactory.CreateShape(shapeType, topLeftPoint, bottomRightPoint.X - topLeftPoint.X, bottomRightPoint.Y - topLeftPoint.Y);
                AddCommand addCommand = new AddCommand(this, newShape, SlideIndex, _pages[SlideIndex].GetShapeListSize());
                _commandManager.Execute(addCommand, _canvasSize);
                NotifyObserver();
            }
        }

        // execute MoveCommand
        public virtual void ExecuteMoveCommand(Point firstPoint, Point endPoint)
        {
            if ( (firstPoint.X != endPoint.X) || (firstPoint.Y != endPoint.Y))
            {
                MoveCommand moveCommand = new MoveCommand(this, GetSelectedShape(), SlideIndex);
                _commandManager.Execute(moveCommand, _canvasSize);
                moveCommand.SetDelta(firstPoint, endPoint);
                NotifyObserver();
            }
        }

        // execute MoveCommand
        public virtual void ExecuteResizeCommand(Point originTopLeftPoint, Point originBottomRightPoint, Point ReleasePoint)
        {
            ResizeCommand resizeCommand = new ResizeCommand(this, GetSelectedShape(), originTopLeftPoint, originBottomRightPoint, ReleasePoint);
            resizeCommand.SetSlideIndex(SlideIndex);
            _commandManager.Execute(resizeCommand, _canvasSize);
            NotifyObserver();
        }

        // execute DeleteCommand
        public virtual void ExecuteDeleteCommand(int index)
        {
            DeleteCommand drawCommand = new DeleteCommand(this, _pages[SlideIndex].GetShapeList[index], SlideIndex, index);
            _commandManager.Execute(drawCommand, _canvasSize);
            NotifyObserver();
        }

        // Mouse Press
        public virtual void PressedMouse(string shapeType, Point topLeftPoint, bool isPressed)
        {
            _state.PressedMouse(shapeType, topLeftPoint, isPressed);
            NotifyObserver();
        }

        // Mouse Move
        public virtual void MovedMouse(Point firstPoint, Point mousePoint)
        {
            _state.MovedMouse(firstPoint, mousePoint);
            NotifyObserver();
        }

        // Mouse Press
        public virtual void ReleaseMouse(Point firstPoint, Point nowPoint, bool isPressed)
        {
            _state.ReleaseMouse(firstPoint, nowPoint, isPressed);
            NotifyObserver();
        }

        // On KeyDown
        public virtual void HandleKeyDown(Keys keyCode)
        {
            if (keyCode == Keys.Delete && _pages[SlideIndex].GetSelectedShape() != null)
            {
                int index = _pages[SlideIndex].GetShapeList.IndexOf(_pages[SlideIndex].GetSelectedShape());
                ExecuteDeleteCommand(index);
                _pages[SlideIndex].ClearSelectedShape();
                NotifyObserver();
            }
            else if(keyCode == Keys.Delete && _pages[SlideIndex].GetSelectedShape() == null)
            {
                ExecuteDeletePageCommand(SlideIndex);
            }
        }

        // Get Base Point
        public virtual Shape GetSelectedShape()
        {
            return _pages[SlideIndex].GetSelectedShape();
        }

        //get shape name
        public BindingList<Shape> GetShapeList()
        {
            return _pages[SlideIndex].GetShapeList;
        }

        // Model Select a shape by point
        public virtual bool SelectShapeByPoint(Point nowPoint)
        {
            return _pages[SlideIndex].SelectedShapeByPoint(nowPoint);
        }

        //Model Get Select and Handle
        public virtual bool IsPointInSelectedShapeHandle(Point mousePoint)
        {
            return _pages[SlideIndex].IsPointInSelectedShapeHandle(mousePoint);
        }

        // Move Shape
        public virtual void MoveShape(double deltaX, double deltaY)
        {
            _pages[SlideIndex].MovedSelectedShapeByMouse(deltaX, deltaY);
        }

        // Add to shape list
        public virtual void AddShapeToList(Shape shape, int slideIndex, int indexOfStack = -1)
        {
            _pages[slideIndex].IncreaseShapeToList(shape, indexOfStack);
            _hint = null;
        }

        //Create Hint Shape
        public virtual void CreateHintShape(string shapeType, Point topLeftPoint, double width, double height)
        {
            _hint = _shapeFactory.CreateShape(shapeType, topLeftPoint, width, height);
        }

        // Resize Shape
        public virtual void ResizeShape(Point basePoint, Point mousePoint)
        {
            if (_hint != null)
            {
                _hint.Resize(basePoint, mousePoint);
            }
            else
            {
                _pages[SlideIndex].ResizeSelectedShape(basePoint, mousePoint);
            }
            NotifyObserver();
        }

        //delete expected row
        public virtual void DeleteShapeOfStack(int slideIndex, int indexOfRow)
        {
            _pages[slideIndex].DeleteShape(indexOfRow);
            NotifyObserver();
        }

        // Draw graphic
        public virtual void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            if (_hint != null)
            {
                _hint.Draw(graphics);
            }
            foreach (Shape aShape in _pages[SlideIndex].GetShapeList)
            {
                aShape.Draw(graphics);
            }
        }

        // notify observer
        public void NotifyObserver()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
        }

        //Set canvas Size
        public virtual void SetCanvasSize(Size newSize)
        {
            if (_canvasSize != Size.Empty)
            {
                _pages[SlideIndex].SetPanelSize(newSize, _canvasSize);
            }
            _canvasSize = new Size(newSize.Width, newSize.Height);
            NotifyObserver();
        }


    }
}
