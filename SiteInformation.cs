using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UV_Quant
{
    public partial class SiteInformation : Form
    {

        private string site_name = null;
        private string operator_name = null;
        private double path_length = -1;
        private int hardware_type = 1;

        public SiteInformation()
        {
            InitializeComponent();
        }

        private void SiteInformation_Load(object sender, EventArgs e)
        {
            site_name = Util.getStringInRegistry(Constants.UVQuantRegistryKeys.SITE_NAME, "");
            operator_name = Util.getStringInRegistry(Constants.UVQuantRegistryKeys.USER_NAME, "");
            path_length = Util.getDoubleInRegistry(Constants.UVQuantRegistryKeys.PATH_LENGTH, -1);
            hardware_type = Util.getIntInRegistry(Constants.UVQuantRegistryKeys.HARDWARE_TYPE, 1);

            txtSiteName.Text = site_name;
            txtUserName.Text = operator_name;
            txtPathLength.Text = path_length.ToString();

            if (hardware_type != Constants.HardWareType.OPEN_PATH)
            {
                txtPathLength.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                Util.saveValueInRegistry(txtSiteName.Text, Constants.UVQuantRegistryKeys.SITE_NAME);
                Util.saveValueInRegistry(txtUserName.Text, Constants.UVQuantRegistryKeys.USER_NAME);

                if (txtPathLength.Text.Trim() != "0.0")
                {
                    Util.saveValueInRegistry(Double.Parse(txtPathLength.Text.Trim()), Constants.UVQuantRegistryKeys.PATH_LENGTH);
                }
            }
            else
            {
                MessageBox.Show("That was a valid site information!");
            }
        }

        private bool validated()
        {
            if (txtPathLength.Text == null ||
                txtPathLength.Text.Trim() == "")
            {
                txtPathLength.Text = "0.0";
                return true;
            }

            if (Util.isNumeric(txtPathLength.Text.Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}