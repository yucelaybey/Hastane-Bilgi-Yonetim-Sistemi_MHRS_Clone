using AnimatorNS;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class AdminPanelClosingSuccessForm : DevExpress.XtraEditors.XtraForm
    {
        private Animator animator;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AdminPanelClosingSuccessForm()
        {
            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();

            animator = new Animator();
            animator.AnimationType = AnimationType.HorizSlide;

        }
        private void AdminPanelClosingSuccessForm_Load(object sender, EventArgs e)
        {
            Count();
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

                labelTitle.BackColor = Color.FromArgb(26, 26, 26);

                labelDescription.BackColor = Color.FromArgb(26, 26, 26);
                labelDescription.ForeColor = Color.FromArgb(249,249,249);

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);


                labelTitle.BackColor = Color.FromArgb(249, 249, 249);

                labelDescription.BackColor = Color.FromArgb(249, 249, 249);
                labelDescription.ForeColor = Color.FromArgb(26, 26, 26);
            }
        }

        private async void Count()
        {
            for (int i = 8; 0 < i; i--)
            {
                await Task.Delay(1000);
                if (i == 7)
                {
                    await Task.Run(() => animator.Hide(labelDescription));
                    await Task.Run(() => animator.Show(labelDescription2));
                }
                else if (i <= 5)
                {
                    labelCount.Visible = true;
                    labelCount.Text = i.ToString();
                }
            }
            Application.Exit();
        }
    }
}