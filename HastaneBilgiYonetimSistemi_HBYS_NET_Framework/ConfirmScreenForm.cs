using Bunifu.UI.WinForms;
using DevExpress.Emf;
using DevExpress.LookAndFeel;
using DevExpress.XtraReports.UI;
using MailKit.Net.Smtp;
using MimeKit;
using ReaLTaiizor.Controls;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class ConfirmScreenForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string confirmType;
        private string userNumber;
        private readonly string name;
        private readonly string tcNo;
        private readonly string email;
        private int resetNumber;

        private int countdownExpirationDateValue = 40;

        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public ConfirmScreenForm(string _tcNo, string _email, string _name, string confirmType_)
        {
            InitializeComponent();

            confirmType = confirmType_;
            tcNo = _tcNo;
            name = _name;
            email = _email;

            PerformActionBasedOnSetting();
            DarkModeOpen();

            this.KeyPreview = true;
        }

        private async void ConfirmScreenForm_Load(object sender, EventArgs e)
        {
            EmailConfirmNumberTextboxProperty();
            ModeType();
            CountDown();
            resetNumber = GeneratorRandomMS();
            await SendGmailHtmlEmail(email, name, resetNumber, confirmType);
        }

        private int GeneratorRandomMS()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code;
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

                if (confirmType == "mailConfirm" || confirmType == "AdminMailConfirm" || confirmType == "DoctorMailConfirm")
                {
                    pictureBoxConfirmScreenMailMiddleDark.Visible = true;
                    pictureBoxConfirmScreenMailMiddleWhite.Visible = false;

                    pictureBoxConfirmScreenPasswordMiddleDark.Visible = false;
                    pictureBoxConfirmScreenPasswordMiddleWhite.Visible = false;

                    btn_ConfirmMail.Visible = true;

                    btn_ConfirmPassword.Visible = false;
                }
                else if (confirmType == "passConfirm")
                {
                    pictureBoxConfirmScreenMailMiddleDark.Visible = false;
                    pictureBoxConfirmScreenMailMiddleWhite.Visible = false;

                    pictureBoxConfirmScreenPasswordMiddleDark.Visible = true;
                    pictureBoxConfirmScreenPasswordMiddleWhite.Visible = false;

                    btn_ConfirmMail.Visible = false;

                    btn_ConfirmPassword.Visible = true;
                }
                else
                {
                    MessageBox.Show("HATA:Program mail yada şifre doğrulaması almadan açıldı. Yaptığınız işlemler işe yaramaz !!!", "Uyarı");
                }
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                if (confirmType == "mailConfirm" || confirmType == "AdminMailConfirm" || confirmType == "DoctorMailConfirm")
                {
                    pictureBoxConfirmScreenMailMiddleDark.Visible = false;
                    pictureBoxConfirmScreenMailMiddleWhite.Visible = true;

                    pictureBoxConfirmScreenPasswordMiddleDark.Visible = false;
                    pictureBoxConfirmScreenPasswordMiddleWhite.Visible = false;

                    btn_ConfirmMail.Visible = true;

                    btn_ConfirmPassword.Visible = false;
                }
                else if (confirmType == "passConfirm")
                {
                    pictureBoxConfirmScreenMailMiddleDark.Visible = false;
                    pictureBoxConfirmScreenMailMiddleWhite.Visible = false;

                    pictureBoxConfirmScreenPasswordMiddleDark.Visible = false;
                    pictureBoxConfirmScreenPasswordMiddleWhite.Visible = true;

                    btn_ConfirmMail.Visible = false;

                    btn_ConfirmPassword.Visible = true;
                }
                else
                {
                    MessageBox.Show("HATA:Program mail yada şifre doğrulaması almadan açıldır. Yaptığınız işlemler işe yaramaz !!!", "Uyarı");
                }
            }
        }

        private void EmailConfirmNumberTextboxProperty()
        {
            txtEmailConfirmNumber1.Size = new Size(64, 80);
            txtEmailConfirmNumber2.Size = new Size(64, 80);
            txtEmailConfirmNumber3.Size = new Size(64, 80);
            txtEmailConfirmNumber4.Size = new Size(64, 80);
            txtEmailConfirmNumber5.Size = new Size(64, 80);
            txtEmailConfirmNumber6.Size = new Size(64, 80);

            txtEmailConfirmNumber1.BackColor = Color.FromArgb(112, 112, 112);
            txtEmailConfirmNumber2.BackColor = Color.FromArgb(112, 112, 112);
            txtEmailConfirmNumber3.BackColor = Color.FromArgb(112, 112, 112);
            txtEmailConfirmNumber4.BackColor = Color.FromArgb(112, 112, 112);
            txtEmailConfirmNumber5.BackColor = Color.FromArgb(112, 112, 112);
            txtEmailConfirmNumber6.BackColor = Color.FromArgb(112, 112, 112);
        }

        private void ModeType()
        {
            if (confirmType == "mailConfirm" || confirmType == "AdminMailConfirm" || confirmType == "DoctorMailConfirm")
            {
                this.Text = "E-Posta Doğrulama";
                this.IconOptions.Image = HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.check_mail;
            }
            else if (confirmType == "passConfirm")
            {
                this.Text = "Şifre Yenileme";
                this.IconOptions.Image = HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.reset_password;
            }
        }
        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Girişi engelle
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            labelWrongCode.Visible = false;

            if (sender is BunifuTextBox currentTextBox)
            {

                if (currentTextBox.Text.Length >= 1)
                {
                    FocusNextTextBox(currentTextBox);
                }
                else if (currentTextBox.Text.Length == 0)
                {
                    FocusPreviousTextBox(currentTextBox);
                }
                CheckedControl(resetNumber);
            }
        }

        private void CheckedControl(int resetNumber)
        {
            userNumber = txtEmailConfirmNumber1.Text.ToString() + txtEmailConfirmNumber2.Text.ToString() + txtEmailConfirmNumber3.Text.ToString() + txtEmailConfirmNumber4.Text.ToString() + txtEmailConfirmNumber5.Text.ToString() + txtEmailConfirmNumber6.Text.ToString();

            if (resetNumber.ToString() == userNumber)
            {
                btn_Buttons.IdleBorderColor = Color.FromArgb(29, 250, 0);
            }
            else
            {
                btn_Buttons.IdleBorderColor = Color.Red;
            }
        }

        private void FocusPreviousTextBox(BunifuTextBox currentTextBox)
        {
            if (currentTextBox == txtEmailConfirmNumber2)
                txtEmailConfirmNumber1.Focus();
            else if (currentTextBox == txtEmailConfirmNumber3)
                txtEmailConfirmNumber2.Focus();
            else if (currentTextBox == txtEmailConfirmNumber4)
                txtEmailConfirmNumber3.Focus();
            else if (currentTextBox == txtEmailConfirmNumber5)
                txtEmailConfirmNumber4.Focus();
            else if (currentTextBox == txtEmailConfirmNumber6)
                txtEmailConfirmNumber5.Focus();
        }

        private void FocusNextTextBox(BunifuTextBox currentTextBox)
        {
            if (currentTextBox == txtEmailConfirmNumber1)
                txtEmailConfirmNumber2.Focus();
            else if (currentTextBox == txtEmailConfirmNumber2)
                txtEmailConfirmNumber3.Focus();
            else if (currentTextBox == txtEmailConfirmNumber3)
                txtEmailConfirmNumber4.Focus();
            else if (currentTextBox == txtEmailConfirmNumber4)
                txtEmailConfirmNumber5.Focus();
            else if (currentTextBox == txtEmailConfirmNumber5)
                txtEmailConfirmNumber6.Focus();
        }

        private void TextboxEmpty()
        {
            txtEmailConfirmNumber1.Text = string.Empty;
            txtEmailConfirmNumber2.Text = string.Empty;
            txtEmailConfirmNumber3.Text = string.Empty;
            txtEmailConfirmNumber4.Text = string.Empty;
            txtEmailConfirmNumber5.Text = string.Empty;
            txtEmailConfirmNumber6.Text = string.Empty;

            txtEmailConfirmNumber1.Focus();
        }

        private async void CountDown()
        {
            labelRepeatWantToCode.Enabled = false;
            labelRepeatCodeWaitingTime.Visible = true;
            labelCodeValidityPeriod.Location = new Point(202, 162);
            while (countdownExpirationDateValue >= 0)
            {
                labelCodeValidityPeriod.Text = "Kod Geçerlilik Süresi : " + countdownExpirationDateValue + " Saniye";
                labelRepeatCodeWaitingTime.Text = "Yeniden Kod İstemek İçin\r\nKalan Süre : " + countdownExpirationDateValue + " Saniye";
                await Task.Delay(1000);
                countdownExpirationDateValue--;
            }
            countdownExpirationDateValue = 40;
            resetNumber = 898239842;
            labelCodeValidityPeriod.Location = new Point(130, 162);
            labelCodeValidityPeriod.Text = "Kod Geçerliliğini kaybetmiştir. Lütfen yeni kod isteyiniz.";
            btn_Buttons.IdleBorderColor = Color.Red;
            labelRepeatWantToCode.Enabled = true;
            labelRepeatCodeWaitingTime.Visible = false;
        }
        private void ConfirmScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btn_ConfirmMail.Visible)
                {
                    btn_ConfirmMail_Click(sender, e);
                }
                else if (btn_ConfirmPassword.Visible)
                {
                    btn_ConfirmPassword_Click(sender, e);
                }
                e.SuppressKeyPress = true;
            }
        }

        private async void labelRepeatWantToCode_Click(object sender, EventArgs e)
        {
            labelRepeatWantToCode.Enabled = false;
            resetNumber = GeneratorRandomMS();
            await SendGmailHtmlEmail(email, name, resetNumber, confirmType);
            TextboxEmpty();
            CountDown();
        }

        public async Task SendGmailHtmlEmail(string recipientEmail, string _name, int resetCode, string _confirmType)
        {
            var why = "";
            var message = new MimeMessage();
            var birim = "HBYS | Destek";
            if (_confirmType.ToString() == "passConfirm")
            {
                why = "Şifre Sıfırlama İsteği";
                message.Subject = $"Şifre Sıfırlama Kodunuz : {resetCode}";
            }
            else if (_confirmType == "mailConfirm" || _confirmType == "AdminMailConfirm" || _confirmType == "DoctorMailConfirm")
            {
                why = "E-Posta Doğrulama İsteği";
                message.Subject = $"E-Posta Doğrulama Kodunuz : {resetCode}";

            }
            message.From.Add(new MailboxAddress(birim, "hastanebilgiyonetimsistemi@gmail.com")); // Kendi Gmail adresiniz
            message.To.Add(new MailboxAddress(_name, recipientEmail));

            string htmlContent = $@"
    <!DOCTYPE html>
    <html lang='tr'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title></title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }}
            .container {{
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                background-color: #ffffff;
                border-radius: 10px;
                overflow: hidden;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            .header {{
                background-color: #47b6b0;
                color: white;
                padding: 20px;
                text-align: center;
            }}
            .header h1 {{
                margin: 0;
            }}
            .content {{
                padding: 20px;
                font-size: 16px;
                line-height: 1.5;
                color: #333333;
            }}
            .content h2 {{
                color: #47b6b0;
            }}
            .content p {{
                margin: 0 0 20px;
            }}
            .code {{
                font-size: 32px;
                font-weight: bold;
                letter-spacing: 5px;
                color: #d40d12;
                margin: 20px 0;
            }}
            .copy-btn {{
                display: inline-block;
                padding: 10px 20px;
                font-size: 16px;
                background-color: #47b6b0;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                cursor: pointer;
            }}
            .footer {{
                text-align: center;
                padding: 20px;
                font-size: 12px;
                color: #777777;
            }}
            .footer a {{
                color: #47b6b0;
                text-decoration: none;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h1>{why}</h1>
            </div>
            <div class='content'>
                <h2>Merhaba, {_name}</h2>
                <p>Hesabınızı doğrulamak yada şifrenizi sıfırlamak için aşağıdaki kodu kullanın:</p>
                <div class='code'>{resetCode}</div> <!-- Dinamik kod buraya yerleştirildi -->
                <p>
                    <button class='copy-btn' onclick='copyToClipboard()'>Kodu Kopyala</button>
                </p>
                <p>Eğer bu talebi siz yapmadıysanız, lütfen bu e-postayı görmezden gelin. Şifreniz güvende.</p>
            </div>
            <div class='footer'>
                <p>&copy; {DateTime.Now.Year} HBYS. Tüm hakları saklıdır.</p>
                <p><a href='#'>Gizlilik Politikası</a> | <a href='#'>Şartlar ve Koşullar</a></p>
            </div>
        </div>

        <script>
            function copyToClipboard() {{
                const code = document.querySelector('.code').textContent;
                navigator.clipboard.writeText(code).then(() => {{
                    alert('Kod başarıyla kopyalandı!');
                }});
            }}
        </script>
    </body>
    </html>";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlContent
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("hastanebilgiyonetimsistemi@gmail.com", "wksl wuri nvuv tttl");

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private void btn_ConfirmMail_Click(object sender, EventArgs e)
        {
            if (txtEmailConfirmNumber1.Text != "" && txtEmailConfirmNumber1.Text != null &&
                txtEmailConfirmNumber2.Text != "" && txtEmailConfirmNumber2.Text != null &&
                txtEmailConfirmNumber3.Text != "" && txtEmailConfirmNumber3.Text != null &&
                txtEmailConfirmNumber4.Text != "" && txtEmailConfirmNumber4.Text != null &&
                txtEmailConfirmNumber5.Text != "" && txtEmailConfirmNumber5.Text != null &&
                txtEmailConfirmNumber6.Text != "" && txtEmailConfirmNumber6.Text != null && resetNumber == int.Parse(userNumber))
            {
                if (confirmType == "mailConfirm")
                {
                    string updateQuery = "UPDATE Patients SET EmailConfirm = @EmailConfirm WHERE TcNo = @TcNo AND Email = @Email";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@EmailConfirm", true);
                                command.Parameters.AddWithValue("@TcNo", tcNo);
                                command.Parameters.AddWithValue("@Email", email);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    this.Hide();
                                    EmailConfirmSuccessForm emailConfirmSuccesForm = new EmailConfirmSuccessForm();
                                    emailConfirmSuccesForm.ShowDialog();
                                    this.AccessibleDescription = "Success";
                                    this.Close();
                                }
                                else
                                {
                                    this.AccessibleDescription = "Negative";
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
                else if(confirmType == "AdminMailConfirm")
                {
                    string updateQuery = "UPDATE Admins SET EmailConfirm = @EmailConfirm WHERE TcNo = @TcNo AND Email = @Email";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@EmailConfirm", true);
                                command.Parameters.AddWithValue("@TcNo", tcNo);
                                command.Parameters.AddWithValue("@Email", email);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    this.Hide();
                                    EmailConfirmSuccessForm emailConfirmSuccesForm = new EmailConfirmSuccessForm();
                                    emailConfirmSuccesForm.ShowDialog();
                                    this.AccessibleDescription = "Success";
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
                            this.AccessibleDescription = "Negative";
                            MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata");
                        }
                    }
                }
                else if (confirmType == "DoctorMailConfirm")
                {
                    string updateQuery = "UPDATE Doctors SET EmailConfirm = @EmailConfirm WHERE TcNo = @TcNo AND Email = @Email";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@EmailConfirm", true);
                                command.Parameters.AddWithValue("@TcNo", tcNo);
                                command.Parameters.AddWithValue("@Email", email);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    this.Hide();
                                    EmailConfirmSuccessForm emailConfirmSuccesForm = new EmailConfirmSuccessForm();
                                    emailConfirmSuccesForm.ShowDialog();
                                    this.AccessibleDescription = "Success";
                                    this.Close();
                                }
                                else
                                {
                                    this.AccessibleDescription = "Negative";
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
            }
            else
            {
                labelWrongCode.Visible = true;
            }
        }

        private void btn_ConfirmPassword_Click(object sender, EventArgs e)
        {
            if (txtEmailConfirmNumber1.Text != "" && txtEmailConfirmNumber1.Text != null &&
                txtEmailConfirmNumber2.Text != "" && txtEmailConfirmNumber2.Text != null &&
                txtEmailConfirmNumber3.Text != "" && txtEmailConfirmNumber3.Text != null &&
                txtEmailConfirmNumber4.Text != "" && txtEmailConfirmNumber4.Text != null &&
                txtEmailConfirmNumber5.Text != "" && txtEmailConfirmNumber5.Text != null &&
                txtEmailConfirmNumber6.Text != "" && txtEmailConfirmNumber6.Text != null && resetNumber == int.Parse(userNumber))
            {
                this.Hide();
                PasswordConfirmSuccessForm passwordConfirmSucces = new PasswordConfirmSuccessForm();
                passwordConfirmSucces.ShowDialog();
                this.AccessibleDescription = "Success";
                this.Close();
            }
            else
            {
                labelWrongCode.Visible = true;
            }
        }

        private void ConfirmScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.AccessibleDescription == "Success")
            {
                this.AccessibleName = "Continue";
            }
            else
            {
                this.Enabled = false;
                NotVerifiedClosingForm notVerifiedClosingForm = new NotVerifiedClosingForm();
                notVerifiedClosingForm.ShowDialog();
                string result = notVerifiedClosingForm.AccessibleName.ToString();

                if (result == "Continue")
                {
                    e.Cancel = true;
                    this.Enabled = true;
                    this.AccessibleName = "Continue";
                }
                else if (result == "Cancel")
                {
                    this.AccessibleName = "Cancel";
                }
            }
        }
    }
}