namespace UV_Quant
{
    partial class OxyParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OxyParameters));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCommandFile = new System.Windows.Forms.Button();
            this.txtCommandInterval = new System.Windows.Forms.TextBox();
            this.txtCommandFile = new System.Windows.Forms.TextBox();
            this.lblcf = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkCommandProcess = new System.Windows.Forms.CheckBox();
            this.txtCollectInterval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHighPer = new System.Windows.Forms.TextBox();
            this.txtLowPer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.openCommandFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnCommandFile);
            this.groupBox1.Controls.Add(this.txtCommandInterval);
            this.groupBox1.Controls.Add(this.txtCommandFile);
            this.groupBox1.Controls.Add(this.lblcf);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.chkCommandProcess);
            this.groupBox1.Controls.Add(this.txtCollectInterval);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtHighPer);
            this.groupBox1.Controls.Add(this.txtLowPer);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkEnabled);
            this.groupBox1.Controls.Add(this.txtDuration);
            this.groupBox1.Controls.Add(this.txtInterval);
            this.groupBox1.Controls.Add(this.txtEnd);
            this.groupBox1.Controls.Add(this.txtStart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 236);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // btnCommandFile
            // 
            this.btnCommandFile.Location = new System.Drawing.Point(455, 81);
            this.btnCommandFile.Name = "btnCommandFile";
            this.btnCommandFile.Size = new System.Drawing.Size(30, 20);
            this.btnCommandFile.TabIndex = 22;
            this.btnCommandFile.Text = "...";
            this.btnCommandFile.UseVisualStyleBackColor = true;
            this.btnCommandFile.Click += new System.EventHandler(this.btnCommandFile_Click);
            // 
            // txtCommandInterval
            // 
            this.txtCommandInterval.Location = new System.Drawing.Point(358, 107);
            this.txtCommandInterval.Name = "txtCommandInterval";
            this.txtCommandInterval.Size = new System.Drawing.Size(90, 20);
            this.txtCommandInterval.TabIndex = 21;
            // 
            // txtCommandFile
            // 
            this.txtCommandFile.Location = new System.Drawing.Point(358, 81);
            this.txtCommandFile.Name = "txtCommandFile";
            this.txtCommandFile.Size = new System.Drawing.Size(90, 20);
            this.txtCommandFile.TabIndex = 20;
            // 
            // lblcf
            // 
            this.lblcf.AutoSize = true;
            this.lblcf.Location = new System.Drawing.Point(255, 84);
            this.lblcf.Name = "lblcf";
            this.lblcf.Size = new System.Drawing.Size(98, 13);
            this.lblcf.TabIndex = 19;
            this.lblcf.Text = "Command File Path";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(228, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Command Read Interval";
            // 
            // chkCommandProcess
            // 
            this.chkCommandProcess.AutoSize = true;
            this.chkCommandProcess.Location = new System.Drawing.Point(284, 19);
            this.chkCommandProcess.Name = "chkCommandProcess";
            this.chkCommandProcess.Size = new System.Drawing.Size(164, 17);
            this.chkCommandProcess.TabIndex = 17;
            this.chkCommandProcess.Text = "Enable Command Processing";
            this.chkCommandProcess.UseVisualStyleBackColor = true;
            this.chkCommandProcess.CheckedChanged += new System.EventHandler(this.chkCommandProcess_CheckedChanged);
            // 
            // txtCollectInterval
            // 
            this.txtCollectInterval.Location = new System.Drawing.Point(112, 133);
            this.txtCollectInterval.Name = "txtCollectInterval";
            this.txtCollectInterval.Size = new System.Drawing.Size(90, 20);
            this.txtCollectInterval.TabIndex = 16;
            this.txtCollectInterval.TextChanged += new System.EventHandler(this.txtCollectInterval_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Collection Interval";
            // 
            // txtHighPer
            // 
            this.txtHighPer.Location = new System.Drawing.Point(112, 185);
            this.txtHighPer.Name = "txtHighPer";
            this.txtHighPer.Size = new System.Drawing.Size(90, 20);
            this.txtHighPer.TabIndex = 14;
            // 
            // txtLowPer
            // 
            this.txtLowPer.Location = new System.Drawing.Point(112, 159);
            this.txtLowPer.Name = "txtLowPer";
            this.txtLowPer.Size = new System.Drawing.Size(90, 20);
            this.txtLowPer.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Low Reference %";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "High Reference %";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(68, 19);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(136, 17);
            this.chkEnabled.TabIndex = 10;
            this.chkEnabled.Text = "Enable Auto Calibration";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(113, 107);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(90, 20);
            this.txtDuration.TabIndex = 9;
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(113, 81);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(90, 20);
            this.txtInterval.TabIndex = 8;
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(358, 53);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(90, 20);
            this.txtEnd.TabIndex = 7;
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(114, 53);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(90, 20);
            this.txtStart.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Calibration Interval";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Calibration duration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "End Pixel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Pixel";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(126, 254);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(26, 254);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(89, 35);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // openCommandFile
            // 
            this.openCommandFile.FileName = "openFileDialog1";
            // 
            // OxyParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UV_Quant.Properties.Resources.gradient;
            this.ClientSize = new System.Drawing.Size(510, 302);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OxyParameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Calibration Parameters";
            this.Load += new System.EventHandler(this.OxyParameters_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtHighPer;
        private System.Windows.Forms.TextBox txtLowPer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCollectInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkCommandProcess;
        private System.Windows.Forms.TextBox txtCommandInterval;
        private System.Windows.Forms.TextBox txtCommandFile;
        private System.Windows.Forms.Label lblcf;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCommandFile;
        private System.Windows.Forms.OpenFileDialog openCommandFile;
    }
}