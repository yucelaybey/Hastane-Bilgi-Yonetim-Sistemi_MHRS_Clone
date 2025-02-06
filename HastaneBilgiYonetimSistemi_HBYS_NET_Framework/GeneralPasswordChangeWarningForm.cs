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
    public partial class GeneralPasswordChangeWarningForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string email;
        private readonly string tcNo;
        private readonly string name;
        private readonly string confirmType;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public GeneralPasswordChangeWarningForm(string _tcNo, string email_, string _name, string _confirmType)
        {
            InitializeComponent();

            email = email_;
            tcNo = _tcNo;
            name = _name;
            confirmType = _confirmType;

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

                labelNotConfirmMail.BackColor = Color.FromArgb(26, 26, 26);
                labelNotConfirmMail.ForeColor = Color.FromArgb(249, 249, 249);

                labelDescription.BackColor = Color.FromArgb(26, 26, 26);
                labelDescription.ForeColor = Color.Red;
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                labelNotConfirmMail.BackColor = Color.FromArgb(249, 249, 249);
                labelNotConfirmMail.ForeColor = Color.FromArgb(26, 26, 26);

                labelDescription.BackColor = Color.FromArgb(249, 249, 249);
                labelDescription.ForeColor = Color.Red;
            }
        }
        private void Result()
        {
            labelNotConfirmMail.Text = $"({email})";
        }

        private async void btn_MailConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfirmScreenForm confirmScreenForm = new ConfirmScreenForm(tcNo, email, name, confirmType);
            confirmScreenForm.Show();

            while (true)
            {
                await Task.Delay(600);
                if (confirmScreenForm.AccessibleDescription == "Success")
                {
                    this.AccessibleDescription = "Success";
                    break;
                }
                else if (confirmScreenForm.AccessibleDescription == "Negative")
                {
                    this.AccessibleDescription = "Negative";
                    break;
                }
            }
            this.Close();
        }

        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            this.AccessibleDescription = "Negative";
            this.Close();
        }
    }
}