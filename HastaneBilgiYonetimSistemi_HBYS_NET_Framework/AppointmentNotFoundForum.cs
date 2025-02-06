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
    public partial class AppointmentNotFoundForum : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AppointmentNotFoundForum()
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
                return File.ReadAllText(settingsFilePath).Trim(); // Trim ile boşlukları kaldır
            }
            return "light";
        }

        private void DarkModeOpen()
        {
            if (isDarkMode)
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
                this.BackColor = Color.Black;

                panelDescription.BackgroundColor = Color.FromArgb(28, 46, 54);
                panelDescription.BorderColor = Color.FromArgb(57, 83, 99);

                labelInformationTitle.ForeColor = Color.White;
                labelInformationTitle.BackColor = Color.FromArgb(28, 46, 54);

                pictureBoxInformation.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription.ForeColor = Color.White;
                labelDescription.BackColor = Color.FromArgb(28, 46, 54);

                labelNotFound.ForeColor = Color.White;
                labelNotFound.BackColor = Color.FromArgb(28, 46, 54);

                btn_Okey.ForeColor = Color.White;
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.White;

                panelDescription.BackgroundColor = Color.FromArgb(230, 247, 255);
                panelDescription.BorderColor = Color.FromArgb(145, 213, 255);

                labelInformationTitle.ForeColor = Color.Black;
                labelInformationTitle.BackColor = Color.FromArgb(230, 247, 255);

                pictureBoxInformation.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription.ForeColor = Color.Black;
                labelDescription.BackColor = Color.FromArgb(230, 247, 255);

                labelNotFound.ForeColor = Color.Black;
                labelNotFound.BackColor = Color.FromArgb(230, 247, 255);

                btn_Okey.ForeColor = Color.White;
            }
        }

        private void btn_Okey_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}