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
    public partial class EmailConfirmFalseForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string email;
        private readonly string tcNo;
        private readonly string name;
        private readonly string tag;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public EmailConfirmFalseForm(string _tcNo,string email_,string _name,string _tag)
        {
            InitializeComponent();

            email = email_;
            tcNo = _tcNo;
            name = _name;
            tag = _tag;

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

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_MailConfirm_Click(object sender, EventArgs e)
        {
            this.Hide();
            string confirmType = null;
            if (tag == "patient")
            {
                confirmType = "mailConfirm";
            }
            else if (tag == "doctor")
            {
                confirmType = "DoctorMailConfirm";
            }
            else if (tag == "admin")
            {
                confirmType = "AdminMailConfirm";
            }
            ConfirmScreenForm confirmScreenForm = new ConfirmScreenForm(tcNo,email,name, confirmType);
            confirmScreenForm.ShowDialog();
            this.Close();
        }
    }
}