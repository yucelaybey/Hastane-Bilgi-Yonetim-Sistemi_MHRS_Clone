using AnimatorNS;
using Bunifu.UI.WinForms;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.LookAndFeel;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class PasswordResetForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");

        string tcNo;
        string email;
        string tag;

        private Animator animatorPassword;
        private Animator animatorLeft;
        private Animator animator;
        public PasswordResetForm(string _tag)
        {
            tag = _tag;

            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();
            AnimatorProperty();

            this.KeyPreview = true;
        }
        private void PasswordReset_Load(object sender, EventArgs e)
        {
            tcProperty();
            passwordConfirmProperty();
            newPasswordProperty();
        }
        private void tcProperty()
        {
            txtPasswordResetTcNo.PlaceholderText = "TC Kimlik No";
            txtPasswordResetTcNo.MaxLength = 13;
        }

        private void passwordConfirmProperty()
        {
            pictureBoxNewPasswordShow.Visible = false;
            pictureBoxNewPasswordConfirmShow.Visible = false;


            labelNewPasswordRules1.ForeColor = Color.FromArgb(224, 79, 95);
            labelNewPasswordRules2.ForeColor = Color.FromArgb(224, 79, 95);
            labelNewPasswordRules3.ForeColor = Color.FromArgb(224, 79, 95);
            labelNewPasswordRules4.ForeColor = Color.FromArgb(224, 79, 95);
            labelNewPasswordRules5.ForeColor = Color.FromArgb(224, 79, 95);
            labelNewPasswordRules6.ForeColor = Color.FromArgb(224, 79, 95);
        }

        private void newPasswordProperty()
        {
            txtNewPassword.PlaceholderText = "Yeni Şifre";
            txtNewPassword.UseSystemPasswordChar = false;

            txtNewPasswordConfirm.PlaceholderText = "Tekrar Yeni Şifre";
            txtNewPasswordConfirm.UseSystemPasswordChar = false;
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

                pictureBoxPasswordResetMiddleDark.Visible = true;
                pictureBoxPasswordResetMiddleWhite.Visible = false;

                labelPasswordResetDescription.BackColor = Color.FromArgb(26, 26, 26);
                labelPasswordResetDescription.ForeColor = Color.Red;

                labelPasswordResetTcNoRequired.BackColor = Color.FromArgb(26, 26, 26);
                labelPasswordResetTcNoRequired.ForeColor = Color.Red;

                labelPassswordResetMailRequired.BackColor = Color.FromArgb(26, 26, 26);
                labelPassswordResetMailRequired.ForeColor = Color.Red;

                labelPasswordResetTcNo.BackColor = Color.FromArgb(26, 26, 26);
                labelPasswordResetTcNo.ForeColor = Color.FromArgb(249, 249, 249);

                labelPasswordResetMail.BackColor = Color.FromArgb(26, 26, 26);
                labelPasswordResetMail.ForeColor = Color.FromArgb(249, 249, 249);

                labelTcNoRequired.BackColor = Color.FromArgb(26, 26, 26);
                labelTcNoRequired.ForeColor = Color.Red;

                labelMailRequired.BackColor = Color.FromArgb(26, 26, 26);
                labelMailRequired.ForeColor = Color.Red;

                labelPasswordResetMailRules.BackColor = Color.FromArgb(26, 26, 26);
                labelPasswordResetMailRules.ForeColor = Color.FromArgb(249, 249, 249);

                //NewPassword

                pictureBoxNewPasswordMiddleDark.Visible = true;
                pictureBoxNewPasswordMiddleWhite.Visible = false;

                labelNewPassword.BackColor = Color.FromArgb(26, 26, 26);
                labelNewPassword.ForeColor = Color.FromArgb(249, 249, 249);

                labelNewPasswordConfirm.BackColor = Color.FromArgb(26, 26, 26);
                labelNewPasswordConfirm.ForeColor = Color.FromArgb(249, 249, 249);
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                pictureBoxPasswordResetMiddleDark.Visible = false;
                pictureBoxPasswordResetMiddleWhite.Visible = true;

                labelPasswordResetDescription.BackColor = Color.FromArgb(249, 249, 249);
                labelPasswordResetDescription.ForeColor = Color.Red;

                labelPasswordResetTcNoRequired.BackColor = Color.FromArgb(249, 249, 249);
                labelPasswordResetTcNoRequired.ForeColor = Color.Red;

                labelPassswordResetMailRequired.BackColor = Color.FromArgb(249, 249, 249);
                labelPassswordResetMailRequired.ForeColor = Color.Red;

                labelPasswordResetTcNo.BackColor = Color.FromArgb(249, 249, 249);
                labelPasswordResetTcNo.ForeColor = Color.FromArgb(26, 26, 26);

                labelPasswordResetMail.BackColor = Color.FromArgb(249, 249, 249);
                labelPasswordResetMail.ForeColor = Color.FromArgb(26, 26, 26);

                labelTcNoRequired.BackColor = Color.FromArgb(249, 249, 249);
                labelTcNoRequired.ForeColor = Color.Red;

                labelMailRequired.BackColor = Color.FromArgb(249, 249, 249);
                labelMailRequired.ForeColor = Color.Red;

                labelPasswordResetMailRules.BackColor = Color.FromArgb(249, 249, 249);
                labelPasswordResetMailRules.ForeColor = Color.FromArgb(26, 26, 26);

                //NewPassword

                pictureBoxNewPasswordMiddleDark.Visible = false;
                pictureBoxNewPasswordMiddleWhite.Visible = true;

                labelNewPassword.BackColor = Color.FromArgb(249, 249, 249);
                labelNewPassword.ForeColor = Color.FromArgb(26, 26, 26);

                labelNewPasswordConfirm.BackColor = Color.FromArgb(249, 249, 249);
                labelNewPasswordConfirm.ForeColor = Color.FromArgb(26, 26, 26);
            }
        }

        private void AnimatorProperty()
        {
            animator = new Animator();
            animatorLeft = new Animator();
            animatorPassword = new Animator();
            animator.AnimationType = AnimationType.HorizSlide;
            animatorLeft.AnimationType = AnimationType.HorizSlide;
            animatorPassword.AnimationType = AnimationType.Scale;
            animatorPassword.TimeStep = 0.15f;
            animatorLeft.DefaultAnimation.SlideCoeff = new PointF(-1, 0);
        }

        private void ResetPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (panelPasswordResetAuthorize.Visible && btn_ResetPasswordConfirm.Enabled)
                {
                    btn_ResetPasswordConfirm_Click(sender, e);
                }
                else if (panelNewPassword.Visible && btn_NewPasswordRegister.Enabled)
                {
                    btn_NewPasswordRegister_Click(sender, e);
                }
                e.SuppressKeyPress = true; // Enter tuşunun diğer işlevlerini engelle
            }
        }

        private void tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakamlara izin ver
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Karakteri iptal et
            }
        }

        private void TcNo_LeaveFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPasswordResetTcNo.Text))
            {
                tcProperty();
            }
        }
        private void TcNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPasswordResetTcNo.Text))
            {
                tcProperty();
            }
            else
            {
                txtPasswordResetTcNo.MaxLength = 11;
                labelPasswordResetTcNoRequired.Visible = false;
            }
        }

        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {
            string email = txtPasswordResetMail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                panelPasswordResetMailRules.Visible = false;
            }
            else if (IsValidEmail(email))
            {
                panelPasswordResetMailRules.Visible = true;
                labelPassswordResetMailRequired.Visible = false;

            }
            else
            {
                panelPasswordResetMailRules.Visible = false;
                labelPassswordResetMailRequired.Visible = false;
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

            string AllowedCharactersPattern = @"^[a-zA-Z0-9._]+$";
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

            string[] AllowedDomains =
            {
            "gmail.com",
            "outlook.com",
            "hotmail.com",
            "outlook.com.tr",
            "yandex.com",
            "yahoo.com"
            };
            if (!AllowedDomains.Contains(domainPart.ToLower()))
            {
                return false;
            }

            return true;
        }

        private void TxtNewPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text) && string.IsNullOrEmpty(txtNewPasswordConfirm.Text))
            {
                newPasswordProperty();
            }
            else if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                txtNewPassword.PlaceholderText = "Yeni Şifre";
                txtNewPassword.UseSystemPasswordChar = false;
            }
            else if (string.IsNullOrEmpty(txtNewPasswordConfirm.Text))
            {
                txtNewPasswordConfirm.PlaceholderText = "Tekrar Yeni Şifre";
                txtNewPasswordConfirm.UseSystemPasswordChar = false;
            }
        }

        private void TxtNewPassword_TextChanged(object sender, EventArgs e)
        {

            if (txtNewPassword.Text == "Yeni Şifre" || txtNewPassword.Text == null)
            {
                return;
            }

            ValidatePassword();
            CheckPasswordsMatch();

            labelNewPasswordReq.Visible = false;

            if (pictureBoxNewPasswordHide.Visible)
            {
                if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    txtNewPassword.PlaceholderText = "Yeni Şifre";
                    txtNewPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    txtNewPassword.UseSystemPasswordChar = true;
                }
            }


        }

        private void TxtNewPasswordConfirm_TextChanged(object sender, EventArgs e)
        {

            if (txtNewPasswordConfirm.Text == "Tekrar Yeni Şifre" || txtNewPasswordConfirm.Text == null)
            {
                return;
            }

            labelNewPasswordConfirmReq.Visible = false;

            CheckPasswordsMatch();

            if (pictureBoxNewPasswordConfirmHide.Visible)
            {
                if (string.IsNullOrEmpty(txtNewPasswordConfirm.Text))
                {
                    txtNewPasswordConfirm.PlaceholderText = "Tekrar Yeni Şifre";
                    txtNewPasswordConfirm.UseSystemPasswordChar = false;
                }
                else
                {
                    txtNewPasswordConfirm.UseSystemPasswordChar = true;
                }
            }
        }

        //Şifre kurallarını kontrol et ve UI güncelle
        private void ValidatePassword()
        {
            string password = txtNewPassword.Text;

            // 1. Kural: Şifre en az 6 karakter olmalı
            if (password.Length >= 6)
            {
                labelNewPasswordRules1.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxNewPasswordRulesInCorrect1.Visible = false;
                pictureBoxNewPasswordRulesCorrect1.Visible = true;
            }
            else
            {
                labelNewPasswordRules1.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxNewPasswordRulesInCorrect1.Visible = true;
                pictureBoxNewPasswordRulesCorrect1.Visible = false;
            }

            // 2. Kural: Büyük harf içermeli
            if (Regex.IsMatch(password, @"[A-Z]"))
            {
                labelNewPasswordRules2.ForeColor = System.Drawing.Color.FromArgb(50, 190, 166);
                pictureBoxNewPasswordRulesInCorrect2.Visible = false;
                pictureBoxNewPasswordRulesCorrect2.Visible = true;
            }
            else
            {
                labelNewPasswordRules2.ForeColor = System.Drawing.Color.FromArgb(224, 79, 95);
                pictureBoxNewPasswordRulesInCorrect2.Visible = true;
                pictureBoxNewPasswordRulesCorrect2.Visible = false;
            }

            // 3. Kural: Küçük harf içermeli
            if (Regex.IsMatch(password, @"[a-z]"))
            {
                labelNewPasswordRules3.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxNewPasswordRulesInCorrect3.Visible = false;
                pictureBoxNewPasswordRulesCorrect3.Visible = true;
            }
            else
            {
                labelNewPasswordRules3.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxNewPasswordRulesInCorrect3.Visible = true;
                pictureBoxNewPasswordRulesCorrect3.Visible = false;
            }

            // 4. Kural: Rakam içermeli
            if (Regex.IsMatch(password, @"[0-9]"))
            {
                labelNewPasswordRules4.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxNewPasswordRulesInCorrect4.Visible = false;
                pictureBoxNewPasswordRulesCorrect4.Visible = true;
            }
            else
            {
                labelNewPasswordRules4.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxNewPasswordRulesInCorrect4.Visible = true;
                pictureBoxNewPasswordRulesCorrect4.Visible = false;
            }

            // 5. Kural: Özel karakter içermeli
            if (Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>+]"))
            {
                labelNewPasswordRules5.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxNewPasswordRulesInCorrect5.Visible = false;
                pictureBoxNewPasswordRulesCorrect5.Visible = true;
            }
            else
            {
                labelNewPasswordRules5.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxNewPasswordRulesInCorrect5.Visible = true;
                pictureBoxNewPasswordRulesCorrect5.Visible = false;
            }
        }

        //Şifrelerin eşleşmesini kontrol et
        private void CheckPasswordsMatch()
        {
            string password = txtNewPassword.Text;
            string passwordConfirm = txtNewPasswordConfirm.Text;

            if (password == passwordConfirm && !string.IsNullOrEmpty(passwordConfirm))
            {
                labelNewPasswordRules6.Text = "Şifreler uyumlu.";
                labelNewPasswordRules6.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxNewPasswordRulesInCorrect6.Visible = false;
                pictureBoxNewPasswordRulesCorrect6.Visible = true;
            }
            else
            {
                labelNewPasswordRules6.Text = "Şifreler uyuşmuyor.";
                labelNewPasswordRules6.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxNewPasswordRulesInCorrect6.Visible = true;
                pictureBoxNewPasswordRulesCorrect6.Visible = false;
            }
        }

        private void PictureScaleUp(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                if (pictureBox.Name == "pictureBoxNewPasswordHide" || pictureBox.Name == "pictureBoxNewPasswordShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(375, 225);
                }
                else if (pictureBox.Name == "pictureBoxNewPasswordConfirmHide" || pictureBox.Name == "pictureBoxNewPasswordConfirmShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(375, 316);
                }
            }
        }

        private void PictureScaleDown(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                if (pictureBox.Name == "pictureBoxNewPasswordHide" || pictureBox.Name == "pictureBoxNewPasswordShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(378, 227);
                }
                else if (pictureBox.Name == "pictureBoxNewPasswordConfirmHide" || pictureBox.Name == "pictureBoxNewPasswordConfirmShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(378, 318);
                }
            }
        }

        private async void PasswordShowHide(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.Name == "pictureBoxNewPasswordHide" || pictureBox.Name == "pictureBoxNewPasswordShow" || pictureBox.Name == "pictureBoxNewPasswordConfirmHide" || pictureBox.Name == "pictureBoxNewPasswordConfirmShow")
            {
                if (pictureBox.AccessibleName == "0")
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxNewPasswordHide));
                    await Task.Run(() => animatorPassword.Hide(pictureBoxNewPasswordConfirmHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxNewPasswordShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxNewPasswordConfirmShow));
                    txtNewPassword.UseSystemPasswordChar = false;
                    txtNewPasswordConfirm.UseSystemPasswordChar = false;
                }
                else
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxNewPasswordShow));
                    await Task.Run(() => animatorPassword.Hide(pictureBoxNewPasswordConfirmShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxNewPasswordHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxNewPasswordConfirmHide));
                    if (string.IsNullOrEmpty(txtNewPassword.Text) && string.IsNullOrEmpty(txtNewPasswordConfirm.Text))
                    {
                        txtNewPassword.UseSystemPasswordChar = false;
                        txtNewPasswordConfirm.UseSystemPasswordChar = false;
                    }
                    else if (string.IsNullOrEmpty(txtNewPassword.Text))
                    {
                        txtNewPassword.UseSystemPasswordChar = false;
                        txtNewPasswordConfirm.UseSystemPasswordChar = true;
                    }
                    else if (string.IsNullOrEmpty(txtNewPasswordConfirm.Text))
                    {
                        txtNewPassword.UseSystemPasswordChar = true;
                        txtNewPasswordConfirm.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        txtNewPassword.UseSystemPasswordChar = true;
                        txtNewPasswordConfirm.UseSystemPasswordChar = true;
                    }

                }
            }
        }

        private  void NewPasswordPanel()
        {
            panelPasswordResetAuthorize.Visible = false;
            panelNewPassword.Visible = true;
        }

        private void btn_ResetPasswordConfirm_Click(object sender, EventArgs e)
        {
            if (txtPasswordResetTcNo.Text != "" && txtPasswordResetMail.Text != "" && txtPasswordResetTcNo.Text != null && txtPasswordResetMail.Text != null)
            {

                tcNo = txtPasswordResetTcNo.Text;
                email = txtPasswordResetMail.Text;

                if (tag == "patient")
                {
                    PatientDataConfirm();
                }
                else if (tag == "doctor")
                {
                    DoctorDataConfirm();
                }
                else if (tag == "admin")
                {
                    AdminDataConfirm();
                }
                else if (txtPasswordResetTcNo.Text == "" && txtPasswordResetMail.Text == "")
                {
                    labelPasswordResetTcNoRequired.Visible = true;
                    labelPassswordResetMailRequired.Visible = true;
                }
                else if (txtPasswordResetTcNo.Text == "")
                {
                    labelPasswordResetTcNoRequired.Visible = true;
                }
                else if (txtPasswordResetMail.Text == "")
                {
                    labelPassswordResetMailRequired.Visible = true;
                }
            }
        }
        private void PatientDataConfirm()
        {
            string query = "SELECT Name,Surname,EmailConfirm FROM Patients WHERE TcNo = @TcNo AND Email = @Email";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TcNo", tcNo);
                        command.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                if (reader.Read())
                                {

                                    string name = reader["Name"].ToString();
                                    string surname = reader["Surname"].ToString();
                                    string emailConfirm = reader["EmailConfirm"].ToString();

                                    string nameSurname = name + " " + surname;

                                    if (emailConfirm == "True")
                                    {
                                        this.Hide();
                                        ConfirmScreenForm confirmScreenForm = new ConfirmScreenForm(tcNo, email, nameSurname, "passConfirm");
                                        confirmScreenForm.ShowDialog();
                                        string result = confirmScreenForm.AccessibleName.ToString();
                                        if (result == "Continue")
                                        {
                                            this.Show();
                                            NewPasswordPanel();
                                        }
                                        else if (result == "Cancel")
                                        {
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        this.Hide();
                                        EmailConfirmFalseForm emailConfirmFalseForm = new EmailConfirmFalseForm(tcNo, email, nameSurname,tag);
                                        emailConfirmFalseForm.ShowDialog();
                                        this.Show();
                                    }
                                }
                                else
                                {
                                    UserNotFoundForm userNotFoundForm = new UserNotFoundForm();
                                    userNotFoundForm.ShowDialog();
                                }
                            }
                            else
                            {
                                UserNotFoundForm userNotFoundForm = new UserNotFoundForm();
                                userNotFoundForm.ShowDialog();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajını göster
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }
        }

        private void DoctorDataConfirm()
        {
            string query = "SELECT Name,Surname,EmailConfirm FROM Doctors WHERE TcNo = @TcNo AND Email = @Email";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TcNo", tcNo);
                        command.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                if (reader.Read())
                                {

                                    string name = reader["Name"].ToString();
                                    string surname = reader["Surname"].ToString();
                                    string emailConfirm = reader["EmailConfirm"].ToString();

                                    string nameSurname = name + " " + surname;

                                    if (emailConfirm == "True")
                                    {
                                        this.Hide();
                                        ConfirmScreenForm confirmScreenForm = new ConfirmScreenForm(tcNo, email, nameSurname, "passConfirm");
                                        confirmScreenForm.ShowDialog();
                                        string result = confirmScreenForm.AccessibleName.ToString();
                                        if (result == "Continue")
                                        {
                                            this.Show();
                                            NewPasswordPanel();
                                        }
                                        else if (result == "Cancel")
                                        {
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        this.Hide();
                                        EmailConfirmFalseForm emailConfirmFalseForm = new EmailConfirmFalseForm(tcNo, email, nameSurname,tag);
                                        emailConfirmFalseForm.ShowDialog();
                                        this.Show();
                                    }
                                }
                                else
                                {
                                    UserNotFoundForm userNotFoundForm = new UserNotFoundForm();
                                    userNotFoundForm.ShowDialog();
                                }
                            }
                            else
                            {
                                UserNotFoundForm userNotFoundForm = new UserNotFoundForm();
                                userNotFoundForm.ShowDialog();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajını göster
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }
        }

        private void AdminDataConfirm()
        {
            string query = "SELECT Name,Surname,EmailConfirm FROM Admins WHERE TcNo = @TcNo AND Email = @Email";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TcNo", tcNo);
                        command.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                if (reader.Read())
                                {

                                    string name = reader["Name"].ToString();
                                    string surname = reader["Surname"].ToString();
                                    string emailConfirm = reader["EmailConfirm"].ToString();

                                    string nameSurname = name + " " + surname;

                                    if (emailConfirm == "True")
                                    {
                                        this.Hide();
                                        ConfirmScreenForm confirmScreenForm = new ConfirmScreenForm(tcNo, email, nameSurname, "passConfirm");
                                        confirmScreenForm.ShowDialog();
                                        string result = confirmScreenForm.AccessibleName.ToString();
                                        if (result == "Continue")
                                        {
                                            this.Show();
                                            NewPasswordPanel();
                                        }
                                        else if (result == "Cancel")
                                        {
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        this.Hide();
                                        EmailConfirmFalseForm emailConfirmFalseForm = new EmailConfirmFalseForm(tcNo, email, nameSurname,tag);
                                        emailConfirmFalseForm.ShowDialog();
                                        this.Show();
                                    }
                                }
                                else
                                {
                                    UserNotFoundForm userNotFoundForm = new UserNotFoundForm();
                                    userNotFoundForm.ShowDialog();
                                }
                            }
                            else
                            {
                                UserNotFoundForm userNotFoundForm = new UserNotFoundForm();
                                userNotFoundForm.ShowDialog();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajını göster
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }
        }
        private void btn_NewPasswordRegister_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == txtNewPasswordConfirm.Text &&
                pictureBoxNewPasswordRulesCorrect1.Visible &&
                pictureBoxNewPasswordRulesCorrect2.Visible &&
                pictureBoxNewPasswordRulesCorrect3.Visible &&
                pictureBoxNewPasswordRulesCorrect4.Visible &&
                pictureBoxNewPasswordRulesCorrect5.Visible &&
                pictureBoxNewPasswordRulesCorrect6.Visible)
            {
                string password = txtNewPassword.Text;
                string passwordConfirm = txtNewPasswordConfirm.Text;
                
                if (tag == "patient")
                {
                    PatientDataConfirm(password,passwordConfirm,tcNo,email);
                }
                else if (tag == "doctor")
                {
                    DoctorDataConfirm(password, passwordConfirm, tcNo, email);
                }
                else if (tag == "admin")
                {
                    AdminDataConfirm(password, passwordConfirm, tcNo, email);
                }
            }
            else if (txtNewPassword.Text == "" && txtNewPasswordConfirm.Text == "")
            {
                labelNewPasswordReq.Visible = true;
                labelNewPasswordConfirmReq.Visible = true;
            }
            else if (txtNewPassword.Text == "")
            {
                labelNewPasswordReq.Visible = true;
            }
            else if (txtNewPasswordConfirm.Text == "")
            {
                labelNewPasswordConfirmReq.Visible = true;
            }
        }
        private void PatientDataConfirm(string password, string passwordConfirm,string tcNo,string email)
        {
            string updateQuery = "UPDATE Patients SET Password = @Password,PasswordConfirm = @PasswordConfirm WHERE TcNo = @TcNo AND Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@PasswordConfirm", passwordConfirm);
                        command.Parameters.AddWithValue("@TcNo", tcNo);
                        command.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            this.Hide();
                            NewPasswordSuccesForm newPasswordSuccesForm = new NewPasswordSuccesForm();
                            newPasswordSuccesForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya güncellenemedi.", "Hata");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }
        }
        private void DoctorDataConfirm(string password, string passwordConfirm, string tcNo, string email)
        {
            string updateQuery = "UPDATE Doctors SET Password = @Password,PasswordConfirm = @PasswordConfirm WHERE TcNo = @TcNo AND Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@PasswordConfirm", passwordConfirm);
                        command.Parameters.AddWithValue("@TcNo", tcNo);
                        command.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            this.Hide();
                            NewPasswordSuccesForm newPasswordSuccesForm = new NewPasswordSuccesForm();
                            newPasswordSuccesForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya güncellenemedi.", "Hata");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }
        }
        private void AdminDataConfirm(string password, string passwordConfirm, string tcNo, string email)
        {
            string updateQuery = "UPDATE Admins SET Password = @Password,PasswordConfirm = @PasswordConfirm WHERE TcNo = @TcNo AND Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@PasswordConfirm", passwordConfirm);
                        command.Parameters.AddWithValue("@TcNo", tcNo);
                        command.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            this.Hide();
                            NewPasswordSuccesForm newPasswordSuccesForm = new NewPasswordSuccesForm();
                            newPasswordSuccesForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya güncellenemedi.", "Hata");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                }
            }
        }
        private void PasswordResetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (panelNewPassword.Visible)
            {
                NotVerifiedClosingForm notVerifiedClosingForm = new NotVerifiedClosingForm();
                notVerifiedClosingForm.ShowDialog();

                if (notVerifiedClosingForm.AccessibleName == "Cancel")
                {

                }
                else if(notVerifiedClosingForm.AccessibleName == "Continue")
                {
                    e.Cancel = true;
                }
            }  
        }
    }
}