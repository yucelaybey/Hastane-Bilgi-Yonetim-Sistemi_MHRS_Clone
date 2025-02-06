using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class PatientInformationAddUpdateDeleteForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string operation;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public PatientInformationAddUpdateDeleteForm(string _operation)
        {
            InitializeComponent();

            operation = _operation;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }

        private void PatientInformationAddUpdateDeleteForm_Load(object sender, EventArgs e)
        {
            ScreenProperty();
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
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.White;
            }
        }

        private void ScreenProperty()
        {
            if (operation == "update")
            {
                labelDescription.Location = new Point(120, 60);
                labelDescription.Text = "Şifre başarıyla güncellenmiştir.";
            }
            else if (operation == "updateInformation")
            {
                labelDescription.Location = new Point(100, 65);
                labelDescription.Text = "Hasta bilgisi başarıyla güncellenmiştir.";
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void PatientInformationAddUpdateDeleteForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Close_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}