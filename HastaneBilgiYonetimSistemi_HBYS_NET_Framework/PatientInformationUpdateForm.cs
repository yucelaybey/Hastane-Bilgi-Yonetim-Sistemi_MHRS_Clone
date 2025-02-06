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
    public partial class PatientInformationUpdateForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string tcno;
        private readonly int id;
        private readonly string name;
        private readonly string surname;
        private readonly string gender;
        private readonly string date;
        private readonly string mail;
        private readonly string phonenumber;
        private readonly string username;

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
        public PatientInformationUpdateForm(string _tcno, int _id, string _name, string _surname, string _gender, string _date, string _mail, string _phonenumber)
        {
            tcno = _tcno;
            id = _id;
            name = _name;
            surname = _surname;
            gender = _gender;
            date = _date;
            mail = _mail;
            phonenumber = _phonenumber;

            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private void PatientUpdateForm_Load(object sender, EventArgs e)
        {
            tcProperty();
            otherProperty();
            ScreenProperty();
        }
        private void tcProperty()
        {
            txtPatientTcNo.PlaceholderText = "TC Kimlik No";
            txtPatientTcNo.MaxLength = 13;
        }
        private void otherProperty()
        {
            labelPatientMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
        }
        private void ScreenProperty()
        {
            txtPatientTcNo.Text = tcno;
            txtPatientName.Text = name;
            txtPatientSurname.Text = surname;
            DropdownPatientGender.SelectedItem = gender.ToString();
            DatePickerPatientBorn.Text = date;
            txtPatientMail.Text = mail;
            txtPatientPhoneNumber.Text = phonenumber;
        }
        private void PatientPhoneNumberProperty()
        {
            txtPatientPhoneNumber.PlaceholderText = "(5xx) xxx xxxx";
            txtPatientPhoneNumber.MaxLength = 15;
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

                LabelPatientDarkOpen();

                TxtPatientTcNoDarkOpen();

                TxtPatientNameDarkOpen();

                TxtPatientSurnameDarkOpen();

                TxtPatientMailDarkOpen();

                TxtPatientPhoneNumberOpen();

                DropDownPatientGenderDarkOpen();

                DatePickerPatientBornDarkOpen();

                label_FooterDarkOpen();
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(245, 245, 245);

                picturboxDarkClose();

                PanelDarkClose();

                LabelPatientDarkClose();

                TxtPatientTcNoDarkClose();

                TxtPatientNameDarkClose();

                TxtPatientSurnameDarkClose();

                TxtPatientMailDarkClose();

                TxtPatientPhoneNumberClose();

                DropDownPatientGenderDarkClose();

                DatePickerPatientBornDarkClose();

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
            panelGradient.GradientBottomLeft = Color.Black;
            panelGradient.GradientBottomRight = Color.Black;
            panelGradient.GradientTopLeft = Color.Black;
            panelGradient.GradientTopRight = Color.Black;

            panelInPatientUpdate.BackgroundColor = Color.Black;
        }

        private void PanelDarkClose()
        {
            panelGradient.GradientBottomLeft = Color.White;
            panelGradient.GradientBottomRight = Color.White;
            panelGradient.GradientTopLeft = Color.White;
            panelGradient.GradientTopRight = Color.White;

            panelInPatientUpdate.BackgroundColor = Color.White;
        }

        private void LabelPatientDarkOpen()
        {
            labelPatientTcNo.ForeColor = Color.White;
            labelPatientName.ForeColor = Color.White;
            labelPatientSurname.ForeColor = Color.White;
            labelPatientMail.ForeColor = Color.White;
            labelPatientPhoneNumber.ForeColor = Color.White;
            labelPatientGender.ForeColor = Color.White;
            labelPatientBorn.ForeColor = Color.White;
        }
        private void LabelPatientDarkClose()
        {
            labelPatientTcNo.ForeColor = Color.Black;
            labelPatientName.ForeColor = Color.Black;
            labelPatientSurname.ForeColor = Color.Black;
            labelPatientMail.ForeColor = Color.Black;
            labelPatientPhoneNumber.ForeColor = Color.Black;
            labelPatientGender.ForeColor = Color.Black;
            labelPatientBorn.ForeColor = Color.Black;
        }

        private void TxtPatientTcNoDarkOpen()
        {
            txtPatientTcNo.ForeColor = Color.FromArgb(249, 249, 249);
            txtPatientTcNo.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientTcNo.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtPatientTcNo.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtPatientTcNo.BorderColorIdle = Color.Gray;
            txtPatientTcNo.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientTcNo.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientTcNo.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }

        private void TxtPatientTcNoDarkClose()
        {
            txtPatientTcNo.ForeColor = Color.FromArgb(26, 26, 26);
            txtPatientTcNo.FillColor = Color.White;
            txtPatientTcNo.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtPatientTcNo.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtPatientTcNo.BorderColorIdle = Color.Gray;
            txtPatientTcNo.OnHoverState.FillColor = Color.White;
            txtPatientTcNo.OnIdleState.FillColor = Color.White;
            txtPatientTcNo.OnActiveState.FillColor = Color.White;
        }

        private void TxtPatientNameDarkOpen()
        {
            txtPatientName.ForeColor = Color.FromArgb(249, 249, 249);
            txtPatientName.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientName.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtPatientName.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtPatientName.BorderColorIdle = Color.Gray;
            txtPatientName.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientName.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientName.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtPatientNameDarkClose()
        {
            txtPatientName.ForeColor = Color.FromArgb(26, 26, 26);
            txtPatientName.FillColor = Color.White;
            txtPatientName.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtPatientName.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtPatientName.BorderColorIdle = Color.Gray;
            txtPatientName.OnHoverState.FillColor = Color.White;
            txtPatientName.OnIdleState.FillColor = Color.White;
            txtPatientName.OnActiveState.FillColor = Color.White;
        }

        private void TxtPatientSurnameDarkOpen()
        {
            txtPatientSurname.ForeColor = Color.FromArgb(249, 249, 249);
            txtPatientSurname.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientSurname.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtPatientSurname.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtPatientSurname.BorderColorIdle = Color.Gray;
            txtPatientSurname.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientSurname.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientSurname.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }

        private void TxtPatientSurnameDarkClose()
        {
            txtPatientSurname.ForeColor = Color.FromArgb(26, 26, 26);
            txtPatientSurname.FillColor = Color.White;
            txtPatientSurname.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtPatientSurname.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtPatientSurname.BorderColorIdle = Color.Gray;
            txtPatientSurname.OnHoverState.FillColor = Color.White;
            txtPatientSurname.OnIdleState.FillColor = Color.White;
            txtPatientSurname.OnActiveState.FillColor = Color.White;
        }
        private void TxtPatientMailDarkOpen()
        {
            txtPatientMail.ForeColor = Color.FromArgb(249, 249, 249);
            txtPatientMail.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientMail.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtPatientMail.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtPatientMail.BorderColorIdle = Color.Gray;
            txtPatientMail.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientMail.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientMail.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtPatientMailDarkClose()
        {
            txtPatientMail.ForeColor = Color.FromArgb(26, 26, 26);
            txtPatientMail.FillColor = Color.White;
            txtPatientMail.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtPatientMail.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtPatientMail.BorderColorIdle = Color.Gray;
            txtPatientMail.OnHoverState.FillColor = Color.White;
            txtPatientMail.OnIdleState.FillColor = Color.White;
            txtPatientMail.OnActiveState.FillColor = Color.White;
        }
        private void TxtPatientPhoneNumberOpen()
        {
            txtPatientPhoneNumber.ForeColor = Color.FromArgb(249, 249, 249);
            txtPatientPhoneNumber.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientPhoneNumber.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtPatientPhoneNumber.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtPatientPhoneNumber.BorderColorIdle = Color.Gray;
            txtPatientPhoneNumber.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientPhoneNumber.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtPatientPhoneNumber.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
        }
        private void TxtPatientPhoneNumberClose()
        {
            txtPatientPhoneNumber.ForeColor = Color.FromArgb(26, 26, 26);
            txtPatientPhoneNumber.FillColor = Color.White;
            txtPatientPhoneNumber.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtPatientPhoneNumber.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtPatientPhoneNumber.BorderColorIdle = Color.Gray;
            txtPatientPhoneNumber.OnHoverState.FillColor = Color.White;
            txtPatientPhoneNumber.OnIdleState.FillColor = Color.White;
            txtPatientPhoneNumber.OnActiveState.FillColor = Color.White;
        }

        private void DropDownPatientGenderDarkOpen()
        {
            DropdownPatientGender.ForeColor = Color.FromArgb(249, 249, 249);
            DropdownPatientGender.BackgroundColor = Color.FromArgb(38, 38, 38);
            DropdownPatientGender.ItemBackColor = Color.FromArgb(38, 38, 38);
            DropdownPatientGender.ItemBorderColor = Color.FromArgb(38, 38, 38);
        }
        private void DropDownPatientGenderDarkClose()
        {
            DropdownPatientGender.ForeColor = Color.Black;
            DropdownPatientGender.BackgroundColor = Color.White;
            DropdownPatientGender.ItemBackColor = Color.White;
            DropdownPatientGender.ItemBorderColor = Color.White;
            DropdownPatientGender.ItemForeColor = Color.Black;
        }
        private void DatePickerPatientBornDarkOpen()
        {
            DatePickerPatientBorn.ForeColor = Color.FromArgb(249, 249, 249);
            DatePickerPatientBorn.BackColor = Color.FromArgb(38, 38, 38);
        }
        private void DatePickerPatientBornDarkClose()
        {
            DatePickerPatientBorn.ForeColor = Color.Black;
            DatePickerPatientBorn.BackColor = Color.White;
        }

        private void label_FooterDarkOpen()
        {
            labelFooter.ForeColor = Color.White;
            labelFooter.BackColor = Color.Black;
        }
        private void label_FooterDarkClose()
        {
            labelFooter.ForeColor = Color.Black;
            labelFooter.BackColor = Color.White;
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
                if (bunifuTextBox.Name == "txtPatientTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtPatientPhoneNumber")
                {
                    PatientPhoneNumberProperty();
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
                if (bunifuTextBox.Name == "txtPatientTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtPatientPhoneNumber")
                {
                    PatientPhoneNumberProperty();
                }
            }
            else
            {
                if (bunifuTextBox.Name == "txtPatientTcNo")
                {
                    txtPatientTcNo.MaxLength = 11;
                }
                else if (bunifuTextBox.Name == "txtPatientPhoneNumber")
                {
                    txtPatientPhoneNumber.MaxLength = 10;
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
            panelPatientMailRules.Visible = true;
            string email = txtPatientMail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                panelPatientMailRules.Visible = false;
            }
            else if (IsValidEmail(email))
            {
                labelPatientMailRules1.Text = "Geçerli Mail";
                labelPatientMailRules1.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxPatientMailRulesInCorrect.Visible = false;
                pictureBoxPatientMailRulesCorrect.Visible = true;
            }
            else
            {
                labelPatientMailRules1.Text = "Geçersiz Mail !";
                labelPatientMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxPatientMailRulesInCorrect.Visible = true;
                pictureBoxPatientMailRulesCorrect.Visible = false;
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


        private async void btn_PatientUpdate_Click(object sender, EventArgs e)
        {
            string tc = txtPatientTcNo.Text.Trim();
            string ad = txtPatientName.Text.Trim();
            string soyad = txtPatientSurname.Text.Trim();
            string mail = txtPatientMail.Text.Trim();
            string telefon = txtPatientPhoneNumber.Text.Trim();
            string cinsiyet = DropdownPatientGender.SelectedItem?.ToString();
            DateTime dogumTarihi = DatePickerPatientBorn.Value;

            if (!(tc == "") && !(ad == "") && !(soyad == "") && !(mail == "") && !(telefon == "") && !(cinsiyet == "") && !(dogumTarihi.ToString() == "") && !(tc == null) && !(ad == null) && !(soyad == null) && !(mail == null) && !(telefon == null) && !(cinsiyet == null) && !(dogumTarihi.ToString() == null) && pictureBoxPatientMailRulesCorrect.Visible)
            {
                await PatientSave(tc, ad, soyad, mail, telefon, cinsiyet, dogumTarihi, username);
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("required");
                dataChooseForm.Show();
            }
        }

        public async Task PatientSave(string tc, string ad, string soyad, string mail, string telefon, string cinsiyet, DateTime dogumTarihi, string username)
        {
            string query = @"UPDATE Patients SET TcNo = @TcNo, Name = @Name, Surname = @Surname, Gender = @Gender, [Date] = @Date, Email = @Email, PhoneNumber = @PhoneNumber WHERE Id = @Id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@TcNo", tc);
                        command.Parameters.AddWithValue("@Name", ad);
                        command.Parameters.AddWithValue("@Surname", soyad);
                        command.Parameters.AddWithValue("@Email", mail);
                        command.Parameters.AddWithValue("@PhoneNumber", telefon);
                        command.Parameters.AddWithValue("@Gender", cinsiyet);
                        command.Parameters.AddWithValue("@Date", dogumTarihi);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            this.AccessibleName = "Success";
                            ClearPatientUpdate();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Hasta güncelleme yapılamadı!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
        private void ClearPatientUpdate()
        {
            txtPatientTcNo.Clear();
            txtPatientName.Clear();
            txtPatientSurname.Clear();
            txtPatientMail.Clear();
            txtPatientPhoneNumber.Clear();
            DropdownPatientGender.Text = "Cinsiyet Seçiniz...";
            DatePickerPatientBorn.Value = DateTime.Now;
        }

        private void PatientUpdateForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_PatientUpdate_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}