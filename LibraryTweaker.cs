using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;

namespace UV_Quant
{
    public partial class LibraryTweaker : Form
    {
        LibraryMatrix benzene_lib;
        LibraryMatrix ammonia_lib;
        LibraryMatrix xenon_so2;
        LibraryMatrix det_so2;
        LibraryMatrix no2;
        LibraryMatrix no;
        LibraryMatrix pxyl;
        LibraryMatrix tol;
        LibraryMatrix napthalene;
        LibraryMatrix cs2;
        LibraryMatrix ozone;
        
        public string temp_concetrations;
        public string temp_pixel_shifts;

        //public LibraryTweaker(LibraryMatrix benzene_library, LibraryMatrix ammonia_library)
        public LibraryTweaker(LibraryMatrix[] matrices)
        {
            for (int i = 0; i < matrices.Length; ++i)
            {
                if ("Benzene" == matrices[i].library_name)
                {
                    benzene_lib = matrices[i];
                }
                else if ("Ammonia" == matrices[i].library_name)
                {
                    ammonia_lib = matrices[i];
                }
                else if ("Raw SO2 - Dt" == matrices[i].library_name)
                {
                    det_so2 = matrices[i];
                }
                else if ("Raw SO2 - Xe" == matrices[i].library_name)
                {
                    xenon_so2 = matrices[i];
                }
                else if ("Napthalene" == matrices[i].library_name)
                {
                    napthalene = matrices[i];
                }
                else if ("Toluene" == matrices[i].library_name)
                {
                    tol = matrices[i];
                }
                else if ("NO2" == matrices[i].library_name)
                {
                    no2 = matrices[i];
                }
                else if ("pXylene" == matrices[i].library_name)
                {
                    pxyl = matrices[i];
                }
                else if ("NO" == matrices[i].library_name)
                {
                    no = matrices[i];
                }
                else if ("CS2" == matrices[i].library_name)
                {
                    cs2 = matrices[i];
                }
                else if ("Ozone" == matrices[i].library_name)
                {
                    ozone = matrices[i];
                }
            }
            
            InitializeComponent();
        }

