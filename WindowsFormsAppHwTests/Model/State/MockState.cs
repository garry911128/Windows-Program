using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsAppHomework;
using System.Drawing;

namespace WindowsFormsAppHomework.Tests
{
    public class MockState : IState
    {
        public bool PressedMouseCalled 
        { 
            get; 
            private set; 
        }
        public bool MovedMouseCalled 
        { 
            get; 
            private set; 
        }
        public bool ReleaseMouseCalled 
        { 
            get; 
            private set; 
        }

        // get Press
        public void PressedMouse(string shapeType, Point topLeftPoint, bool isPressed)
        {
            PressedMouseCalled = true;
        }

        // get move
        public void MovedMouse(Point firstPoint, Point nowPoint)
        {
            MovedMouseCalled = true;
        }

        // get release
        public void ReleaseMouse(Point firstPoint, Point nowPoint, bool isPressed)
        {
            ReleaseMouseCalled = true;
        }

        // reset
        public void ResetCalls()
        {
            PressedMouseCalled = false;
            MovedMouseCalled = false;
            ReleaseMouseCalled = false;
        }
    }
}

