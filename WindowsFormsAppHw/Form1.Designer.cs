
namespace WindowsFormsAppHomework
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this._dataGridViewRight = new System.Windows.Forms.DataGridView();
            this._deleteObject = new System.Windows.Forms.DataGridViewButtonColumn();
            this._shapeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._smallSlide = new System.Windows.Forms.Button();
            this._comboBoxShapeType = new System.Windows.Forms.ComboBox();
            this._backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this._buttonAdd = new System.Windows.Forms.Button();
            this._groupBoxRight = new System.Windows.Forms.GroupBox();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._toolStripButtonLine = new WindowsFormsAppHomework.BindingToolStripButton();
            this._toolStripButtonRectangle = new WindowsFormsAppHomework.BindingToolStripButton();
            this._toolStripButtonEllipse = new WindowsFormsAppHomework.BindingToolStripButton();
            this._toolStripButtonCursor = new WindowsFormsAppHomework.BindingToolStripButton();
            this._toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this._toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this._splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._panel1 = new WindowsFormsAppHomework.DoubleBufferedPanel();
            this.toolStripButtonAddNewSlide = new System.Windows.Forms.ToolStripButton();
            this._menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewRight)).BeginInit();
            this._groupBoxRight.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).BeginInit();
            this._splitContainer1.Panel1.SuspendLayout();
            this._splitContainer1.Panel2.SuspendLayout();
            this._splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).BeginInit();
            this._splitContainer2.Panel1.SuspendLayout();
            this._splitContainer2.Panel2.SuspendLayout();
            this._splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contextMenuStrip1
            // 
            this._contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._contextMenuStrip1.Name = "contextMenuStrip1";
            this._contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // _menuStrip1
            // 
            this._menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItem1});
            this._menuStrip1.Location = new System.Drawing.Point(0, 0);
            this._menuStrip1.Name = "_menuStrip1";
            this._menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this._menuStrip1.Size = new System.Drawing.Size(1328, 30);
            this._menuStrip1.TabIndex = 0;
            this._menuStrip1.Text = "menuStrip1";
            // 
            // _toolStripMenuItem1
            // 
            this._toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItem2});
            this._toolStripMenuItem1.Name = "_toolStripMenuItem1";
            this._toolStripMenuItem1.Size = new System.Drawing.Size(53, 26);
            this._toolStripMenuItem1.Text = "說明";
            // 
            // _toolStripMenuItem2
            // 
            this._toolStripMenuItem2.Name = "_toolStripMenuItem2";
            this._toolStripMenuItem2.Size = new System.Drawing.Size(122, 26);
            this._toolStripMenuItem2.Text = "關於";
            // 
            // _dataGridViewRight
            // 
            this._dataGridViewRight.AllowUserToAddRows = false;
            this._dataGridViewRight.AllowUserToResizeColumns = false;
            this._dataGridViewRight.AllowUserToResizeRows = false;
            this._dataGridViewRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewRight.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._deleteObject,
            this._shapeType,
            this._location});
            this._dataGridViewRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._dataGridViewRight.Location = new System.Drawing.Point(3, 57);
            this._dataGridViewRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._dataGridViewRight.Name = "_dataGridViewRight";
            this._dataGridViewRight.ReadOnly = true;
            this._dataGridViewRight.RowHeadersVisible = false;
            this._dataGridViewRight.RowHeadersWidth = 51;
            this._dataGridViewRight.RowTemplate.Height = 27;
            this._dataGridViewRight.Size = new System.Drawing.Size(221, 548);
            this._dataGridViewRight.TabIndex = 2;
            this._dataGridViewRight.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClickDataGridViewRightCellContent);
            // 
            // _deleteObject
            // 
            this._deleteObject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._deleteObject.HeaderText = "刪除";
            this._deleteObject.MinimumWidth = 6;
            this._deleteObject.Name = "_deleteObject";
            this._deleteObject.ReadOnly = true;
            this._deleteObject.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._deleteObject.UseColumnTextForButtonValue = true;
            // 
            // _shapeType
            // 
            this._shapeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._shapeType.HeaderText = "形狀";
            this._shapeType.MinimumWidth = 6;
            this._shapeType.Name = "_shapeType";
            this._shapeType.ReadOnly = true;
            this._shapeType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._shapeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _location
            // 
            this._location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._location.HeaderText = "資訊";
            this._location.MinimumWidth = 6;
            this._location.Name = "_location";
            this._location.ReadOnly = true;
            this._location.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._location.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _smallSlide
            // 
            this._smallSlide.Location = new System.Drawing.Point(4, 24);
            this._smallSlide.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._smallSlide.Name = "_smallSlide";
            this._smallSlide.Size = new System.Drawing.Size(99, 68);
            this._smallSlide.TabIndex = 4;
            this._smallSlide.UseVisualStyleBackColor = true;
            // 
            // _comboBoxShapeType
            // 
            this._comboBoxShapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxShapeType.FormattingEnabled = true;
            this._comboBoxShapeType.Items.AddRange(new object[] {
            "矩形",
            "線",
            "橢圓形"});
            this._comboBoxShapeType.Location = new System.Drawing.Point(100, 22);
            this._comboBoxShapeType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._comboBoxShapeType.Name = "_comboBoxShapeType";
            this._comboBoxShapeType.Size = new System.Drawing.Size(121, 23);
            this._comboBoxShapeType.TabIndex = 6;
            // 
            // _buttonAdd
            // 
            this._buttonAdd.Location = new System.Drawing.Point(5, 24);
            this._buttonAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._buttonAdd.Name = "_buttonAdd";
            this._buttonAdd.Size = new System.Drawing.Size(75, 22);
            this._buttonAdd.TabIndex = 7;
            this._buttonAdd.Text = "新增";
            this._buttonAdd.UseVisualStyleBackColor = true;
            this._buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
            // 
            // _groupBoxRight
            // 
            this._groupBoxRight.Controls.Add(this._comboBoxShapeType);
            this._groupBoxRight.Controls.Add(this._buttonAdd);
            this._groupBoxRight.Controls.Add(this._dataGridViewRight);
            this._groupBoxRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBoxRight.Location = new System.Drawing.Point(0, 0);
            this._groupBoxRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._groupBoxRight.Name = "_groupBoxRight";
            this._groupBoxRight.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._groupBoxRight.Size = new System.Drawing.Size(227, 607);
            this._groupBoxRight.TabIndex = 8;
            this._groupBoxRight.TabStop = false;
            this._groupBoxRight.Text = "groupBox1";
            // 
            // _toolStrip1
            // 
            this._toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripButtonLine,
            this._toolStripButtonRectangle,
            this._toolStripButtonEllipse,
            this._toolStripButtonCursor,
            this.toolStripButtonAddNewSlide,
            this._toolStripButtonUndo,
            this._toolStripButtonRedo});
            this._toolStrip1.Location = new System.Drawing.Point(0, 30);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(1328, 31);
            this._toolStrip1.TabIndex = 9;
            this._toolStrip1.Text = "toolStrip1";
            // 
            // _toolStripButtonLine
            // 
            this._toolStripButtonLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonLine.Image = global::WindowsFormsAppHomework.Properties.Resources.Line;
            this._toolStripButtonLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonLine.Name = "_toolStripButtonLine";
            this._toolStripButtonLine.Size = new System.Drawing.Size(29, 28);
            this._toolStripButtonLine.Text = "線";
            // 
            // _toolStripButtonRectangle
            // 
            this._toolStripButtonRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonRectangle.Image = global::WindowsFormsAppHomework.Properties.Resources.Rectangle;
            this._toolStripButtonRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonRectangle.Name = "_toolStripButtonRectangle";
            this._toolStripButtonRectangle.Size = new System.Drawing.Size(29, 28);
            this._toolStripButtonRectangle.Text = "矩形";
            // 
            // _toolStripButtonEllipse
            // 
            this._toolStripButtonEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonEllipse.Image = global::WindowsFormsAppHomework.Properties.Resources.Ellipse;
            this._toolStripButtonEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonEllipse.Name = "_toolStripButtonEllipse";
            this._toolStripButtonEllipse.Size = new System.Drawing.Size(29, 28);
            this._toolStripButtonEllipse.Text = "橢圓形";
            // 
            // _toolStripButtonCursor
            // 
            this._toolStripButtonCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonCursor.Image = global::WindowsFormsAppHomework.Properties.Resources.Cursor;
            this._toolStripButtonCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonCursor.Name = "_toolStripButtonCursor";
            this._toolStripButtonCursor.Size = new System.Drawing.Size(29, 28);
            this._toolStripButtonCursor.Text = "Cursor";
            this._toolStripButtonCursor.Click += new System.EventHandler(this.ToolStripButtonCursorClick);
            // 
            // _toolStripButtonUndo
            // 
            this._toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonUndo.Image")));
            this._toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonUndo.Name = "_toolStripButtonUndo";
            this._toolStripButtonUndo.Size = new System.Drawing.Size(29, 28);
            this._toolStripButtonUndo.Text = "toolStripButtonUndo";
            this._toolStripButtonUndo.Click += new System.EventHandler(this.ToolStripButtonUndoClick);
            // 
            // _toolStripButtonRedo
            // 
            this._toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripButtonRedo.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonRedo.Image")));
            this._toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonRedo.Name = "_toolStripButtonRedo";
            this._toolStripButtonRedo.Size = new System.Drawing.Size(29, 28);
            this._toolStripButtonRedo.Text = "toolStripButtonRedo";
            this._toolStripButtonRedo.Click += new System.EventHandler(this.ToolStripButtonRedoClick);
            // 
            // _splitContainer1
            // 
            this._splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer1.Location = new System.Drawing.Point(0, 61);
            this._splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._splitContainer1.Name = "_splitContainer1";
            // 
            // _splitContainer1.Panel1
            // 
            this._splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._splitContainer1.Panel1.Controls.Add(this._smallSlide);
            // 
            // _splitContainer1.Panel2
            // 
            this._splitContainer1.Panel2.Controls.Add(this._splitContainer2);
            this._splitContainer1.Size = new System.Drawing.Size(1328, 607);
            this._splitContainer1.SplitterDistance = 106;
            this._splitContainer1.TabIndex = 11;
            this._splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.AdjustLeftSideSplitContainer);
            // 
            // _splitContainer2
            // 
            this._splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._splitContainer2.Location = new System.Drawing.Point(0, 0);
            this._splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._splitContainer2.Name = "_splitContainer2";
            // 
            // _splitContainer2.Panel1
            // 
            this._splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this._splitContainer2.Panel1.Controls.Add(this._panel1);
            // 
            // _splitContainer2.Panel2
            // 
            this._splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._splitContainer2.Panel2.Controls.Add(this._groupBoxRight);
            this._splitContainer2.Size = new System.Drawing.Size(1218, 607);
            this._splitContainer2.SplitterDistance = 987;
            this._splitContainer2.TabIndex = 0;
            this._splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.AdjustRightSideSplitContainer);
            // 
            // _panel1
            // 
            this._panel1.Location = new System.Drawing.Point(3, 2);
            this._panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(800, 450);
            this._panel1.TabIndex = 10;
            // 
            // toolStripButtonAddNewSlide
            // 
            this.toolStripButtonAddNewSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddNewSlide.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddNewSlide.Image")));
            this.toolStripButtonAddNewSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddNewSlide.Name = "toolStripButtonAddNewSlide";
            this.toolStripButtonAddNewSlide.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonAddNewSlide.Text = "toolStripButtonAddNewSlide";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 668);
            this.Controls.Add(this._splitContainer1);
            this.Controls.Add(this._toolStrip1);
            this.Controls.Add(this._menuStrip1);
            this.MainMenuStrip = this._menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "HW";
            this._menuStrip1.ResumeLayout(false);
            this._menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewRight)).EndInit();
            this._groupBoxRight.ResumeLayout(false);
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._splitContainer1.Panel1.ResumeLayout(false);
            this._splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).EndInit();
            this._splitContainer1.ResumeLayout(false);
            this._splitContainer2.Panel1.ResumeLayout(false);
            this._splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).EndInit();
            this._splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip1;
        private System.Windows.Forms.MenuStrip _menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItem1;
        private System.Windows.Forms.DataGridView _dataGridViewRight;
        private System.Windows.Forms.Button _smallSlide;
        private System.Windows.Forms.ComboBox _comboBoxShapeType;
        private System.ComponentModel.BackgroundWorker _backgroundWorker1;
        private System.Windows.Forms.Button _buttonAdd;
        private System.Windows.Forms.GroupBox _groupBoxRight;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItem2;
        private System.Windows.Forms.DataGridViewButtonColumn _deleteObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shapeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _location;
        private System.Windows.Forms.ToolStrip _toolStrip1;
        private WindowsFormsAppHomework.BindingToolStripButton _toolStripButtonRectangle;
        private WindowsFormsAppHomework.BindingToolStripButton _toolStripButtonEllipse;
        private WindowsFormsAppHomework.BindingToolStripButton _toolStripButtonLine;
        private WindowsFormsAppHomework.BindingToolStripButton _toolStripButtonCursor;
        private WindowsFormsAppHomework.DoubleBufferedPanel _panel1;
        private System.Windows.Forms.SplitContainer _splitContainer1;
        private System.Windows.Forms.SplitContainer _splitContainer2;
        private System.Windows.Forms.ToolStripButton _toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton _toolStripButtonRedo;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddNewSlide;
    }
}

