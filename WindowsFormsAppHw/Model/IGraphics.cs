using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public interface IGraphics
    {
        // clearall port
        void ClearAll();

        // Draw Line port
        void DrawLine(double x1, double y1, double x2, double y2);

        // Draw Rectangle port
        void DrawRectangle(double x1, double y1, double width, double height);

        // Draw Ellipse port
        void DrawEllipse(double x1, double y1, double width, double height);

        // Draw Handle
        void DrawHandle(double centerX, double centerY);
    }
}