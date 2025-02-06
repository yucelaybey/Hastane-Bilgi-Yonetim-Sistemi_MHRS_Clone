using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Org.BouncyCastle.Bcpg.OpenPgp;
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
    public partial class AppointmentRulesWarningForm : DevExpress.XtraEditors.XtraForm
    {
        private string status;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AppointmentRulesWarningForm(string _status)
        {
            InitializeComponent();

            status = _status;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private void AppointmentRulesWarningForm_Load(object sender, EventArgs e)
        {
            if (status == "MaxAppointment")
            {
                labelDescription.Text = "Hastane Bilgi Randevu Sistemimizden\r\nen fazla     randevu alabilirsiniz. Anlayışınız\r\niçin teşekkürler. Sağlıklı günler dileriz.\r\n";
                labelDescription.Location = new Point(116, 62);

                labelThree.Text = "3";
                labelThree.Location = new Point(182, 83);
            }
            else if (status == "Day")
            {
                labelDescription.Text = "Hastane Bilgi Randevu Sistemimizden aynı\r\nklinik ve hekimimizden gün için en fazla\r\n    randevu alabilirsiniz. Anlayışınız\r\niçin teşekkürler. Sağlıklı günler dileriz.\r\n";
                labelDescription.Location = new Point(110, 58);

                labelThree.Text = "1";
                labelThree.Location = new Point(111, 99);
            }
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

        private void btn_Okey_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}