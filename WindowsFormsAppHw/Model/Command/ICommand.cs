using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public interface ICommand
    {
        // command execute
        void DoExecute(Size canvasSize);

        //command unexecute
        void UndoExecute(Size canvasSize);

        // Set Size
        void SetSize(Size canvasSize);

        // AdjustnowSize
        void AdjustNowSize(Size canvasSize);
    }

}
