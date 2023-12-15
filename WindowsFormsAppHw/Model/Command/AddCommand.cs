﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class AddCommand : ICommand
    {
        Shape _shape;
        Model _model;
        int _indexOfStack;
        Size _canvasSize;

        public AddCommand(Model model, Shape shape, int indexOfStack)
        {
            _shape = shape;
            _model = model;
            _indexOfStack = indexOfStack;
        }

        // execute redo insert
        public void DoExecute(Size nowSize)
        {
            AdjustNowSize(nowSize);
            _model.AddShapeToList(_shape, _indexOfStack);
        }

        // unexecute undo
        public void UndoExecute(Size nowSize)
        {
            SetSize(nowSize);
            _model.DeleteShapeOfStack(_indexOfStack);
        }

        // Set Size
        public void SetSize(Size canvasSize)
        {
            _canvasSize = canvasSize;
        }

        // new Size
        public void AdjustNowSize(Size newCanvasSize)
        {
            _shape.SetPanelSize(newCanvasSize, _canvasSize);
            _canvasSize = newCanvasSize;
        }
    }
}
