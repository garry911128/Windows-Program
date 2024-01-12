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
    public partial class DialogForm : Form
    {
        Size _canvasSize;
        Model _model;
        string _shapeName;
        int _top;
        int _left;
        int _right;
        int _bottom;

        public DialogForm()
        {
            InitializeComponent();
        }

        // construct
        public void SetupDialog(Size canvasSize, string shapeName, Model model)
        {
            _canvasSize = canvasSize;
            _model = model;
            _shapeName = shapeName;
            this._buttonOk.Enabled = false;
        }

        // Click Ok
        private void ClickButtonOk(object sender, EventArgs e)
        {
            _model.ExecuteAddCommand(_shapeName, new Point(_left, _top), new Point(_right, _bottom));
            _textBox1.Text = "";
            _textBox2.Text = "";
            _textBox3.Text = "";
            _textBox4.Text = "";
            this.Close();
        }

        // Click Cancel
        private void ClickButtonCancel(object sender, EventArgs e)
        {
            this.Close();
        }

        // change top left X in text box
        private void ChangedTextInTextBox1(object sender, EventArgs e)
        {
            try
            {
                _left = Convert.ToInt32(_textBox1.Text);
            }
            catch (Exception)
            {
                _left = 0;
            }
            NotifyTextBoxChanged();
        }

        // change top left Y in text box
        private void ChangedTextInTextBox2(object sender, EventArgs e)
        {
            try
            {
                _top = Convert.ToInt32(_textBox2.Text);
            }
            catch (Exception)
            {
                _top = 0;
            }
            NotifyTextBoxChanged();
        }

        // change text in bottom right X text box
        private void ChangedTextInTextBox3(object sender, EventArgs e)
        {
            try
            {
                _right = Convert.ToInt32(_textBox3.Text);
            }
            catch (Exception)
            {
                _right = 0;
            }
            NotifyTextBoxChanged();
        }

        // change text in bottom right Y textbox
        private void ChangedTextInTextBox4(object sender, EventArgs e)
        {
            try
            {
                _bottom = Convert.ToInt32(_textBox4.Text);
            }
            catch (Exception)
            {
                _bottom = 0;
            }
            NotifyTextBoxChanged();
        }

        // observer
        private void NotifyTextBoxChanged()
        {
            if (_left >= 0 && _left <= _canvasSize.Width &&
                _top >= 0 && _top <= _canvasSize.Height &&
                _right >= 0 && _right <= _canvasSize.Width &&
                _bottom >= 0 && _bottom <= _canvasSize.Height &&
                _left < _right && _top < _bottom)
            {
                this._buttonOk.Enabled = true;
            }
            else
            {
                this._buttonOk.Enabled = false;
            }
        }

    }
}