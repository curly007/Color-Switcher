namespace Color_Switcher {
    partial class OptionsDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.difficulty1 = new System.Windows.Forms.RadioButton();
            this.difficulty2 = new System.Windows.Forms.RadioButton();
            this.difficulty3 = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.difficulty3);
            this.groupBox1.Controls.Add(this.difficulty2);
            this.groupBox1.Controls.Add(this.difficulty1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(76, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Difficulty";
            // 
            // difficulty1
            // 
            this.difficulty1.AutoSize = true;
            this.difficulty1.Location = new System.Drawing.Point(6, 19);
            this.difficulty1.Name = "difficulty1";
            this.difficulty1.Size = new System.Drawing.Size(48, 17);
            this.difficulty1.TabIndex = 0;
            this.difficulty1.TabStop = true;
            this.difficulty1.Text = "Easy";
            this.difficulty1.UseVisualStyleBackColor = true;
            // 
            // difficulty2
            // 
            this.difficulty2.AutoSize = true;
            this.difficulty2.Location = new System.Drawing.Point(7, 43);
            this.difficulty2.Name = "difficulty2";
            this.difficulty2.Size = new System.Drawing.Size(62, 17);
            this.difficulty2.TabIndex = 1;
            this.difficulty2.TabStop = true;
            this.difficulty2.Text = "Medium";
            this.difficulty2.UseVisualStyleBackColor = true;
            // 
            // difficulty3
            // 
            this.difficulty3.AutoSize = true;
            this.difficulty3.Location = new System.Drawing.Point(7, 67);
            this.difficulty3.Name = "difficulty3";
            this.difficulty3.Size = new System.Drawing.Size(48, 17);
            this.difficulty3.TabIndex = 2;
            this.difficulty3.TabStop = true;
            this.difficulty3.Text = "Hard";
            this.difficulty3.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(13, 120);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(95, 120);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(181, 156);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.optionsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton difficulty3;
        private System.Windows.Forms.RadioButton difficulty2;
        private System.Windows.Forms.RadioButton difficulty1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}