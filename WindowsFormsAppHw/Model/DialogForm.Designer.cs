
namespace WindowsFormsAppHomework
{
    partial class DialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._label1 = new System.Windows.Forms.Label();
            this._label2 = new System.Windows.Forms.Label();
            this._label3 = new System.Windows.Forms.Label();
            this._label4 = new System.Windows.Forms.Label();
            this._textBox1 = new System.Windows.Forms.TextBox();
            this._textBox2 = new System.Windows.Forms.TextBox();
            this._textBox3 = new System.Windows.Forms.TextBox();
            this._textBox4 = new System.Windows.Forms.TextBox();
            this._buttonOk = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _label1
            // 
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point(59, 57);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(92, 15);
            this._label1.TabIndex = 0;
            this._label1.Text = "左上角座標X";
            // 
            // _label2
            // 
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(268, 57);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(92, 15);
            this._label2.TabIndex = 1;
            this._label2.Text = "左上角座標Y";
            // 
            // _label3
            // 
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(59, 180);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(92, 15);
            this._label3.TabIndex = 2;
            this._label3.Text = "右下角座標X";
            // 
            // _label4
            // 
            this._label4.AutoSize = true;
            this._label4.Location = new System.Drawing.Point(268, 180);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(92, 15);
            this._label4.TabIndex = 3;
            this._label4.Text = "右下角座標Y";
            // 
            // _textBox1
            // 
            this._textBox1.Location = new System.Drawing.Point(62, 90);
            this._textBox1.Name = "_textBox1";
            this._textBox1.Size = new System.Drawing.Size(100, 25);
            this._textBox1.TabIndex = 4;
            this._textBox1.TextChanged += new System.EventHandler(this.ChangedTextInTextBox1);
            // 
            // _textBox2
            // 
            this._textBox2.Location = new System.Drawing.Point(271, 90);
            this._textBox2.Name = "_textBox2";
            this._textBox2.Size = new System.Drawing.Size(100, 25);
            this._textBox2.TabIndex = 5;
            this._textBox2.TextChanged += new System.EventHandler(this.ChangedTextInTextBox2);
            // 
            // _textBox3
            // 
            this._textBox3.Location = new System.Drawing.Point(62, 211);
            this._textBox3.Name = "_textBox3";
            this._textBox3.Size = new System.Drawing.Size(100, 25);
            this._textBox3.TabIndex = 6;
            this._textBox3.TextChanged += new System.EventHandler(this.ChangedTextInTextBox3);
            // 
            // _textBox4
            // 
            this._textBox4.Location = new System.Drawing.Point(271, 211);
            this._textBox4.Name = "_textBox4";
            this._textBox4.Size = new System.Drawing.Size(100, 25);
            this._textBox4.TabIndex = 7;
            this._textBox4.TextChanged += new System.EventHandler(this.ChangedTextInTextBox4);
            // 
            // _buttonOk
            // 
            this._buttonOk.Location = new System.Drawing.Point(62, 272);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 23);
            this._buttonOk.TabIndex = 8;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._buttonOk.Click += new System.EventHandler(this.ClickButtonOk);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Location = new System.Drawing.Point(271, 272);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 9;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.ClickButtonCancel);
            // 
            // DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 312);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._textBox4);
            this.Controls.Add(this._textBox3);
            this.Controls.Add(this._textBox2);
            this.Controls.Add(this._textBox1);
            this.Controls.Add(this._label4);
            this.Controls.Add(this._label3);
            this.Controls.Add(this._label2);
            this.Controls.Add(this._label1);
            this.Name = "DialogForm";
            this.Text = "DialogForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _label1;
        private System.Windows.Forms.Label _label2;
        private System.Windows.Forms.Label _label3;
        private System.Windows.Forms.Label _label4;
        private System.Windows.Forms.TextBox _textBox1;
        private System.Windows.Forms.TextBox _textBox2;
        private System.Windows.Forms.TextBox _textBox3;
        private System.Windows.Forms.TextBox _textBox4;
        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.Button _buttonCancel;
    }
}