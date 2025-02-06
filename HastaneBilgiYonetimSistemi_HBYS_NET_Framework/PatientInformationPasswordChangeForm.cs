using AnimatorNS;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class PatientInformationPasswordChangeForm : DevExpress.XtraEditors.XtraForm
    {
        private Animator animatorPassword;
        private readonly int id;
        private readonly string oldPassword;
        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public PatientInformationPasswordChangeForm(int _id, string _oldPassword)
        {
            InitializeComponent();

            id = _id;
            oldPassword = _oldPassword;

            animatorPassword = new Animator();
            animatorPassword.AnimationType = AnimationType.Scale;
            animatorPassword.TimeStep = 0.15f;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private void PatientInformationPasswordChangeForm_Load(object sender, EventArgs e)
        {
            PasswordProperty();
            OldPasswordProperty();
            PasswordRulesProperty();
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
                this.BackColor = Color.FromArgb(56,56,56);

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                panelGradient.GradientBottomLeft = Color.Black;
                panelGradient.GradientBottomRight = Color.Black;
                panelGradient.GradientTopLeft = Color.Black;
                panelGradient.GradientTopRight = Color.Black;

                panelInPatientPasswordUpdate.BackgroundColor = Color.Black;

                labelPreviousPassword.ForeColor = Color.White;
                labelPreviousPassword.BackColor = Color.Black;

                labelNewPasssword.ForeColor = Color.White;
                labelNewPasssword.BackColor = Color.Black;

                labelNewRepeatPassword.ForeColor = Color.White;
                labelNewRepeatPassword.BackColor = Color.Black;

                txtOldPassword.ForeColor = Color.FromArgb(249, 249, 249);
                txtOldPassword.BorderColorActive = Color.FromArgb(124, 86, 216);
                txtOldPassword.BorderColorHover = Color.FromArgb(167, 114, 242);
                txtOldPassword.BorderColorIdle = Color.Gray;
                txtOldPassword.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
                txtOldPassword.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
                txtOldPassword.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
                txtOldPassword.FillColor = Color.FromArgb(38, 38, 38);

                txtNewPassword.ForeColor = Color.FromArgb(249, 249, 249);
                txtNewPassword.BorderColorActive = Color.FromArgb(124, 86, 216);
                txtNewPassword.BorderColorHover = Color.FromArgb(167, 114, 242);
                txtNewPassword.BorderColorIdle = Color.Gray;
                txtNewPassword.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
                txtNewPassword.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
                txtNewPassword.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
                txtNewPassword.FillColor = Color.FromArgb(38, 38, 38);

                txtNewPasswordRepeat.ForeColor = Color.FromArgb(249, 249, 249);
                txtNewPasswordRepeat.BorderColorActive = Color.FromArgb(124, 86, 216);
                txtNewPasswordRepeat.BorderColorHover = Color.FromArgb(167, 114, 242);
                txtNewPasswordRepeat.BorderColorIdle = Color.Gray;
                txtNewPasswordRepeat.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
                txtNewPasswordRepeat.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
                txtNewPasswordRepeat.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
                txtNewPasswordRepeat.FillColor = Color.FromArgb(38, 38, 38);

                btn_PasswordSave.ForeColor = Color.FromArgb(230, 230, 230);

                btn_PasswordSave.IdleBorderColor = Color.FromArgb(124, 86, 216);
                btn_PasswordSave.IdleFillColor = Color.FromArgb(124, 86, 216);

                btn_PasswordSave.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
                btn_PasswordSave.onHoverState.FillColor = Color.FromArgb(38, 38, 38);

                btn_PasswordSave.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
                btn_PasswordSave.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

                btn_PasswordSave.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
                btn_PasswordSave.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

                labelFooter.ForeColor = Color.White;
                labelFooter.BackColor = Color.Black;

                pictureBoxOldPasswordHide.Image = Properties.Resources.eyeHideNewDark;
                pictureBoxPasswordHide.Image = Properties.Resources.eyeHideNewDark;
                pictureBoxPasswordConfirmHide.Image = Properties.Resources.eyeHideNewDark;

                pictureBoxOldPasswordShow.Image = Properties.Resources.eye2NEWDark;
                pictureBoxPasswordShow.Image = Properties.Resources.eye2NEWDark;
                pictureBoxPasswordConfirmShow.Image = Properties.Resources.eye2NEWDark;

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(245, 245, 245);

                pictureBoxDark.Visible = false;
                pictureBoxWhite.Visible = true;

                panelGradient.GradientBottomLeft = Color.White;
                panelGradient.GradientBottomRight = Color.White;
                panelGradient.GradientTopLeft = Color.White;
                panelGradient.GradientTopRight = Color.White;

                panelInPatientPasswordUpdate.BackgroundColor = Color.White;

                labelPreviousPassword.ForeColor = Color.Black;
                labelPreviousPassword.BackColor = Color.White;

                labelNewPasssword.ForeColor = Color.Black;
                labelNewPasssword.BackColor = Color.White;

                labelNewRepeatPassword.ForeColor = Color.Black;
                labelNewRepeatPassword.BackColor = Color.White;

                txtOldPassword.ForeColor = Color.FromArgb(26, 26, 26);
                txtOldPassword.FillColor = Color.White;
                txtOldPassword.BorderColorActive = Color.FromArgb(230, 230, 230);
                txtOldPassword.BorderColorHover = Color.FromArgb(235, 235, 235);
                txtOldPassword.BorderColorIdle = Color.Gray;
                txtOldPassword.OnHoverState.FillColor = Color.White;
                txtOldPassword.OnIdleState.FillColor = Color.White;
                txtOldPassword.OnActiveState.FillColor = Color.White;

                txtNewPassword.ForeColor = Color.FromArgb(26, 26, 26);
                txtNewPassword.FillColor = Color.White;
                txtNewPassword.BorderColorActive = Color.FromArgb(230, 230, 230);
                txtNewPassword.BorderColorHover = Color.FromArgb(235, 235, 235);
                txtNewPassword.BorderColorIdle = Color.Gray;
                txtNewPassword.OnHoverState.FillColor = Color.White;
                txtNewPassword.OnIdleState.FillColor = Color.White;
                txtNewPassword.OnActiveState.FillColor = Color.White;

                txtNewPasswordRepeat.ForeColor = Color.FromArgb(26, 26, 26);
                txtNewPasswordRepeat.FillColor = Color.White;
                txtNewPasswordRepeat.BorderColorActive = Color.FromArgb(230, 230, 230);
                txtNewPasswordRepeat.BorderColorHover = Color.FromArgb(235, 235, 235);
                txtNewPasswordRepeat.BorderColorIdle = Color.Gray;
                txtNewPasswordRepeat.OnHoverState.FillColor = Color.White;
                txtNewPasswordRepeat.OnIdleState.FillColor = Color.White;
                txtNewPasswordRepeat.OnActiveState.FillColor = Color.White;


                btn_PasswordSave.ForeColor = Color.FromArgb(64, 64, 64);

                btn_PasswordSave.IdleBorderColor = Color.Black;
                btn_PasswordSave.IdleFillColor = Color.FromArgb(230, 230, 230);

                btn_PasswordSave.onHoverState.BorderColor = Color.Black;
                btn_PasswordSave.onHoverState.FillColor = Color.DimGray;

                btn_PasswordSave.OnIdleState.BorderColor = Color.Black;
                btn_PasswordSave.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

                btn_PasswordSave.OnPressedState.BorderColor = Color.Black;
                btn_PasswordSave.OnPressedState.FillColor = Color.Gray;

                labelFooter.ForeColor = Color.Black;
                labelFooter.BackColor = Color.White;

                pictureBoxOldPasswordHide.Image = Properties.Resources.eyeHide;
                pictureBoxPasswordHide.Image = Properties.Resources.eyeHide;
                pictureBoxPasswordConfirmHide.Image = Properties.Resources.eyeHide;

                pictureBoxOldPasswordShow.Image = Properties.Resources.eyeShow;
                pictureBoxPasswordShow.Image = Properties.Resources.eyeShow;
                pictureBoxPasswordConfirmShow.Image = Properties.Resources.eyeShow;
            }
        }

        private void PatientInformationPasswordChangeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AccessibleName = "Negative";
        }

        private async void btn_Save_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            string password = txtNewPassword.Text;
            string passwordConfirm = txtNewPasswordRepeat.Text;

            if (oldPassword == this.oldPassword && !(oldPassword == "") && !(password == "") && !(passwordConfirm == "") && !(oldPassword == null) && !(password == null) && !(passwordConfirm == null) &&
                pictureBoxPasswordRulesCorrect1.Visible && pictureBoxPasswordRulesCorrect2.Visible && pictureBoxPasswordRulesCorrect3.Visible && pictureBoxPasswordRulesCorrect4.Visible && pictureBoxPasswordRulesCorrect5.Visible && pictureBoxPasswordRulesCorrect6.Visible)
            {
                await PasswordUpdate(id, oldPassword, password);

            }
            else if ((oldPassword == "") && (password == "") && (passwordConfirm == ""))
            {
                labelRequired.Visible = true;
                labelNewPasRequired.Visible = true;
                labelNewPasRepeatRequired.Visible = true;
            }
            else if (oldPassword == "" && password == "")
            {
                labelRequired.Visible = true;
                labelNewPasRequired.Visible = true;
            }
            else if (password == "" && passwordConfirm == "")
            {
                labelNewPasRequired.Visible = true;
                labelNewPasRepeatRequired.Visible = true;
            }
            else if (oldPassword == "" && passwordConfirm == "")
            {
                labelRequired.Visible = true;
                labelNewPasRepeatRequired.Visible = true;
            }
            else if (oldPassword == "")
            {
                labelRequired.Visible = true;
            }
            else if (password == "")
            {
                labelNewPasRequired.Visible = true;
            }
            else if (passwordConfirm == "")
            {
                labelNewPasRepeatRequired.Visible = true;
            }
            else
            {
                labelOldPasswordWrong.Visible = true;
            }
        }

        private async Task PasswordUpdate(int id, string oldPassword, string password)
        {
            string query = @"UPDATE Patients SET Password = @NewPassword, PasswordConfirm = @NewPasswordConfirm WHERE Id = @Id AND Password = @OldPassword";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@OldPassword", oldPassword);
                        command.Parameters.AddWithValue("@NewPassword", password);
                        command.Parameters.AddWithValue("@NewPasswordConfirm", password);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            this.AccessibleName = "Success";
                            this.Close();
                        }
                        else
                        {
                            this.AccessibleName = "Negative";
                            DataChooseForm dataChooseForm = new DataChooseForm("adminPasswordChangeWarning");
                            dataChooseForm.ShowDialog();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PatientInformationPasswordChangeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            panelPatientPassResetRules.Visible = true;
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

            if (txtNewPassword.Text == "Şifre" || txtNewPassword.Text == null)
            {
                return;
            }

            ValidatePassword();
            CheckPasswordsMatch();
            if (pictureBoxPasswordHide.Visible)
            {
                if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    txtNewPassword.PlaceholderText = "Şifre";
                    txtNewPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    txtNewPassword.UseSystemPasswordChar = true;
                }
            }


        }

        private void TxtPasswordConfirm_TextChanged(object sender, EventArgs e)
        {

            if (txtNewPasswordRepeat.Text == "Tekrar Şifre" || txtNewPasswordRepeat.Text == null)
            {
                return;
            }

            CheckPasswordsMatch();

            if (pictureBoxPasswordConfirmHide.Visible)
            {
                if (string.IsNullOrEmpty(txtNewPasswordRepeat.Text))
                {
                    txtNewPasswordRepeat.PlaceholderText = "Tekrar Şifre";
                    txtNewPasswordRepeat.UseSystemPasswordChar = false;
                }
                else
                {
                    txtNewPasswordRepeat.UseSystemPasswordChar = true;
                }
            }
        }

        private void ValidatePassword()
        {
            string password = txtNewPassword.Text;

            // 1. Kural: Şifre en az 6 karakter olmalı
            if (password.Length >= 6)
            {
                labelPasswordRules1.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxPasswordRulesInCorrect1.Visible = false;
                pictureBoxPasswordRulesCorrect1.Visible = true;
            }
            else
            {
                labelPasswordRules1.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxPasswordRulesInCorrect1.Visible = true;
                pictureBoxPasswordRulesCorrect1.Visible = false;
            }

            // 2. Kural: Büyük harf içermeli
            if (Regex.IsMatch(password, @"[A-Z]"))
            {
                labelPasswordRules2.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxPasswordRulesInCorrect2.Visible = false;
                pictureBoxPasswordRulesCorrect2.Visible = true;
            }
            else
            {
                labelPasswordRules2.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxPasswordRulesInCorrect2.Visible = true;
                pictureBoxPasswordRulesCorrect2.Visible = false;
            }

            // 3. Kural: Küçük harf içermeli
            if (Regex.IsMatch(password, @"[a-z]"))
            {
                labelPasswordRules3.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxPasswordRulesInCorrect3.Visible = false;
                pictureBoxPasswordRulesCorrect3.Visible = true;
            }
            else
            {
                labelPasswordRules3.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxPasswordRulesInCorrect3.Visible = true;
                pictureBoxPasswordRulesCorrect3.Visible = false;
            }

            // 4. Kural: Rakam içermeli
            if (Regex.IsMatch(password, @"[0-9]"))
            {
                labelPasswordRules4.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxPasswordRulesInCorrect4.Visible = false;
                pictureBoxPasswordRulesCorrect4.Visible = true;
            }
            else
            {
                labelPasswordRules4.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxPasswordRulesInCorrect4.Visible = true;
                pictureBoxPasswordRulesCorrect4.Visible = false;
            }

            // 5. Kural: Özel karakter içermeli
            if (Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>+]"))
            {
                labelPasswordRules5.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxPasswordRulesInCorrect5.Visible = false;
                pictureBoxPasswordRulesCorrect5.Visible = true;
            }
            else
            {
                labelPasswordRules5.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxPasswordRulesInCorrect5.Visible = true;
                pictureBoxPasswordRulesCorrect5.Visible = false;
            }
        }

        //Şifrelerin eşleşmesini kontrol et
        private void CheckPasswordsMatch()
        {
            string password = txtNewPassword.Text;
            string passwordConfirm = txtNewPasswordRepeat.Text;

            if (password == passwordConfirm && !string.IsNullOrEmpty(passwordConfirm))
            {
                labelPasswordRules6.Text = "Şifreler uyumlu.";
                labelPasswordRules6.ForeColor = Color.FromArgb(50, 190, 166);
                pictureBoxPasswordRulesInCorrect6.Visible = false;
                pictureBoxPasswordRulesCorrect6.Visible = true;
            }
            else
            {
                labelPasswordRules6.Text = "Şifreler uyuşmuyor.";
                labelPasswordRules6.ForeColor = Color.FromArgb(224, 79, 95);
                pictureBoxPasswordRulesInCorrect6.Visible = true;
                pictureBoxPasswordRulesCorrect6.Visible = false;
            }
        }
        private void PasswordProperty()
        {
            txtNewPassword.PlaceholderText = "Yeni Şifre";
            txtNewPassword.UseSystemPasswordChar = false;

            txtNewPasswordRepeat.PlaceholderText = "Tekrar Yeni Şifre";
            txtNewPasswordRepeat.UseSystemPasswordChar = false;
        }
        private void OldPasswordProperty()
        {
            txtOldPassword.PlaceholderText = "Eski Şifre";
            txtOldPassword.UseSystemPasswordChar = false;
        }

        private void TxtOldPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOldPassword.Text))
            {
                panelPatientPassResetRules.Visible = false;
                OldPasswordProperty();
            }
        }

        private void PasswordRulesProperty()
        {
            labelPasswordRules1.ForeColor = Color.FromArgb(224, 79, 95);
            labelPasswordRules2.ForeColor = Color.FromArgb(224, 79, 95);
            labelPasswordRules3.ForeColor = Color.FromArgb(224, 79, 95);
            labelPasswordRules4.ForeColor = Color.FromArgb(224, 79, 95);
            labelPasswordRules5.ForeColor = Color.FromArgb(224, 79, 95);
            labelPasswordRules6.ForeColor = Color.FromArgb(224, 79, 95);
        }

        private void TxtOldPassword_TextChanged(object sender, EventArgs e)
        {
            labelOldPasswordWrong.Visible = false;

            if (txtOldPassword.Text == "Eski Şifre" || txtOldPassword.Text == null)
            {
                return;
            }

            ValidatePassword();
            CheckPasswordsMatch();
            if (pictureBoxOldPasswordHide.Visible)
            {
                if (string.IsNullOrEmpty(txtOldPassword.Text))
                {
                    txtOldPassword.PlaceholderText = "Eski Şifre";
                    txtOldPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    txtOldPassword.UseSystemPasswordChar = true;
                }
            }


        }

        private void PictureScaleUp(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                if (pictureBox.Name == "pictureBoxPasswordHide" || pictureBox.Name == "pictureBoxPasswordShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(307, 279);
                }
                else if (pictureBox.Name == "pictureBoxPasswordConfirmHide" || pictureBox.Name == "pictureBoxPasswordConfirmShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(307, 361);
                }
                else if (pictureBox.Name == "pictureBoxOldPasswordHide" || pictureBox.Name == "pictureBoxOldPasswordShow")
                {
                    pictureBox.Size = new Size(28, 28);
                    pictureBox.Location = new Point(307, 195);
                }
            }
        }

        private void PictureScaleDown(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                if (pictureBox.Name == "pictureBoxPasswordHide" || pictureBox.Name == "pictureBoxHPasswordShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(310, 281);
                }
                else if (pictureBox.Name == "pictureBoxPasswordConfirmHide" || pictureBox.Name == "pictureBoxPasswordConfirmShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(310, 362);
                }
                else if (pictureBox.Name == "pictureBoxOldPasswordHide" || pictureBox.Name == "pictureBoxOldPasswordShow")
                {
                    pictureBox.Size = new Size(25, 25);
                    pictureBox.Location = new Point(310, 197);
                }
            }
        }

        private async void PasswordShowHide(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.Name == "pictureBoxPasswordHide" || pictureBox.Name == "pictureBoxPasswordShow" || pictureBox.Name == "pictureBoxPasswordConfirmHide" || pictureBox.Name == "pictureBoxPasswordConfirmShow")
            {
                if (pictureBox.AccessibleName == "0")
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxPasswordHide));
                    await Task.Run(() => animatorPassword.Hide(pictureBoxPasswordConfirmHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxPasswordShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxPasswordConfirmShow));
                    txtNewPassword.UseSystemPasswordChar = false;
                    txtNewPasswordRepeat.UseSystemPasswordChar = false;
                }
                else
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxPasswordShow));
                    await Task.Run(() => animatorPassword.Hide(pictureBoxPasswordConfirmShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxPasswordHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxPasswordConfirmHide));
                    if (string.IsNullOrEmpty(txtNewPassword.Text) && string.IsNullOrEmpty(txtNewPasswordRepeat.Text))
                    {
                        txtNewPassword.UseSystemPasswordChar = false;
                        txtNewPasswordRepeat.UseSystemPasswordChar = false;
                    }
                    else if (string.IsNullOrEmpty(txtNewPassword.Text))
                    {
                        txtNewPassword.UseSystemPasswordChar = false;
                        txtNewPasswordRepeat.UseSystemPasswordChar = true;
                    }
                    else if (string.IsNullOrEmpty(txtNewPasswordRepeat.Text))
                    {
                        txtNewPassword.UseSystemPasswordChar = true;
                        txtNewPasswordRepeat.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        txtNewPassword.UseSystemPasswordChar = true;
                        txtNewPasswordRepeat.UseSystemPasswordChar = true;
                    }

                }
            }
            else if (pictureBox.Name == "pictureBoxOldPasswordHide" || pictureBox.Name == "pictureBoxOldPasswordShow")
            {
                if (pictureBox.AccessibleName == "0")
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxOldPasswordHide));
                    await Task.Run(() => animatorPassword.Show(pictureBoxOldPasswordShow));
                    txtOldPassword.UseSystemPasswordChar = false;
                }
                else
                {
                    await Task.Run(() => animatorPassword.Hide(pictureBoxOldPasswordShow));
                    await Task.Run(() => animatorPassword.Show(pictureBoxOldPasswordHide));
                    if (string.IsNullOrEmpty(txtOldPassword.Text))
                    {
                        txtOldPassword.UseSystemPasswordChar = false;
                    }
                    else
                    {
                        txtOldPassword.UseSystemPasswordChar = true;
                    }

                }
            }
        }
    }
}