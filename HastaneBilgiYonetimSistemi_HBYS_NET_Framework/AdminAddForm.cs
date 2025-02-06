using Bunifu.UI.WinForms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
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

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class AdminAddForm : DevExpress.XtraEditors.XtraForm
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
        public AdminAddForm()
        {
            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private void AdminAddForm_Load(object sender, EventArgs e)
        {
            tcProperty();
            otherProperty();
        }
        private void tcProperty()
        {
            txtAdminTcNo.PlaceholderText = "TC Kimlik No";
            txtAdminTcNo.MaxLength = 13;
        }
        private void otherProperty()
        {
            labelAdminMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
        }
        private void adminPhoneNumberProperty()
        {
            txtAdminPhoneNumber.PlaceholderText = "(5xx) xxx xxxx";
            txtAdminPhoneNumber.MaxLength = 15;
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

                picturboxDarkOpen();

                PanelDarkOpen();

                LabelAdminDarkOpen();

                TxtAdminTcNoDarkOpen();

                TxtAdminNameDarkOpen();

                TxtAdminSurnameDarkOpen();

                TxtAdminUsernameDarkOpen();

                TxtAdminMailDarkOpen();

                TxtAdminPhoneNumberOpen();

                DropDownAdminGenderDarkOpen();

                DatePickerAdminBornDarkOpen();

                btn_AdminAddDarkOpen();

                label_FooterDarkOpen();
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(230, 230, 230);

                picturboxDarkClose();

                PanelDarkClose();

                LabelAdminDarkClose();

                TxtAdminTcNoDarkClose();

                TxtAdminNameDarkClose();

                TxtAdminSurnameDarkClose();

                TxtAdminUsernameDarkClose();

                TxtAdminMailDarkClose();

                TxtAdminPhoneNumberClose();

                DropDownAdminGenderDarkClose();

                DatePickerAdminBornDarkClose();

                btn_AdminAddDarkClose();

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

            panelInAdminAdd.BackgroundColor = Color.FromArgb(64, 64, 64);
        }

        private void PanelDarkClose()
        {
            panelGradient.GradientBottomLeft = Color.FromArgb(230, 230, 230);
            panelGradient.GradientBottomRight = Color.Gray;
            panelGradient.GradientTopLeft = Color.Gray;
            panelGradient.GradientTopRight = Color.FromArgb(230, 230, 230);

            panelInAdminAdd.BackgroundColor = Color.FromArgb(230, 230, 230);
        }

        private void LabelAdminDarkOpen()
        {
            labelAdminTcNo.ForeColor = Color.White;
            labelAdminName.ForeColor = Color.White;
            labelAdminSurname.ForeColor = Color.White;
            labelAdminUsername.ForeColor = Color.White;
            labelAdminMail.ForeColor = Color.White;
            labelAdminPhoneNumber.ForeColor = Color.White;
            labelAdminGender.ForeColor = Color.White;
            labelAdminBorn.ForeColor = Color.White;
            labelRandomUsername.ForeColor = Color.FromArgb(52, 160, 243);
        }
        private void LabelAdminDarkClose()
        {
            labelAdminTcNo.ForeColor = Color.Black;
            labelAdminName.ForeColor = Color.Black;
            labelAdminSurname.ForeColor = Color.Black;
            labelAdminUsername.ForeColor = Color.Black;
            labelAdminMail.ForeColor = Color.Black;
            labelAdminPhoneNumber.ForeColor = Color.Black;
            labelAdminGender.ForeColor = Color.Black;
            labelAdminBorn.ForeColor = Color.Black;
            labelRandomUsername.ForeColor = Color.FromArgb(52, 160, 243);
        }

        private void TxtAdminTcNoDarkOpen()
        {
            txtAdminTcNo.ForeColor = Color.FromArgb(249, 249, 249);
            txtAdminTcNo.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminTcNo.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminTcNo.BorderColorIdle = Color.Gray;
            txtAdminTcNo.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }

        private void TxtAdminTcNoDarkClose()
        {
            txtAdminTcNo.ForeColor = Color.FromArgb(26, 26, 26);
            txtAdminTcNo.FillColor = Color.White;
            txtAdminTcNo.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminTcNo.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminTcNo.BorderColorIdle = Color.Gray;
            txtAdminTcNo.OnHoverState.FillColor = Color.White;
            txtAdminTcNo.OnIdleState.FillColor = Color.White;
            txtAdminTcNo.OnActiveState.FillColor = Color.White;
        }

        private void TxtAdminNameDarkOpen()
        {
            txtAdminName.ForeColor = Color.FromArgb(249, 249, 249);
            txtAdminName.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminName.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminName.BorderColorIdle = Color.Gray;
            txtAdminName.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtAdminNameDarkClose()
        {
            txtAdminName.ForeColor = Color.FromArgb(26, 26, 26);
            txtAdminName.FillColor = Color.White;
            txtAdminName.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminName.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminName.BorderColorIdle = Color.Gray;
            txtAdminName.OnHoverState.FillColor = Color.White;
            txtAdminName.OnIdleState.FillColor = Color.White;
            txtAdminName.OnActiveState.FillColor = Color.White;
        }

        private void TxtAdminSurnameDarkOpen()
        {
            txtAdminSurname.ForeColor = Color.FromArgb(249, 249, 249);
            txtAdminSurname.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminSurname.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminSurname.BorderColorIdle = Color.Gray;
            txtAdminSurname.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }

        private void TxtAdminSurnameDarkClose()
        {
            txtAdminSurname.ForeColor = Color.FromArgb(26, 26, 26);
            txtAdminSurname.FillColor = Color.White;
            txtAdminSurname.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminSurname.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminSurname.BorderColorIdle = Color.Gray;
            txtAdminSurname.OnHoverState.FillColor = Color.White;
            txtAdminSurname.OnIdleState.FillColor = Color.White;
            txtAdminSurname.OnActiveState.FillColor = Color.White;
        }
        private void TxtAdminUsernameDarkOpen()
        {
            txtAdminUsername.ForeColor = Color.FromArgb(249, 249, 249);
            txtAdminUsername.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminUsername.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminUsername.BorderColorIdle = Color.Gray;
            txtAdminUsername.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtAdminUsernameDarkClose()
        {
            txtAdminUsername.ForeColor = Color.FromArgb(26, 26, 26);
            txtAdminUsername.FillColor = Color.White;
            txtAdminUsername.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminUsername.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminUsername.BorderColorIdle = Color.Gray;
            txtAdminUsername.OnHoverState.FillColor = Color.White;
            txtAdminUsername.OnIdleState.FillColor = Color.White;
            txtAdminUsername.OnActiveState.FillColor = Color.White;
        }
        private void TxtAdminMailDarkOpen()
        {
            txtAdminMail.ForeColor = Color.FromArgb(249, 249, 249);
            txtAdminMail.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminMail.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminMail.BorderColorIdle = Color.Gray;
            txtAdminMail.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtAdminMailDarkClose()
        {
            txtAdminMail.ForeColor = Color.FromArgb(26, 26, 26);
            txtAdminMail.FillColor = Color.White;
            txtAdminMail.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminMail.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminMail.BorderColorIdle = Color.Gray;
            txtAdminMail.OnHoverState.FillColor = Color.White;
            txtAdminMail.OnIdleState.FillColor = Color.White;
            txtAdminMail.OnActiveState.FillColor = Color.White;
        }
        private void TxtAdminPhoneNumberOpen()
        {
            txtAdminPhoneNumber.ForeColor = Color.FromArgb(249, 249, 249);
            txtAdminPhoneNumber.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminPhoneNumber.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminPhoneNumber.BorderColorIdle = Color.Gray;
            txtAdminPhoneNumber.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtAdminPhoneNumberClose()
        {
            txtAdminPhoneNumber.ForeColor = Color.FromArgb(26, 26, 26);
            txtAdminPhoneNumber.FillColor = Color.White;
            txtAdminPhoneNumber.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminPhoneNumber.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminPhoneNumber.BorderColorIdle = Color.Gray;
            txtAdminPhoneNumber.OnHoverState.FillColor = Color.White;
            txtAdminPhoneNumber.OnIdleState.FillColor = Color.White;
            txtAdminPhoneNumber.OnActiveState.FillColor = Color.White;
        }

        private void DropDownAdminGenderDarkOpen()
        {
            DropdownAdminGender.ForeColor = Color.FromArgb(249, 249, 249);
            DropdownAdminGender.BackgroundColor = Color.FromArgb(38, 38, 38);
            DropdownAdminGender.ItemBackColor = Color.FromArgb(38, 38, 38);
            DropdownAdminGender.ItemBorderColor = Color.FromArgb(38, 38, 38);
        }
        private void DropDownAdminGenderDarkClose()
        {
            DropdownAdminGender.ForeColor = Color.Black;
            DropdownAdminGender.BackgroundColor = Color.White;
            DropdownAdminGender.ItemBackColor = Color.White;
            DropdownAdminGender.ItemBorderColor = Color.White;
            DropdownAdminGender.ItemForeColor = Color.Black;
        }
        private void DatePickerAdminBornDarkOpen()
        {
            DatePickerAdminBorn.ForeColor = Color.FromArgb(249, 249, 249);
            DatePickerAdminBorn.BackColor = Color.FromArgb(38, 38, 38);
        }
        private void DatePickerAdminBornDarkClose()
        {
            DatePickerAdminBorn.ForeColor = Color.Black;
            DatePickerAdminBorn.BackColor = Color.White;
        }

        private void btn_AdminAddDarkOpen()
        {
            btn_AdminAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AdminAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminAdd.IdleFillColor = Color.FromArgb(124, 86, 216);

            btn_AdminAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AdminAdd.onHoverState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AdminAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminAdd.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AdminAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AdminAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);
        }

        private void btn_AdminAddDarkClose()
        {
            btn_AdminAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AdminAdd.IdleBorderColor = Color.Black;
            btn_AdminAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AdminAdd.onHoverState.BorderColor = Color.Black;
            btn_AdminAdd.onHoverState.FillColor = Color.DimGray;

            btn_AdminAdd.OnIdleState.BorderColor = Color.Black;
            btn_AdminAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AdminAdd.OnPressedState.BorderColor = Color.Black;
            btn_AdminAdd.OnPressedState.FillColor = Color.Gray;
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
                if (bunifuTextBox.Name == "txtAdminTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtAdminPhoneNumber")
                {
                    adminPhoneNumberProperty();
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
                if (bunifuTextBox.Name == "txtAdminTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtAdminPhoneNumber")
                {
                    adminPhoneNumberProperty();
                }
            }
            else
            {
                if (bunifuTextBox.Name == "txtAdminTcNo")
                {
                    txtAdminTcNo.MaxLength = 11;
                }
                else if (bunifuTextBox.Name == "txtAdminPhoneNumber")
                {
                    txtAdminPhoneNumber.MaxLength = 10;
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
        private void adminUsername_TextChanged(object sender, EventArgs e)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string currentText = txtAdminUsername.Text;

            string filteredText = new string(currentText.Where(c => allowedCharacters.Contains(c)).ToArray()); //her bir karakteri teker teker kontl eder. allowCharacters ile aynıysa kalır.degilse karakteri siler.

            if (currentText != filteredText)
            {
                int cursorPosition = txtAdminUsername.SelectionStart;
                txtAdminUsername.Text = filteredText;
                txtAdminUsername.SelectionStart = Math.Min(cursorPosition, filteredText.Length); // kücük olanı seçer
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
            panelAdminMailRules.Visible = true;
            string email = txtAdminMail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                panelAdminMailRules.Visible = false;
            }
            else if (IsValidEmail(email))
            {
                labelAdminMailRules1.Text = "Geçerli Mail";
                labelAdminMailRules1.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxAdminMailRulesInCorrect.Visible = false;
                pictureBoxAdminMailRulesCorrect.Visible = true;
            }
            else
            {
                labelAdminMailRules1.Text = "Geçersiz Mail !";
                labelAdminMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxAdminMailRulesInCorrect.Visible = true;
                pictureBoxAdminMailRulesCorrect.Visible = false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

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


        private async void btn_AdminAdd_Click(object sender, EventArgs e)
        {
            string tc = txtAdminTcNo.Text.Trim();
            string ad = txtAdminName.Text.Trim();
            string soyad = txtAdminSurname.Text.Trim();
            string mail = txtAdminMail.Text.Trim();
            string telefon = txtAdminPhoneNumber.Text.Trim();
            string cinsiyet = DropdownAdminGender.SelectedItem?.ToString();
            DateTime dogumTarihi = DatePickerAdminBorn.Value;
            string username = txtAdminUsername.Text.Trim();

            if (!(tc == "") && !(ad == "") && !(soyad == "") && !(mail == "") && !(telefon == "") && !(cinsiyet == "") && !(dogumTarihi.ToString() == "") &&
                !(username == "") && !(tc == null) && !(ad == null) && !(soyad == null) && !(mail == null) && !(telefon == null) && !(cinsiyet == null) && !(dogumTarihi.ToString() == null) &&
                !(username == null) && pictureBoxAdminMailRulesCorrect.Visible)
            {
                await AdminSave(tc, ad, soyad, mail, telefon, cinsiyet, dogumTarihi, username);
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("required");
                dataChooseForm.Show();
            }
        }

        public async Task AdminSave(string tc, string ad, string soyad, string mail, string telefon, string cinsiyet, DateTime dogumTarihi, string username)
        {
            string operation = "add";
            string query = @"INSERT INTO Admins (Name, Surname, Gender, Date, TcNo, Email, EmailConfirm, PhoneNumber,Username,HaveToPassword) VALUES (@Name, @Surname, @Gender, @Date, @TcNo, @Email, @EmailConfirm, @PhoneNumber,@Username,@HaveToPassword)";

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

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            AdminAddUpdateDeleteSuccessForm adminAddUpdateDeleteSuccessForm = new AdminAddUpdateDeleteSuccessForm(operation, 1);
                            adminAddUpdateDeleteSuccessForm.Show();
                            this.AccessibleName = "Success";
                            ClearAdminAdd();
                        }
                        else
                        {
                            MessageBox.Show("Yönetici kaydı yapılamadı!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
        private void ClearAdminAdd()
        {
            txtAdminTcNo.Clear();
            txtAdminName.Clear();
            txtAdminSurname.Clear();
            txtAdminMail.Clear();
            txtAdminPhoneNumber.Clear();
            txtAdminUsername.Clear();
            DropdownAdminGender.Text = "Cinsiyet Seçiniz...";
            DatePickerAdminBorn.Value = DateTime.Now;
        }

        private void AdminAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_AdminAdd_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void labelRandomUsername_Click(object sender, EventArgs e)
        {
            if (txtAdminName.Text != "" && txtAdminName.Text != null && txtAdminSurname.Text != "" && txtAdminSurname.Text != null)
            {
                string combinedName = RemoveTurkishCharacters(txtAdminName.Text.ToLower().Replace(" ", "") + txtAdminSurname.Text.ToLower().Replace(" ", ""));

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

                txtAdminUsername.Text = userName;
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