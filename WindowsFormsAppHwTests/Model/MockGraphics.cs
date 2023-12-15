using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework.Tests
{
    public class MockGraphics : IGraphics
    {

        private int _drawCountLine;
        private int _drawCountRectangle;
        private int _drawCountEllipse;
        private int _drawCountHandle;

        // f
        public MockGraphics()
        {
            ResetAllCount();
        }

        // f
        public void DrawLine(double startX, double startY, double endX, double endY)
        {
            _drawCountLine++;
        }

        // f
        public void DrawEllipse(double topLeftX, double topLeftY, double width, double height)
        {
            _drawCountEllipse++;
        }

        // f
        public void DrawRectangle(double topLeftX, double topLeftY, double width, double height)
        {
            _drawCountRectangle++;
        }

        // f
        public void DrawHandle(double centerX, double centerY)
        {
            _drawCountHandle++;
        }

        // f
        public void ClearAll()
        {
            _drawCountLine = 0;
            _drawCountRectangle = 0;
            _drawCountEllipse = 0;
            _drawCountHandle = 0;
        }

        // f
        public void ResetAllCount()
        {
            _drawCountLine = 0;
            _drawCountRectangle = 0;
            _drawCountEllipse = 0;
            _drawCountHandle = 0;
        }

        public int CountLine
        {
            get
            {
                return _drawCountLine;
            }
        }

        public int CountEllipse
        {
            get
            {
                return _drawCountEllipse;
            }
        }

        public int CountRectangle
        {
            get
            {
                return _drawCountRectangle;
            }
        }

        public int CountHandle
        {
            get
            {
                return _drawCountHandle;
            }
        }

        public int CountAll
        {
            get
            {
                return _drawCountLine + _drawCountEllipse + _drawCountRectangle + _drawCountHandle;
            }
        }
    }
}
