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
    public partial class AppointmentAcceptWarningScreenForm : DevExpress.XtraEditors.XtraForm
    {
        private DateTime date;

        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AppointmentAcceptWarningScreenForm(DateTime _date)
        {
            InitializeComponent();

            date = _date;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private void AppointmentAcceptWarningScreenForm_Load(object sender, EventArgs e)
        {
            int difference = -1 * (DateTime.Now.Date - date.Date).Days;
            if (difference == 0)
            {
                labelDescription7.Text = $"en geç {DateTime.Now.ToShortDateString()} saat 20:00’ye kadar";
            }
            else if (difference > 0)
            {
                labelDescription7.Text = $"en geç {DateTime.Now.AddDays(difference).ToShortDateString()} saat 20:00’ye kadar";
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

                panelAcceptWarning.BackgroundColor = Color.FromArgb(28, 46, 54);
                panelAcceptWarning.BorderColor = Color.FromArgb(57, 83, 99);

                pictureBoxInformation.BackColor = Color.FromArgb(28, 46, 54);

                labelTitle.BackColor = Color.FromArgb(28, 46, 54);
                labelTitle.ForeColor = Color.White;

                labelImportant.BackColor = Color.FromArgb(28, 46, 54);
                labelImportant.ForeColor = Color.White;

                labelDescription1.ForeColor = Color.White;
                labelDescription1.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription2.ForeColor = Color.White;
                labelDescription2.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription3.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription4.ForeColor = Color.White;
                labelDescription4.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription5.ForeColor = Color.White;
                labelDescription5.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription6.ForeColor = Color.White;
                labelDescription6.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription7.BackColor = Color.FromArgb(28, 46, 54);

                labelDescription8.ForeColor = Color.White;
                labelDescription8.BackColor = Color.FromArgb(28, 46, 54);
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.White;

                panelAcceptWarning.BackgroundColor = Color.FromArgb(230, 247, 255);
                panelAcceptWarning.BorderColor = Color.FromArgb(145, 213, 255);

                pictureBoxInformation.BackColor = Color.FromArgb(230, 247, 255);

                labelTitle.BackColor = Color.FromArgb(230, 247, 255);
                labelTitle.ForeColor = Color.Black;

                labelImportant.BackColor = Color.FromArgb(230, 247, 255);
                labelImportant.ForeColor = Color.Black;

                labelDescription1.ForeColor = Color.Black;
                labelDescription1.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription2.ForeColor = Color.Black;
                labelDescription2.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription3.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription4.ForeColor = Color.Black;
                labelDescription4.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription5.ForeColor = Color.Black;
                labelDescription5.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription6.ForeColor = Color.Black;
                labelDescription6.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription7.BackColor = Color.FromArgb(230, 247, 255);

                labelDescription8.ForeColor = Color.Black;
                labelDescription8.BackColor = Color.FromArgb(230, 247, 255);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.AccessibleName = string.Empty;
            this.Close();
        }

        private void btn_Okey_Click(object sender, EventArgs e)
        {
            this.AccessibleName = "Success";
            this.Close();
        }
    }
}