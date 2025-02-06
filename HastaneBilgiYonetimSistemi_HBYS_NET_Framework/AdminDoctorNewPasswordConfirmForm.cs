using DevExpress.Internal.WinApi.Windows.UI.Notifications;
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
    public partial class AdminDoctorNewPasswordConfirmForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string email;
        private readonly string name;
        private string result;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AdminDoctorNewPasswordConfirmForm(string email_, string _name)
        {
            InitializeComponent();

            email = email_;
            name = _name;

            PerformActionBasedOnSetting();
            DarkModeOpen();
            Result();
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

                labelNewPasswordMail.BackColor = Color.FromArgb(26, 26, 26);
                labelNewPasswordMail.ForeColor = Color.FromArgb(249, 249, 249);

                labelDescription.BackColor = Color.FromArgb(26, 26, 26);
                labelDescription.ForeColor = Color.Red;
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                labelNewPasswordMail.BackColor = Color.FromArgb(249, 249, 249);
                labelNewPasswordMail.ForeColor = Color.FromArgb(26, 26, 26);

                labelDescription.BackColor = Color.FromArgb(249, 249, 249);
                labelDescription.ForeColor = Color.Red;
            }
        }
        private void Result()
        {
            labelNewPasswordMail.Text = $"({email})";

            labelDescription.Text = $"{name} için yukarıdaki\r\ne-posta adresine yeni şifre göndermek istediğine\r\neminmisin ?\r\n";
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            result = "Negative";
            this.Close();
        }

        private void btn_MailConfirm_Click(object sender, EventArgs e)
        {
            result = "success";
            this.Close();
        }

        private void DoctorNewPasswordConfirmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (result == "success")
            {
                this.AccessibleName = "Success";
            }
            else if (result == "Negative")
            {
                this.AccessibleName = "Negative";
            }
        }
    }
}