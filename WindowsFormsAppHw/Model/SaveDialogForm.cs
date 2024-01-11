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
    public partial class SaveDialogForm : Form
    {
        Model _model;

        public SaveDialogForm(Model model)
        {
            InitializeComponent();
            _model = model;
        }

        // handle button save click
        private void ClickSaveButton(object sender, EventArgs e)
        {
            _model.HandleSave();
            Close();
        }

        // handle button cancel click
        private void ClickCancelButton(object sender, EventArgs e)
        {
            Close();
        }

    }
}
