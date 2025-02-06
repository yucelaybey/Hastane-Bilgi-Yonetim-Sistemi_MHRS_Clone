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
    public partial class PatientPanelClosingForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public PatientPanelClosingForm()
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
            return "light"; // Varsayılan değer
        }

        private void DarkModeOpen()
        {
            if (isDarkMode)
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
                this.BackColor = Color.Black;
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.White;
            }
        }

        private void PatientPanelClosingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.AccessibleDescription == "LoginOpen" || this.AccessibleDescription == "Close")
            {
                return;
            }
            this.AccessibleName = "Cancel";
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            this.AccessibleName = "LoginOpen";
            this.AccessibleDescription = "LoginOpen";
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.AccessibleName = "Close";
            this.AccessibleDescription = "Close";
            this.Close();
        }
    }
}