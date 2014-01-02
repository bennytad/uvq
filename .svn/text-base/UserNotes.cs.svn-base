using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UV_Quant
{
    public partial class UserNotes : Form
    {

        public UserNotes()
        {
            InitializeComponent();
        }

        private void userNotes_Load(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                string user_notes = (string)regKey.GetValue("user_notes", "");
                txtUserNotes.Text = user_notes;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                regKey.SetValue("user_notes", txtUserNotes.Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}