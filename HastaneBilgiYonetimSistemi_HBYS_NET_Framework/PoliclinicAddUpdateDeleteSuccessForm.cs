using ComponentFactory.Krypton.Toolkit;
using DevExpress.LookAndFeel;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Core.Model;
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
    public partial class PoliclinicAddUpdateDeleteSuccessForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string operation;
        private readonly int sayac;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public PoliclinicAddUpdateDeleteSuccessForm(string _operation, int _sayac)
        {
            InitializeComponent();

            operation = _operation;
            sayac = _sayac;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }

        private void PoliklinikEkleSilGuncelleSuccessForm_Load(object sender, EventArgs e)
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
                return File.ReadAllText(settingsFilePath).Trim(); // Trim ile boşlukları kaldır
            }
            return "light"; // Varsayılan değer
        }

        private void DarkModeOpen()
        {
            if (isDarkMode)
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
                this.BackColor = Color.FromArgb(38, 38, 38);

                pictureBoxPurple.Visible = true;
                pictureBoxGreen.Visible = false;

                labelTitle.ForeColor = Color.FromArgb(124, 86, 216);

                labelDescription.ForeColor = Color.FromArgb(249, 249, 249);
                labelDescription.BackColor = Color.FromArgb(38, 38, 38);

                btn_Close.ForeColor = Color.White;

                btn_Close.IdleBorderColor = Color.FromArgb(167, 114, 242);
                btn_Close.IdleFillColor = Color.FromArgb(167, 114, 242);

                btn_Close.onHoverState.ForeColor = Color.FromArgb(38, 38, 38);
                btn_Close.onHoverState.FillColor = Color.FromArgb(167, 114, 242);

                btn_Close.OnIdleState.ForeColor = Color.FromArgb(38, 38, 38);
                btn_Close.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

                btn_Close.OnPressedState.ForeColor = Color.FromArgb(38, 38, 38);
                btn_Close.OnPressedState.FillColor = Color.FromArgb(97, 50, 209);
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(230, 230, 230);

                pictureBoxPurple.Visible = false;
                pictureBoxGreen.Visible = true;

                labelTitle.ForeColor = Color.FromArgb(60, 184, 121);

                labelDescription.ForeColor = Color.DimGray;
                labelDescription.BackColor = Color.FromArgb(230, 230, 230);

                btn_Close.ForeColor = Color.White;

                btn_Close.IdleBorderColor = Color.DimGray;
                btn_Close.IdleFillColor = Color.FromArgb(200, 200, 200);

                btn_Close.onHoverState.ForeColor = Color.Black;
                btn_Close.onHoverState.BorderColor = Color.DimGray;
                btn_Close.onHoverState.FillColor = Color.FromArgb(249, 249, 249);

                btn_Close.OnIdleState.ForeColor = Color.White;
                btn_Close.OnIdleState.FillColor = Color.FromArgb(200, 200, 200);

                btn_Close.OnPressedState.ForeColor = Color.White;
                btn_Close.OnPressedState.FillColor = Color.FromArgb(200, 200, 200);
            }
        }

        private void ScreenProperty()
        {
            if (operation == "add")
            {
                labelDescription.Location = new Point(93, 132);
                labelDescription.Text = "Poliklinik kaydedilmiştir...";
            }
            else if (operation == "update")
            {
                labelDescription.Location = new Point(87, 132);
                labelDescription.Text = "Poliklinik güncellenmiştir...";
            }
            else if (operation == "delete")
            {
                labelDescription.Location = new Point(111, 132);
                labelDescription.Text = $"{sayac} Poliklinik silinmiştir...";
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PoliclinicAddUpdateDeleteSuccessForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Close_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}