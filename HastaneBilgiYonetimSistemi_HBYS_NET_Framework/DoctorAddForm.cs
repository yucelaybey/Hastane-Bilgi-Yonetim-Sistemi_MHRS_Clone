using Bunifu.UI.WinForms;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class DoctorAddForm : DevExpress.XtraEditors.XtraForm
    {

        private static readonly string[] AllowedDomains =
        {
        "gmail.com",
        "outlook.com",
        "hotmail.com",
        "outlook.com.tr",
        "yandex.com",
        "yahoo.com"
        };

        private static readonly string AllowedCharactersPattern = @"^[a-zA-Z0-9._]+$";

        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public DoctorAddForm()
        {
            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private async void DoctorAddForm_Load(object sender, EventArgs e)
        {
            tcProperty();
            otherProperty();
            await PoliclinicCategoryLoad();
        }
        private void tcProperty()
        {
            txtDoctorTcNo.PlaceholderText = "TC Kimlik No";
            txtDoctorTcNo.MaxLength = 13;
        }
        private void otherProperty()
        {
            labelDoctorMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
        }
        private void doctorPhoneNumberProperty()
        {
            txtDoctorPhoneNumber.PlaceholderText = "(5xx) xxx xxxx";
            txtDoctorPhoneNumber.MaxLength = 15;
        }
        public void PerformActionBasedOnSetting()
        {
            string setting = LoadSettings();

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
                this.BackColor = Color.FromArgb(38, 38, 38);

                picturboxDarkOpen();

                PanelDarkOpen();

                LabelDoctorDarkOpen();

                TxtDoctorTcNoDarkOpen();

                TxtDoctorNameDarkOpen();

                TxtDoctorSurnameDarkOpen();

                TxtDoctorUsernameDarkOpen();

                TxtDoctorMailDarkOpen();

                TxtDoctorPhoneNumberOpen();

                DropDownDoctorGenderDarkOpen();

                DropDownPoliclinicDarkOpen();

                DatePickerDoctorBornDarkOpen();

                btn_DoctorAddDarkOpen();

                label_FooterDarkOpen();
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(230, 230, 230);

                picturboxDarkClose();

                PanelDarkClose();

                LabelDoctorDarkClose();

                TxtDoctorTcNoDarkClose();

                TxtDoctorNameDarkClose();

                TxtDoctorSurnameDarkClose();

                TxtDoctorUsernameDarkClose();

                TxtDoctorMailDarkClose();

                TxtDoctorPhoneNumberClose();

                DropDownDoctorGenderDarkClose();

                DropDownPoliclinicDarkClose();

                DatePickerDoctorBornDarkClose();

                btn_DoctorAddDarkClose();

                label_FooterDarkClose();
            }
        }
        private void picturboxDarkOpen()
        {
            pictureBoxDark.Visible = true;
            pictureBoxWhite.Visible = false;
        }

        private void picturboxDarkClose()
        {
            pictureBoxDark.Visible = false;
            pictureBoxWhite.Visible = true;
        }

        private void PanelDarkOpen()
        {
            panelGradient.GradientBottomLeft = Color.FromArgb(236, 92, 188);
            panelGradient.GradientBottomRight = Color.DeepPink;
            panelGradient.GradientTopLeft = Color.FromArgb(124, 8, 216);
            panelGradient.GradientTopRight = Color.FromArgb(198, 60, 212);

            panelInDoctorAdd.BackgroundColor = Color.FromArgb(64, 64, 64);
        }

        private void PanelDarkClose()
        {
            panelGradient.GradientBottomLeft = Color.FromArgb(230, 230, 230);
            panelGradient.GradientBottomRight = Color.Gray;
            panelGradient.GradientTopLeft = Color.Gray;
            panelGradient.GradientTopRight = Color.FromArgb(230, 230, 230);

            panelInDoctorAdd.BackgroundColor = Color.FromArgb(230, 230, 230);
        }

        private void LabelDoctorDarkOpen()
        {
            labelDoctorTcNo.ForeColor = Color.White;
            labelDoctorName.ForeColor = Color.White;
            labelDoctorSurname.ForeColor = Color.White;
            labelDoctorUsername.ForeColor = Color.White;
            labelDoctorMail.ForeColor = Color.White;
            labelDoctorPhoneNumber.ForeColor = Color.White;
            labelDoctorGender.ForeColor = Color.White;
            labelPoliclinics.ForeColor = Color.White;
            labelDoctorBorn.ForeColor = Color.White;
            labelRandomUsername.ForeColor = Color.FromArgb(52, 160, 243);
        }
        private void LabelDoctorDarkClose()
        {
            labelDoctorTcNo.ForeColor = Color.Black;
            labelDoctorName.ForeColor = Color.Black;
            labelDoctorSurname.ForeColor = Color.Black;
            labelDoctorUsername.ForeColor = Color.Black;
            labelDoctorMail.ForeColor = Color.Black;
            labelDoctorPhoneNumber.ForeColor = Color.Black;
            labelDoctorGender.ForeColor = Color.Black;
            labelPoliclinics.ForeColor = Color.Black;
            labelDoctorBorn.ForeColor = Color.Black;
            labelRandomUsername.ForeColor = Color.FromArgb(52, 160, 243);
        }

        private void TxtDoctorTcNoDarkOpen()
        {
            txtDoctorTcNo.ForeColor = Color.FromArgb(249, 249, 249);
            txtDoctorTcNo.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorTcNo.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorTcNo.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorTcNo.BorderColorIdle = Color.Gray;
            txtDoctorTcNo.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorTcNo.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorTcNo.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }

        private void TxtDoctorTcNoDarkClose()
        {
            txtDoctorTcNo.ForeColor = Color.FromArgb(26, 26, 26);
            txtDoctorTcNo.FillColor = Color.White;
            txtDoctorTcNo.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorTcNo.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorTcNo.BorderColorIdle = Color.Gray;
            txtDoctorTcNo.OnHoverState.FillColor = Color.White;
            txtDoctorTcNo.OnIdleState.FillColor = Color.White;
            txtDoctorTcNo.OnActiveState.FillColor = Color.White;
        }

        private void TxtDoctorNameDarkOpen()
        {
            txtDoctorName.ForeColor = Color.FromArgb(249, 249, 249);
            txtDoctorName.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorName.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorName.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorName.BorderColorIdle = Color.Gray;
            txtDoctorName.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorName.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorName.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtDoctorNameDarkClose()
        {
            txtDoctorName.ForeColor = Color.FromArgb(26, 26, 26);
            txtDoctorName.FillColor = Color.White;
            txtDoctorName.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorName.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorName.BorderColorIdle = Color.Gray;
            txtDoctorName.OnHoverState.FillColor = Color.White;
            txtDoctorName.OnIdleState.FillColor = Color.White;
            txtDoctorName.OnActiveState.FillColor = Color.White;
        }

        private void TxtDoctorSurnameDarkOpen()
        {
            txtDoctorSurname.ForeColor = Color.FromArgb(249, 249, 249);
            txtDoctorSurname.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorSurname.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorSurname.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorSurname.BorderColorIdle = Color.Gray;
            txtDoctorSurname.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorSurname.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorSurname.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }

        private void TxtDoctorSurnameDarkClose()
        {
            txtDoctorSurname.ForeColor = Color.FromArgb(26, 26, 26);
            txtDoctorSurname.FillColor = Color.White;
            txtDoctorSurname.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorSurname.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorSurname.BorderColorIdle = Color.Gray;
            txtDoctorSurname.OnHoverState.FillColor = Color.White;
            txtDoctorSurname.OnIdleState.FillColor = Color.White;
            txtDoctorSurname.OnActiveState.FillColor = Color.White;
        }
        private void TxtDoctorUsernameDarkOpen()
        {
            txtDoctorUsername.ForeColor = Color.FromArgb(249, 249, 249);
            txtDoctorUsername.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorUsername.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorUsername.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorUsername.BorderColorIdle = Color.Gray;
            txtDoctorUsername.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorUsername.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorUsername.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtDoctorUsernameDarkClose()
        {
            txtDoctorUsername.ForeColor = Color.FromArgb(26, 26, 26);
            txtDoctorUsername.FillColor = Color.White;
            txtDoctorUsername.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorUsername.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorUsername.BorderColorIdle = Color.Gray;
            txtDoctorUsername.OnHoverState.FillColor = Color.White;
            txtDoctorUsername.OnIdleState.FillColor = Color.White;
            txtDoctorUsername.OnActiveState.FillColor = Color.White;
        }
        private void TxtDoctorMailDarkOpen()
        {
            txtDoctorMail.ForeColor = Color.FromArgb(249, 249, 249);
            txtDoctorMail.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorMail.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorMail.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorMail.BorderColorIdle = Color.Gray;
            txtDoctorMail.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorMail.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorMail.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtDoctorMailDarkClose()
        {
            txtDoctorMail.ForeColor = Color.FromArgb(26, 26, 26);
            txtDoctorMail.FillColor = Color.White;
            txtDoctorMail.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorMail.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorMail.BorderColorIdle = Color.Gray;
            txtDoctorMail.OnHoverState.FillColor = Color.White;
            txtDoctorMail.OnIdleState.FillColor = Color.White;
            txtDoctorMail.OnActiveState.FillColor = Color.White;
        }
        private void TxtDoctorPhoneNumberOpen()
        {
            txtDoctorPhoneNumber.ForeColor = Color.FromArgb(249, 249, 249);
            txtDoctorPhoneNumber.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorPhoneNumber.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorPhoneNumber.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorPhoneNumber.BorderColorIdle = Color.Gray;
            txtDoctorPhoneNumber.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorPhoneNumber.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorPhoneNumber.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtDoctorPhoneNumberClose()
        {
            txtDoctorPhoneNumber.ForeColor = Color.FromArgb(26, 26, 26);
            txtDoctorPhoneNumber.FillColor = Color.White;
            txtDoctorPhoneNumber.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorPhoneNumber.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorPhoneNumber.BorderColorIdle = Color.Gray;
            txtDoctorPhoneNumber.OnHoverState.FillColor = Color.White;
            txtDoctorPhoneNumber.OnIdleState.FillColor = Color.White;
            txtDoctorPhoneNumber.OnActiveState.FillColor = Color.White;
        }

        private void DropDownDoctorGenderDarkOpen()
        {
            DropdownDoctorGender.ForeColor = Color.FromArgb(249, 249, 249);
            DropdownDoctorGender.BackgroundColor = Color.FromArgb(38, 38, 38);
            DropdownDoctorGender.ItemBackColor = Color.FromArgb(38, 38, 38);
            DropdownDoctorGender.ItemBorderColor = Color.FromArgb(38, 38, 38);
        }
        private void DropDownDoctorGenderDarkClose()
        {
            DropdownDoctorGender.ForeColor = Color.Black;
            DropdownDoctorGender.BackgroundColor = Color.White;
            DropdownDoctorGender.ItemBackColor = Color.White;
            DropdownDoctorGender.ItemBorderColor = Color.White;
            DropdownDoctorGender.ItemForeColor = Color.Black;
        }

        private void DropDownPoliclinicDarkOpen()
        {
            DropDownPoliclinic.ForeColor = Color.FromArgb(249, 249, 249);
            DropDownPoliclinic.BackgroundColor = Color.FromArgb(38, 38, 38);
            DropDownPoliclinic.ItemBackColor = Color.FromArgb(38, 38, 38);
            DropDownPoliclinic.ItemBorderColor = Color.FromArgb(38, 38, 38);
        }
        private void DropDownPoliclinicDarkClose()
        {
            DropDownPoliclinic.ForeColor = Color.Black;
            DropDownPoliclinic.BackgroundColor = Color.White;
            DropDownPoliclinic.ItemBackColor = Color.White;
            DropDownPoliclinic.ItemBorderColor = Color.White;
            DropDownPoliclinic.ItemForeColor = Color.Black;
        }

        private void DatePickerDoctorBornDarkOpen()
        {
            DatePickerDoctorBorn.ForeColor = Color.FromArgb(249, 249, 249);
            DatePickerDoctorBorn.BackColor = Color.FromArgb(38, 38, 38);
        }
        private void DatePickerDoctorBornDarkClose()
        {
            DatePickerDoctorBorn.ForeColor = Color.Black;
            DatePickerDoctorBorn.BackColor = Color.White;
        }

        private void btn_DoctorAddDarkOpen()
        {
            btn_DoctorAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_DoctorAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorAdd.IdleFillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorAdd.onHoverState.FillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorAdd.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);
        }

        private void btn_DoctorAddDarkClose()
        {
            btn_DoctorAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_DoctorAdd.IdleBorderColor = Color.Black;
            btn_DoctorAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorAdd.onHoverState.BorderColor = Color.Black;
            btn_DoctorAdd.onHoverState.FillColor = Color.DimGray;

            btn_DoctorAdd.OnIdleState.BorderColor = Color.Black;
            btn_DoctorAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorAdd.OnPressedState.BorderColor = Color.Black;
            btn_DoctorAdd.OnPressedState.FillColor = Color.Gray;
        }

        private void label_FooterDarkOpen()
        {
            labelFooter.ForeColor = Color.White;
            labelFooter.BackColor = Color.FromArgb(64, 64, 64);
        }
        private void label_FooterDarkClose()
        {
            labelFooter.ForeColor = Color.FromArgb(26, 26, 26);
            labelFooter.BackColor = Color.FromArgb(230, 230, 230);
        }

        private void tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TcNo_LeaveFocus(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (string.IsNullOrEmpty(bunifuTextBox.Text))
            {
                if (bunifuTextBox.Name == "txtDoctorTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtDoctorPhoneNumber")
                {
                    doctorPhoneNumberProperty();
                }
            }
        }
        private void TcNo_TextChanged(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (bunifuTextBox == null || bunifuTextBox.Text == "1" || bunifuTextBox.Text == " " || bunifuTextBox.Text == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(bunifuTextBox.Text))
            {
                if (bunifuTextBox.Name == "txtDoctorTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtDoctorPhoneNumber")
                {
                    doctorPhoneNumberProperty();
                }
            }
            else
            {
                if (bunifuTextBox.Name == "txtDoctorTcNo")
                {
                    txtDoctorTcNo.MaxLength = 11;
                }
                else if (bunifuTextBox.Name == "txtDoctorPhoneNumber")
                {
                    txtDoctorPhoneNumber.MaxLength = 10;
                }
            }
        }

        private void txtNameSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
        private void doctorUsername_TextChanged(object sender, EventArgs e)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string currentText = txtDoctorUsername.Text;

            string filteredText = new string(currentText.Where(c => allowedCharacters.Contains(c)).ToArray()); //her bir karakteri teker teker kontl eder. allowCharacters ile aynıysa kalır.degilse karakteri siler.

            if (currentText != filteredText)
            {
                int cursorPosition = txtDoctorUsername.SelectionStart;
                txtDoctorUsername.Text = filteredText;
                txtDoctorUsername.SelectionStart = Math.Min(cursorPosition, filteredText.Length); // kücük olanı seçer
            }
        }

        private void txtNameSurname_TextChanged(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (bunifuTextBox == null)
            {
                return;
            }

            bunifuTextBox.Text = bunifuTextBox.Text.ToUpper();
            bunifuTextBox.SelectionStart = bunifuTextBox.Text.Length;
        }

        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {
            panelDoctorMailRules.Visible = true;
            string email = txtDoctorMail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                panelDoctorMailRules.Visible = false;
            }
            else if (IsValidEmail(email))
            {
                labelDoctorMailRules1.Text = "Geçerli Mail";
                labelDoctorMailRules1.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxDoctorMailRulesInCorrect.Visible = false;
                pictureBoxDoctorMailRulesCorrect.Visible = true;
            }
            else
            {
                labelDoctorMailRules1.Text = "Geçersiz Mail !";
                labelDoctorMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxDoctorMailRulesInCorrect.Visible = true;
                pictureBoxDoctorMailRulesCorrect.Visible = false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false; // Geçersiz: E-posta boş veya null
            }

            // E-posta adresini parçala
            string[] emailParts = email.Split('@');
            if (emailParts.Length != 2 || string.IsNullOrWhiteSpace(emailParts[0]) || string.IsNullOrWhiteSpace(emailParts[1]))
            {
                return false;
            }

            string localPart = emailParts[0];
            string domainPart = emailParts[1];


            if (!Regex.IsMatch(localPart, AllowedCharactersPattern))
            {
                return false;
            }


            string specialCharactersPattern = @"[İÜÖıöü]";
            if (Regex.IsMatch(localPart, specialCharactersPattern))
            {
                return false;
            }


            if (!domainPart.EndsWith(".com"))
            {
                return false;
            }

            if (!AllowedDomains.Contains(domainPart.ToLower()))
            {
                return false;
            }

            return true;
        }

        public class PoliclinicCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public PoliclinicCategory(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }


        private async void btn_DoctorAdd_Click(object sender, EventArgs e)
        {
            string tc = txtDoctorTcNo.Text.Trim();
            string ad = txtDoctorName.Text.Trim();
            string soyad = txtDoctorSurname.Text.Trim();
            string mail = txtDoctorMail.Text.Trim();
            string telefon = txtDoctorPhoneNumber.Text.Trim();
            string cinsiyet = DropdownDoctorGender.SelectedItem?.ToString();
            DateTime dogumTarihi = DatePickerDoctorBorn.Value;
            string username = txtDoctorUsername.Text.Trim();
            int policlinicId = 0;

            if (DropDownPoliclinic.SelectedItem != null)
            {
                PoliclinicCategory policlinicCategory = (PoliclinicCategory)DropDownPoliclinic.SelectedItem;

                policlinicId = policlinicCategory.Id;
            }

            if (!(tc == "") && !(policlinicId.ToString() == "") && !(policlinicId == 0) && !(policlinicId.ToString() == null) && !(ad == "") && !(soyad == "") && !(mail == "") && !(telefon == "") && !(cinsiyet == "") && !(dogumTarihi.ToString() == "") &&
                !(username == "") && !(tc == null) && !(ad == null) && !(soyad == null) && !(mail == null) && !(telefon == null) && !(cinsiyet == null) && !(dogumTarihi.ToString() == null) &&
                !(username == null) && pictureBoxDoctorMailRulesCorrect.Visible)
            {
                await DoctorKayit(tc, ad, soyad, mail, telefon, cinsiyet, dogumTarihi, policlinicId, username);
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("required");
                dataChooseForm.Show();
            }
        }
        private async Task PoliclinicCategoryLoad()
        {
            string query = "SELECT Id, [Poliklinik Adı] FROM Policlinics";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            DropDownPoliclinic.Items.Clear();

                            while (await reader.ReadAsync())
                            {
                                int poliklinikId = reader.GetInt32(0);
                                string poliklinikAd = reader.GetString(1);
                                DropDownPoliclinic.Items.Add(new PoliclinicCategory(poliklinikId, poliklinikAd));
                            }

                            DropDownPoliclinic.DisplayMember = "Name";
                            DropDownPoliclinic.ValueMember = "Id";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public async Task DoctorKayit(string tc, string ad, string soyad, string mail, string telefon, string cinsiyet, DateTime dogumTarihi, int policlinicId, string username)
        {
            string operation = "add";
            string query = @"INSERT INTO Doctors (Name, Surname, Gender, Date, TcNo, Email, EmailConfirm, PhoneNumber,Username,HaveToPassword,Money,PoliclinicId) VALUES (@Name, @Surname, @Gender, @Date, @TcNo, @Email, @EmailConfirm, @PhoneNumber,@Username,@HaveToPassword,@Money,@PoliclinicId)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TcNo", tc);
                        command.Parameters.AddWithValue("@Name", ad);
                        command.Parameters.AddWithValue("@Surname", soyad);
                        command.Parameters.AddWithValue("@Email", mail);
                        command.Parameters.AddWithValue("@EmailConfirm", "false");
                        command.Parameters.AddWithValue("@PhoneNumber", telefon);
                        command.Parameters.AddWithValue("@Gender", cinsiyet);
                        command.Parameters.AddWithValue("@Date", dogumTarihi);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@HaveToPassword", false);
                        command.Parameters.AddWithValue("@PoliclinicId", policlinicId);
                        command.Parameters.AddWithValue("@Money", "Belirlenmemiştir");

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            DoctorAddUpdateDeleteSuccessForm doctorAddUpdateDeleteSuccess = new DoctorAddUpdateDeleteSuccessForm(operation, 1);
                            doctorAddUpdateDeleteSuccess.Show();
                            this.AccessibleName = "Success";
                            ClearDoctorAdd();
                        }
                        else
                        {
                            MessageBox.Show("Doktor kaydı yapılamadı!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
        private void ClearDoctorAdd()
        {
            txtDoctorTcNo.Clear();
            txtDoctorName.Clear();
            txtDoctorSurname.Clear();
            txtDoctorMail.Clear();
            txtDoctorPhoneNumber.Clear();
            txtDoctorUsername.Clear();
            DropdownDoctorGender.Text = "Cinsiyet Seçiniz...";
            DropDownPoliclinic.Text = "Poliklinik Seçiniz...";
            DatePickerDoctorBorn.Value = DateTime.Now;
        }

        private void DoctorAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_DoctorAdd_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void labelRandomUsername_Click(object sender, EventArgs e)
        {
            if (txtDoctorName.Text != "" && txtDoctorName.Text != null && txtDoctorSurname.Text != "" && txtDoctorSurname.Text != null)
            {
                string combinedName = RemoveTurkishCharacters(txtDoctorName.Text.ToLower().Replace(" ", "") + txtDoctorSurname.Text.ToLower().Replace(" ", ""));

                Random random = new Random();
                int randomNumber = random.Next(100, 999);

                string userName;
                int position = random.Next(1, 4);

                switch (position)
                {
                    case 1:

                        userName = randomNumber + combinedName;
                        break;
                    case 2:

                        int midIndex = combinedName.Length / 2;
                        userName = combinedName.Substring(0, midIndex) + randomNumber + combinedName.Substring(midIndex);
                        break;
                    case 3:

                        userName = combinedName + randomNumber;
                        break;
                    default:
                        userName = combinedName + randomNumber;
                        break;
                }

                txtDoctorUsername.Text = userName;
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("randomUsername");
                dataChooseForm.Show();
            }
        }
        public static string RemoveTurkishCharacters(string input)
        {
            return input
                .Replace("ç", "c")
                .Replace("Ç", "c")
                .Replace("ğ", "g")
                .Replace("Ğ", "g")
                .Replace("ı", "i")
                .Replace("İ", "i")
                .Replace("ö", "o")
                .Replace("Ö", "o")
                .Replace("ş", "s")
                .Replace("Ş", "s")
                .Replace("ü", "u")
                .Replace("Ü", "u")
                .Replace("ı", "i");
        }
    }
}