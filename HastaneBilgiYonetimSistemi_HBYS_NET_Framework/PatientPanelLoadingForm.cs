using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class PatientPanelLoadingForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public PatientPanelLoadingForm()
        {
            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        public void PerformActionBasedOnSetting()
        {
            string setting = LoadSettings(); // Ayarı yükle

            if (setting == "dark")
            {
                isDarkMode = true;
            }
            else if (setting == "light")
            {
                isDarkMode = false;
            }
            else
            {
                isDarkMode = false;
            }
        }

        public string LoadSettings()
        {
            if (File.Exists(settingsFilePath))
            {
                return File.ReadAllText(settingsFilePath).Trim();
            }
            return "light";
        }

        private void DarkModeOpen()
        {
            if (isDarkMode)
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
                this.BackColor = Color.Black;

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                labelDescription.BackColor = Color.Black;
                labelDescription.ForeColor = Color.White;
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.White;

                pictureBoxDark.Visible = false;
                pictureBoxWhite.Visible = true;

                labelDescription.BackColor = Color.White;
                labelDescription.ForeColor = Color.Black;
            }
        }

        public void UpdateProgress(int value)
        {
            if (value >= 0 && value <= 100)
            {
                if (progressPatientLoading.InvokeRequired)
                {
                    progressPatientLoading.Invoke(new Action(() =>
                    {
                        progressPatientLoading.Value = value;
                    }));
                }
                else
                {
                    progressPatientLoading.Value = value;
                }
            }
        }
        public void UpdateMessage(string message)
        {
            if (labelDescription.InvokeRequired)
            {
                labelDescription.Invoke(new Action(() =>
                {
                    labelDescription.Text = message;
                }));
            }
            else
            {
                labelDescription.Text = message;
            }
        }
    }
}