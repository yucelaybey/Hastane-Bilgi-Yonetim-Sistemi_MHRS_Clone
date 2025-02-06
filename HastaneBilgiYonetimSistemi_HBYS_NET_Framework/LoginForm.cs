using AnimatorNS;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.LookAndFeel;
using System.Text.RegularExpressions;
using Bunifu.UI.WinForms;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using System.Data;
using DevExpress.XtraReports.UI;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
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

        private Animator animator;    //Animator degiskeni
        private Animator animatorLeft;  //Animator degiskeni
        private Animator animatorPassword;  //Animator degiskeni

        private string date;        // date degeri
        private int adminPanelId;
        private string adminName;
        private bool loginLoading = true;
        private string adminSurname;
        private string mail;
        private string adminMail;
        private string nameSurname;
        private bool isDarkMode;
        private bool isProcessing = false;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");

        private Panel[] panels;
        private PictureBox[] Rightpanels;
        private int currentPanel;
        private int currentRightPanel;

        public LoginForm()
        {
            InitializeComponent();
            PerformActionBasedOnSetting();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(LoginForm_KeyDown);

            panels = new Panel[] { panelDoctorLogin, panelHastaLogin, panelAdminLogin };
            Rightpanels = new PictureBox[] { pictureBoxDoctorLoginBanner, pictureBoxHastaLoginBanner, pictureBoxAdminLoginBanner };

            currentPanel = 1;
            currentRightPanel = 1;


            animator = new Animator();
            animatorLeft = new Animator();
            animatorPassword = new Animator();
            animator.AnimationType = AnimationType.HorizSlide;
            animatorLeft.AnimationType = AnimationType.HorizSlide;
            animatorPassword.AnimationType = AnimationType.Scale;
            animatorPassword.TimeStep = 0.15f;
            animatorLeft.DefaultAnimation.SlideCoeff = new PointF(-1, 0);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            tcProperty();
            hastaregisterPhoneNumberProperty();
            hastaregisterTcProperty();
            hastaregisterPasswordProperty();
            passwordConfirmProperty();
            hastaPasswordProperty();
            doctorProperty();
            adminProperty();
            hastaregisterProperty();
            footerProperty();
            panelProperty();
            panelRightBannerProperty();
            btnProperty();
            bringToFrontProperty();

            if (loginLoading)
            {
                loginLoading = false;
                this.Hide();
                LoginLoadingForm loginLoadingForm = new LoginLoadingForm();
                loginLoadingForm.ShowDialog();
                this.Show();
            }
        }

        private void tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtHastaRegisterPassword_Enter(object sender, EventArgs e)
        {
            panelHastaRegisterPasswordRules.Visible = true;
        }

        private void TxtHastaRegisterPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHastaRegisterPassword.Text) && string.IsNullOrEmpty(txtHastaRegisterPasswordConfirm.Text))
            {
                panelHastaRegisterPasswordRules.Visible = false;
                hastaregisterPasswordProperty();
            }
            else if (string.IsNullOrEmpty(txtHastaRegisterPassword.Text))
            {
                txtHastaRegisterPassword.PlaceholderText = "Şifre";
                txtHastaRegisterPassword.UseSystemPasswordChar = false;
            }
            else if (string.IsNullOrEmpty(txtHastaRegisterPasswordConfirm.Text))
            {
                txtHastaRegisterPasswordConfirm.PlaceholderText = "Tekrar Şifre";
                txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = false;
            }
        }

        private void TxtRegisterPassword_TextChanged(object sender, EventArgs e)
        {

            if (txtHastaRegisterPassword.Text == "Şifre" || txtHastaRegisterPassword.Text == null)
            {
                return;
            }

            ValidatePassword();
            CheckPasswordsMatch();
            if (pictureBoxHastaRegisterPasswordHide.Visible)
            {
                if (string.IsNullOrEmpty(txtHastaRegisterPassword.Text))
                {
                    txtHastaRegisterPassword.PlaceholderText = "Şifre";
                    txtHastaRegisterPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    txtHastaRegisterPassword.UseSystemPasswordChar = true;
                }
            }


        }

        private void TxtRegisterPasswordConfirm_TextChanged(object sender, EventArgs e)
        {

            if (txtHastaRegisterPasswordConfirm.Text == "Tekrar Şifre" || txtHastaRegisterPasswordConfirm.Text == null)
            {
                return;
            }

            CheckPasswordsMatch();

            if (pictureBoxHastaRegisterPasswordConfirmHide.Visible)
            {
                if (string.IsNullOrEmpty(txtHastaRegisterPasswordConfirm.Text))
                {
                    txtHastaRegisterPasswordConfirm.PlaceholderText = "Tekrar Şifre";
                    txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = false;
                }
                else
                {
                    txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = true;
                }
            }
        }

        //Şifre kurallarını kontrol et ve UI güncelle
        private void ValidatePassword()
        {
            string password = txtHastaRegisterPassword.Text;

            // 1. Kural: Şifre en az 6 karakter olmalı
            if (password.Length >= 6)
            {
                labelHastaRegisterPasswordRules1.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxHastaRegisterPasswordRulesInCorrect1.Visible = false;
                pictureBoxHastaRegisterPasswordRulesCorrect1.Visible = true;
            }
            else
            {
                labelHastaRegisterPasswordRules1.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxHastaRegisterPasswordRulesInCorrect1.Visible = true;
                pictureBoxHastaRegisterPasswordRulesCorrect1.Visible = false;
            }

            // 2. Kural: Büyük harf içermeli
            if (Regex.IsMatch(password, @"[A-Z]"))
            {
                labelHastaRegisterPasswordRules2.ForeColor = System.Drawing.Color.FromArgb(50, 190, 166);
                pictureBoxHastaRegisterPasswordRulesInCorrect2.Visible = false;
                pictureBoxHastaRegisterPasswordRulesCorrect2.Visible = true;
            }
            else
            {
                labelHastaRegisterPasswordRules2.ForeColor = System.Drawing.Color.FromArgb(224, 79, 95);
                pictureBoxHastaRegisterPasswordRulesInCorrect2.Visible = true;
                pictureBoxHastaRegisterPasswordRulesCorrect2.Visible = false;
            }

            // 3. Kural: Küçük harf içermeli
            if (Regex.IsMatch(password, @"[a-z]"))
            {
                labelHastaRegisterPasswordRules3.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxHastaRegisterPasswordRulesInCorrect3.Visible = false;
                pictureBoxHastaRegisterPasswordRulesCorrect3.Visible = true;
            }
            else
            {
                labelHastaRegisterPasswordRules3.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxHastaRegisterPasswordRulesInCorrect3.Visible = true;
                pictureBoxHastaRegisterPasswordRulesCorrect3.Visible = false;
            }

            // 4. Kural: Rakam içermeli
            if (Regex.IsMatch(password, @"[0-9]"))
            {
                labelHastaRegisterPasswordRules4.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxHastaRegisterPasswordRulesInCorrect4.Visible = false;
                pictureBoxHastaRegisterPasswordRulesCorrect4.Visible = true;
            }
            else
            {
                labelHastaRegisterPasswordRules4.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxHastaRegisterPasswordRulesInCorrect4.Visible = true;
                pictureBoxHastaRegisterPasswordRulesCorrect4.Visible = false;
            }

            // 5. Kural: Özel karakter içermeli
            if (Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>+]"))
            {
                labelHastaRegisterPasswordRules5.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxHastaRegisterPasswordRulesInCorrect5.Visible = false;
                pictureBoxHastaRegisterPasswordRulesCorrect5.Visible = true;
            }
            else
            {
                labelHastaRegisterPasswordRules5.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxHastaRegisterPasswordRulesInCorrect5.Visible = true;
                pictureBoxHastaRegisterPasswordRulesCorrect5.Visible = false;
            }
        }

        //Şifrelerin eşleşmesini kontrol et
        private void CheckPasswordsMatch()
        {
            string password = txtHastaRegisterPassword.Text;
            string passwordConfirm = txtHastaRegisterPasswordConfirm.Text;

            if (password == passwordConfirm && !string.IsNullOrEmpty(passwordConfirm))
            {
                labelHastaRegisterPasswordRules6.Text = "Şifreler uyumlu.";
                labelHastaRegisterPasswordRules6.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxHastaRegisterPasswordRulesInCorrect6.Visible = false;
                pictureBoxHastaRegisterPasswordRulesCorrect6.Visible = true;
            }
            else
            {
                labelHastaRegisterPasswordRules6.Text = "Şifreler uyuşmuyor.";
                labelHastaRegisterPasswordRules6.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxHastaRegisterPasswordRulesInCorrect6.Visible = true;
                pictureBoxHastaRegisterPasswordRulesCorrect6.Visible = false;
            }
        }
        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {
            panelHastaRegisterMailRules.Visible = true;
            string email = txtHastaRegisterMail.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                panelHastaRegisterMailRules.Visible = false;
            }
            else if (IsValidEmail(email))
            {
                labelHastaRegisterMailRules1.Text = "Geçerli Mail";
                labelHastaRegisterMailRules1.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxHastaRegisterMailRulesInCorrect.Visible = false;
                pictureBoxHastaRegisterMailRulesCorrect.Visible = true;
            }
            else
            {
                labelHastaRegisterMailRules1.Text = "Geçersiz Mail !";
                labelHastaRegisterMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxHastaRegisterMailRulesInCorrect.Visible = true;
                pictureBoxHastaRegisterMailRulesCorrect.Visible = false;
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

        private void DarkModeOpen(object sender, BunifuToggleSwitch.CheckedChangedEventArgs e)
        {

            if (toggleDarkMode.Checked)
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");

                isDarkMode = true;
                SaveSettings(isDarkMode);
                otherDarkModeOpenProperty();

                if (panelHastaRegister.Visible)
                {
                    // dark mode aktif ise

                    //diger componentler

                    pictureButtonLeftDark.Visible = false;
                    pictureButtonRightDark.Visible = false;


                }
                else
                {
                    pictureButtonLeftDark.Visible = true;
                    pictureButtonRightDark.Visible = true;
                }
                //hasta paneli
                panelHastaLoginDarkModeOpenProperty();

                //doctor paneli
                panelDoctorLoginDarkModeOpenProperty();

                //admin paneli
                panelAdminLoginDarkModeOpenProperty();

                panelHastaRegisterDarkModeOpenProperty();
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");

                isDarkMode = false;
                SaveSettings(isDarkMode);
                //diger componentler
                otherDarkModeClosedProperty();
                if (panelHastaRegister.Visible)
                {
                    // dark mode aktif ise

                    //diger componentler

                    pictureButtonLeftWhite.Visible = false;
                    pictureButtonRightWhite.Visible = false;


                }
                else
                {
                    pictureButtonLeftWhite.Visible = true;
                    pictureButtonRightWhite.Visible = true;
                }
                //hasta paneli
                panelHastaLoginDarkModeClosedProperty();

                //doctor paneli
                panelDoctorLoginDarkModeClosedProperty();

                //admin paneli
                panelAdminLoginDarkModeClosedProperty();

                //hasta register paneli
                panelHastaRegisterDarkModeClosedProperty();
            }
        }

        public void PerformActionBasedOnSetting()
        {
            string setting = LoadSettings(); // Ayarı yükle

            if (setting == "dark")
            {
                toggleDarkMode.Checked = true;
                toggleDarkMode.Size = new Size(39, 20);
            }
            else if (setting == "light")
            {
                toggleDarkMode.Checked = false;
            }
            else
            {
                toggleDarkMode.Checked = false;
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
        private void SaveSettings(bool isDarkMode)
        {
            File.WriteAllText(settingsFilePath, isDarkMode ? "dark" : "light");
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (panelHastaLogin.Visible && btn_HastaLogin.Enabled)
                {
                    btn_Login_Click(sender, e);
                }
                else if (panelDoctorLogin.Visible && btn_DoctorLogin.Enabled)
                {
                    btn_DoctorLogin_Click(sender, e);
                }
                else if (panelAdminLogin.Visible && btn_AdminLogin.Enabled)
                {
                    btn_AdminLogin_Click(sender, e);
                }
                else if (panelHastaRegister.Visible && btn_HastaRegister.Enabled)
                {
                    btnRegister_Click(sender, e);
                }
            }
        }

        private void HastaRegisterEmpty()
        {
            txtHastaRegisterTcNo.Focus();
            txtHastaRegisterTcNo.Text = string.Empty;
            txtHastaRegisterName.Text = string.Empty;
            txtHastaRegisterSurname.Text = string.Empty;
            txtHastaRegisterPhoneNumber.Text = string.Empty;
            txtHastaRegisterMail.Text = string.Empty;
            txtHastaRegisterPassword.Text = string.Empty;
            txtHastaRegisterPasswordConfirm.Text = string.Empty;
            DropdownHastaRegisterGender.SelectedIndex = -1;
            DropdownHastaRegisterGender.Text = "Cinsiyet Seçiniz";
            DatePickerHastaRegisterBorn.Value = DateTime.Now.Date;

            panelHastaRegisterPasswordRules.Visible = false;
            panelHastaRegisterMailRules.Visible = false;

            pictureBoxHastaRegisterPasswordShow.Visible = false;
            pictureBoxHastaRegisterPasswordHide.Visible = true;

            pictureBoxHastaRegisterPasswordConfirmShow.Visible = false;
            pictureBoxHastaRegisterPasswordConfirmHide.Visible = true;

            hastaregisterPasswordProperty();
        }
        private void HastaLoginEmpty()
        {
            txtHastaTcNo.Text = string.Empty;
            txtHastaTcNo.PlaceholderText = "TC Kimlik";
            txtHastaPassword.Text = string.Empty;

            pictureBoxHastaLoginPasswordShow.Visible = false;
            pictureBoxHastaLoginPasswordHide.Visible = true;
        }
        private void DoctorLoginEmpty()
        {
            txtDoctorUsername.Text = string.Empty;
            txtDoctorUsername.PlaceholderText = "Kullanıcı Adınız";
            txtDoctorPassword.Text = string.Empty;

            pictureBoxDoctorLoginPasswordShow.Visible = false;
            pictureBoxDoctorLoginPasswordHide.Visible = true;
        }
        private void AdminLoginEmpty()
        {
            txtAdminUsername.Text = string.Empty;
            txtAdminUsername.PlaceholderText = "Kullanıcı Adınız";

            txtAdminPassword.Text = string.Empty;

            pictureBoxAdminLoginPasswordShow.Visible = false;
            pictureBoxAdminLoginPasswordHide.Visible = true;
        }

        private void PictureScaleUp(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            Label label = sender as Label;
            if (pictureBox != null)
            {
                if (pictureBox.Name == "pictureBoxHastaRegisterPasswordHide" || pictureBox.Name == "pictureBoxHastaRegisterPasswordShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(438, 452);
                }
                else if (pictureBox.Name == "pictureBoxHastaRegisterPasswordConfirmHide" || pictureBox.Name == "pictureBoxHastaRegisterPasswordConfirmShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(438, 502);
                }
                else if (pictureBox.Name == "pictureBoxHastaLoginPasswordHide" || pictureBox.Name == "pictureBoxHastaLoginPasswordShow" ||
                         pictureBox.Name == "pictureBoxAdminLoginPasswordHide" || pictureBox.Name == "pictureBoxAdminLoginPasswordShow" ||
                         pictureBox.Name == "pictureBoxDoctorLoginPasswordHide" || pictureBox.Name == "pictureBoxDoctorLoginPasswordShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(530, 390);
                }
            }
            else if (label != null)
            {
                if (label.Name == "labelHastaPasswordReset" || label.Name == "labelAdminPasswordReset" || label.Name == "labelDoctorPasswordReset")
                {
                    label.Font = new Font(label.Font.FontFamily, 10);
                    label.Location = new Point(425, 425);
                }
            }
        }

        private void PictureScaleDown(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            Label label = sender as Label;

            if (pictureBox != null)
            {
                if (pictureBox.Name == "pictureBoxHastaRegisterPasswordHide" || pictureBox.Name == "pictureBoxHastaRegisterPasswordShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(441, 454);
                }
                else if (pictureBox.Name == "pictureBoxHastaRegisterPasswordConfirmHide" || pictureBox.Name == "pictureBoxHastaRegisterPasswordConfirmShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(441, 504);
                }
                else if (pictureBox.Name == "pictureBoxHastaLoginPasswordHide" || pictureBox.Name == "pictureBoxHastaLoginPasswordShow" ||
                         pictureBox.Name == "pictureBoxDoctorLoginPasswordHide" || pictureBox.Name == "pictureBoxDoctorLoginPasswordShow" ||
                         pictureBox.Name == "pictureBoxAdminLoginPasswordHide" || pictureBox.Name == "pictureBoxAdminLoginPasswordShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(533, 392);
                }
            }
            else if (label != null)
            {
                if (label.Name == "labelHastaPasswordReset" || label.Name == "labelAdminPasswordReset" || label.Name == "labelDoctorPasswordReset")
                {
                    label.Font = new Font(label.Font.FontFamily, 9);
                    label.Location = new Point(438, 428);
                }
            }
        }

        private async void PasswordShowHide(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.Name == "pictureBoxHastaRegisterPasswordHide" || pictureBox.Name == "pictureBoxHastaRegisterPasswordShow" || pictureBox.Name == "pictureBoxHastaRegisterPasswordConfirmHide" || pictureBox.Name == "pictureBoxHastaRegisterPasswordConfirmShow")
            {
                if (pictureBox.AccessibleName == "0")
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxHastaRegisterPasswordHide));
                    await Task.Run(() => animatorPassword.Hide(pictureBoxHastaRegisterPasswordConfirmHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxHastaRegisterPasswordShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxHastaRegisterPasswordConfirmShow));
                    txtHastaRegisterPassword.UseSystemPasswordChar = false;
                    txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = false;
                }
                else
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxHastaRegisterPasswordShow));
                    await Task.Run(() => animatorPassword.Hide(pictureBoxHastaRegisterPasswordConfirmShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxHastaRegisterPasswordHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxHastaRegisterPasswordConfirmHide));
                    if (string.IsNullOrEmpty(txtHastaRegisterPassword.Text) && string.IsNullOrEmpty(txtHastaRegisterPasswordConfirm.Text))
                    {
                        txtHastaRegisterPassword.UseSystemPasswordChar = false;
                        txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = false;
                    }
                    else if (string.IsNullOrEmpty(txtHastaRegisterPassword.Text))
                    {
                        txtHastaRegisterPassword.UseSystemPasswordChar = false;
                        txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = true;
                    }
                    else if (string.IsNullOrEmpty(txtHastaRegisterPasswordConfirm.Text))
                    {
                        txtHastaRegisterPassword.UseSystemPasswordChar = true;
                        txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        txtHastaRegisterPassword.UseSystemPasswordChar = true;
                        txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = true;
                    }

                }
            }
            else if (pictureBox.Name == "pictureBoxHastaLoginPasswordHide" || pictureBox.Name == "pictureBoxHastaLoginPasswordShow")
            {
                if (pictureBox.AccessibleName == "0")
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxHastaLoginPasswordHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxHastaLoginPasswordShow));
                    txtHastaPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxHastaLoginPasswordShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxHastaLoginPasswordHide));
                    if (string.IsNullOrEmpty(txtHastaPassword.Text))
                    {
                        txtHastaPassword.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        txtHastaPassword.UseSystemPasswordChar = true;
                    }

                }
            }
            else if (pictureBox.Name == "pictureBoxAdminLoginPasswordHide" || pictureBox.Name == "pictureBoxAdminLoginPasswordShow")
            {
                if (pictureBox.AccessibleName == "0")
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxAdminLoginPasswordHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxAdminLoginPasswordShow));
                    txtAdminPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxAdminLoginPasswordShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxAdminLoginPasswordHide));
                    if (string.IsNullOrEmpty(txtAdminPassword.Text))
                    {
                        txtAdminPassword.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        txtAdminPassword.UseSystemPasswordChar = true;
                    }

                }

            }
            else if (pictureBox.Name == "pictureBoxDoctorLoginPasswordHide" || pictureBox.Name == "pictureBoxDoctorLoginPasswordShow")
            {
                if (pictureBox.AccessibleName == "0")
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxDoctorLoginPasswordHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxDoctorLoginPasswordShow));
                    txtDoctorPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxDoctorLoginPasswordShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxDoctorLoginPasswordHide));
                    if (string.IsNullOrEmpty(txtDoctorPassword.Text))
                    {
                        txtDoctorPassword.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        txtDoctorPassword.UseSystemPasswordChar = true;
                    }
                }

            }
        }

        private async void Right_Click(object sender, EventArgs e)
        {
            toggleDisabled();
            if (toggleDarkMode.Checked)
            {
                pictureButtonRightDark.Visible = false;
                pictureButtonLeftDark.Visible = false;

                animator.Hide(panels[currentPanel], true);
                Rightpanels[currentRightPanel].Visible = false;
                pictureLoading.Visible = true;

                if (currentPanel == panels.Length - 1 || currentRightPanel == Rightpanels.Length - 1)
                {
                    currentPanel = 0;
                    currentRightPanel = 0;

                }
                else
                {
                    currentPanel++;
                    currentRightPanel++;
                }
                await Task.Delay(500);
                animatorLeft.Show(panels[currentPanel], true);
                await Task.Delay(500);
                Rightpanels[currentRightPanel].Visible = true;
                pictureLoading.Visible = false;

                pictureButtonRightDark.Visible = true;
                pictureButtonLeftDark.Visible = true;
            }
            else
            {
                pictureButtonRightWhite.Visible = false;
                pictureButtonLeftWhite.Visible = false;

                animator.Hide(panels[currentPanel], true);
                Rightpanels[currentRightPanel].Visible = false;
                pictureLoading.Visible = true;
                await Task.Delay(500);

                if (currentPanel == panels.Length - 1 || currentRightPanel == Rightpanels.Length - 1)
                {
                    currentPanel = 0;
                    currentRightPanel = 0;
                }
                else
                {
                    currentPanel++;
                    currentRightPanel++;
                }
                animatorLeft.Show(panels[currentPanel], true);
                await Task.Delay(500);
                Rightpanels[currentRightPanel].Visible = true;
                pictureLoading.Visible = false;

                pictureButtonRightWhite.Visible = true;
                pictureButtonLeftWhite.Visible = true;
            }
            toggleActive();
            panelCompanentEmpty();
        }

        private async void Left_Click(object sender, EventArgs e)
        {
            toggleDisabled();
            if (toggleDarkMode.Checked)
            {
                pictureButtonRightDark.Visible = false;
                pictureButtonLeftDark.Visible = false;

                // Eğer ilk paneldeysek, en sondaki panele geç
                if (currentPanel == 0 || currentRightPanel == 0)
                {
                    animatorLeft.Hide(panels[currentPanel], true);
                    Rightpanels[currentRightPanel].Visible = false;
                    pictureLoading.Visible = true;
                    await Task.Delay(500);
                    currentPanel = panels.Length - 1;
                    currentRightPanel = Rightpanels.Length - 1;
                    animator.Show(panels[currentPanel], true);
                    await Task.Delay(500);
                    Rightpanels[currentRightPanel].Visible = true;
                    pictureLoading.Visible = false;
                }
                else
                {
                    animatorLeft.Hide(panels[currentPanel], true);
                    Rightpanels[currentRightPanel].Visible = false;
                    pictureLoading.Visible = true;
                    await Task.Delay(500);
                    // Aksi takdirde bir öncekine geç
                    currentPanel--;
                    currentRightPanel--;
                    animator.Show(panels[currentPanel], true);
                    await Task.Delay(500);
                    Rightpanels[currentRightPanel].Visible = true;
                    pictureLoading.Visible = false;
                }
                pictureButtonRightDark.Visible = true;
                pictureButtonLeftDark.Visible = true;
            }
            else
            {
                pictureButtonRightWhite.Visible = false;
                pictureButtonLeftWhite.Visible = false;
                // Eğer ilk paneldeysek, en sondaki panele geç
                if (currentPanel == 0 || currentRightPanel == 0)
                {
                    animatorLeft.Hide(panels[currentPanel], true);
                    Rightpanels[currentRightPanel].Visible = false;
                    pictureLoading.Visible = true;
                    await Task.Delay(500);
                    currentPanel = panels.Length - 1;
                    currentRightPanel = Rightpanels.Length - 1;
                    animator.Show(panels[currentPanel], true);
                    await Task.Delay(500);
                    Rightpanels[currentRightPanel].Visible = true;
                    pictureLoading.Visible = false;
                }
                else
                {
                    animatorLeft.Hide(panels[currentPanel], true);
                    Rightpanels[currentRightPanel].Visible = false;
                    pictureLoading.Visible = true;
                    await Task.Delay(500);
                    // Aksi takdirde bir öncekine geç
                    currentPanel--;
                    currentRightPanel--;
                    animator.Show(panels[currentPanel], true);
                    await Task.Delay(500);
                    Rightpanels[currentRightPanel].Visible = true;
                    pictureLoading.Visible = false;
                }
                pictureButtonRightWhite.Visible = true;
                pictureButtonLeftWhite.Visible = true;
            }
            toggleActive();
            panelCompanentEmpty();
        }
        private void panelCompanentEmpty()
        {
            if (panelAdminLogin.Visible)
            {
                HastaLoginEmpty();
                DoctorLoginEmpty();
            }
            else if (panelDoctorLogin.Visible)
            {
                AdminLoginEmpty();
                HastaLoginEmpty();
            }
            else if (panelHastaLogin.Visible)
            {
                AdminLoginEmpty();
                DoctorLoginEmpty();
            }
        }
        private async void panelHastaRegisterOpen(object sender, EventArgs e)
        {
            toggleDisabled();
            if (toggleDarkMode.Checked)
            {
                pictureButtonRightDark.Visible = false;
                pictureButtonLeftDark.Visible = false;

                pictureButtonGoingToHastaLoginWhite.Visible = false;
                pictureButtonGoingToHastaLoginDark.Visible = true;
            }
            else
            {
                pictureButtonRightWhite.Visible = false;
                pictureButtonLeftWhite.Visible = false;

                pictureButtonGoingToHastaLoginWhite.Visible = true;
                pictureButtonGoingToHastaLoginDark.Visible = false;

            }
            animatorLeft.Hide(panelHastaLogin, true);
            pictureBoxHastaLoginBanner.Visible = false;
            pictureLoading.Visible = true;
            await Task.Delay(500);
            animator.Show(panelHastaRegister, true);
            await Task.Delay(500);
            pictureBoxRegisterLoginBanner.Visible = true;
            pictureLoading.Visible = false;

            toggleActive();
            HastaLoginEmpty();
        }

        private async void panelHastaLoginOpen(object sender, EventArgs e)
        {
            toggleDisabled();
            pictureButtonGoingToHastaLoginWhite.Visible = false;
            pictureButtonGoingToHastaLoginDark.Visible = false;
            goingToHastaLogin();
            await Task.Delay(1000);
            if (toggleDarkMode.Checked)
            {
                pictureButtonRightDark.Visible = true;
                pictureButtonLeftDark.Visible = true;
            }
            else
            {
                pictureButtonRightWhite.Visible = true;
                pictureButtonLeftWhite.Visible = true;
            }
            toggleActive();
            HastaRegisterEmpty();
            hastaRegisterEnabledProperty();
        }

        // ToggleButton Active/Disabled Properties
        private void toggleActive()
        {
            toggleDarkMode.Enabled = true;
            if (toggleDarkMode.Checked)
            {
                toggleDarkMode.ToggleStateOn.BackColor = Color.FromArgb(249, 249, 249);
                toggleDarkMode.ToggleStateOn.BorderColor = Color.FromArgb(249, 249, 249);

                toggleDarkMode.ToggleStateOn.BackColorInner = Color.FromArgb(26, 26, 26);
                toggleDarkMode.ToggleStateOn.BorderColorInner = Color.FromArgb(26, 26, 26);
            }
            else
            {
                toggleDarkMode.ToggleStateOff.BackColor = Color.FromArgb(26, 26, 26);
                toggleDarkMode.ToggleStateOff.BorderColor = Color.FromArgb(26, 26, 26);

                toggleDarkMode.ToggleStateOff.BackColorInner = Color.FromArgb(249, 249, 249);
                toggleDarkMode.ToggleStateOff.BorderColorInner = Color.FromArgb(249, 249, 249);

            }
        }
        private void toggleDisabled()
        {
            toggleDarkMode.Enabled = false;

            if (toggleDarkMode.Checked)
            {
                toggleDarkMode.ToggleStateOn.BackColor = Color.FromArgb(120, 120, 120);
                toggleDarkMode.ToggleStateOn.BorderColor = Color.FromArgb(120, 120, 120);
            }
            else
            {
                toggleDarkMode.ToggleStateOff.BackColor = Color.FromArgb(120, 120, 120);
                toggleDarkMode.ToggleStateOff.BorderColor = Color.FromArgb(120, 120, 120);
            }
        }

        // FormMain_Load Properties

        private void tcProperty()
        {
            txtHastaTcNo.PlaceholderText = "TC Kimlik No";
            txtHastaTcNo.MaxLength = 13;
        }

        private void hastaregisterTcProperty()
        {
            txtHastaRegisterTcNo.PlaceholderText = "TC Kimlik No";
            txtHastaRegisterTcNo.MaxLength = 13;
        }

        private void hastaregisterPasswordProperty()
        {
            txtHastaRegisterPassword.PlaceholderText = "Şifre";
            txtHastaRegisterPassword.UseSystemPasswordChar = false;

            txtHastaRegisterPasswordConfirm.PlaceholderText = "Tekrar Şifre";
            txtHastaRegisterPasswordConfirm.UseSystemPasswordChar = false;
        }

        private void hastaregisterPhoneNumberProperty()
        {
            txtHastaRegisterPhoneNumber.PlaceholderText = "(5xx) xxx xxxx";
            txtHastaRegisterPhoneNumber.MaxLength = 15;
        }

        private void passwordConfirmProperty()
        {

            panelHastaRegisterPasswordRules.Visible = false;
            panelHastaRegisterMailRules.Visible = false;
            pictureBoxHastaRegisterPasswordShow.Visible = false;
            pictureBoxHastaRegisterPasswordConfirmShow.Visible = false;


            labelHastaRegisterPasswordRules1.ForeColor = Color.FromArgb(224, 79, 95);
            labelHastaRegisterPasswordRules2.ForeColor = Color.FromArgb(224, 79, 95);
            labelHastaRegisterPasswordRules3.ForeColor = Color.FromArgb(224, 79, 95);
            labelHastaRegisterPasswordRules4.ForeColor = Color.FromArgb(224, 79, 95);
            labelHastaRegisterPasswordRules5.ForeColor = Color.FromArgb(224, 79, 95);
            labelHastaRegisterPasswordRules6.ForeColor = Color.FromArgb(224, 79, 95);
            labelHastaRegisterMailRules1.ForeColor = Color.FromArgb(224, 79, 95);
        }

        private void doctorProperty()
        {
            txtDoctorUsername.PlaceholderText = "Kullanıcı Adınız";
        }
        private void adminProperty()
        {
            txtAdminUsername.PlaceholderText = "Kullanıcı Adınız";
        }

        private void hastaregisterProperty()
        {
            txtHastaRegisterName.Text = null;
            txtHastaRegisterSurname.Text = null;
        }

        private void hastaPasswordProperty()
        {
            txtHastaPassword.UseSystemPasswordChar = false;  // Maskeyi kaldır
            txtHastaPassword.PlaceholderText = "Parola"; // Placeholder yazısını göster
            txtHastaPassword.ForeColor = Color.DarkGray; // Placeholder rengini ayarla
        }
        private void doctorPasswordProperty()
        {
            txtDoctorPassword.UseSystemPasswordChar = false;  // Maskeyi kaldır
            txtDoctorPassword.PlaceholderText = "Parola"; // Placeholder yazısını göster
            txtDoctorPassword.ForeColor = Color.DarkGray; // Placeholder rengini ayarla
        }
        private void adminPasswordProperty()
        {
            txtAdminPassword.UseSystemPasswordChar = false;  // Maskeyi kaldır
            txtAdminPassword.PlaceholderText = "Parola"; // Placeholder yazısını göster
            txtAdminPassword.ForeColor = Color.DarkGray; // Placeholder rengini ayarla
        }

        private void footerProperty()
        {
            date = DateTime.Now.Year.ToString();
            labelHastaFooter.Text = $"Tüm Hakları Saklıdır @ {date}";
            labelDoctorFooter.Text = $"Tüm Hakları Saklıdır @ {date}";
            labelAdminFooter.Text = $"Tüm Hakları Saklıdır @ {date}";
        }

        private void panelProperty()
        {
            panelHastaLogin.Location = new Point(0, 0);
            panelDoctorLogin.Location = new Point(0, 0);
            panelAdminLogin.Location = new Point(0, 0);
            panelHastaRegister.Location = new Point(0, 0);

            panelHastaLogin.Visible = true;
            panelDoctorLogin.Visible = false;
            panelAdminLogin.Visible = false;
            panelHastaRegister.Visible = false;
        }
        private void panelRightBannerProperty()
        {
            pictureBoxHastaLoginBanner.Location = new Point(675, 0);
            pictureBoxDoctorLoginBanner.Location = new Point(675, 0);
            pictureBoxAdminLoginBanner.Location = new Point(675, 0);
            pictureBoxRegisterLoginBanner.Location = new Point(675, 0);

            pictureBoxHastaLoginBanner.Visible = true;
            pictureBoxDoctorLoginBanner.Visible = false;
            pictureBoxAdminLoginBanner.Visible = false;
            pictureBoxRegisterLoginBanner.Visible = false;
        }
        private void btnProperty()
        {
            pictureButtonRightWhite.Location = new Point(592, 344);
            pictureButtonRightDark.Location = new Point(592, 344);

            pictureButtonLeftWhite.Location = new Point(19, 344);
            pictureButtonLeftDark.Location = new Point(19, 344);
        }

        private void bringToFrontProperty()
        {
            pictureButtonRightDark.BringToFront();
            pictureButtonRightWhite.BringToFront();

            pictureButtonLeftWhite.BringToFront();
            pictureButtonLeftDark.BringToFront();

            pictureBoxSunnyWhite.BringToFront();
            pictureBoxSunnyDark.BringToFront();

            pictureBoxMoonDark.BringToFront();
            pictureBoxMoonWhite.BringToFront();

            toggleDarkMode.BringToFront();
        }


        // Dark Mode Properties
        private void otherDarkModeOpenProperty()
        {
            //Forms,header,toggleButton,darkModePictures,Minimized
            this.BackColor = Color.FromArgb(26, 26, 26);

            pictureBoxMoonDark.Visible = true;
            pictureBoxMoonWhite.Visible = false;

            pictureBoxSunnyWhite.Visible = false;
            pictureBoxSunnyDark.Visible = true;

            pictureButtonLeftWhite.Visible = false;
            pictureButtonLeftDark.Visible = true;

            pictureButtonRightWhite.Visible = false;
            pictureButtonRightDark.Visible = true;
        }

        private void otherDarkModeClosedProperty()
        {
            this.BackColor = Color.FromArgb(249, 249, 249);

            pictureBoxMoonDark.Visible = false;
            pictureBoxMoonWhite.Visible = true;

            pictureBoxSunnyWhite.Visible = true;
            pictureBoxSunnyDark.Visible = false;

            pictureButtonLeftWhite.Visible = true;
            pictureButtonLeftDark.Visible = false;

            pictureButtonRightWhite.Visible = true;
            pictureButtonRightDark.Visible = false;
        }

        private void panelHastaLoginDarkModeOpenProperty()
        {

            labelHastaPasswordReset.BackColor = Color.FromArgb(26, 26, 26);

            labelHastaFooter.ForeColor = Color.FromArgb(249, 249, 249);
            labelHastaFooter.BackColor = Color.FromArgb(26, 26, 26);

            pictureBoxHastaLoginMiddleWhite.Visible = false;
            pictureBoxHastaLoginMiddleDark.Visible = true;
        }

        private void panelHastaLoginDarkModeClosedProperty()
        {

            labelHastaPasswordReset.BackColor = Color.FromArgb(249, 249, 249);

            labelHastaFooter.ForeColor = Color.FromArgb(26, 26, 26);
            labelHastaFooter.BackColor = Color.FromArgb(249, 249, 249);

            pictureBoxHastaLoginMiddleWhite.Visible = true;
            pictureBoxHastaLoginMiddleDark.Visible = false;
        }
        private void panelDoctorLoginDarkModeOpenProperty()
        {

            labelDoctorPasswordReset.BackColor = Color.FromArgb(26, 26, 26);
            labelDoctorFooter.ForeColor = Color.FromArgb(249, 249, 249);

            labelDoctorWarning.BackColor = Color.FromArgb(26, 26, 26);

            pictureBoxDoctorLoginMiddleWhite.Visible = false;
            pictureBoxDoctorLoginMiddleDark.Visible = true;
        }

        private void panelDoctorLoginDarkModeClosedProperty()
        {

            labelDoctorPasswordReset.BackColor = Color.FromArgb(249, 249, 249);

            labelDoctorFooter.ForeColor = Color.FromArgb(26, 26, 26);

            labelDoctorWarning.BackColor = Color.FromArgb(249, 249, 249);

            pictureBoxDoctorLoginMiddleWhite.Visible = true;
            pictureBoxDoctorLoginMiddleDark.Visible = false;
        }
        private void panelAdminLoginDarkModeOpenProperty()
        {

            labelAdminPasswordReset.BackColor = Color.FromArgb(26, 26, 26);

            labelAdminFooter.ForeColor = Color.FromArgb(249, 249, 249);
            labelAdminFooter.BackColor = Color.FromArgb(26, 26, 26);

            pictureBoxAdminLoginMiddleWhite.Visible = false;
            pictureBoxAdminLoginMiddleDark.Visible = true;
        }

        private void panelAdminLoginDarkModeClosedProperty()
        {

            labelAdminPasswordReset.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminFooter.ForeColor = Color.FromArgb(26, 26, 26);
            labelAdminFooter.BackColor = Color.FromArgb(249, 249, 249);

            pictureBoxAdminLoginMiddleWhite.Visible = true;
            pictureBoxAdminLoginMiddleDark.Visible = false;
        }

        private void panelHastaRegisterDarkModeOpenProperty()
        {

            pictureButtonGoingToHastaLoginDark.Visible = true;
            pictureButtonGoingToHastaLoginWhite.Visible = false;

            labelGoingToHastaLogin.BackColor = Color.FromArgb(26, 26, 26);
            labelGoingToHastaLogin.ForeColor = Color.FromArgb(249, 249, 249);

            pictureBoxHastaRegisterMiddleWhite.Visible = false;
            pictureBoxHastaRegisterMiddleDark.Visible = true;

            labelHastaRegisterTitle.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterTitle.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterWarning.BackColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterTcNo.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterTcNo.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterName.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterName.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterSurname.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterSurname.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterMail.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterMail.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterPhoneNumber.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterPhoneNumber.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterGender.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterGender.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterPassword.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterPassword.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterPasswordConfirm.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterPasswordConfirm.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterBorn.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterBorn.ForeColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterFooter.BackColor = Color.FromArgb(26, 26, 26);
            labelHastaRegisterFooter.ForeColor = Color.FromArgb(249, 249, 249);
        }

        private void panelHastaRegisterDarkModeClosedProperty()
        {

            pictureButtonGoingToHastaLoginDark.Visible = false;
            pictureButtonGoingToHastaLoginWhite.Visible = true;

            labelGoingToHastaLogin.BackColor = Color.FromArgb(249, 249, 249);
            labelGoingToHastaLogin.ForeColor = Color.FromArgb(26, 26, 26);

            pictureBoxHastaRegisterMiddleWhite.Visible = true;
            pictureBoxHastaRegisterMiddleDark.Visible = false;

            labelHastaRegisterTitle.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterTitle.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterWarning.BackColor = Color.FromArgb(249, 249, 249);

            labelHastaRegisterTcNo.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterTcNo.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterName.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterName.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterSurname.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterSurname.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterMail.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterMail.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterPhoneNumber.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterPhoneNumber.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterGender.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterGender.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterPassword.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterPassword.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterPasswordConfirm.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterPasswordConfirm.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterBorn.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterBorn.ForeColor = Color.FromArgb(26, 26, 26);

            labelHastaRegisterFooter.BackColor = Color.FromArgb(249, 249, 249);
            labelHastaRegisterFooter.ForeColor = Color.FromArgb(26, 26, 26);
        }

        private void hastaRegisterEnabledProperty()
        {
            txtHastaRegisterTcNo.Enabled = true;
            txtHastaRegisterName.Enabled = true;
            txtHastaRegisterSurname.Enabled = true;
            txtHastaRegisterMail.Enabled = true;
            txtHastaRegisterPhoneNumber.Enabled = true;
            txtHastaRegisterPassword.Enabled = true;
            txtHastaRegisterPasswordConfirm.Enabled = true;
            DropdownHastaRegisterGender.Enabled = true;
            DatePickerHastaRegisterBorn.Enabled = true;

            btn_HastaRegister.Enabled = true;

            toggleDisabled();
        }
        private void hastaRegisterDisabledProperty()
        {
            txtHastaRegisterTcNo.Enabled = false;
            txtHastaRegisterName.Enabled = false;
            txtHastaRegisterSurname.Enabled = false;
            txtHastaRegisterMail.Enabled = false;
            txtHastaRegisterPhoneNumber.Enabled = false;
            txtHastaRegisterPassword.Enabled = false;
            txtHastaRegisterPasswordConfirm.Enabled = false;
            DropdownHastaRegisterGender.Enabled = false;
            DatePickerHastaRegisterBorn.Enabled = false;

            btn_HastaRegister.Enabled = false;

            toggleActive();
        }

        private void Password_EnterFocus(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (bunifuTextBox.Name == "txtHastaPassword")
            {
                if (pictureBoxHastaLoginPasswordHide.Visible)
                {
                    if (string.IsNullOrEmpty(txtHastaPassword.Text))
                    {
                        txtHastaPassword.Text = "";
                        txtHastaPassword.UseSystemPasswordChar = true; // PasswordChar etkinleştir
                        txtHastaPassword.ForeColor = Color.DarkGray; // Normal yazı rengine dön
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtHastaPassword.Text))
                    {
                        txtHastaPassword.UseSystemPasswordChar = false;
                    }
                }
            }
            else if (bunifuTextBox.Name == "txtDoctorPassword")
            {
                if (pictureBoxDoctorLoginPasswordHide.Visible)
                {
                    if (string.IsNullOrEmpty(txtDoctorPassword.Text))
                    {
                        txtDoctorPassword.Text = "";
                        txtDoctorPassword.UseSystemPasswordChar = true; // PasswordChar etkinleştir
                        txtDoctorPassword.ForeColor = Color.DarkGray; // Normal yazı rengine dön
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDoctorPassword.Text))
                    {
                        txtHastaPassword.UseSystemPasswordChar = false;
                    }
                }
            }
            else if (bunifuTextBox.Name == "txtAdminPassword")
            {
                if (pictureBoxAdminLoginPasswordHide.Visible)
                {
                    if (string.IsNullOrEmpty(txtAdminPassword.Text))
                    {
                        txtAdminPassword.Text = "";
                        txtAdminPassword.UseSystemPasswordChar = true; // PasswordChar etkinleştir
                        txtAdminPassword.ForeColor = Color.DarkGray; // Normal yazı rengine dön
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtAdminPassword.Text))
                    {
                        txtHastaPassword.UseSystemPasswordChar = false;
                    }
                }
            }
        }

        private void Password_LeaveFocus(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (bunifuTextBox.Name == "txtHastaPassword")
            {
                if (pictureBoxHastaLoginPasswordHide.Visible)
                {
                    if (string.IsNullOrEmpty(txtHastaPassword.Text))
                    {
                        hastaPasswordProperty();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtHastaPassword.Text))
                    {
                        txtHastaPassword.UseSystemPasswordChar = false;
                    }
                }
            }
            else if (bunifuTextBox.Name == "txtDoctorPassword")
            {
                if (pictureBoxDoctorLoginPasswordHide.Visible)
                {
                    if (string.IsNullOrEmpty(txtDoctorPassword.Text))
                    {
                        doctorPasswordProperty();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDoctorPassword.Text))
                    {
                        txtHastaPassword.UseSystemPasswordChar = false;
                    }
                }
            }
            else if (bunifuTextBox.Name == "txtAdminPassword")
            {
                if (pictureBoxAdminLoginPasswordHide.Visible)
                {
                    if (string.IsNullOrEmpty(txtAdminPassword.Text))
                    {
                        adminPasswordProperty();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtAdminPassword.Text))
                    {
                        txtHastaPassword.UseSystemPasswordChar = false;
                    }
                }
            }
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            labelHastaAccountWrong.Visible = false;

            if (pictureBoxHastaLoginPasswordHide.Visible)
            {
                if (string.IsNullOrEmpty(txtHastaPassword.Text))
                {
                    if (txtHastaPassword.Name == "txtHastaPassword")
                    {
                        hastaPasswordProperty();
                    }
                }
                else
                {
                    labelAdminAccountWrong.Visible = false;
                    labelHastaPasswordRequired.Visible = false;
                    txtHastaPassword.UseSystemPasswordChar = true;
                    txtHastaPassword.ForeColor = Color.DarkGray;
                }
            }
            else if (pictureBoxDoctorLoginPasswordHide.Visible)
            {
                if (string.IsNullOrEmpty(txtDoctorPassword.Text))
                {
                    if (txtDoctorPassword.Name == "txtDoctorPassword")
                    {
                        doctorPasswordProperty();
                    }
                }
                else
                {
                    labelAdminAccountWrong.Visible = false;
                    labelDoctorPasswordRequired.Visible = false;
                    txtDoctorPassword.UseSystemPasswordChar = true;
                    txtDoctorPassword.ForeColor = Color.DarkGray;
                }
            }
            else if (pictureBoxAdminLoginPasswordHide.Visible)
            {
                if (string.IsNullOrEmpty(txtAdminPassword.Text))
                {
                    if (txtAdminPassword.Name == "txtAdminPassword")
                    {
                        adminPasswordProperty();
                    }
                }
                else
                {
                    labelAdminAccountWrong.Visible = false;
                    labelAdminPasswordRequired.Visible = false;
                    txtAdminPassword.UseSystemPasswordChar = true;
                    txtAdminPassword.ForeColor = Color.DarkGray;
                }
            }
        }

        private void TcNo_LeaveFocus(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (string.IsNullOrEmpty(bunifuTextBox.Text))
            {
                if (bunifuTextBox.Name == "txtHastaTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtHastaRegisterTcNo")
                {
                    hastaregisterTcProperty();
                }
                else if (bunifuTextBox.Name == "txtHastaRegisterPhoneNumber")
                {
                    hastaregisterPhoneNumberProperty();
                }
            }
        }
        private void TcNo_TextChanged(object sender, EventArgs e)
        {
            labelHastaAccountWrong.Visible = false;

            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;
            if (bunifuTextBox == null || bunifuTextBox.Text == "1" || bunifuTextBox.Text == " " || bunifuTextBox.Text == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(bunifuTextBox.Text))
            {
                if (bunifuTextBox.Name == "txtHastaTcNo")
                {
                    tcProperty();
                }
                else if (bunifuTextBox.Name == "txtHastaRegisterTcNo")
                {
                    hastaregisterTcProperty();
                }
                else if (bunifuTextBox.Name == "txtHastaRegisterPhoneNumber")
                {
                    hastaregisterPhoneNumberProperty();
                }
            }
            else
            {
                if (bunifuTextBox.Name == "txtHastaTcNo")
                {
                    txtHastaTcNo.MaxLength = 11;
                }
                else if (bunifuTextBox.Name == "txtHastaRegisterTcNo")
                {
                    txtHastaRegisterTcNo.MaxLength = 11;
                }
                else if (bunifuTextBox.Name == "txtHastaRegisterPhoneNumber")
                {
                    txtHastaRegisterPhoneNumber.MaxLength = 10;
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
        private async void goingToHastaLogin()
        {
            animator.Hide(panelHastaRegister, true);
            pictureBoxRegisterLoginBanner.Visible = false;
            pictureLoading.Visible = true;
            await Task.Delay(500);
            animatorLeft.Show(panelHastaLogin, true);
            await Task.Delay(500);
            pictureBoxHastaLoginBanner.Visible = true;
            pictureLoading.Visible = false;
            HastaLoginEmpty();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHastaRegisterName.Text) &&
                    !string.IsNullOrEmpty(txtHastaRegisterSurname.Text) &&
                    !string.IsNullOrEmpty(DropdownHastaRegisterGender.SelectedItem?.ToString()) &&
                    !string.IsNullOrEmpty(DatePickerHastaRegisterBorn.Text) &&
                    !string.IsNullOrEmpty(txtHastaRegisterTcNo.Text) &&
                    !string.IsNullOrEmpty(txtHastaRegisterMail.Text) &&
                    !string.IsNullOrEmpty(txtHastaRegisterPhoneNumber.Text))
            {
                if (txtHastaRegisterPassword.Text == txtHastaRegisterPasswordConfirm.Text &&
                    pictureBoxHastaRegisterPasswordRulesCorrect1.Visible &&
                    pictureBoxHastaRegisterPasswordRulesCorrect2.Visible &&
                    pictureBoxHastaRegisterPasswordRulesCorrect3.Visible &&
                    pictureBoxHastaRegisterPasswordRulesCorrect4.Visible &&
                    pictureBoxHastaRegisterPasswordRulesCorrect5.Visible &&
                    pictureBoxHastaRegisterPasswordRulesCorrect6.Visible &&
                    pictureBoxHastaRegisterMailRulesCorrect.Visible)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            bool isUserRegistered = false;
                            string checkQuery = "SELECT COUNT(1) FROM Patients WHERE TcNo = @TcNo";

                            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                            {
                                checkCommand.Parameters.AddWithValue("@TcNo", txtHastaRegisterTcNo.Text.ToString());


                                int userExists = (int)checkCommand.ExecuteScalar();

                                if (userExists > 0)
                                {
                                    isUserRegistered = true;
                                }
                            }

                            if (isUserRegistered)
                            {
                                UserAvailableForm userAvailableForm = new UserAvailableForm();
                                userAvailableForm.ShowDialog();
                            }
                            else
                            {
                                string name = txtHastaRegisterName.Text.Trim().ToString();
                                string normalizeName = Regex.Replace(name, @"\s+", " ").Trim();

                                string surname = txtHastaRegisterSurname.Text.Trim().ToString();
                                string normalizeSurname = Regex.Replace(surname, @"\s+", " ").Trim();

                                string query = @"INSERT INTO Patients (Name, Surname, Gender, Date, TcNo, Email, EmailConfirm, PhoneNumber, Password, PasswordConfirm)
                                                VALUES 
                                                (@Name, @Surname, @Gender, @Date, @TcNo, @Email, @EmailConfirm, @PhoneNumber, @Password, @PasswordConfirm)";

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@Name", normalizeName);
                                    command.Parameters.AddWithValue("@Surname", normalizeSurname);
                                    command.Parameters.AddWithValue("@Gender", DropdownHastaRegisterGender.SelectedItem.ToString());
                                    command.Parameters.AddWithValue("@Date", DatePickerHastaRegisterBorn.Value);
                                    command.Parameters.AddWithValue("@TcNo", txtHastaRegisterTcNo.Text.Trim().ToString());
                                    command.Parameters.AddWithValue("@Email", txtHastaRegisterMail.Text.Trim().ToString());
                                    command.Parameters.AddWithValue("@EmailConfirm", "false");
                                    command.Parameters.AddWithValue("@Password", txtHastaRegisterPassword.Text.Trim().ToString());
                                    command.Parameters.AddWithValue("@PasswordConfirm", txtHastaRegisterPasswordConfirm.Text.Trim().ToString());
                                    command.Parameters.AddWithValue("@PhoneNumber", txtHastaRegisterPhoneNumber.Text.Trim().ToString());

                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        hastaRegisterDisabledProperty();
                                        RegisterSuccessForm registerSuccess = new RegisterSuccessForm();
                                        registerSuccess.ShowDialog();
                                        panelHastaLoginOpen(null, null);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Kayıt eklenirken bir hata oluştu.");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message + ex.Source);
                        }
                    }
                }
            }
        }


        private async void btn_Login_Click(object sender, EventArgs e)
        {
            if (isProcessing)
            {
                return;
            }

            isProcessing = true;
            btn_HastaLogin.Enabled = false;


            try
            {
                if (string.IsNullOrWhiteSpace(txtHastaTcNo.Text) || string.IsNullOrWhiteSpace(txtHastaPassword.Text))
                {
                    patientEmptyTextboxProperty();
                    return;
                }

                string tcNo = txtHastaTcNo.Text.Trim().Replace(" ", "");
                string password = txtHastaPassword.Text.Trim().Replace(" ", "");

                var result = await CheckPatient(tcNo, password);

                if (result.IsAuthenticated)
                {
                    if (result.PatientId != null)
                    {
                        await PatientPanelLoading((int)result.PatientId);
                    }
                }
                else
                {
                    labelHastaAccountWrong.Visible = true;
                }
            }
            finally
            {
                isProcessing = false;
                btn_HastaLogin.Enabled = true;
            }
        }

        private void patientEmptyTextboxProperty()
        {
            labelTcNoRequired.Visible = string.IsNullOrWhiteSpace(txtHastaTcNo.Text);
            labelHastaPasswordRequired.Visible = string.IsNullOrWhiteSpace(txtHastaPassword.Text);
        }

        public async Task<(bool IsAuthenticated, int? PatientId)> CheckPatient(string tcNo, string password)
        {
            const string query = "SELECT Id FROM Patients WHERE TcNo = @TcNo AND Password = @Password";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@TcNo", SqlDbType.NVarChar) { Value = tcNo });
                        command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password });

                        var result = await command.ExecuteScalarAsync();

                        return result != null ? (true, (int?)Convert.ToInt32(result)) : (false, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                return (false, null);
            }
        }

        private async Task PatientPanelLoading(int id)
        {
            if (!IsFormOpen(typeof(PatientPanelLoadingForm)))
            {
                using (PatientPanelLoadingForm loadingForm = new PatientPanelLoadingForm())
                {
                    loadingForm.Show();

                    await Task.Run(async () =>
                    {
                        loadingForm.UpdateMessage("Hasta paneli yükleniyor...");
                        loadingForm.UpdateProgress(25);
                        await Task.Delay(GeneratorRandomMS());

                        loadingForm.UpdateMessage("Veriler yükleniyor...");
                        loadingForm.UpdateProgress(60);
                        await Task.Delay(GeneratorRandomMS());

                        loadingForm.UpdateMessage("Yükleme tamamlanıyor...");
                        loadingForm.UpdateProgress(85);
                        await Task.Delay(GeneratorRandomMS());
                    });

                    loadingForm.Close();
                }
            }
            else
            {
                MessageBox.Show("Panel yükleniyor. Lütfen bekleyin.");
            }

            this.Hide();
            string startType = null;
            bool exit = true;
            do
            {
                using (PatientPanelForm patientPanelForm = new PatientPanelForm(id, startType))
                {
                    patientPanelForm.Show();

                    try
                    {
                        while (true)
                        {
                            await Task.Delay(1000);

                            if (patientPanelForm.AccessibleName != null)
                            {
                                string result = patientPanelForm.AccessibleName.ToString();

                                if (result == "LoginOpen" || result == "Close")
                                {
                                    PanelResult(result);
                                    startType = null;
                                    exit = false;
                                    break;
                                }
                                else if (result == "restart")
                                {
                                    startType = "restart";
                                    exit = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                    }
                }
            } while (exit);
        }



        private void btn_DoctorLogin_Click(object sender, EventArgs e)
        {

        }

        private async void btn_AdminLogin_Click(object sender, EventArgs e)
        {
            if (isProcessing)
            {
                return;
            }

            isProcessing = true;
            btn_AdminLogin.Enabled = false;

            try
            {
                if (string.IsNullOrWhiteSpace(txtAdminUsername.Text) || string.IsNullOrWhiteSpace(txtAdminPassword.Text))
                {
                    adminEmptyTextboxProperty();
                    return;
                }

                string username = txtAdminUsername.Text.Trim();
                string password = txtAdminPassword.Text.Trim();
                bool isLogin = await LoginValidate(username, password);

                if (isLogin)
                {
                    await AdminPanelLoading();
                }
                else
                {
                    labelAdminAccountWrong.Visible = true;
                }
            }
            finally
            {
                isProcessing = false;
                btn_AdminLogin.Enabled = true;
            }
        }

        private void adminEmptyTextboxProperty()
        {
            labelAdminUsernameRequired.Visible = string.IsNullOrWhiteSpace(txtAdminUsername.Text);
            labelAdminPasswordRequired.Visible = string.IsNullOrWhiteSpace(txtAdminPassword.Text);
        }


        private async Task<bool> LoginValidate(string username, string password)
        {
            bool isLogin = false;

            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string userQuery = @"SELECT Id, Name, Surname, Email FROM Admins WHERE Username = @Username AND Password = @Password";

                        using (SqlCommand userCommand = new SqlCommand(userQuery, connection))
                        {
                            userCommand.Parameters.AddWithValue("@Username", username);
                            userCommand.Parameters.AddWithValue("@Password", password);

                            using (SqlDataReader reader = userCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int userId = reader.GetInt32(0);
                                    string name = reader.GetString(1);
                                    string surname = reader.GetString(2);
                                    string email = reader.GetString(3);

                                    adminPanelId = userId;
                                    isLogin = true;

                                    nameSurname = $"{name} {surname}";
                                    mail = email;
                                }
                                else
                                {
                                    isLogin = false;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            });

            return isLogin;
        }

        private async Task AdminPanelLoading()
        {
            if (!IsFormOpen(typeof(AdminPanelLoadingForm)))
            {
                using (AdminPanelLoadingForm loadingForm = new AdminPanelLoadingForm())
                {
                    loadingForm.Show();

                    await Task.Run(async () =>
                    {
                        loadingForm.UpdateMessage("Admin paneli yükleniyor...");
                        loadingForm.UpdateProgress(25);
                        await Task.Delay(GeneratorRandomMS());

                        loadingForm.UpdateMessage("Veriler yükleniyor...");
                        loadingForm.UpdateProgress(60);
                        await Task.Delay(GeneratorRandomMS());

                        loadingForm.UpdateMessage("Yükleme tamamlanıyor...");
                        loadingForm.UpdateProgress(85);
                        await Task.Delay(GeneratorRandomMS());
                    });

                    loadingForm.Close();
                }
            }

            this.Hide();
            bool exit = true;
            string startType = null;
            do
            {
                using (AdminPanelForm adminPanel = new AdminPanelForm(adminPanelId, nameSurname, mail, startType))
                {
                    adminPanel.Show();

                    try
                    {
                        while (true)
                        {
                            await Task.Delay(1000);

                            if (adminPanel.AccessibleName != null)
                            {
                                if (adminPanel.AccessibleName.ToString() == "LoginOpen" || adminPanel.AccessibleName.ToString() == "Close")
                                {
                                    PanelResult(adminPanel.AccessibleName.ToString());
                                    exit = false;
                                    startType = null;
                                    break;
                                }
                                else if (adminPanel.AccessibleName.ToString() == "restart")
                                {
                                    startType = "restart";
                                    exit = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata:" + ex.Message);
                    }
                }
            } while (exit);
        }


        private void PanelResult(string result)
        {
            if (result == "LoginOpen")
            {
                using (ReturnToLoginSuccessForm returnToLoginSuccess = new ReturnToLoginSuccessForm())
                {
                    returnToLoginSuccess.ShowDialog();
                }
                HastaLoginEmpty();
                hastaPasswordProperty();
                AdminLoginEmpty();
                adminPasswordProperty();
                PerformActionBasedOnSetting();
                this.Show();
            }
            else if (result == "Close")
            {
                this.Close();
            }
        }


        private bool IsFormOpen(Type formType)
        {
            return Application.OpenForms.Cast<Form>().Any(form => form.GetType() == formType);
        }


        private void PasswordReset_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            string tag = label.Tag.ToString();
            PasswordResetForm passwordReset = new PasswordResetForm(tag);
            passwordReset.Show();
        }
        private int GeneratorRandomMS()
        {
            Random random = new Random();
            int code = random.Next(1000, 3000);
            return code;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (AdminPanelClosingSuccessForm adminPanelClosingSuccessForm = new AdminPanelClosingSuccessForm())
            {
                this.Hide();
                adminPanelClosingSuccessForm.ShowDialog();
            }

            loginLoading = true;
        }
    }
}