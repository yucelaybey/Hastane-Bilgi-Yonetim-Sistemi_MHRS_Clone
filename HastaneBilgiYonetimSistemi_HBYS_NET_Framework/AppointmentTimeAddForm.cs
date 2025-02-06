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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class AppointmentTimeAddForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AppointmentTimeAddForm()
        {
            InitializeComponent();
            PerformActionBasedOnSetting();
            DarkModeOpen();
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
                this.BackColor = Color.FromArgb(38, 38, 38);

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                panelGradient.GradientBottomLeft = Color.FromArgb(236, 92, 188);
                panelGradient.GradientBottomRight = Color.DeepPink;
                panelGradient.GradientTopLeft = Color.FromArgb(124, 8, 216);
                panelGradient.GradientTopRight = Color.FromArgb(198, 60, 212);

                panelInAppoinmentTimeAdd.BackgroundColor = Color.FromArgb(64, 64, 64);

                labelPoliclinicName.ForeColor = Color.FromArgb(249, 249, 249);
                labelPoliclinicName.BackColor = Color.FromArgb(64, 64, 64);

                dateTimeOffsetAppointmentTime.Properties.Appearance.ForeColor = Color.FromArgb(249, 249, 249);
                dateTimeOffsetAppointmentTime.Properties.Appearance.BackColor = Color.FromArgb(64, 64, 64);
                dateTimeOffsetAppointmentTime.Properties.Appearance.BorderColor = Color.FromArgb(124, 86, 216);

                dateTimeOffsetAppointmentTime.Properties.AppearanceFocused.ForeColor = Color.FromArgb(249, 249, 249);
                dateTimeOffsetAppointmentTime.Properties.AppearanceFocused.BackColor = Color.FromArgb(64, 64, 64);
                dateTimeOffsetAppointmentTime.Properties.AppearanceFocused.BorderColor = Color.FromArgb(167, 114, 242);

                btn_AppoinmentTimeAdd.ForeColor = Color.FromArgb(230, 230, 230);

                btn_AppoinmentTimeAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
                btn_AppoinmentTimeAdd.IdleFillColor = Color.FromArgb(124, 86, 216);

                btn_AppoinmentTimeAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
                btn_AppoinmentTimeAdd.onHoverState.FillColor = Color.FromArgb(38, 38, 38);

                btn_AppoinmentTimeAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
                btn_AppoinmentTimeAdd.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

                btn_AppoinmentTimeAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
                btn_AppoinmentTimeAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

                labelFooter.ForeColor = Color.FromArgb(249, 249, 249);
                labelFooter.BackColor = Color.FromArgb(64, 64, 64);

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(230, 230, 230);

                pictureBoxDark.Visible = false;
                pictureBoxWhite.Visible = true;

                panelGradient.GradientBottomLeft = Color.FromArgb(230, 230, 230);
                panelGradient.GradientBottomRight = Color.Gray;
                panelGradient.GradientTopLeft = Color.Gray;
                panelGradient.GradientTopRight = Color.FromArgb(230, 230, 230);

                panelInAppoinmentTimeAdd.BackgroundColor = Color.FromArgb(230, 230, 230);

                labelPoliclinicName.ForeColor = Color.FromArgb(26, 26, 26);
                labelPoliclinicName.BackColor = Color.FromArgb(230, 230, 230);

                dateTimeOffsetAppointmentTime.Properties.Appearance.ForeColor = Color.FromArgb(26, 26, 26);
                dateTimeOffsetAppointmentTime.Properties.Appearance.BackColor = Color.White;
                dateTimeOffsetAppointmentTime.Properties.Appearance.BorderColor = Color.Gray;

                dateTimeOffsetAppointmentTime.Properties.AppearanceFocused.ForeColor = Color.FromArgb(26, 26, 26);
                dateTimeOffsetAppointmentTime.Properties.AppearanceFocused.BackColor = Color.White;
                dateTimeOffsetAppointmentTime.Properties.AppearanceFocused.BorderColor = Color.Black;

                btn_AppoinmentTimeAdd.ForeColor = Color.FromArgb(64, 64, 64);

                btn_AppoinmentTimeAdd.IdleBorderColor = Color.Black;
                btn_AppoinmentTimeAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

                btn_AppoinmentTimeAdd.onHoverState.BorderColor = Color.Black;
                btn_AppoinmentTimeAdd.onHoverState.FillColor = Color.DimGray;

                btn_AppoinmentTimeAdd.OnIdleState.BorderColor = Color.Black;
                btn_AppoinmentTimeAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

                btn_AppoinmentTimeAdd.OnPressedState.BorderColor = Color.Black;
                btn_AppoinmentTimeAdd.OnPressedState.FillColor = Color.Gray;

                labelFooter.ForeColor = Color.FromArgb(26, 26, 26);
                labelFooter.BackColor = Color.FromArgb(230, 230, 230);

            }
        }

        private void AppoinmentTimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AccessibleName = "Negative";
        }

        private async void btn_AppointmentTimeAdd_Click(object sender, EventArgs e)
        {
            string operation = "add";


            if (string.IsNullOrWhiteSpace(dateTimeOffsetAppointmentTime.Text.ToString()) || dateTimeOffsetAppointmentTime.Text == "Saat Seçiniz")
            {
                labelRequired.Visible = true;
                return;
            }
            else
            {
                DateTime appoinmentTimeName = DateTime.Parse(dateTimeOffsetAppointmentTime.Text);

                string query = "INSERT INTO AppointmentTime (AppointmentTime) VALUES (@AppointmentTime)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@AppointmentTime", appoinmentTimeName);

                            int rowsAffected = await command.ExecuteNonQueryAsync();

                            if (rowsAffected > 0)
                            {
                                AppointmentTimeAddUpdateDeleteForm appointmentTimeAddUpdateDeleteForm = new AppointmentTimeAddUpdateDeleteForm(operation, 1);
                                appointmentTimeAddUpdateDeleteForm.Show();
                                this.Hide();

                                while (true)
                                {
                                    await Task.Delay(100);
                                    if (appointmentTimeAddUpdateDeleteForm.IsDisposed)
                                    {

                                        this.AccessibleName = "Success";
                                        dateTimeOffsetAppointmentTime.Clear();
                                        this.Show();
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Veri eklenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtAppointmentTime_TextChange(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dateTimeOffsetAppointmentTime.Text))
            {
                labelRequired.Visible = false;
            }
        }

        private void AppointmentTimeAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_AppointmentTimeAdd_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}