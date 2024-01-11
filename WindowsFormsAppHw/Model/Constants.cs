using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppHomework
{
    public class Constants
    {
        public const string LEFT_SMALL_BRACKET = "(";
        public const string RIGHT_SMALL_BRACKET = ")";
        public const string DOT = ",";
        public const string SPACE = " ";
        public const string LEFT_CURLY_BRACKET = "{";
        public const string RIGHT_CURLY_BRACKET = "}";
        public const string SPLASH = "\\";
        public const int TWO = 2;
        public const int EIGHT = 8;
        public const string DELETE = "刪除";
        public const string CHECKED = "Checked";
        public const string IS_LINE_CHECKED = "IsLineButtonChecked";
        public const string IS_RECTANGLE_CHECKED = "IsRectangleButtonChecked";
        public const string IS_CIRCLE_CHECKED = "IsEllipseButtonChecked";
        public const string IS_CURSOR_CHECKED = "IsCursorButtonChecked";
        public const string RECTANGLE = "矩形";
        public const string ELLIPSE = "橢圓形";
        public const string LINE = "線";
        public const string POINTER_MODE_TYPE = "Cursor";
        public const double WINDOWS_RATIO = 16.0 / 9.0;
        public const double ZERO = 0;
        public const string REDO = "redo";
        public const string UNDO = "undo";
        public const string ENABLED = "Enabled";
        public const string IS_REDO_ENABLED = "IsRedoEnabled";
        public const string IS_UNDO_ENABLED = "IsUndoEnabled";
        public const string CAN_NOT_UNDO = "Cannot Undo exception\n";
        public const string CAN_NOT_REDO = "Cannot Redo exception\n";
        public const string TEXT_PLAIN = "text/plain";
        public const char EOL = '\n';
        public enum ToolStripButtonType
        {
            LINE,
            RECTANGLE,
            CIRCLE,
            Arrow,
            UNDO,
            REDO
        }
    }
}
