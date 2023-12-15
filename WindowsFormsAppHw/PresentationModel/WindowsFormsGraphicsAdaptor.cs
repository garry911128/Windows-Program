using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsAppHomework.PresentationModel
{
    public class WindowsFormsGraphicsAdaptor : IGraphics
    {
        private Graphics _graphics;

        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }

        // clear graphic
        public void ClearAll()
        {
                // OnPaint時會自動清除畫面，因此不需實作
        }

        // Draw Line
        public void DrawLine(double startX, double startY, double endX, double endY)
        {
            _graphics.DrawLine(Pens.Black, (float)startX, (float)startY, (float)endX, (float)endY);
        }

        // Draw Rectangle
        public void DrawRectangle(double topLeftX, double topLeftY, double width, double height)
        {
            _graphics.DrawRectangle(Pens.Black, (float)topLeftX, (float)topLeftY, (float)width, (float)height);
        }

        // Draw Ellipse
        public void DrawEllipse(double topLeftX, double topLeftY, double width, double height)
        {
            _graphics.DrawEllipse(Pens.Black,(float)topLeftX, (float)topLeftY, (float)width, (float)height);
        }

        // Draw Handle
        public void DrawHandle(double centerX, double centerY)
        {
            float handleSize = (float)Constants.EIGHT;
            float handleX = (float)centerX - handleSize / Constants.TWO;
            float handleY = (float)centerY - handleSize / Constants.TWO;
            _graphics.DrawEllipse(Pens.Gray, handleX, handleY, handleSize, handleSize);
        }
    }
}
