using ComponentFactory.Krypton.Toolkit;
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
    public partial class DataChooseForm : DevExpress.XtraEditors.XtraForm
    {
        private string operation;
        private bool isDarkMode;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public DataChooseForm(string _operation)
        {
            InitializeComponent();
            operation = _operation;

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
                pictureBoxYellow.Visible = false;

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
                pictureBoxYellow.Visible = true;

                labelTitle.ForeColor = Color.FromArgb(250, 191, 50);

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
            if (operation == "update")
            {
                labelDescription.Location = new Point(11, 128);
                labelDescription.Text = "Lütfen güncellemek istediğiniz veriyi seçiniz.";
            }
            else if (operation == "delete")
            {
                labelDescription.Location = new Point(38, 128);
                labelDescription.Text = "Lütfen silmek istediğiniz veriyi seçiniz.";
            }
            else if (operation == "warning")
            {
                labelDescription.Location = new Point(33, 128);
                labelDescription.Text = "Silmek istediğiniz verilerden maksimum\r\n 5 tane seçebilirsin !";
            }
            else if (operation == "many")
            {
                labelDescription.Location = new Point(47, 128);
                labelDescription.Text = "Lütfen güncellemek için birden fazla\r\nveri seçmeyiniz !\r\n";
            }
            else if (operation == "manypassword")
            {
                labelDescription.Location = new Point(47, 128);
                labelDescription.Text = "Lütfen şifre göndermek için birden \r\nfazla veri seçmeyiniz !\r\n";
            }
            else if (operation == "password")
            {
                labelDescription.Location = new Point(55, 128);
                labelDescription.Text = "Lütfen şifre göndermek istediğiniz\r\ndoktoru seçiniz !\r\n";
            }
            else if (operation == "adminPassword")
            {
                labelDescription.Location = new Point(55, 128);
                labelDescription.Text = "Lütfen şifre göndermek istediğiniz\r\nyöneticiyi seçiniz !\r\n";
            }
            else if (operation == "required")
            {
                labelDescription.Location = new Point(47, 128);
                labelDescription.Text = "Lütfen gerekli yerleri doldurunuz !";
            }
            else if (operation == "doctorRestSame")
            {
                labelDescription.Location = new Point(99, 128);
                labelDescription.Text = "Bu tarih zaten eklenmiş!";
            }
            else if (operation == "doctorRestAdd")
            {
                labelDescription.Location = new Point(78, 128);
                labelDescription.Text = "Eklemek için bir tarih seçiniz!";
            }
            else if (operation == "randomUsername")
            {
                labelDescription.Location = new Point(16, 128);
                labelDescription.Text = "Lütfen önce Doktor Adı ve Soyadını giriniz !";
            }
            else if (operation == "adminPasswordChangeWarning")
            {
                labelDescription.Location = new Point(12, 128);
                labelDescription.Text = "Eski şifreniz yanlış yada kullanıcı bulunamadı !";
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PoliclinicDataChooseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Close_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}