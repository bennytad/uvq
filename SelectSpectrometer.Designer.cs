namespace UV_Quant
{
    partial class SelectSpectrometer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectSpectrometer));
            this.lstType = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstSn = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSelectSpectrometer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstType
            // 
            this.lstType.FormattingEnabled = true;
            this.lstType.Location = new System.Drawing.Point(6, 45);
            this.lstType.Name = "lstType";
            this.lstType.Size = new System.Drawing.Size(113, 173);
            this.lstType.TabIndex = 0;
            this.lstType.SelectedIndexChanged += new System.EventHandler(this.lstType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lstSn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdSelectSpectrometer);
            this.groupBox1.Controls.Add(this.lstType);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 231);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Spectrometers";
            // 
            // lstSn
            // 
            this.lstSn.FormattingEnabled = true;
            this.lstSn.Location = new System.Drawing.Point(125, 44);
            this.lstSn.Name = "lstSn";
            this.lstSn.Size = new System.Drawing.Size(116, 173);
            this.lstSn.TabIndex = 3;
            this.lstSn.SelectedIndexChanged += new System.EventHandler(this.lstSn_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Spectrometer                    SN";
            // 
            // cmdSelectSpectrometer
            // 
            this.cmdSelectSpectrometer.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSelectSpectrometer.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSelectSpectrometer.Image = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.cmdSelectSpectrometer.Location = new System.Drawing.Point(247, 55);
            this.cmdSelectSpectrometer.Name = "cmdSelectSpectrometer";
            this.cmdSelectSpectrometer.Size = new System.Drawing.Size(69, 142);
            this.cmdSelectSpectrometer.TabIndex = 1;
            this.cmdSelectSpectrometer.Text = "OK";
            this.cmdSelectSpectrometer.UseVisualStyleBackColor = true;
            this.cmdSelectSpectrometer.Click += new System.EventHandler(this.cmdSelectSpectrometer_Click);
            // 
            // SelectSpectrometer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UV_Quant.Properties.Resources.gradient;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(345, 266);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectSpectrometer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Spectrometer";
            this.Load += new System.EventHandler(this.SelectSpectrometer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSelectSpectrometer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstSn;
    }
}