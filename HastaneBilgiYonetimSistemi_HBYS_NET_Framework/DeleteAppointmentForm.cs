using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class DeleteAppointmentForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly int id;
        private readonly int patientId;
        private readonly string policlinicName;
        private readonly string doctorName;
        private readonly string appointmentDateTime;
        private readonly string mail;
        private readonly string patientName;

        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public DeleteAppointmentForm(int _id, string _policlinicName, string _doctorName, string _appointmentDateTime, string _mail, int _patientId,string _patientName)
        {
            InitializeComponent();

            id = _id;
            doctorName = _doctorName;
            policlinicName = _policlinicName;
            appointmentDateTime = _appointmentDateTime;
            mail = _mail;
            patientId = _patientId;
            patientName = _patientName;

            PerformActionBasedOnSetting();
            DarkModeOpen();
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
                this.BackColor = Color.Black;


            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.White;

            }
        }

        private void DeleteAppointmentForm_Load(object sender, EventArgs e)
        {
            labelDoctorName.Text = doctorName;
            labelPoliclinicName.Text = policlinicName;
            labelDateTime.Text = appointmentDateTime;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btn_Okey_Click(object sender, EventArgs e)
        {
            bool isSuccess = await DeleteAppointmentById(id);

            if (isSuccess)
            {
                this.AccessibleName = "delete";

                this.Hide();

                await SendGmailHtmlEmail(mail, patientName, doctorName, policlinicName, appointmentDateTime);
                
            }
            else
            {
                MessageBox.Show($"Hata : Kayıt Başarısız...");
            }
        }
        
        private async Task<bool> DeleteAppointmentById(int id)
        {
            string query = "Update PatientAppointment SET Status = @Status WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Status", "İptal");

                        int affectedRows = await command.ExecuteNonQueryAsync();

                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                return false;
            }
        }
        private async Task SendGmailHtmlEmail(string recipientEmail, string _name, string _doctorName, string _policlinicName, string appointmentDate)
        {
            try
            {
                var message = new MimeMessage();
                var birim = "HBRS | Hasta Destek";
                message.Subject = "Randevu İptal Bildirimi";
                message.From.Add(new MailboxAddress(birim, "hastanebilgiyonetimsistemi@gmail.com"));
                message.To.Add(new MailboxAddress(_name, recipientEmail));

                string _date = DateTime.Parse(appointmentDate).ToShortDateString();
                string _time = DateTime.Parse(appointmentDate).ToShortTimeString();

                string htmlContent = $@"
    <!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{message.Subject}</title>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 0;
            background: linear-gradient(135deg, #d40d12, #47b6b0);
            color: #333333;
        }}
        .container {{
            max-width: 700px;
            margin: 40px auto;
            background-color: #ffffff;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #d40d12, #f36c6f);
            color: #ffffff;
            padding: 20px;
            text-align: center;
            font-size: 28px;
            font-weight: bold;
        }}
        .content {{
            padding: 30px;
        }}
        .greeting {{
            font-size: 22px;
            font-weight: bold;
            margin-bottom: 20px;
        }}
        .details {{
            margin: 20px 0;
            background: linear-gradient(135deg, #47b6b0, #58d3c1);
            color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }}
        .details p {{
            margin: 10px 0;
            font-size: 18px;
        }}
        .details strong {{
            font-weight: bold;
            color: #ffffff;
        }}
        .footer {{
            background-color: #f7f7f7;
            color: #666666;
            text-align: center;
            padding: 15px;
            font-size: 14px;
        }}
        .footer a {{
            color: #d40d12;
            text-decoration: none;
        }}
        .footer a:hover {{
            text-decoration: underline;
        }}
        .icon {{
            font-size: 30px;
            margin-right: 10px;
            vertical-align: middle;
        }}
        .highlight {{
            color: #d40d12;
            font-weight: bold;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <span class=""icon"">📅</span> {message.Subject}
        </div>
        <div class=""content"">
            <div class=""greeting"">
                Merhaba, <span id=""patient-name"" class=""highlight"">{_name}</span>
            </div>
            <p>Aşağıda bilgileri verilen randevunuz iptal edilmiştir.</p>
            <div class=""details"">
                <p><strong>📆 Randevu Tarihi:</strong> <span id=""appointment-date"">{_date}</span></p>
                <p><strong>⏰ Randevu Saati:</strong> <span id=""appointment-time"">{_time}</span></p>
                <p><strong>👨‍⚕️ Doktor Adı:</strong> <span id=""doctor-name"">{_doctorName}</span></p>
                <p><strong>🏥 Poliklinik:</strong> <span id=""policlinic-name"">{_policlinicName}</span></p>
                <p><strong>📝 Randevu Türü:</strong> Muayene</p>
            </div>
            <p>
                Sağlıklı günler dileriz. Randevu alırken aciliyet durumunu düşünerek randevu alınız.
            </p>
        </div>
        <div class=""footer"">
            Bu e-posta otomatik olarak oluşturulmuştur. Lütfen yanıtlamayın.<br>
            Daha fazla bilgi için <a href=""#"">web sitemizi ziyaret edin</a>.
        </div>
    </div>
</body>
</html>
";
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
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}