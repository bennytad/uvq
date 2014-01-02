namespace UV_Quant
{
    partial class SampleTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleTime));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.opt1Min = new System.Windows.Forms.RadioButton();
            this.opt5Min = new System.Windows.Forms.RadioButton();
            this.opt30Sec = new System.Windows.Forms.RadioButton();
            this.optInst = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(80, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(12, 221);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 24);
            this.button2.TabIndex = 2;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.opt1Min);
            this.groupBox1.Controls.Add(this.opt5Min);
            this.groupBox1.Controls.Add(this.opt30Sec);
            this.groupBox1.Controls.Add(this.optInst);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 194);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Averages";
            // 
            // opt1Min
            // 
            this.opt1Min.AutoSize = true;
            this.opt1Min.Location = new System.Drawing.Point(35, 121);
            this.opt1Min.Name = "opt1Min";
            this.opt1Min.Size = new System.Drawing.Size(54, 17);
            this.opt1Min.TabIndex = 4;
            this.opt1Min.TabStop = true;
            this.opt1Min.Text = "1 Min.";
            this.opt1Min.UseVisualStyleBackColor = true;
            this.opt1Min.CheckedChanged += new System.EventHandler(this.opt1Min_CheckedChanged);
            // 
            // opt5Min
            // 
            this.opt5Min.AutoSize = true;
            this.opt5Min.Location = new System.Drawing.Point(35, 161);
            this.opt5Min.Name = "opt5Min";
            this.opt5Min.Size = new System.Drawing.Size(54, 17);
            this.opt5Min.TabIndex = 3;
            this.opt5Min.TabStop = true;
            this.opt5Min.Text = "5 Min.";
            this.opt5Min.UseVisualStyleBackColor = true;
            this.opt5Min.CheckedChanged += new System.EventHandler(this.opt5Min_CheckedChanged);
            // 
            // opt30Sec
            // 
            this.opt30Sec.AutoSize = true;
            this.opt30Sec.Location = new System.Drawing.Point(35, 81);
            this.opt30Sec.Name = "opt30Sec";
            this.opt30Sec.Size = new System.Drawing.Size(59, 17);
            this.opt30Sec.TabIndex = 2;
            this.opt30Sec.TabStop = true;
            this.opt30Sec.Text = "30 Sec";
            this.opt30Sec.UseVisualStyleBackColor = true;
            this.opt30Sec.CheckedChanged += new System.EventHandler(this.opt30Sec_CheckedChanged);
            // 
            // optInst
            // 
            this.optInst.AutoSize = true;
            this.optInst.Location = new System.Drawing.Point(35, 42);
            this.optInst.Name = "optInst";
            this.optInst.Size = new System.Drawing.Size(57, 17);
            this.optInst.TabIndex = 1;
            this.optInst.TabStop = true;
            this.optInst.Text = "Instant";
            this.optInst.UseVisualStyleBackColor = true;
            this.optInst.CheckedChanged += new System.EventHandler(this.optInst_CheckedChanged);
            // 
            // SampleTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UV_Quant.Properties.Resources.gradient;
            this.ClientSize = new System.Drawing.Size(181, 265);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SampleTime";
            this.Text = "SampleTime";
            this.Load += new System.EventHandler(this.SampleTime_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton opt1Min;
        private System.Windows.Forms.RadioButton opt5Min;
        private System.Windows.Forms.RadioButton opt30Sec;
        private System.Windows.Forms.RadioButton optInst;
    }
}