using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppHomework
{
    public class BindingToolStripButton : ToolStripButton, IBindableComponent
    {
        public BindingToolStripButton()
        {
            _dataBindings = new ControlBindingsCollection(this);
            _bindingContext = new BindingContext();
        }
        private ControlBindingsCollection _dataBindings;
        private BindingContext _bindingContext;
        public ControlBindingsCollection DataBindings
        {
            get
            {
                return _dataBindings;
            }
        }
        public BindingContext BindingContext
        {
            get
            {
                return _bindingContext;
            }
            set
            {
                _bindingContext = value;
            }
        }
    }
}