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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class AdminPanelLoadingForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AdminPanelLoadingForm()
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
                this.BackColor = Color.FromArgb(26, 26, 26);

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                labelDescription.BackColor = Color.FromArgb(26, 26, 26);
                labelDescription.ForeColor = Color.FromArgb(241,241,241);
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                pictureBoxDark.Visible = false;
                pictureBoxWhite.Visible = true;

                labelDescription.BackColor = Color.FromArgb(249, 249, 249);
                labelDescription.ForeColor = Color.FromArgb(26, 26, 26);
            }
        }

        public void UpdateProgress(int value)
        {
            if (value >= 0 && value <= 100)
            {
                if (progressAdminLoading.InvokeRequired)
                {
                    progressAdminLoading.Invoke(new Action(() =>
                    {
                        progressAdminLoading.Value = value;
                    }));
                }
                else
                {
                    progressAdminLoading.Value = value;
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