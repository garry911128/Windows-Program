using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppHomework
{
    public class DrawingState : IState
    {

        private Model _model;
        public DrawingState(Model model)
        {
            _model = model;
        }

        // Press Mouse
        public void PressedMouse(string shapeType, Point topLeftPoint, bool isPressed)
        {
            _model.CreateHintShape(shapeType, topLeftPoint, 0, 0);
            _model.ShowHint = true;
        }

        // Move Mouse
        public void MovedMouse(Point firstPoint, Point nowPoint)
        {
            _model.ResizeShape(firstPoint, nowPoint);
        }

        // Releasee Mouse
        public void ReleaseMouse(Point firstPoint, Point nowPoint, bool isPressed)
        {
            _model.ResizeShape(firstPoint, nowPoint);
            _model.ExecuteDrawCommand();
            _model.ShowHint = false;
        }

    }
}
