using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public partial class Model
    {
        //Save file
        public virtual async Task HandleSave()
        {
            var csv = _pages.Encode(_canvasSize);
            File.WriteAllText(_filePath, csv);
            if (_fileId == "")
                _fileId = await _service.UploadFile(_filePath, Constants.TEXT_PLAIN);
            else
                _service.UpdateFile("Garry's.txt", _fileId, Constants.TEXT_PLAIN);
            Thread.Sleep(10);
        }

        // load pages from drive
        public virtual void HandleLoad()
        {
            ClearSlides();
            _commandManager.Clear();
            _service.DownloadFile(_fileId, _filePath);
            ReadFile();
        }

        // clear slides
        public void ClearSlides()
        {
            SlideIndex = 0;
            int count = _pages.Clear();
            for (int i=0; i < count; i++)
            {
                _pageChanged(0, Shapes.Action.Remove);
            }
            NotifyObserver();
        }

        // read file
        public virtual void ReadFile()
        {
            string data = System.IO.File.ReadAllText("Garry's.txt");
            var splitedData = new List<string>(data.Split(Constants.EOL));
            for (int i = 0; i < splitedData.Count - 1; i++)
            {
                var slide = new Shapes();
                LoadShapesIntoList(splitedData[i], _canvasSize, slide);
                if (i == 0)
                {
                    _pages[SlideIndex].LoadShapes(slide);
                    NotifyObserver();
                }
                else
                {
                    InsertPage(_pages.Count, slide);
                }
            }
        }


        // decode
        public virtual void LoadShapesIntoList(string shapesData, Size canvasSize, Shapes shapes)
        {
            var pattern = @"\{([^,]+),\{([^,]+),([^,]+),([^,]+),([^}]+)\}\}";
            int index = 0;
            foreach (Match match in Regex.Matches(shapesData, pattern))
            {
                var type = match.Groups[1].Value.Trim();
                Point topLeftPoint = new Point(double.Parse(match.Groups[2].Value.Trim())*_canvasSize.Width, double.Parse(match.Groups[3].Value.Trim())*_canvasSize.Height);
                Point bottomRightPoint = new Point(double.Parse(match.Groups[4].Value.Trim()) * _canvasSize.Width, double.Parse(match.Groups[5].Value.Trim()) * _canvasSize.Height);
                //Console.WriteLine(type + "," + topLeftPoint + "," + bottomRightPoint);
                shapes.IncreaseShapeToList(_shapeFactory.CreateShape(type, topLeftPoint, bottomRightPoint.X - topLeftPoint.X, bottomRightPoint.Y - topLeftPoint.Y), index);
            }
        }

    }
}
