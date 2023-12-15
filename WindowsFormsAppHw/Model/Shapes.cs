using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAppHomework
{
    public class Shapes
    {
        private BindingList<Shape> _shapeList;
        private Shape _selectedShape;
        public Shapes()
        {
            _shapeList = new BindingList<Shape>();
        }

        //add shape to stack , index = -1 already defense in model.cs
        public virtual void IncreaseShapeToList(Shape shape, int index)
        {
            if (index != -1 && index >= 0 && index <= _shapeList.Count)
            {
                _shapeList.Insert(index, shape);
            }
            else if (index == -1)
            {
                _shapeList.Add(shape);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        // delete expected row 
        public virtual void DeleteShape(int indexOfStack = -1)
        {
            if (indexOfStack != -1)
            {
                _shapeList.RemoveAt(indexOfStack);
            }
            else if (_selectedShape != null)
            {
                _shapeList.Remove(_selectedShape);
                _selectedShape = null;
            }
        }

        // return shapestack size
        public virtual int GetShapeListSize()
        {
            return _shapeList.Count();
        }

        // return shape name
        public string GetShapeTypeName(int indexOfStack)
        {
            return _shapeList[indexOfStack].GetShapeName();
        }

        // return shape loc
        public string GetShapeLocation(int indexOfStack)
        {
            string message = _shapeList[indexOfStack].GetInfo();
            return message;
        }

        // getter
        public virtual BindingList<Shape> GetShapeList
        {
            get
            {
                return _shapeList;
            }
        }

        // Getter , we prevent selected shape is null in presentationModel
        public virtual Shape GetSelectedShape()
        {
            return _selectedShape;
        }

        // get the newest shape and the mouse in the shape
        public virtual bool SelectedShapeByPoint(Point nowPoint)
        {
            ClearSelection();
            foreach (Shape shape in _shapeList)
            {
                if (shape.IsPointInsideShape(nowPoint))
                {
                    shape.isSelected = true;
                    _selectedShape = shape;
                    return true;
                }
            }
            return false;
        }

        // assert is point in handle
        public virtual bool IsPointInSelectedShapeHandle(Point mousePoint)
        {
            return _selectedShape != null && _selectedShape.IsPointInsideHandle(mousePoint);
        }

        // Move Shape
        public virtual void MovedSelectedShapeByMouse(double deltaX, double deltaY)
        {
            if (_selectedShape != null)
            {
                _selectedShape.Move(deltaX, deltaY);
            }
        }

        // Resize selected Shape
        public void ResizeSelectedShape(Point basePoint, Point mousePoint)
        {
            _selectedShape.Resize(basePoint, mousePoint);
        }

        // clear selection
        private void ClearSelection()
        {
            if (_selectedShape != null)
            {
                _selectedShape.isSelected = false;
                _selectedShape = null;
            }
        }

        // canvas dynaic coordinate, prevent null size in model
        public virtual void SetPanelSize(Size newCanvasSize, Size oldSize)
        {
            foreach (Shape shape in _shapeList)
            {
                shape.SetPanelSize(newCanvasSize, oldSize);
            }
        }

    }
}
