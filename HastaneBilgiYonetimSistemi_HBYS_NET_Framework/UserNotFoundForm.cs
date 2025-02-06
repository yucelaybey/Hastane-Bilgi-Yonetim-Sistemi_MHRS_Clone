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
    public partial class UserNotFoundForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public UserNotFoundForm()
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

                pictureBoxWarningDark.Visible = true;
                pictureBoxWarningWhite.Visible = false;

                labelUserNullWarningTitle.BackColor = Color.FromArgb(26, 26, 26);
                labelUserNullWarningTitle.ForeColor = Color.Red;

                labelUserNullWarningDesc.BackColor = Color.FromArgb(26, 26, 26);
                labelUserNullWarningDesc.ForeColor = Color.FromArgb(249, 249, 249);

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                pictureBoxWarningDark.Visible = false;
                pictureBoxWarningWhite.Visible = true;

                labelUserNullWarningTitle.BackColor = Color.FromArgb(249, 249, 249);
                labelUserNullWarningTitle.ForeColor = Color.Red;

                labelUserNullWarningDesc.BackColor = Color.FromArgb(249, 249, 249);
                labelUserNullWarningDesc.ForeColor = Color.FromArgb(26, 26, 26);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}