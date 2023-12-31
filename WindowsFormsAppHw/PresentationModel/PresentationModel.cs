using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAppHomework.PresentationModel
{
    public class PresentationModel : INotifyPropertyChanged
    {
        public event PresentationModelChangedEventHandler _presentationModelChanged;
        public delegate void PresentationModelChangedEventHandler();
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void CursorChangedEventHandler(Cursor cursor);
        public event CursorChangedEventHandler _cursorChanged;

        private Model _model;
        private double _firstX;
        private double _firstY;
        private bool _isPressed = false;
        private bool _isEllipseChecked = false;
        private bool _isLineChecked = false;
        private bool _isRectangleChecked = false;
        private bool _isCursorChecked = false;
        private int _slideIndex = 0;
        private string _hintShapeType = Constants.POINTER_MODE_TYPE;
        private WindowsFormsGraphicsAdaptor _graphics;

        public PresentationModel(Model model)
        {
            _model = model;
        }

        // Set Tool strip
        public void SetToolStripButton()
        {
            if (_hintShapeType == Constants.RECTANGLE)
            {
                _isRectangleChecked = true;
            }
            else if (_hintShapeType == Constants.ELLIPSE)
            {
                _isEllipseChecked = true;
            }
            else if (_hintShapeType == Constants.LINE)
            {
                _isLineChecked = true;
            }
            else 
            {
                _isCursorChecked = true;
            }
            NotifyObserver();
        }

        // We click the toolstrip to know the type and know im gonna draw
        public void SetMode(string shapeType)
        {
            _isCursorChecked = _isRectangleChecked = _isLineChecked = _isEllipseChecked = false;
            
            if (shapeType == Constants.POINTER_MODE_TYPE)
            {
                _model.SetState(new PointerState(_model));
            }
            else
            {
                _model.SetState(new DrawingState(_model));
            }
            _hintShapeType = shapeType;
            SetToolStripButton();
            // NotifyObserver();
        }

        // pressed the mouse(once)
        public virtual void PressedPointer(double mouseX, double mouseY)
        {
            if (mouseX > 0 && mouseY > 0 && !_isPressed)
            {
                _firstX = mouseX;
                _firstY = mouseY;
                _isPressed = true;
                if (_model.IsPointInSelectedShapeHandle(new Point(mouseX, mouseY)))
                {
                    _model.SetState(new ResizeState(_model));
                }
                _model.PressedMouse(_hintShapeType, new Point(_firstX, _firstY), _isPressed);
            }
        }

        // x,y change ,and resize the shape of last stack(because when we move is already create shape)
        public virtual void MovedPointer(double mouseX, double mouseY)
        {
            if (_isPressed)
            {
                _model.MovedMouse(new Point(_firstX, _firstY), new Point(mouseX, mouseY));
            }
            else
            {
                if (_model.IsPointInSelectedShapeHandle(new Point(mouseX, mouseY)))
                {
                    _cursorChanged(Cursors.SizeNWSE);
                }
            }
            //NotifyObserver();
        }

        // pointer released
        public virtual void ReleasedPointer(double mouseX, double mouseY)
        {
            if (_isPressed)
            {
                _isPressed = false;
                _model.ReleaseMouse(new Point(_firstX, _firstY), new Point(mouseX, mouseY), _isPressed);
                _hintShapeType = "";
                _model.SetState(new PointerState(_model));
                _cursorChanged(Cursors.Default);
            }
            _isCursorChecked = true;
            _isRectangleChecked = _isLineChecked = _isEllipseChecked = false;
            NotifyObserver();
        }

        // process key down
        public void ProcessKeyDown(Keys keyCode)
        {
            _model.HandleKeyDown(keyCode);
        }
        
        // handle add page or switch page
        public void ProcessSlideChange(int index)
        {
            _slideIndex = index;
            _model.SwitchSlide(index);
        }

        // insert page
        public void InsertPage(int newSlideIndex)
        {
            _slideIndex = newSlideIndex;
            _model.ExecuteAddPageCommand(newSlideIndex);
        }

        // graphics物件是Paint事件帶進來的，只能在當次Paint使用
        // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
        // 因此，Adaptor不能重複使用，每次都要重新new
        public void Draw(System.Drawing.Graphics graphics)
        {
            _graphics = new WindowsFormsGraphicsAdaptor(graphics);
            _model.Draw(_graphics);
        }

        //Draw on button
        public void DrawOnButton(System.Drawing.Graphics graphics, Size buttonSize, Size canvasSize)
        {
            float scaleX = (float)buttonSize.Width / canvasSize.Width;
            float scaleY = (float)buttonSize.Height / canvasSize.Height;
            // Apply scaling to the graphics object
            graphics.ScaleTransform(scaleX, scaleY);
            _graphics = new WindowsFormsGraphicsAdaptor(graphics);
            _model.Draw(_graphics);
        }

        // is circle checked
        public bool IsEllipseButtonChecked
        {
            get
            {
                return _isEllipseChecked;
            }
        }

        // is line checked
        public bool IsLineButtonChecked
        {
            get
            {
                return _isLineChecked;
            }
        }

        // is rectangle checked
        public bool IsRectangleButtonChecked
        {
            get
            {
                return _isRectangleChecked;
            }
        }

        // is cursor checked
        public bool IsCursorButtonChecked
        {
            get
            {
                return _isCursorChecked;
            }
        }
        
        // notify observer
        public void NotifyObserver()
        {
            if (_presentationModelChanged != null)
            {
                _presentationModelChanged();
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.IS_LINE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.IS_CIRCLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.IS_RECTANGLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constants.IS_CURSOR_CHECKED));
            }
        }

    }
}
