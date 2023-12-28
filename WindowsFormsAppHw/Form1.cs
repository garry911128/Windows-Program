using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsAppHomework
{
    public partial class Form1 : Form
    {
        private Model _model;
        private PresentationModel.PresentationModel _presentationModel;
        private BindingSource _bindingSource = new BindingSource();
        private DialogForm _dialog;

        public Form1(Model model)
        {
            InitializeComponent();
            _model = model;
            _presentationModel = new PresentationModel.PresentationModel(model);
            _model._modelChanged += UpdateView;
            // _presentationModel._presentationModelChanged += UpdateView;
            _presentationModel._cursorChanged += SetCursor;
            SetDataGridView();
            SetCanvas();
            SetToolStripButton();
            _toolStripButtonLine.Click += ButtonModeClick;
            _toolStripButtonEllipse.Click += ButtonModeClick;
            _toolStripButtonRectangle.Click += ButtonModeClick;
            _toolStripButtonRedo.Enabled = false;
            _toolStripButtonUndo.Enabled = false;
            this.KeyPreview = true;
            this.KeyDown += HandleKeyDown;
            _dialog = new DialogForm();
        }

        // Set Cursor
        public void SetCursor(Cursor cursor)
        {
            Cursor = cursor;
        }

        // DataGrid View Setup 
        private void SetDataGridView()
        {
            _dataGridViewRight.ClearSelection();
            _dataGridViewRight.AutoGenerateColumns = false;
            _dataGridViewRight.DataSource = _model.GetShapeList();
            _dataGridViewRight.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            _deleteObject.Text = "刪除";
            _shapeType.DataPropertyName = "ShapeName";
            _location.DataPropertyName = "Location";
        }

        // Setup toolStrip databinding 
        private void SetToolStripButton()
        {
            _toolStripButtonLine.DataBindings.Add(Constants.CHECKED, _presentationModel, "IsLineButtonChecked");
            _toolStripButtonEllipse.DataBindings.Add(Constants.CHECKED, _presentationModel, "IsEllipseButtonChecked");
            _toolStripButtonRectangle.DataBindings.Add(Constants.CHECKED, _presentationModel, "IsRectangleButtonChecked");
            _toolStripButtonCursor.DataBindings.Add(Constants.CHECKED, _presentationModel, "IsCursorButtonChecked");
        }

        // Canvas Setup
        private void SetCanvas()
        {
            //_canvas.Dock = DockStyle.Fill;
            _panel1.BackColor = System.Drawing.Color.LightYellow;
            _panel1.Height = 360;
            _panel1.Width = 640;
            _panel1.MouseDown += HandleCanvasPressed;
            _panel1.MouseUp += HandleCanvasReleased;
            _panel1.MouseMove += HandleCanvasMoved;
            _panel1.Paint += HandleCanvasPaint;
            _smallSlide.Paint += HandleButtonPaint;
            SizeChanged += CanvasSizeChanged;
            CanvasSizeChanged(this, null); // lazy resize
        }

        // Because cant change to cross cursor, so must sperate code
        private void ToolStripButtonCursorClick(object sender, EventArgs e)
        {
            _presentationModel.SetMode(_toolStripButtonCursor.Text);
            _panel1.Cursor = Cursors.Default;
        }

        // Set Mode 
        private void ButtonModeClick(object sender, EventArgs e)
        {
            ToolStripButton clickedButton = (ToolStripButton)sender;
            string buttonText = clickedButton.Text;
            _presentationModel.SetMode(buttonText);
            _panel1.Cursor = Cursors.Cross;
        }

        //add new shape
        private void ButtonAddClick(object sender, EventArgs e)
        {
            _dialog.SetupDialog(_panel1.Size, _comboBoxShapeType.Text, _model);
            _dialog.ShowDialog();
            UpdateUndoRedoButton();
        }

        // delete row
        private void ClickDataGridViewRightCellContent(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == _dataGridViewRight.Columns[0].Index && e.RowIndex >= 0)
            {
                _model.ExecuteDeleteCommand(e.RowIndex);
                UpdateUndoRedoButton();
            }
        }

        // when we press mouse in panel
        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.PressedPointer(e.X, e.Y);
        }

        // when we release mouse in panel
        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.ReleasedPointer(e.X, e.Y);
            _panel1.Cursor = DefaultCursor;
            UpdateUndoRedoButton();
        }

        //when we want to paint in panel
        public void HandleCanvasPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics);
        }

        //when we move mouse in panel
        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _presentationModel.MovedPointer(e.X, e.Y);
        }

        // OnKeyDown delete 
        public void HandleKeyDown(object sender, KeyEventArgs e)
        {
            _presentationModel.ProcessKeyDown(e.KeyCode);
            UpdateUndoRedoButton();
        }

        // Button Paint
        public void HandleButtonPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _presentationModel.DrawOnButton(e.Graphics, _smallSlide.Size, _panel1.Size);
        }

        // update view
        public void UpdateView()
        {
            Invalidate(true);
        }

        // undo
        private void ToolStripButtonUndoClick(object sender, EventArgs e)
        {
            _model.Undo();
            ResizeCanvas(sender, e);
            UpdateUndoRedoButton();
        }

        //redo
        private void ToolStripButtonRedoClick(object sender, EventArgs e)
        {
            _model.Redo();
            ResizeCanvas(sender, e);
            UpdateUndoRedoButton();
        }

        // handle window size changed
        private void CanvasSizeChanged(object sender, EventArgs e)
        {
            ResizeCanvas(sender, e);
        }

        // resize canvas
        private void ResizeCanvas(object sender, EventArgs e)
        {
            double panel2Width = _splitContainer2.Panel1.Width;
            double panel2Height = _splitContainer2.Panel1.Height;
            if ((panel2Width - _splitContainer2.Panel1.Margin.Horizontal) / (panel2Height - _splitContainer2.Panel1.Margin.Vertical) > Constants.WINDOWS_RATIO)
            {
                AdjustPanelSizeForHorizontalRatio(panel2Width, panel2Height);
            }
            else
            {
                AdjustPanelSizeForVerticalRatio(panel2Width, panel2Height);    
            }
            if ( _model != null && _panel1.Width != Constants.ZERO && _panel1.Height != Constants.ZERO)
            {
                _model.SetCanvasSize(new Size(_panel1.Width, _panel1.Height));
            }
        }

        //根据水平比例调整面板尺寸的方法
        private void AdjustPanelSizeForHorizontalRatio(double panel2Width, double panel2Height)
        {
            _panel1.Height = (int)panel2Height - _splitContainer2.Panel1.Margin.Vertical;
            _panel1.Width = (int)(_panel1.Height * Constants.WINDOWS_RATIO);
            _panel1.Location = new System.Drawing.Point((int)((panel2Width - _panel1.Width) / Constants.TWO), (int)((panel2Height - _panel1.Height) / Constants.TWO));
        }

        //根据垂直比例调整面板尺寸的方法
        private void AdjustPanelSizeForVerticalRatio(double panel2Width, double panel2Height)
        {
            _panel1.Width = (int)panel2Width - _splitContainer2.Panel1.Margin.Horizontal;
            _panel1.Height = (int)(_panel1.Width / Constants.WINDOWS_RATIO);
            _panel1.Location = new System.Drawing.Point((int)((panel2Width - _panel1.Width) / Constants.TWO), (int)((panel2Height - _panel1.Height) / Constants.TWO));
        }

        // adjust right side (DGV)
        private void AdjustRightSideSplitContainer(object sender, SplitterEventArgs e)
        {
            ResizeCanvas(sender, e);
        }

        // adjust the left side (Button)
        private void AdjustLeftSideSplitContainer(object sender, SplitterEventArgs e)
        {
            double panel1Width = _splitContainer1.Panel1.Width;
            _smallSlide.Width = (int)panel1Width - _splitContainer1.Panel1.Margin.Horizontal;
            _smallSlide.Height = (int)(_smallSlide.Width / Constants.WINDOWS_RATIO);
            _smallSlide.Left = (int)((panel1Width - _smallSlide.Width) / Constants.TWO);
            ResizeCanvas(sender, e);
        }

        // Update Undo Redo
        private void UpdateUndoRedoButton()
        {
            _toolStripButtonRedo.Enabled = _model.IsRedoEnabled;
            _toolStripButtonUndo.Enabled = _model.IsUndoEnabled;
        }

        // Click add slide
        private void toolStripButtonAddNewSlide_Click(object sender, EventArgs e)
        {

        }

    }
}
