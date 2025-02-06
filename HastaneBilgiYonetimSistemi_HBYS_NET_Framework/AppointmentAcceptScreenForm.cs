using Bunifu.UI.WinForms.BunifuButton;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Design;
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
    public partial class AppointmentAcceptScreenForm : DevExpress.XtraEditors.XtraForm
    {
        private int id;
        private string mail;
        private int policlinicId;
        private int doctorId;
        private DateTime date;
        private DateTime combinedDateTime;
        private string timeText;
        private string policlinicName;
        private string doctorName;
        private string patientName;
        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AppointmentAcceptScreenForm(DateTime _date, string _timeText, int _policlinicId, string _policlinicName, int _doctorId, string _doctorName, string _patientName, string _mail, int _id)
        {
            InitializeComponent();

            date = _date;
            policlinicName = _policlinicName;
            doctorName = _doctorName;
            id = _id;
            timeText = _timeText;
            policlinicId = _policlinicId;
            doctorId = _doctorId;
            patientName = _patientName;
            mail = _mail;


            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private void AppointmentAcceptScreenForm_Load(object sender, EventArgs e)
        {


            TimeSpan timeSpan = TimeSpan.Parse(timeText);

            combinedDateTime = date.Add(timeSpan);

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
                this.BackColor = Color.FromArgb(32, 32, 32);
                pictureBoxClose.Image = Properties.Resources.close_darkMode;
                datagridAppointmentAccept.BackgroundColor = Color.White;
                panelBack.BorderColor = Color.FromArgb(232, 232, 232);
                panelBack.BackColor = Color.Black;
                datagridAppointmentAccept.GridColor = Color.FromArgb(232, 232, 232);

                labelAppointmentAccept.ForeColor = Color.White;
                labelAppointmentAccept.BackColor = Color.FromArgb(32, 32, 32);

                panelLoading.BackgroundColor = Color.Black;
                panelLoading.BorderColor = Color.White;

                pictureBoxAppointmentLoading.BackColor = Color.Black;

                labelAppointmentSuccess.BackColor = Color.Black;
                labelAppointmentSuccess.ForeColor = Color.White;

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.White;
                pictureBoxClose.Image = Properties.Resources.closewhite;
                datagridAppointmentAccept.BackgroundColor = Color.Black;
                panelBack.BorderColor = Color.FromArgb(232, 232, 232);
                panelBack.BackColor = Color.White;
                datagridAppointmentAccept.GridColor = Color.FromArgb(232, 232, 232);

                labelAppointmentAccept.ForeColor = Color.Black;
                labelAppointmentAccept.BackColor = Color.White;

                panelLoading.BackgroundColor = Color.White;
                panelLoading.BorderColor = Color.Black;

                pictureBoxAppointmentLoading.BackColor = Color.White;

                labelAppointmentSuccess.BackColor = Color.White;
                labelAppointmentSuccess.ForeColor = Color.Black;
            }
        }
        private void ScreenProperty()
        {
            datagridAppointmentAccept.Rows.Add("Randevu Zamanı", $"{combinedDateTime.ToString("dd.MM.yyyy HH:mm")}");
            datagridAppointmentAccept.Rows.Add("Randevu Türü", "Muayene");
            datagridAppointmentAccept.Rows.Add("Poliklinik Adı", $"{policlinicName}");
            datagridAppointmentAccept.Rows.Add("Hekim", $"{doctorName}");
            datagridAppointmentAccept.Rows.Add("Randevu Sahibi", $"{patientName}");


            datagridAppointmentAccept.Columns[0].Width = 200;
            datagridAppointmentAccept.Columns[1].Width = 401;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void datagridAppointmentAccept_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.CellStyle.Font = new Font("Roboto", 10, FontStyle.Bold);
                e.CellStyle.BackColor = isDarkMode ? Color.FromArgb(25, 25, 25) : Color.FromArgb(250, 250, 250);
                e.CellStyle.SelectionBackColor = isDarkMode ? Color.FromArgb(25, 25, 25) : Color.FromArgb(250, 250, 250);
                e.CellStyle.ForeColor = isDarkMode ? Color.White : Color.Black;
                e.CellStyle.SelectionForeColor = isDarkMode ? Color.White : Color.Black;
            }
            if (e.ColumnIndex == 1)
            {
                e.CellStyle.BackColor = isDarkMode ? Color.Black : Color.White;
                e.CellStyle.SelectionBackColor = isDarkMode ? Color.Black : Color.White;
            }
            if (e.RowIndex == 0 && e.ColumnIndex == 1)
            {
                e.CellStyle.ForeColor = isDarkMode ? Color.White : Color.Black;
                e.CellStyle.SelectionForeColor = isDarkMode ? Color.White : Color.Black;
            }

            if (e.RowIndex == 1 && e.ColumnIndex == 1)
            {
                e.CellStyle.ForeColor = Color.Red;
                e.CellStyle.SelectionForeColor = Color.Red;
            }

            if (e.RowIndex == 2 && e.ColumnIndex == 1)
            {
                e.CellStyle.ForeColor = Color.FromArgb(160, 160, 160);
                e.CellStyle.SelectionForeColor = Color.FromArgb(160, 160, 160);
            }

            if (e.RowIndex == 3 && e.ColumnIndex == 1)
            {
                e.CellStyle.ForeColor = isDarkMode ? Color.White : Color.Black;
                e.CellStyle.SelectionForeColor = isDarkMode ? Color.White : Color.Black;
            }

            if (e.RowIndex == 4 && e.ColumnIndex == 1)
            {
                e.CellStyle.ForeColor = Color.FromArgb(160, 160, 160);
                e.CellStyle.SelectionForeColor = Color.FromArgb(160, 160, 160);
            }
        }

        private async void btn_AppointmentAccept_Click(object sender, EventArgs e)
        {
            int timeId = await GetTimeId(timeText);
            int sayac = 0;
            bool isSuccess = await AddAppointment(policlinicId, doctorId, id, timeId, date);

            if (isSuccess)
            {
                panelLoading.Visible = true;
                while (true)
                {
                    await Task.Delay(500);
                    sayac += 500;
                    if (sayac == 500)
                    {
                        labelAppointmentSuccess.Text = "Randevu Oluşturuluyor.";
                    }
                    else if (sayac == 1000)
                    {
                        labelAppointmentSuccess.Text = "Randevu Oluşturuluyor..";
                    }
                    else if (sayac == 1500)
                    {
                        await SendGmailHtmlEmail(mail, patientName, doctorName, policlinicName, date, timeText);
                        this.AccessibleName = "Success";
                        labelAppointmentSuccess.Text = "Randevu Oluşturuluyor...";
                    }
                    else if (sayac == 2000)
                    {
                        labelAppointmentSuccess.Text = "Randevu Oluşturuluyor.";
                    }
                    else if (sayac == 2500)
                    {
                        labelAppointmentSuccess.Text = "Randevu Oluşturuluyor..";
                        sayac = 0;
                        break;
                    }
                }
                panelLoading.Visible = false;
                this.Close();
            }
        }

        private async Task<int> GetTimeId(string appointmentTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id FROM AppointmentTime WHERE AppointmentTime = @AppointmentTime";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentTime", appointmentTime);

                        await connection.OpenAsync();

                        var result = await command.ExecuteScalarAsync();

                        if (result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }

                        MessageBox.Show("Hata:", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private async Task<bool> AddAppointment(int policlinicId, int doctorId, int id, int timeId, DateTime appointmentDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                    INSERT INTO PatientAppointment (PoliclinicId, DoctorId, UserId, AppointmentTimeId, Date, Status)
                    VALUES (@PoliclinicId, @DoctorId, @UserId, @AppointmentTimeId, @Date, @Status)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PoliclinicId", policlinicId);
                        command.Parameters.AddWithValue("@DoctorId", doctorId);
                        command.Parameters.AddWithValue("@UserId", id);
                        command.Parameters.AddWithValue("@AppointmentTimeId", timeId);
                        command.Parameters.AddWithValue("@Date", appointmentDate);
                        command.Parameters.AddWithValue("@Status", "Aktif");

                        await connection.OpenAsync();

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Appointment could not be saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task SendGmailHtmlEmail(string recipientEmail, string _name, string _doctorName, string _policlinicName, DateTime _date, string _time)
        {
            try
            {
                var message = new MimeMessage();
                var birim = "HBRS | Hasta Destek";
                message.Subject = "Randevu Bildirimi";
                message.From.Add(new MailboxAddress(birim, "hastanebilgiyonetimsistemi@gmail.com"));
                message.To.Add(new MailboxAddress(_name, recipientEmail));

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
            <p>Randevu bilgileriniz aşağıda yer almaktadır. Lütfen randevu tarihinizi ve saatinizi not almayı unutmayın.</p>
            <div class=""details"">
                <p><strong>📆 Randevu Tarihi:</strong> <span id=""appointment-date"">{_date.ToShortDateString()}</span></p>
                <p><strong>⏰ Randevu Saati:</strong> <span id=""appointment-time"">{_time}</span></p>
                <p><strong>👨‍⚕️ Doktor Adı:</strong> <span id=""doctor-name"">{_doctorName}</span></p>
                <p><strong>🏥 Poliklinik:</strong> <span id=""policlinic-name"">{_policlinicName}</span></p>
                <p><strong>📝 Randevu Türü:</strong> Muayene</p>
            </div>
            <p>
                Sağlıklı günler dileriz. Randevu saatinden 15 dakika önce poliklinikte hazır bulunmayı unutmayın.
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