namespace IntervalTimer
{
    partial class IntervalTimerForm
    {
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnTimerAction = new System.Windows.Forms.Button();
            this.btnSaveBreak = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveBreak);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.btnTimerAction);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 104);
            this.panel1.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(169, 65);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(45, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(16, 65);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(12, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(58, 24);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "TIME";
            // 
            // btnTimerAction
            // 
            this.btnTimerAction.Location = new System.Drawing.Point(16, 36);
            this.btnTimerAction.Name = "btnTimerAction";
            this.btnTimerAction.Size = new System.Drawing.Size(111, 23);
            this.btnTimerAction.TabIndex = 0;
            this.btnTimerAction.Text = "Timer Toggle";
            this.btnTimerAction.UseVisualStyleBackColor = true;
            this.btnTimerAction.Click += new System.EventHandler(this.btnTimerAction_Click);
            // 
            // btnSaveBreak
            // 
            this.btnSaveBreak.Location = new System.Drawing.Point(139, 12);
            this.btnSaveBreak.Name = "btnSaveBreak";
            this.btnSaveBreak.Size = new System.Drawing.Size(75, 23);
            this.btnSaveBreak.TabIndex = 4;
            this.btnSaveBreak.Text = "Save Break";
            this.btnSaveBreak.UseVisualStyleBackColor = true;
            this.btnSaveBreak.Click += new System.EventHandler(this.btnSaveBreak_Click);
            // 
            // IntervalTimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkMagenta;
            this.ClientSize = new System.Drawing.Size(230, 104);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IntervalTimerForm";
            this.Text = "Interval Timer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTimerAction;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSaveBreak;
    }
}

