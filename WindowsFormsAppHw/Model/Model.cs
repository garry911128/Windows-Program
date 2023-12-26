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

namespace WindowsFormsAppHomework
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        private ShapeFactory _shapeFactory;
        private Shapes _shapes;
        private Shape _hint;
        private CommandManager _commandManager;
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

        public Model()
        {
            _shapes = new Shapes();
            _shapeFactory = new ShapeFactory(); //Point topLeftPoint,double width,double height
            _state = new PointerState(this);
            _commandManager = new CommandManager();
        }

        // Set State
        public virtual void SetState(IState state)
        {
            _state = state;
        }

        //undo
        public virtual void Undo()
        {
            _commandManager.Undo(_canvasSize);
            NotifyObserver();
        }

        // redo
        public virtual void Redo()
        {
            _commandManager.Redo(_canvasSize);
            NotifyObserver();
        }

        // execute DrawCommand
        public virtual void ExecuteDrawCommand()
        {
            DrawCommand drawCommand = new DrawCommand(this, _hint, _shapes.GetShapeListSize());
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
                AddCommand addCommand = new AddCommand(this, newShape, _shapes.GetShapeListSize());
                _commandManager.Execute(addCommand, _canvasSize);
                NotifyObserver();
            }
        }

        // execute MoveCommand
        public virtual void ExecuteMoveCommand(Point firstPoint, Point endPoint)
        {
            if ( (firstPoint.X != endPoint.X) || (firstPoint.Y != endPoint.Y))
            {
                MoveCommand moveCommand = new MoveCommand(this, GetSelectedShape());
                _commandManager.Execute(moveCommand, _canvasSize);
                moveCommand.SetDelta(firstPoint, endPoint);
                NotifyObserver();
            }
        }

        // execute MoveCommand
        public virtual void ExecuteResizeCommand(Point originTopLeftPoint, Point originBottomRightPoint, Point ReleasePoint)
        {
            ResizeCommand resizeCommand = new ResizeCommand(this, GetSelectedShape(), originTopLeftPoint, originBottomRightPoint, ReleasePoint);
            _commandManager.Execute(resizeCommand, _canvasSize);
            NotifyObserver();
        }

        // execute DeleteCommand
        public virtual void ExecuteDeleteCommand(int index)
        {
            DeleteCommand drawCommand = new DeleteCommand(this, _shapes.GetShapeList[index], index);
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
            if (keyCode == Keys.Delete && _shapes.GetSelectedShape() != null)
            {
                int index = _shapes.GetShapeList.IndexOf(_shapes.GetSelectedShape());
                ExecuteDeleteCommand(index);
                NotifyObserver();
            }
        }

        // Get Base Point
        public virtual Shape GetSelectedShape()
        {
            return _shapes.GetSelectedShape();
        }

        //get shape name
        public BindingList<Shape> GetShapeList()
        {
            return _shapes.GetShapeList;
        }

        // Model Select a shape by point
        public virtual bool SelectShapeByPoint(Point nowPoint)
        {
            return _shapes.SelectedShapeByPoint(nowPoint);
        }

        //Model Get Select and Handle
        public virtual bool IsPointInSelectedShapeHandle(Point mousePoint)
        {
            return _shapes.IsPointInSelectedShapeHandle(mousePoint);
        }

        // Move Shape
        public virtual void MoveShape(double deltaX, double deltaY)
        {
            _shapes.MovedSelectedShapeByMouse(deltaX, deltaY);
        }

        // Add to shape list
        public virtual void AddShapeToList(Shape shape, int indexOfStack = -1)
        {
            _shapes.IncreaseShapeToList(shape, indexOfStack);
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
                _shapes.ResizeSelectedShape(basePoint, mousePoint);
            }
            NotifyObserver();
        }

        //delete expected row
        public virtual void DeleteShapeOfStack(int indexOfRow)
        {
            _shapes.DeleteShape(indexOfRow);
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
            foreach (Shape aShape in _shapes.GetShapeList)
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
                _shapes.SetPanelSize(newSize, _canvasSize);
            }
            _canvasSize = new Size(newSize.Width, newSize.Height);
            NotifyObserver();
        }

    }
}
