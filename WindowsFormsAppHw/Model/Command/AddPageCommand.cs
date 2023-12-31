using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class AddPageCommand : ICommand
    {
        Shapes _shapes;
        Model _model;
        int _slideIndex;
        Size _canvasSize;

        public AddPageCommand(Model model, Shapes shapes, int slideIndex)
        {
            Console.WriteLine("Add page init" + slideIndex);
            _shapes = shapes;
            _model = model;
            _slideIndex = slideIndex;
        }

        // execute redo insert
        public void DoExecute(Size nowSize)
        {
            AdjustNowSize(nowSize);
            _model.InsertPage(_slideIndex, _shapes);
        }

        // unexecute undo
        public void UndoExecute(Size nowSize)
        {
            SetSize(nowSize);
            Console.WriteLine("Add Page Undo, silde Index:" +  _slideIndex);
            _model.DeletePage(_slideIndex, _shapes);
        }

        // Set Size
        public void SetSize(Size canvasSize)
        {
            _canvasSize = canvasSize;
        }

        // new Size
        public void AdjustNowSize(Size newCanvasSize)
        {
            _shapes.SetPanelSize(newCanvasSize, _canvasSize);
            _canvasSize = newCanvasSize;
        }

        //get slideIndex
        public int GetSlideIndex()
        {
            return _slideIndex;
        }
    }
}
