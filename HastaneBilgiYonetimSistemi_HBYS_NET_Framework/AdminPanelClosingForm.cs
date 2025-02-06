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
    public partial class AdminPanelClosingForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private bool isClosing = false;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AdminPanelClosingForm()
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
                this.BackColor = Color.FromArgb(26, 26, 26);

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                labelQuestion.BackColor = Color.FromArgb(26, 26, 26);

                labelDescription.BackColor = Color.FromArgb(26, 26, 26);
                labelDescription.ForeColor = Color.FromArgb(249,249,249);

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                pictureBoxDark.Visible = false;
                pictureBoxWhite.Visible = true;

                labelQuestion.BackColor = Color.FromArgb(249, 249, 249);

                labelDescription.BackColor = Color.FromArgb(249, 249, 249);
                labelDescription.ForeColor = Color.FromArgb(26, 26, 26);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.AccessibleName = "Close";
            this.DialogResult = DialogResult.Abort;
        }

        private void btn_ReturnLogin_Click(object sender, EventArgs e)
        {
            this.AccessibleName = "LoginOpen";
            this.DialogResult = DialogResult.Yes;
        }

        private void AdminPanelClosingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Yes)
            {
                this.AccessibleName = "LoginOpen";
            }
            else if (DialogResult == DialogResult.Abort)
            {
                this.AccessibleName = "Close";
            }
            else
            {
                this.AccessibleName = "NotClose";
            }
        }
    }
}