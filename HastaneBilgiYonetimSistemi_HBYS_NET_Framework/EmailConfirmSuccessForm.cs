﻿using DevExpress.LookAndFeel;
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
    
    public partial class EmailConfirmSuccessForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public EmailConfirmSuccessForm()
        {
            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private void EmailConfirmSuccesForm_Load_1(object sender, EventArgs e)
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

                labelDescription1.BackColor = Color.FromArgb(26, 26, 26);
                labelDescription1.ForeColor = Color.FromArgb(249, 249, 249);

                labelDescription2.BackColor = Color.FromArgb(26, 26, 26);

                labelDescription3.BackColor = Color.FromArgb(26, 26, 26);
                labelDescription3.ForeColor = Color.FromArgb(249, 249, 249);

                labelCount.BackColor = Color.FromArgb(26, 26, 26);
                labelCount.ForeColor = Color.FromArgb(249, 249, 249);

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                labelDescription1.BackColor = Color.FromArgb(249, 249, 249);
                labelDescription1.ForeColor = Color.FromArgb(26, 26, 26);

                labelDescription2.BackColor = Color.FromArgb(249, 249, 249);

                labelDescription3.BackColor = Color.FromArgb(249, 249, 249);
                labelDescription3.ForeColor = Color.FromArgb(26, 26, 26);

                labelCount.BackColor = Color.FromArgb(249, 249, 249);
                labelCount.ForeColor = Color.FromArgb(26, 26, 26);
            }
        }

        private async void Count()
        {
            for (int i = 3; 0 < i; i--)
            {
                labelCount.Text = $"{i} Saniye";
                await Task.Delay(1000);
            }
            this.Close();
        }
    }
}