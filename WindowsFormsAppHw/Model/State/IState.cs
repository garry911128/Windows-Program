using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppHomework
{
    public interface IState
    {

        // Pointer Press
        void PressedMouse(string shapeType, Point topLeftPoint, bool isPressed);

        // Pointer Move
        void MovedMouse(Point firstPoint, Point nowPoint);

        // Pointer release
        void ReleaseMouse(Point firstPoint, Point nowPoint, bool isPressed);

    }
}
