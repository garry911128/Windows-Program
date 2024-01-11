using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppHomework
{
    public partial class LoadDialogForm : Form
    {
        Model _model;
        public LoadDialogForm(Model model)
        {
            InitializeComponent();
            _model = model;
        }

        // click load
        private void ClickLoadbutton(object sender, EventArgs e)
        {
            _model.HandleLoad();
            Close();
        }

        // click cancel
        private void ClickCancelbutton(object sender, EventArgs e)
        {
            Close();
        }
    }
}
