using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UV_Quant
{
    public partial class PasswordInquiry : Form
    {
        public PasswordInquiry()
        {
            InitializeComponent();
        }

        private void PasswordInquiry_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        public String getPassword()
        {
            return txtPassword.Text;
        }

    }
}