        private void LibraryTweaker_Load(object sender, EventArgs e)
        {

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            string concentration_factors = (string)regKey.GetValue("concentration_factors", null);
            if (concentration_factors != null)
            {
                string[] pairs = concentration_factors.Split('|');
                if (pairs != null)
                {
                    for (int i = 0; i < pairs.Length; ++i)
                    {
                        string[] values = pairs[i].Split('&');
                        if (values != null && values.Length == 2)
                        {
                            if ((string)values[0] == "Benzene_Benzene")
                            {
                                txtBenzeneConc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Ammonia_Ammonia")
                            {
                                txtAmmoniaConc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Raw SO2 - Dt_SO2")
                            {
                                txtDtSo2Conc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Raw SO2 - Xe_SO2")
                            {
                                txtXenSo2Conc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Napthalene_Napthalene")
                            {
                                txtHgConc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Toluene_Toluene")
                            {
                                txtTolConc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "NO2_NO2")
                            {
                                txtButConc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "pXylene_pXylene")
                            {
                                txtPxyConc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "NO_NO")
                            {
                                txtNOConc.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "CS2_CS2")
                            {
                                txtCS2ConcFactor.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Ozone_Ozone")
                            {
                                txtOzoneConcFactor.Text = (string)values[1];
                            }
                        }
                    }
                }
            }

            if (txtBenzeneConc.Text.Trim() == "")
            {
                txtBenzeneConc.Text = benzene_lib.getConcentration("Benzene").ToString();
            }

            if (txtAmmoniaConc.Text.Trim() == "")
            {
                txtAmmoniaConc.Text = ammonia_lib.getConcentration("Ammonia").ToString();
            }

            if (txtDtSo2Conc.Text.Trim() == "")
            {
                txtDtSo2Conc.Text = det_so2.getConcentration("SO2").ToString();
            }

            if (txtXenSo2Conc.Text.Trim() == "")
            {
                txtXenSo2Conc.Text = xenon_so2.getConcentration("SO2").ToString();
            }

            if (txtHgConc.Text.Trim() == "")
            {
                txtHgConc.Text = napthalene.getConcentration("Napthalene").ToString();
            }

            if (txtButConc.Text.Trim() == "")
            {
                txtButConc.Text = no2.getConcentration("NO2").ToString();
            }

            if (txtTolConc.Text.Trim() == "")
            {
                txtTolConc.Text = tol.getConcentration("Toluene").ToString();
            }

            if (txtPxyConc.Text.Trim() == "")
            {
                txtPxyConc.Text = pxyl.getConcentration("pXylene").ToString();
            }

            if (txtNOConc.Text.Trim() == "")
            {
                txtNOConc.Text = no.getConcentration("NO").ToString();
            }

            if (txtCS2ConcFactor.Text.Trim() == "")
            {
                txtCS2ConcFactor.Text = cs2.getConcentration("CS2").ToString();
            }

            if (txtOzoneConcFactor.Text.Trim() == "")
            {
                txtOzoneConcFactor.Text = ozone.getConcentration("Ozone").ToString();
            }

            string pixel_shifts = (string)regKey.GetValue("pixel_shifts", null);

            if (pixel_shifts != null)
            {
                string[] pairs = pixel_shifts.Split('|');
                if (pairs != null)
                {
                    for (int i = 0; i < pairs.Length; ++i)
                    {
                        string[] values = pairs[i].Split('&');
                        if (values != null && values.Length == 2)
                        {
                            if ((string)values[0] == "Benzene")
                            {
                                txtBenzenePixelShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Ammonia")
                            {
                                txtAmmoniaPixelShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Raw SO2 - Xe")
                            {
                                txtXenSo2Shift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Raw SO2 - Dt")
                            {
                                txtdetSo2Shift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Napthalene")
                            {
                                txtHgPixShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "NO")
                            {
                                txtNOPixShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "NO2")
                            {
                                txtButPixShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "pXylene")
                            {
                                txtPXylPixShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Toluene")
                            {
                                txtTolPixShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "CS2")
                            {
                                txtCS2PixelShift.Text = (string)values[1];
                            }
                            else if ((string)values[0] == "Ozone")
                            {
                                txtOzonePixelShift.Text = (string)values[1];
                            }
                        }
                    }
                }
            }
            if(txtBenzenePixelShift.Text.Trim()=="")
            {
                txtBenzenePixelShift.Text = benzene_lib.pixle_shift.ToString();
            }
            if(txtAmmoniaPixelShift.Text.Trim() == "")
            {
                txtAmmoniaPixelShift.Text = ammonia_lib.pixle_shift.ToString();
            }
            if (txtXenSo2Shift.Text.Trim() == "")
            {
                txtXenSo2Shift.Text = xenon_so2.pixle_shift.ToString();
            }
            if (txtdetSo2Shift.Text.Trim() == "")
            {
                txtdetSo2Shift.Text = det_so2.pixle_shift.ToString();
            }

            if (txtHgPixShift.Text.Trim() == "")
            {
                txtHgPixShift.Text = napthalene.pixle_shift.ToString();
            }
            if (txtButPixShift.Text.Trim() == "")
            {
                txtButPixShift.Text = no2.pixle_shift.ToString();
            }
            if (txtTolPixShift.Text.Trim() == "")
            {
                txtTolPixShift.Text = tol.pixle_shift.ToString();
            }
            if (txtPXylPixShift.Text.Trim() == "")
            {
                txtPXylPixShift.Text = pxyl.pixle_shift.ToString();
            }
            if (txtNOPixShift.Text.Trim() == "")
            {
                txtNOPixShift.Text = no.pixle_shift.ToString();
            }
            if (txtCS2PixelShift.Text.Trim() == "")
            {
                txtCS2PixelShift.Text = cs2.pixle_shift.ToString();
            }
            if (txtOzonePixelShift.Text.Trim() == "")
            {
                txtOzonePixelShift.Text = ozone.pixle_shift.ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!validated())
            {
                MessageBox.Show("You have entered incorrect values. Please correct!");
                return;
            }

            RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            string concentrations = 
                "Benzene_Benzene&" + txtBenzeneConc.Text+ 
//                "|Benzene_Ozone&" + txtOzoneConc.Text +
                "|Ammonia_Ammonia&" + txtAmmoniaConc.Text +
                "|Raw SO2 - Dt_SO2&" + txtDtSo2Conc.Text +
                "|Raw SO2 - Xe_SO2&" + txtXenSo2Conc.Text+
                "|Napthalene_Napthalene&" + txtHgConc.Text +
                "|NO2_NO2&" + txtButConc.Text +
                "|NO_NO&" + txtNOConc.Text +
                "|pXylene_pXylene&" + txtPxyConc.Text+
                "|Toluene_Toluene&" + txtTolConc.Text +
                "|CS2_CS2&" + txtCS2ConcFactor.Text +
                "|Ozone_Ozone&" + txtOzoneConcFactor.Text;

            string pix_shifts = 
                "Benzene&" + txtBenzenePixelShift.Text +
                "|Ammonia&" + txtAmmoniaPixelShift.Text +
                "|Raw SO2 - Dt&" + txtdetSo2Shift.Text +
                "|Raw SO2 - Xe&" + txtXenSo2Shift.Text +
                "|Napthalene&" + txtHgPixShift.Text +
                "|NO2&" + txtButPixShift.Text +
                "|NO&" + txtNOPixShift.Text +
                "|pXylene&" + txtPXylPixShift.Text +
                "|Toluene&" + txtTolPixShift.Text +
                "|CS2&" + txtCS2PixelShift.Text +
                "|Ozone&" + txtOzonePixelShift.Text;

            regKey.SetValue("concentration_factors", concentrations);
            regKey.SetValue("pixel_shifts", pix_shifts);
            temp_concetrations = concentrations;
            temp_pixel_shifts = pix_shifts;
            
        }

        /// <summary>
        /// Checks if everything entered has the correct numeric value
        /// </summary>
        /// <returns></returns>
        private bool validated()
        {
            if (Util.isNumeric(txtBenzeneConc.Text) &&
                Util.isNumeric(txtAmmoniaConc.Text) &&
                Util.isNumeric(txtBenzenePixelShift.Text) &&
                Util.isNumeric(txtAmmoniaPixelShift.Text) &&
                Util.isNumeric(txtdetSo2Shift.Text) &&
                Util.isNumeric(txtDtSo2Conc.Text) &&
                Util.isNumeric(txtXenSo2Shift.Text) &&
                Util.isNumeric(txtXenSo2Conc.Text)&&                
                Util.isNumeric(txtHgConc.Text) &&
                Util.isNumeric(txtHgPixShift.Text) &&
                Util.isNumeric(txtButConc.Text) &&
                Util.isNumeric(txtButPixShift.Text) &&
                Util.isNumeric(txtTolConc.Text) &&
                Util.isNumeric(txtTolPixShift.Text) &&
                Util.isNumeric(txtPXylPixShift.Text) &&
                Util.isNumeric(txtPxyConc.Text) &&
                Util.isNumeric(txtNOConc.Text) &&
                Util.isNumeric(txtNOPixShift.Text)&&
                Util.isNumeric(txtCS2ConcFactor.Text) &&
                Util.isNumeric(txtCS2PixelShift.Text) &&
                Util.isNumeric(txtOzoneConcFactor.Text) &&
                Util.isNumeric(txtOzonePixelShift.Text))
            {
                return true;
            }
            return false;
        }

    }
}