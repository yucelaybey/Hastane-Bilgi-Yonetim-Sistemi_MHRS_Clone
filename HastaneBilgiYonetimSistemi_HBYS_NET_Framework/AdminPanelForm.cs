using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MimeKit;
using MailKit.Net.Smtp;
using System.Windows.Forms.DataVisualization.Charting;
using DevExpress.XtraBars.Customization;
using CuoreUI.Controls.Charts;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class AdminPanelForm : DevExpress.XtraEditors.XtraForm
    {
        public AdminPanelForm adminBeforeForm { get; set; }

        private string ConfirmMail;
        private int ConfirmId;
        private string ConfirmTc;
        private string ConfirmName;
        private string startType;
        private readonly int id;
        private readonly string nameSurname;
        private readonly string mail;
        private bool isDarkMode;
        private bool isClosing = false;
        private int maxSelectionCount = 5;
        private DataTable dataTable;
        private DataTable dataDoctorTable;
        private DataTable dataDoctorRestTable;
        private DataTable dataAppointmentTimeTable;
        private DataTable dataAdminTable;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public AdminPanelForm(int _id, string _nameSurname, string _mail, string _startType)
        {
            InitializeComponent();

            id = _id;
            nameSurname = _nameSurname;
            mail = _mail;
            startType = _startType;

            PerformActionBasedOnSetting();
            DarkModeOpen(isDarkMode);
        }

        private async void AdminPanelForm_Load(object sender, EventArgs e)
        {
            searchProperty();
            panelBackgroundProperty();
            panelBackgroundLocationProperty();
            otherProperty();
            ScreenProperty();
            panelDashboardProperty();
            panelDashboardLoadingData();
            DarkModeSettingProperty();

            if (startType == "restart")
            {
                await Task.Delay(1000);

                AdminAddUpdateDeleteSuccessForm adminAddUpdateDeleteSuccessForm = new AdminAddUpdateDeleteSuccessForm("restart", 0);
                adminAddUpdateDeleteSuccessForm.Show();
            }
        }

        private void DarkModeSettingProperty()
        {
            if (isDarkMode)
            {
                dropDownDarkMode.SelectedItem = "Koyu Renk";
                pictureBoxDarkMode.Image = Properties.Resources.KoyuMod;
            }
            else
            {
                dropDownDarkMode.SelectedItem = "Açık Renk";
                pictureBoxDarkMode.Image = Properties.Resources.AçıkMod;
            }
        }
        private void panelDashboardProperty()
        {
            txtActiveAppointment.Height = 70;
            txtAllDoctorNumber.Height = 70;
            txtAllRestDoctorNumber.Height = 70;
            txtAllPatientNumber.Height = 70;
        }
        private async void panelDashboardLoadingData()
        {
            int apponitmentData = await ActiveAppointment();
            txtActiveAppointment.Text = $"Aktif Randevular : {apponitmentData}";

            int doctorData = await AllDoctorNumber();
            txtAllDoctorNumber.Text = $"Doktor Sayımız : {doctorData}";

            int restDoctorData = await RestDoctorNumber();
            txtAllRestDoctorNumber.Text = $"İzinli Doktor Sayımız : {restDoctorData}";

            int patientData = await AllPatientNumber();
            txtAllPatientNumber.Text = $"Toplam Hasta Sayımız : {patientData}";

            var result = await Top5Policlinics();
            chartControl1.Series[0].Points.Clear();
            foreach (var (PoliclinicName, VisitCount) in result)
            {
                var seriesPoint = new SeriesPoint(PoliclinicName, VisitCount);

                chartControl1.Series[0].Points.Add(seriesPoint);
            }

            chartControl1.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chartControl1.Series[0].Label.TextPattern = "{A} {V}";

            try
            {
                var topDoctors = await Top5Doctors();

                var series = new DevExpress.XtraCharts.Series("Top 5 Doktor", DevExpress.XtraCharts.ViewType.Bar);

                foreach (var (DoctorName, DoctorSurname, AppointmentCount) in topDoctors)
                {
                    string fullName = $"{DoctorName} {DoctorSurname}";
                    series.Points.Add(new SeriesPoint(fullName, AppointmentCount));
                }

                chartControl2.Series.Clear();
                chartControl2.Series.Add(series);

                chartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series.Label.TextPattern = "{A}: {V}";

                var diagram = chartControl2.Diagram as DevExpress.XtraCharts.XYDiagram;
                if (diagram != null)
                {
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public async Task<int> ActiveAppointment()
        {
            const string query = "SELECT COUNT(*) FROM PatientAppointment WHERE Status = @Status";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar) { Value = "Aktif" });

                        var result = await command.ExecuteScalarAsync();

                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                throw;
            }
        }

        public async Task<int> AllDoctorNumber()
        {
            const string query = "SELECT COUNT(*) FROM Doctors";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var result = await command.ExecuteScalarAsync();

                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                throw;
            }
        }
        public async Task<int> RestDoctorNumber()
        {
            const string query = @"
            SELECT COUNT(DISTINCT d.Id)
            FROM Doctors d
            INNER JOIN DoctorRestDate drd ON d.Id = drd.DoctorId
            WHERE CAST(drd.RestDate AS DATE) = CAST(GETDATE() AS DATE)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var result = await command.ExecuteScalarAsync();

                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                throw;
            }
        }
        public async Task<int> AllPatientNumber()
        {
            const string query = "SELECT COUNT(*) FROM Patients";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var result = await command.ExecuteScalarAsync();

                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                throw;
            }
        }
        public async Task<List<(string PoliclinicName, int VisitCount)>> Top5Policlinics()
        {
            const string query = @"
            SELECT TOP 5 
                   p.[Poliklinik Adı] AS PoliclinicName, 
                   COUNT(pa.Id) AS VisitCount
            FROM PatientAppointment pa
            INNER JOIN Policlinics p ON pa.PoliclinicId = p.Id
            WHERE pa.Date >= DATEADD(MONTH, -1, GETDATE())
            GROUP BY p.[Poliklinik Adı]
            ORDER BY VisitCount DESC";

            var result = new List<(string PoliclinicName, int VisitCount)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string policlinicName = reader.GetString(0);
                                int visitCount = reader.GetInt32(1);

                                result.Add((policlinicName, visitCount));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                throw;
            }

            return result;
        }

        public async Task<List<(string DoctorName, string DoctorSurname, int AppointmentCount)>> Top5Doctors()
        {

            var topDoctors = new List<(string DoctorName, string DoctorSurname, int AppointmentCount)>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT TOP 5 
                D.Name AS DoctorName, 
                D.Surname AS DoctorSurname, 
                COUNT(PA.Id) AS AppointmentCount
            FROM Doctors D
            INNER JOIN PatientAppointment PA ON D.Id = PA.DoctorId
            GROUP BY D.Name, D.Surname
            ORDER BY COUNT(PA.Id) DESC;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string doctorName = reader["DoctorName"].ToString();
                            string doctorSurname = reader["DoctorSurname"].ToString();
                            int appointmentCount = Convert.ToInt32(reader["AppointmentCount"]);

                            topDoctors.Add((doctorName, doctorSurname, appointmentCount));
                        }
                    }
                }
            }

            return topDoctors;
        }
        private void ScreenProperty()
        {
            labelPanelName.Text = "Anasayfa";
            panelDashboard.Visible = true;

            labelAdminName.Text = nameSurname;
            labelAdminMail.Text = mail;
        }

        private void searchProperty()
        {
            txtSearch.Size = new Size(415, 45);
            txtDoctorsSearch.Size = new Size(415, 45);
            txtDoctorRestSearch.Size = new Size(415, 45);
            txtAppointmentTimeSearch.Size = new Size(415, 45);
            txtAdminSearch.Size = new Size(415, 45);
        }

        private void panelBackgroundProperty()
        {
            int startWidth = this.ClientSize.Width;
            int startHeight = this.ClientSize.Height;


            int newX = (int)Math.Floor(startWidth / 1.279147235176549);
            int newY = (int)Math.Floor(startHeight * 0.8202020202020202);

            panelBackground.Size = new Size(newX, newY);
        }

        private void panelBackgroundLocationProperty()
        {
            int startWidth = this.ClientSize.Width;
            int startHeight = this.ClientSize.Height;

            int newX = (int)Math.Floor(startWidth * 0.1942708333333333) + 1;
            int newY = (int)Math.Floor(startHeight * 0.1373737373737374);
            panelBackground.Location = new Point(newX, newY);
        }

        private void otherProperty()
        {
            toolTipLabelDoctorApproved.SetToolTip(labelDoctorApprovedWarning, "Bu kutucuğu işaretlerseniz ve Sil butonuna basarsanız, onay beklemeden veri silinir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelPoliclinicDeleteWarning, "Bu kutucuğu işaretlerseniz ve Sil butonuna basarsanız, onay beklemeden veri silinir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelDoctorRestApprovedWarning, "Bu kutucuğu işaretlerseniz ve Sil butonuna basarsanız, onay beklemeden veri silinir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelAdminApprovedDelete, "Bu kutucuğu işaretlerseniz ve Sil butonuna basarsanız, onay beklemeden veri silinir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelAppointmentTimeApprovedDelete, "Bu kutucuğu işaretlerseniz ve Sil butonuna basarsanız, onay beklemeden veri silinir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelSlowSearch, "Bu kutucuğu işaretlerseniz Arama işlemi sadece arama butonuna bastığınızda gerçekleşir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelSlowSearch, "Bu kutucuğu işaretlerseniz Arama işlemi sadece arama butonuna bastığınızda gerçekleşir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelSlowSearchAppointmentTime, "Bu kutucuğu işaretlerseniz Arama işlemi sadece arama butonuna bastığınızda gerçekleşir. (DİKKAT)");
            toolTipLabelDoctorApproved.SetToolTip(labelSlowAdminSearch, "Bu kutucuğu işaretlerseniz Arama işlemi sadece arama butonuna bastığınızda gerçekleşir. (DİKKAT)");
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

        private void DarkModeOpen(bool isDarkMode)
        {
            if (isDarkMode)
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
                this.BackColor = Color.FromArgb(64, 64, 64);
                panelBackground.BackgroundColor = Color.FromArgb(124, 86, 216);

                labelPanelName.ForeColor = Color.White;
                labelPanelName.BackColor = Color.FromArgb(64, 64, 64);
                panelDashboard.BackColor = Color.FromArgb(64, 64, 64);

                labelMaxDoctor.ForeColor = Color.White;
                labelMaxDoctor.BackColor = Color.FromArgb(64, 64, 64);

                labelDashboardChart.ForeColor = Color.White;
                labelDashboardChart.BackColor = Color.FromArgb(64, 64, 64);

                chartControl1.BackColor = Color.FromArgb(64, 64, 64);
                chartControl1.BorderOptions.Color = Color.FromArgb(64, 64, 64);

                chartControl2.BackColor = Color.FromArgb(64, 64, 64);
                chartControl2.BorderOptions.Color = Color.FromArgb(64, 64, 64);

                sidebarDarkModeOpenProperty();

                searchDarkModeOpenProperty();

                buttonDarkModeOpenProperty();

                panelBackgroundDarkModeOpenProperty();

                darkModeOpenProperty();

                //doctor

                panelDoctorPropertyDarkOpen();

                buttonDoctorPropertyDarkModeOpen();

                doctorSearchPropertyDarkOpen();

                //doctorRest

                panelDoctorRestPropertyDarkOpen();

                buttonDoctorRestDarkOpen();

                doctorRestSearchPropertyDarkOpen();

                //appointmentTime

                panelAppointmentTimePropertyDarkOpen();

                buttonAppointmentTimeDarkOpen();

                doctorAppointmentTimeSearchPropertyDarkOpen();

                //admin

                panelAdminPropertyDarkOpen();

                buttonAdminDarkOpen();

                doctorAdminSearchPropertyDarkOpen();

                //adminInformation

                panelAdminInformationPropertyDarkOpen();

                panelAdminLabelInformationPropertyDarkOpen();

                //panelDarkMode

                panelDarkModeOpenPropery();
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);
                panelBackground.BackgroundColor = Color.FromArgb(200, 200, 200);
                panelDashboard.BackColor = Color.FromArgb(249, 249, 249);
                labelPanelName.ForeColor = Color.Gray;
                labelPanelName.BackColor = Color.FromArgb(230, 230, 230);

                labelMaxDoctor.ForeColor = Color.Black;
                labelMaxDoctor.BackColor = Color.FromArgb(249, 249, 249);

                labelDashboardChart.ForeColor = Color.Black;
                labelDashboardChart.BackColor = Color.FromArgb(249, 249, 249);

                chartControl1.BackColor = Color.FromArgb(249, 249, 249);
                chartControl1.BorderOptions.Color = Color.FromArgb(249, 249, 249);

                chartControl2.BackColor = Color.FromArgb(249, 249, 249);
                chartControl2.BorderOptions.Color = Color.FromArgb(249, 249, 249);


                //policlinic
                sidebarDarkModeCloseProperty();

                panelBackgroundDarkModeCloseProperty();

                searchDarkModeCloseProperty();

                buttonDarkModeCloseProperty();

                darkModeCloseProperty();

                //doctor

                panelDoctorPropertyDarkClose();

                buttonDoctorPropertyDarkModeClose();

                doctorSearchProperyDarkClose();

                //doctorRest

                panelDoctorRestPropertyDarkClose();

                buttonDoctorRestDarkClose();

                doctorRestSearchPropertyDarkClose();

                //appointmentTime

                panelAppointmentTimePropertyDarkClose();

                buttonAppointmentTimeDarkClose();

                doctorAppointmentTimeSearchPropertyDarkClose();

                //admin

                panelAdminPropertyDarkClose();

                buttonAdminDarkClose();

                doctorAdminSearchPropertyDarkClose();

                //adminInformation

                panelAdminInformationPropertyDarkClose();

                panelAdminLabelInformationPropertyDarkClose();

                //panelDarkMode

                panelDarkModeClosePropery();
            }
        }

        private void panelDarkModeOpenPropery()
        {
            panelDarkModeSetting.BackColor = Color.FromArgb(64, 64, 64);

            labelDarkMode.ForeColor = Color.White;
            labelDarkMode.BackColor = Color.FromArgb(64, 64, 64);

            labelDarkModeWarning.BackColor = Color.FromArgb(64, 64, 64);

            pictureBoxDarkMode.Image = Properties.Resources.darkMode;
        }

        private void panelDarkModeClosePropery()
        {
            panelDarkModeSetting.BackColor = Color.FromArgb(249, 249, 249);

            labelDarkMode.ForeColor = Color.Black;
            labelDarkMode.BackColor = Color.FromArgb(249, 249, 249);

            labelDarkModeWarning.BackColor = Color.FromArgb(249, 249, 249);

            pictureBoxDarkMode.Image = Properties.Resources.lightMode;
        }
        private void buttonDarkModeOpenProperty()
        {
            btn_PoliclinicAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_PoliclinicAdd.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_PoliclinicAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_PoliclinicAdd.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_PoliclinicAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_PoliclinicAdd.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_PoliclinicAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_PoliclinicAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //policlinicUpdate

            btn_PoliclinicUpdate.ForeColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicUpdate.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_PoliclinicUpdate.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_PoliclinicUpdate.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_PoliclinicUpdate.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_PoliclinicUpdate.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_PoliclinicUpdate.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_PoliclinicUpdate.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_PoliclinicUpdate.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //policlinicDelete

            btn_PoliclinicDelete.ForeColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicDelete.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_PoliclinicDelete.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_PoliclinicDelete.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_PoliclinicDelete.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_PoliclinicDelete.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_PoliclinicDelete.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_PoliclinicDelete.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_PoliclinicDelete.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            labelPoliclinicDeleteWarning.ForeColor = Color.White;
            labelPoliclinicDeleteWarning.BackColor = Color.FromArgb(64, 64, 64);

            checkboxPoliclinicDelete.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxPoliclinicDelete.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxPoliclinicDelete.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxPoliclinicDelete.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxPoliclinicDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxPoliclinicDelete.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxPoliclinicDelete.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }

        private void buttonDarkModeCloseProperty()
        {
            btn_PoliclinicAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_PoliclinicAdd.IdleBorderColor = Color.Black;
            btn_PoliclinicAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicAdd.onHoverState.BorderColor = Color.Black;
            btn_PoliclinicAdd.onHoverState.FillColor = Color.DimGray;

            btn_PoliclinicAdd.OnIdleState.BorderColor = Color.Black;
            btn_PoliclinicAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicAdd.OnPressedState.BorderColor = Color.Black;
            btn_PoliclinicAdd.OnPressedState.FillColor = Color.Gray;

            //policlinicUpdate

            btn_PoliclinicUpdate.ForeColor = Color.FromArgb(64, 64, 64);

            btn_PoliclinicUpdate.IdleBorderColor = Color.Black;
            btn_PoliclinicUpdate.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicUpdate.onHoverState.BorderColor = Color.Black;
            btn_PoliclinicUpdate.onHoverState.FillColor = Color.DimGray;

            btn_PoliclinicUpdate.OnIdleState.BorderColor = Color.Black;
            btn_PoliclinicUpdate.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicUpdate.OnPressedState.BorderColor = Color.Black;
            btn_PoliclinicUpdate.OnPressedState.FillColor = Color.Gray;

            //policlinicDelete

            btn_PoliclinicDelete.ForeColor = Color.FromArgb(64, 64, 64);

            btn_PoliclinicDelete.IdleBorderColor = Color.Black;
            btn_PoliclinicDelete.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicDelete.onHoverState.BorderColor = Color.Black;
            btn_PoliclinicDelete.onHoverState.FillColor = Color.DimGray;

            btn_PoliclinicDelete.OnIdleState.BorderColor = Color.Black;
            btn_PoliclinicDelete.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_PoliclinicDelete.OnPressedState.BorderColor = Color.Black;
            btn_PoliclinicDelete.OnPressedState.FillColor = Color.Gray;

            labelPoliclinicDeleteWarning.ForeColor = Color.Black;
            labelPoliclinicDeleteWarning.BackColor = Color.FromArgb(249, 249, 249);

            checkboxPoliclinicDelete.OnCheck.BorderColor = Color.Black;
            checkboxPoliclinicDelete.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxPoliclinicDelete.OnCheck.CheckmarkColor = Color.Black;

            checkboxPoliclinicDelete.OnHoverChecked.BorderColor = Color.Black;
            checkboxPoliclinicDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxPoliclinicDelete.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxPoliclinicDelete.OnUncheck.BorderColor = Color.Black;
        }

        private void panelBackgroundDarkModeOpenProperty()
        {
            panelPoliclinic.BackColor = Color.FromArgb(64, 64, 64);
            datagridPoliclinics.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 86, 216);
            datagridPoliclinics.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 86, 216);
            datagridPoliclinics.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            datagridPoliclinics.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            datagridPoliclinics.DefaultCellStyle.BackColor = Color.FromArgb(167, 114, 242);
            datagridPoliclinics.DefaultCellStyle.ForeColor = Color.FromArgb(249, 249, 249);
            datagridPoliclinics.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            datagridPoliclinics.DefaultCellStyle.SelectionForeColor = Color.FromArgb(249, 249, 249);

            datagridPoliclinics.GridColor = Color.FromArgb(64, 64, 64);
        }

        private void panelBackgroundDarkModeCloseProperty()
        {
            panelPoliclinic.BackColor = Color.FromArgb(249, 249, 249);

            datagridPoliclinics.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            datagridPoliclinics.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            datagridPoliclinics.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            datagridPoliclinics.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            datagridPoliclinics.DefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200);
            datagridPoliclinics.DefaultCellStyle.ForeColor = Color.Black;
            datagridPoliclinics.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 249, 249);
            datagridPoliclinics.DefaultCellStyle.SelectionForeColor = Color.Black;

            datagridPoliclinics.GridColor = Color.FromArgb(249, 249, 249);
            datagridPoliclinics.BackgroundColor = Color.FromArgb(249, 249, 249);
        }

        private void searchDarkModeOpenProperty()
        {
            btn_Search.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_Search.IdleFillColor = Color.FromArgb(124, 86, 216);

            btn_Search.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_Search.onHoverState.FillColor = Color.FromArgb(167, 114, 242);

            btn_Search.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_Search.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

            btn_Search.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_Search.OnPressedState.FillColor = Color.FromArgb(97, 50, 209);

            btn_Search.IdleIconLeftImage = HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.search25x25White;


            txtSearch.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtSearch.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtSearch.BorderColorIdle = Color.Gray;

            txtSearch.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtSearch.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtSearch.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);

            txtSearch.FillColor = Color.FromArgb(38, 38, 38);


            labelSlowSearchPoliclinic.ForeColor = Color.White;
            labelSlowSearchPoliclinic.BackColor = Color.FromArgb(64, 64, 64);

            checkboxSlowSearchPoliclinic.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxSlowSearchPoliclinic.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxSlowSearchPoliclinic.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxSlowSearchPoliclinic.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxSlowSearchPoliclinic.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxSlowSearchPoliclinic.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxSlowSearchPoliclinic.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }

        private void searchDarkModeCloseProperty()
        {
            btn_Search.IdleBorderColor = Color.Black;
            btn_Search.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_Search.onHoverState.BorderColor = Color.Black;
            btn_Search.onHoverState.FillColor = Color.FromArgb(235, 235, 235);

            btn_Search.OnIdleState.BorderColor = Color.Black;
            btn_Search.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_Search.OnPressedState.BorderColor = Color.Black;
            btn_Search.OnPressedState.FillColor = Color.FromArgb(200, 200, 200);

            btn_Search.IdleIconLeftImage = HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.search25x25Black;


            txtSearch.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtSearch.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtSearch.BorderColorIdle = Color.Gray;

            txtSearch.OnHoverState.FillColor = Color.White;
            txtSearch.OnIdleState.FillColor = Color.White;
            txtSearch.OnActiveState.FillColor = Color.White;

            txtSearch.FillColor = Color.White;


            labelSlowSearchPoliclinic.ForeColor = Color.Black;
            labelSlowSearchPoliclinic.BackColor = Color.FromArgb(249, 249, 249);

            checkboxSlowSearchPoliclinic.OnCheck.BorderColor = Color.Black;
            checkboxSlowSearchPoliclinic.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxSlowSearchPoliclinic.OnCheck.CheckmarkColor = Color.Black;

            checkboxSlowSearchPoliclinic.OnHoverChecked.BorderColor = Color.Black;
            checkboxSlowSearchPoliclinic.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxSlowSearchPoliclinic.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxSlowSearchPoliclinic.OnUncheck.BorderColor = Color.Black;
        }

        private void panelAppointmentTimePropertyDarkOpen()
        {
            panelAppointmentTime.BackColor = Color.FromArgb(64, 64, 64);

            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 86, 216);
            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 86, 216);
            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAppointmentTime.DefaultCellStyle.BackColor = Color.FromArgb(167, 114, 242);
            dataGridAppointmentTime.DefaultCellStyle.ForeColor = Color.FromArgb(249, 249, 249);
            dataGridAppointmentTime.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            dataGridAppointmentTime.DefaultCellStyle.SelectionForeColor = Color.FromArgb(249, 249, 249);

            dataGridAppointmentTime.GridColor = Color.FromArgb(64, 64, 64);
        }
        private void panelAppointmentTimePropertyDarkClose()
        {
            panelAppointmentTime.BackColor = Color.FromArgb(249, 249, 249);

            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridAppointmentTime.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAppointmentTime.DefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200);
            dataGridAppointmentTime.DefaultCellStyle.ForeColor = Color.Black;
            dataGridAppointmentTime.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 249, 249);
            dataGridAppointmentTime.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAppointmentTime.GridColor = Color.FromArgb(249, 249, 249);
            dataGridAppointmentTime.BackgroundColor = Color.FromArgb(249, 249, 249);
        }
        private void buttonAppointmentTimeDarkOpen()
        {
            btn_AppointmentTimeAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentTimeAdd.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_AppointmentTimeAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AppointmentTimeAdd.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AppointmentTimeAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentTimeAdd.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AppointmentTimeAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AppointmentTimeAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //AppointmentTimeUpdate

            btn_AppointmentTimeUpdate.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeUpdate.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentTimeUpdate.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_AppointmentTimeUpdate.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AppointmentTimeUpdate.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AppointmentTimeUpdate.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentTimeUpdate.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AppointmentTimeUpdate.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AppointmentTimeUpdate.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //AppointmentTimeDelete

            btn_AppointmentTimeDelete.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeDelete.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentTimeDelete.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_AppointmentTimeDelete.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AppointmentTimeDelete.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AppointmentTimeDelete.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentTimeDelete.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AppointmentTimeDelete.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AppointmentTimeDelete.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            labelAppointmentTimeApprovedDelete.ForeColor = Color.White;
            labelAppointmentTimeApprovedDelete.BackColor = Color.FromArgb(64, 64, 64);

            checkboxAppointmentApprovedDelete.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxAppointmentApprovedDelete.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxAppointmentApprovedDelete.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxAppointmentApprovedDelete.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxAppointmentApprovedDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxAppointmentApprovedDelete.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxAppointmentApprovedDelete.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }
        private void buttonAppointmentTimeDarkClose()
        {
            btn_AppointmentTimeAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AppointmentTimeAdd.IdleBorderColor = Color.Black;
            btn_AppointmentTimeAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeAdd.onHoverState.BorderColor = Color.Black;
            btn_AppointmentTimeAdd.onHoverState.FillColor = Color.DimGray;

            btn_AppointmentTimeAdd.OnIdleState.BorderColor = Color.Black;
            btn_AppointmentTimeAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeAdd.OnPressedState.BorderColor = Color.Black;
            btn_AppointmentTimeAdd.OnPressedState.FillColor = Color.Gray;

            //AppointmentTimeUpdate

            btn_AppointmentTimeUpdate.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AppointmentTimeUpdate.IdleBorderColor = Color.Black;
            btn_AppointmentTimeUpdate.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeUpdate.onHoverState.BorderColor = Color.Black;
            btn_AppointmentTimeUpdate.onHoverState.FillColor = Color.DimGray;

            btn_AppointmentTimeUpdate.OnIdleState.BorderColor = Color.Black;
            btn_AppointmentTimeUpdate.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeUpdate.OnPressedState.BorderColor = Color.Black;
            btn_AppointmentTimeUpdate.OnPressedState.FillColor = Color.Gray;

            //AppointmentTimeDelete

            btn_AppointmentTimeDelete.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AppointmentTimeDelete.IdleBorderColor = Color.Black;
            btn_AppointmentTimeDelete.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeDelete.onHoverState.BorderColor = Color.Black;
            btn_AppointmentTimeDelete.onHoverState.FillColor = Color.DimGray;

            btn_AppointmentTimeDelete.OnIdleState.BorderColor = Color.Black;
            btn_AppointmentTimeDelete.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentTimeDelete.OnPressedState.BorderColor = Color.Black;
            btn_AppointmentTimeDelete.OnPressedState.FillColor = Color.Gray;

            labelAppointmentTimeApprovedDelete.ForeColor = Color.Black;
            labelAppointmentTimeApprovedDelete.BackColor = Color.FromArgb(249, 249, 249);

            checkboxAppointmentApprovedDelete.OnCheck.BorderColor = Color.Black;
            checkboxAppointmentApprovedDelete.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxAppointmentApprovedDelete.OnCheck.CheckmarkColor = Color.Black;

            checkboxAppointmentApprovedDelete.OnHoverChecked.BorderColor = Color.Black;
            checkboxAppointmentApprovedDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxAppointmentApprovedDelete.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxAppointmentApprovedDelete.OnUncheck.BorderColor = Color.Black;
        }
        private void doctorAppointmentTimeSearchPropertyDarkOpen()
        {
            btn_AppointmentSearch.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentSearch.IdleFillColor = Color.FromArgb(124, 86, 216);

            btn_AppointmentSearch.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AppointmentSearch.onHoverState.FillColor = Color.FromArgb(167, 114, 242);

            btn_AppointmentSearch.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AppointmentSearch.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AppointmentSearch.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AppointmentSearch.OnPressedState.FillColor = Color.FromArgb(97, 50, 209);

            btn_AppointmentSearch.IdleIconLeftImage = HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.search25x25White;


            txtAppointmentTimeSearch.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAppointmentTimeSearch.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAppointmentTimeSearch.BorderColorIdle = Color.Gray;

            txtAppointmentTimeSearch.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAppointmentTimeSearch.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAppointmentTimeSearch.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);

            txtAppointmentTimeSearch.FillColor = Color.FromArgb(38, 38, 38);


            labelSlowSearchAppointmentTime.ForeColor = Color.White;
            labelSlowSearchAppointmentTime.BackColor = Color.FromArgb(64, 64, 64);

            checkboxSlowSearchAppointmentTime.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxSlowSearchAppointmentTime.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxSlowSearchAppointmentTime.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxSlowSearchAppointmentTime.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxSlowSearchAppointmentTime.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxSlowSearchAppointmentTime.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxSlowSearchAppointmentTime.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }
        private void doctorAppointmentTimeSearchPropertyDarkClose()
        {
            btn_AppointmentSearch.IdleBorderColor = Color.Black;
            btn_AppointmentSearch.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentSearch.onHoverState.BorderColor = Color.Black;
            btn_AppointmentSearch.onHoverState.FillColor = Color.FromArgb(235, 235, 235);

            btn_AppointmentSearch.OnIdleState.BorderColor = Color.Black;
            btn_AppointmentSearch.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AppointmentSearch.OnPressedState.BorderColor = Color.Black;
            btn_AppointmentSearch.OnPressedState.FillColor = Color.FromArgb(200, 200, 200);

            btn_AppointmentSearch.IdleIconLeftImage = HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.search25x25Black;


            txtAppointmentTimeSearch.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAppointmentTimeSearch.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAppointmentTimeSearch.BorderColorIdle = Color.Gray;

            txtAppointmentTimeSearch.OnHoverState.FillColor = Color.White;
            txtAppointmentTimeSearch.OnIdleState.FillColor = Color.White;
            txtAppointmentTimeSearch.OnActiveState.FillColor = Color.White;

            txtAppointmentTimeSearch.FillColor = Color.White;


            labelSlowSearchAppointmentTime.ForeColor = Color.Black;
            labelSlowSearchAppointmentTime.BackColor = Color.FromArgb(249, 249, 249);

            checkboxSlowSearchAppointmentTime.OnCheck.BorderColor = Color.Black;
            checkboxSlowSearchAppointmentTime.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxSlowSearchAppointmentTime.OnCheck.CheckmarkColor = Color.Black;

            checkboxSlowSearchAppointmentTime.OnHoverChecked.BorderColor = Color.Black;
            checkboxSlowSearchAppointmentTime.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxSlowSearchAppointmentTime.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxSlowSearchAppointmentTime.OnUncheck.BorderColor = Color.Black;
        }
        private void panelAdminPropertyDarkOpen()
        {
            panelAdmins.BackColor = Color.FromArgb(64, 64, 64);

            dataGridAdmin.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 86, 216);
            dataGridAdmin.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 86, 216);
            dataGridAdmin.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridAdmin.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAdmin.DefaultCellStyle.BackColor = Color.FromArgb(167, 114, 242);
            dataGridAdmin.DefaultCellStyle.ForeColor = Color.FromArgb(249, 249, 249);
            dataGridAdmin.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            dataGridAdmin.DefaultCellStyle.SelectionForeColor = Color.FromArgb(249, 249, 249);

            dataGridAdmin.GridColor = Color.FromArgb(64, 64, 64);
        }
        private void panelAdminPropertyDarkClose()
        {
            panelAdmins.BackColor = Color.FromArgb(249, 249, 249);

            dataGridAdmin.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridAdmin.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            dataGridAdmin.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridAdmin.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAdmin.DefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200);
            dataGridAdmin.DefaultCellStyle.ForeColor = Color.Black;
            dataGridAdmin.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 249, 249);
            dataGridAdmin.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAdmin.GridColor = Color.FromArgb(249, 249, 249);
            dataGridAdmin.BackgroundColor = Color.FromArgb(249, 249, 249);
        }
        private void buttonAdminDarkOpen()
        {
            btn_AdminLoginInformation.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AdminLoginInformation.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminLoginInformation.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_AdminLoginInformation.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AdminLoginInformation.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AdminLoginInformation.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminLoginInformation.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AdminLoginInformation.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AdminLoginInformation.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //adminAdd

            btn_AdminAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AdminAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminAdd.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_AdminAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AdminAdd.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AdminAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminAdd.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AdminAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AdminAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //adminUpdate

            btn_AdminUpdate.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AdminUpdate.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminUpdate.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_AdminUpdate.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AdminUpdate.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AdminUpdate.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminUpdate.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AdminUpdate.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AdminUpdate.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //adminDelete

            btn_AdminDelete.ForeColor = Color.FromArgb(230, 230, 230);

            btn_AdminDelete.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminDelete.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_AdminDelete.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AdminDelete.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AdminDelete.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminDelete.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_AdminDelete.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AdminDelete.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //Approved

            labelAdminApprovedDelete.ForeColor = Color.White;
            labelAdminApprovedDelete.BackColor = Color.FromArgb(64, 64, 64);

            checkboxAdminApprovedDelete.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxAdminApprovedDelete.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxAdminApprovedDelete.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxAdminApprovedDelete.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxAdminApprovedDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxAdminApprovedDelete.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxAdminApprovedDelete.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }
        private void buttonAdminDarkClose()
        {
            btn_AdminLoginInformation.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AdminLoginInformation.IdleBorderColor = Color.Black;
            btn_AdminLoginInformation.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AdminLoginInformation.onHoverState.BorderColor = Color.Black;
            btn_AdminLoginInformation.onHoverState.FillColor = Color.DimGray;

            btn_AdminLoginInformation.OnIdleState.BorderColor = Color.Black;
            btn_AdminLoginInformation.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AdminLoginInformation.OnPressedState.BorderColor = Color.Black;
            btn_AdminLoginInformation.OnPressedState.FillColor = Color.Gray;

            //adminAdd
            btn_AdminAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AdminAdd.IdleBorderColor = Color.Black;
            btn_AdminAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AdminAdd.onHoverState.BorderColor = Color.Black;
            btn_AdminAdd.onHoverState.FillColor = Color.DimGray;

            btn_AdminAdd.OnIdleState.BorderColor = Color.Black;
            btn_AdminAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AdminAdd.OnPressedState.BorderColor = Color.Black;
            btn_AdminAdd.OnPressedState.FillColor = Color.Gray;

            //adminUpdate

            btn_AdminUpdate.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AdminUpdate.IdleBorderColor = Color.Black;
            btn_AdminUpdate.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AdminUpdate.onHoverState.BorderColor = Color.Black;
            btn_AdminUpdate.onHoverState.FillColor = Color.DimGray;

            btn_AdminUpdate.OnIdleState.BorderColor = Color.Black;
            btn_AdminUpdate.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AdminUpdate.OnPressedState.BorderColor = Color.Black;
            btn_AdminUpdate.OnPressedState.FillColor = Color.Gray;

            //adminDelete

            btn_AdminDelete.ForeColor = Color.FromArgb(64, 64, 64);

            btn_AdminDelete.IdleBorderColor = Color.Black;
            btn_AdminDelete.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AdminDelete.onHoverState.BorderColor = Color.Black;
            btn_AdminDelete.onHoverState.FillColor = Color.DimGray;

            btn_AdminDelete.OnIdleState.BorderColor = Color.Black;
            btn_AdminDelete.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AdminDelete.OnPressedState.BorderColor = Color.Black;
            btn_AdminDelete.OnPressedState.FillColor = Color.Gray;

            labelAdminApprovedDelete.ForeColor = Color.Black;
            labelAdminApprovedDelete.BackColor = Color.FromArgb(249, 249, 249);

            checkboxAdminApprovedDelete.OnCheck.BorderColor = Color.Black;
            checkboxAdminApprovedDelete.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxAdminApprovedDelete.OnCheck.CheckmarkColor = Color.Black;

            checkboxAdminApprovedDelete.OnHoverChecked.BorderColor = Color.Black;
            checkboxAdminApprovedDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxAdminApprovedDelete.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxAdminApprovedDelete.OnUncheck.BorderColor = Color.Black;
        }
        private void doctorAdminSearchPropertyDarkOpen()
        {
            btn_AdminSearch.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminSearch.IdleFillColor = Color.FromArgb(124, 86, 216);

            btn_AdminSearch.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_AdminSearch.onHoverState.FillColor = Color.FromArgb(167, 114, 242);

            btn_AdminSearch.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_AdminSearch.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

            btn_AdminSearch.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_AdminSearch.OnPressedState.FillColor = Color.FromArgb(97, 50, 209);

            btn_AdminSearch.IdleIconLeftImage = Properties.Resources.search25x25White;


            txtAdminSearch.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminSearch.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminSearch.BorderColorIdle = Color.Gray;

            txtAdminSearch.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSearch.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSearch.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);

            txtAdminSearch.FillColor = Color.FromArgb(38, 38, 38);


            labelSlowAdminSearch.ForeColor = Color.White;
            labelSlowAdminSearch.BackColor = Color.FromArgb(64, 64, 64);

            checkboxAdminSlowSearch.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxAdminSlowSearch.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxAdminSlowSearch.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxAdminSlowSearch.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxAdminSlowSearch.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxAdminSlowSearch.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxAdminSlowSearch.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }
        private void doctorAdminSearchPropertyDarkClose()
        {
            btn_AdminSearch.IdleBorderColor = Color.Black;
            btn_AdminSearch.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_AdminSearch.onHoverState.BorderColor = Color.Black;
            btn_AdminSearch.onHoverState.FillColor = Color.FromArgb(235, 235, 235);

            btn_AdminSearch.OnIdleState.BorderColor = Color.Black;
            btn_AdminSearch.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_AdminSearch.OnPressedState.BorderColor = Color.Black;
            btn_AdminSearch.OnPressedState.FillColor = Color.FromArgb(200, 200, 200);

            btn_AdminSearch.IdleIconLeftImage = HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.search25x25Black;


            txtAdminSearch.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminSearch.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminSearch.BorderColorIdle = Color.Gray;

            txtAdminSearch.OnHoverState.FillColor = Color.White;
            txtAdminSearch.OnIdleState.FillColor = Color.White;
            txtAdminSearch.OnActiveState.FillColor = Color.White;

            txtAdminSearch.FillColor = Color.White;


            labelSlowAdminSearch.ForeColor = Color.Black;
            labelSlowAdminSearch.BackColor = Color.FromArgb(249, 249, 249);

            checkboxAdminSlowSearch.OnCheck.BorderColor = Color.Black;
            checkboxAdminSlowSearch.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxAdminSlowSearch.OnCheck.CheckmarkColor = Color.Black;

            checkboxAdminSlowSearch.OnHoverChecked.BorderColor = Color.Black;
            checkboxAdminSlowSearch.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxAdminSlowSearch.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxAdminSlowSearch.OnUncheck.BorderColor = Color.Black;
        }
        private void panelAdminInformationPropertyDarkOpen()
        {
            panelMyInformation.BackColor = Color.FromArgb(64, 64, 64);

            txtAdminID.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminID.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminID.BorderColorIdle = Color.Gray;
            txtAdminID.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminID.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminID.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminID.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminID.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminID.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminID.OnDisabledState.ForeColor = Color.White;

            txtAdminTcNo.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminTcNo.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminTcNo.BorderColorIdle = Color.Gray;
            txtAdminTcNo.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminTcNo.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminTcNo.OnDisabledState.ForeColor = Color.White;

            txtAdminMail.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminMail.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminMail.BorderColorIdle = Color.Gray;
            txtAdminMail.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminMail.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminMail.OnDisabledState.ForeColor = Color.White;

            txtAdminPhoneNumber.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminPhoneNumber.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminPhoneNumber.BorderColorIdle = Color.Gray;
            txtAdminPhoneNumber.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminPhoneNumber.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminPhoneNumber.OnDisabledState.ForeColor = Color.White;

            txtAdminName.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminName.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminName.BorderColorIdle = Color.Gray;
            txtAdminName.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminName.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminName.OnDisabledState.ForeColor = Color.White;

            txtAdminSurname.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminSurname.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminSurname.BorderColorIdle = Color.Gray;
            txtAdminSurname.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminSurname.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminSurname.OnDisabledState.ForeColor = Color.White;

            txtAdminGender.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminGender.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminGender.BorderColorIdle = Color.Gray;
            txtAdminGender.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminGender.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminGender.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminGender.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminGender.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminGender.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminGender.OnDisabledState.ForeColor = Color.White;

            txtAdminDate.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminDate.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminDate.BorderColorIdle = Color.Gray;
            txtAdminDate.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminDate.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminDate.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminDate.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminDate.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminDate.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminDate.OnDisabledState.ForeColor = Color.White;

            txtAdminUsername.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminUsername.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminUsername.BorderColorIdle = Color.Gray;
            txtAdminUsername.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminUsername.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminUsername.OnDisabledState.ForeColor = Color.White;

            txtAdminPassword.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtAdminPassword.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtAdminPassword.BorderColorIdle = Color.Gray;
            txtAdminPassword.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPassword.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPassword.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPassword.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPassword.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtAdminPassword.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtAdminPassword.OnDisabledState.ForeColor = Color.White;

            btn_InformationUpdate.ForeColor = Color.FromArgb(230, 230, 230);
            btn_InformationUpdate.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_InformationUpdate.IdleFillColor = Color.FromArgb(38, 38, 38);
            btn_InformationUpdate.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_InformationUpdate.onHoverState.FillColor = Color.FromArgb(124, 86, 216);
            btn_InformationUpdate.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_InformationUpdate.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            btn_InformationUpdate.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_InformationUpdate.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            btn_InformationPasswordChange.ForeColor = Color.FromArgb(230, 230, 230);
            btn_InformationPasswordChange.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_InformationPasswordChange.IdleFillColor = Color.FromArgb(38, 38, 38);
            btn_InformationPasswordChange.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_InformationPasswordChange.onHoverState.FillColor = Color.FromArgb(124, 86, 216);
            btn_InformationPasswordChange.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_InformationPasswordChange.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            btn_InformationPasswordChange.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_InformationPasswordChange.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            labelConfirm.BackColor = Color.FromArgb(64, 64, 64);
            labelCannotConfirmMail.BackColor = Color.FromArgb(64, 64, 64);
        }
        private void panelAdminInformationPropertyDarkClose()
        {
            panelMyInformation.BackColor = Color.FromArgb(249, 249, 249);

            txtAdminID.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminID.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminID.BorderColorIdle = Color.Gray;
            txtAdminID.OnHoverState.FillColor = Color.White;
            txtAdminID.OnIdleState.FillColor = Color.White;
            txtAdminID.OnActiveState.FillColor = Color.White;
            txtAdminID.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminID.OnDisabledState.FillColor = Color.White;
            txtAdminID.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminID.OnDisabledState.ForeColor = Color.Black;

            txtAdminTcNo.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminTcNo.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminTcNo.BorderColorIdle = Color.Gray;
            txtAdminTcNo.OnHoverState.FillColor = Color.White;
            txtAdminTcNo.OnIdleState.FillColor = Color.White;
            txtAdminTcNo.OnActiveState.FillColor = Color.White;
            txtAdminTcNo.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminTcNo.OnDisabledState.FillColor = Color.White;
            txtAdminTcNo.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminTcNo.OnDisabledState.ForeColor = Color.Black;

            txtAdminMail.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminMail.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminMail.BorderColorIdle = Color.Gray;
            txtAdminMail.OnHoverState.FillColor = Color.White;
            txtAdminMail.OnIdleState.FillColor = Color.White;
            txtAdminMail.OnActiveState.FillColor = Color.White;
            txtAdminMail.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminMail.OnDisabledState.FillColor = Color.White;
            txtAdminMail.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminMail.OnDisabledState.ForeColor = Color.Black;

            txtAdminPhoneNumber.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminPhoneNumber.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminPhoneNumber.BorderColorIdle = Color.Gray;
            txtAdminPhoneNumber.OnHoverState.FillColor = Color.White;
            txtAdminPhoneNumber.OnIdleState.FillColor = Color.White;
            txtAdminPhoneNumber.OnActiveState.FillColor = Color.White;
            txtAdminPhoneNumber.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPhoneNumber.OnDisabledState.FillColor = Color.White;
            txtAdminPhoneNumber.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminPhoneNumber.OnDisabledState.ForeColor = Color.Black;

            txtAdminName.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminName.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminName.BorderColorIdle = Color.Gray;
            txtAdminName.OnHoverState.FillColor = Color.White;
            txtAdminName.OnIdleState.FillColor = Color.White;
            txtAdminName.OnActiveState.FillColor = Color.White;
            txtAdminName.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminName.OnDisabledState.FillColor = Color.White;
            txtAdminName.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminName.OnDisabledState.ForeColor = Color.Black;

            txtAdminSurname.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminSurname.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminSurname.BorderColorIdle = Color.Gray;
            txtAdminSurname.OnHoverState.FillColor = Color.White;
            txtAdminSurname.OnIdleState.FillColor = Color.White;
            txtAdminSurname.OnActiveState.FillColor = Color.White;
            txtAdminSurname.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminSurname.OnDisabledState.FillColor = Color.White;
            txtAdminSurname.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminSurname.OnDisabledState.ForeColor = Color.Black;

            txtAdminGender.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminGender.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminGender.BorderColorIdle = Color.Gray;
            txtAdminGender.OnHoverState.FillColor = Color.White;
            txtAdminGender.OnIdleState.FillColor = Color.White;
            txtAdminGender.OnActiveState.FillColor = Color.White;
            txtAdminGender.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminGender.OnDisabledState.FillColor = Color.White;
            txtAdminGender.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminGender.OnDisabledState.ForeColor = Color.Black;

            txtAdminDate.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminDate.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminDate.BorderColorIdle = Color.Gray;
            txtAdminDate.OnHoverState.FillColor = Color.White;
            txtAdminDate.OnIdleState.FillColor = Color.White;
            txtAdminDate.OnActiveState.FillColor = Color.White;
            txtAdminDate.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminDate.OnDisabledState.FillColor = Color.White;
            txtAdminDate.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminDate.OnDisabledState.ForeColor = Color.Black;

            txtAdminUsername.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminUsername.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminUsername.BorderColorIdle = Color.Gray;
            txtAdminUsername.OnHoverState.FillColor = Color.White;
            txtAdminUsername.OnIdleState.FillColor = Color.White;
            txtAdminUsername.OnActiveState.FillColor = Color.White;
            txtAdminUsername.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminUsername.OnDisabledState.FillColor = Color.White;
            txtAdminUsername.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminUsername.OnDisabledState.ForeColor = Color.Black;

            txtAdminPassword.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtAdminPassword.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtAdminPassword.BorderColorIdle = Color.Gray;
            txtAdminPassword.OnHoverState.FillColor = Color.White;
            txtAdminPassword.OnIdleState.FillColor = Color.White;
            txtAdminPassword.OnActiveState.FillColor = Color.White;
            txtAdminPassword.FillColor = Color.FromArgb(38, 38, 38);
            txtAdminPassword.OnDisabledState.FillColor = Color.White;
            txtAdminPassword.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtAdminPassword.OnDisabledState.ForeColor = Color.Black;

            btn_InformationUpdate.ForeColor = Color.FromArgb(64, 64, 64);
            btn_InformationUpdate.IdleBorderColor = Color.Black;
            btn_InformationUpdate.IdleFillColor = Color.FromArgb(230, 230, 230);
            btn_InformationUpdate.onHoverState.BorderColor = Color.Black;
            btn_InformationUpdate.onHoverState.FillColor = Color.DimGray;
            btn_InformationUpdate.OnIdleState.BorderColor = Color.Black;
            btn_InformationUpdate.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);
            btn_InformationUpdate.OnPressedState.BorderColor = Color.Black;
            btn_InformationUpdate.OnPressedState.FillColor = Color.Gray;

            btn_InformationPasswordChange.ForeColor = Color.FromArgb(64, 64, 64);
            btn_InformationPasswordChange.IdleBorderColor = Color.Black;
            btn_InformationPasswordChange.IdleFillColor = Color.FromArgb(230, 230, 230);
            btn_InformationPasswordChange.onHoverState.BorderColor = Color.Black;
            btn_InformationPasswordChange.onHoverState.FillColor = Color.DimGray;
            btn_InformationPasswordChange.OnIdleState.BorderColor = Color.Black;
            btn_InformationPasswordChange.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);
            btn_InformationPasswordChange.OnPressedState.BorderColor = Color.Black;
            btn_InformationPasswordChange.OnPressedState.FillColor = Color.Gray;

            labelConfirm.BackColor = Color.FromArgb(249, 249, 249);
            labelCannotConfirmMail.BackColor = Color.FromArgb(249, 249, 249);
        }
        private void panelAdminLabelInformationPropertyDarkOpen()
        {
            labelAdminInformationID.ForeColor = Color.White;
            labelAdminInformationID.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminTcNo.ForeColor = Color.White;
            labelAdminTcNo.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminInformationName.ForeColor = Color.White;
            labelAdminInformationName.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminInformationSurname.ForeColor = Color.White;
            labelAdminInformationSurname.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminGender.ForeColor = Color.White;
            labelAdminGender.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminDate.ForeColor = Color.White;
            labelAdminDate.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminInformationMail.ForeColor = Color.White;
            labelAdminInformationMail.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminPhoneNumber.ForeColor = Color.White;
            labelAdminPhoneNumber.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminUsername.ForeColor = Color.White;
            labelAdminUsername.BackColor = Color.FromArgb(64, 64, 64);

            labelAdminPassword.ForeColor = Color.White;
            labelAdminPassword.BackColor = Color.FromArgb(64, 64, 64);
        }
        private void panelAdminLabelInformationPropertyDarkClose()
        {
            labelAdminInformationID.ForeColor = Color.Black;
            labelAdminInformationID.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminTcNo.ForeColor = Color.Black;
            labelAdminTcNo.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminInformationName.ForeColor = Color.Black;
            labelAdminInformationName.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminInformationSurname.ForeColor = Color.Black;
            labelAdminInformationSurname.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminGender.ForeColor = Color.Black;
            labelAdminGender.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminDate.ForeColor = Color.Black;
            labelAdminDate.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminInformationMail.ForeColor = Color.Black;
            labelAdminInformationMail.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminPhoneNumber.ForeColor = Color.Black;
            labelAdminPhoneNumber.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminUsername.ForeColor = Color.Black;
            labelAdminUsername.BackColor = Color.FromArgb(249, 249, 249);

            labelAdminPassword.ForeColor = Color.Black;
            labelAdminPassword.BackColor = Color.FromArgb(249, 249, 249);
        }
        private void sidebarDarkModeOpenProperty()
        {
            panelTopBar.BackColor = Color.FromArgb(64, 64, 64);

            panelAdminSideBar.BackColor = Color.FromArgb(38, 38, 38);

            navAdminMenu.BackColor = Color.FromArgb(38, 38, 38);

            panelAdminInfo.BackColor = Color.FromArgb(64, 64, 64);

        }

        private void darkModeOpenProperty()
        {

            pictureBoxMiddleDark.Visible = true;
            pictureBoxMiddleWhite.Visible = false;
        }

        private void sidebarDarkModeCloseProperty()
        {
            panelTopBar.BackColor = Color.FromArgb(230, 230, 230);

            panelAdminSideBar.BackColor = Color.FromArgb(230, 230, 230);

            navAdminMenu.BackColor = Color.FromArgb(230, 230, 230);

            panelAdminInfo.BackColor = Color.FromArgb(200, 200, 200);
        }

        private void darkModeCloseProperty()
        {

            pictureBoxMiddleDark.Visible = false;
            pictureBoxMiddleWhite.Visible = true;
        }

        private void AdminPanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosing)
            {
                return;
            }

            if (this.AccessibleName == "restart")
            {
                return;
            }

            isClosing = false;
            AdminPanelClosingForm adminPanelClosingForm = new AdminPanelClosingForm();
            adminPanelClosingForm.ShowDialog();

            string result = adminPanelClosingForm.AccessibleName.ToString();

            if (result == "LoginOpen")
            {
                this.AccessibleName = "LoginOpen";
            }
            else if (result == "NotClose")
            {
                e.Cancel = true;
            }
            else if (result == "Close")
            {
                this.AccessibleName = "Close";
            }
        }

        private void AdminPanelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClosing = false;
        }

        private async void policlinics_Click(object sender, EventArgs e)
        {
            panelPoliclinic.Visible = true;
            panelDoctors.Visible = false;
            panelDoctorRest.Visible = false;
            panelAdmins.Visible = false;
            panelDashboard.Visible = false;
            panelDarkModeSetting.Visible = false;
            panelMyInformation.Visible = false;

            labelPanelName.Text = "Poliklinikler";
            labelPanelName.Location = new Point(902, -3);

            await Task.Delay(1000);
            await LoadData();
        }

        private void policlinicAdd_Click(object sender, EventArgs e)
        {
            policlinics_Click(sender, e);
        }

        private void policlinicUpdate_Click(object sender, EventArgs e)
        {
            policlinics_Click(sender, e);
        }

        private void policlinicDelete_Click(object sender, EventArgs e)
        {
            policlinics_Click(sender, e);
        }
        private async Task LoadData()
        {
            string query = "SELECT Id, [Poliklinik Adı] FROM Policlinics";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable = new DataTable();
                            dataTable.Load(reader);

                            datagridPoliclinics.Rows.Clear();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                datagridPoliclinics.Rows.Add(row["Id"], row["Poliklinik Adı"]);
                            }
                            datagridPoliclinics.ClearSelection();
                        }
                    }

                    if (datagridPoliclinics.Rows.Count > 20)
                    {
                        checkboxSlowSearchPoliclinic.Checked = true;
                        checkboxSlowSearchPoliclinic.Enabled = false;
                        labelSlowSearchPoliclinic.Text = "Yavaş Arama (DİKKAT) (ZORUNLU SEÇİM)";
                    }
                    else
                    {
                        checkboxSlowSearchPoliclinic.Checked = false;
                        checkboxSlowSearchPoliclinic.Enabled = true;
                        labelSlowSearchPoliclinic.Text = "Yavaş Arama (DİKKAT)";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private void panelPoliclinic_Click(object sender, EventArgs e)
        {
            datagridPoliclinics.ClearSelection();
        }

        private void panelBackground_Click(object sender, EventArgs e)
        {
            datagridPoliclinics.ClearSelection();
            dataGridDoctor.ClearSelection();
            dataGridDoctorRest.ClearSelection();
            dataGridAdmin.ClearSelection();
            dataGridAppointmentTime.ClearSelection();
        }

        private void AdminPanelForm_Click(object sender, EventArgs e)
        {
            datagridPoliclinics.ClearSelection();
            dataGridDoctor.ClearSelection();
            dataGridDoctorRest.ClearSelection();
            dataGridAdmin.ClearSelection();
            dataGridAppointmentTime.ClearSelection();
        }

        private void panelTopBar_Click(object sender, EventArgs e)
        {
            datagridPoliclinics.ClearSelection();
            dataGridDoctor.ClearSelection();
            dataGridDoctorRest.ClearSelection();
            dataGridAdmin.ClearSelection();
            dataGridAppointmentTime.ClearSelection();
        }

        private void panelDarkMode_Click(object sender, EventArgs e)
        {
            datagridPoliclinics.ClearSelection();
            dataGridDoctor.ClearSelection();
            dataGridDoctorRest.ClearSelection();
            dataGridAdmin.ClearSelection();
            dataGridAppointmentTime.ClearSelection();
        }

        private void panelAdminInfo_Click(object sender, EventArgs e)
        {
            datagridPoliclinics.ClearSelection();
            dataGridDoctor.ClearSelection();
            dataGridDoctorRest.ClearSelection();
            dataGridAdmin.ClearSelection();
            dataGridAppointmentTime.ClearSelection();
        }
        private void navAdminMenu_Click(object sender, EventArgs e)
        {
            datagridPoliclinics.ClearSelection();
            dataGridDoctor.ClearSelection();
            dataGridDoctorRest.ClearSelection();
            dataGridAdmin.ClearSelection();
            dataGridAppointmentTime.ClearSelection();
        }

        private async void btn_PoliclinicAdd_Click(object sender, EventArgs e)
        {
            using (PoliclinicAddForm poliklinikEkleForm = new PoliclinicAddForm())
            {
                poliklinikEkleForm.Show();
                await PoliclinicSave(poliklinikEkleForm);
            }
        }
        private async Task PoliclinicSave(PoliclinicAddForm poliklinikEkleForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = poliklinikEkleForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadData();
                        poliklinikEkleForm.AccessibleName = "abcdefgh";
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_PoliclinicDelete_Click(object sender, EventArgs e)
        {

            if (checkboxPoliclinicDelete.Checked)
            {
                PoliclinicDelete();
            }
            else
            {
                if (datagridPoliclinics.SelectedRows.Count > 0 && datagridPoliclinics.SelectedRows.Count > maxSelectionCount)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("warning");
                    dataChooseForm.Show();
                }
                else if (datagridPoliclinics.SelectedRows.Count == 0)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("delete");
                    dataChooseForm.Show();
                }
                else
                {
                    ApproveDeleteForm approveDeleteForm = new ApproveDeleteForm(datagridPoliclinics, "policlinic");
                    approveDeleteForm.ShowDialog();
                    if (approveDeleteForm.AccessibleName == "Yes")
                    {
                        PoliclinicDelete();
                    }
                    else if (approveDeleteForm.AccessibleName == "No")
                    {
                        return;
                    }
                }
            }
        }
        private async void PoliclinicDelete()
        {
            int sayac = 0;
            if (datagridPoliclinics.SelectedRows.Count > 0 && maxSelectionCount >= datagridPoliclinics.SelectedRows.Count)
            {
                foreach (DataGridViewRow row in datagridPoliclinics.SelectedRows)
                {
                    sayac++;
                    int selectedId = Convert.ToInt32(row.Cells["policlinicID"].Value);

                    await PoliclinicDelete(selectedId);
                }

                if (this.AccessibleName == "delete")
                {
                    string operation = "delete";
                    PoliclinicAddUpdateDeleteSuccessForm poliklinikEkleSilGuncelleSuccessForm = new PoliclinicAddUpdateDeleteSuccessForm(operation, sayac);
                    poliklinikEkleSilGuncelleSuccessForm.Show();
                }
                await LoadData();
            }
            else if (datagridPoliclinics.SelectedRows.Count > 0 && datagridPoliclinics.SelectedRows.Count > maxSelectionCount)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("warning");
                dataChooseForm.Show();
            }
        }
        private async Task PoliclinicDelete(int id)
        {
            string query = "DELETE FROM Policlinics WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            this.AccessibleName = "delete";
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya silinemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_PoliclinicUpdate_Click(object sender, EventArgs e)
        {
            if (datagridPoliclinics.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(datagridPoliclinics.SelectedRows[0].Cells["policlinicID"].Value);
                string selectedItem = datagridPoliclinics.SelectedRows[0].Cells["policlinicName"].Value.ToString();

                PoliclinicUpdateForm poliklinikGuncelleForm = new PoliclinicUpdateForm(selectedId, selectedItem);
                poliklinikGuncelleForm.Show();

                await PoliclinicVeriGuncelle(poliklinikGuncelleForm);
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("update");
                dataChooseForm.Show();
            }
        }

        private async Task PoliclinicVeriGuncelle(PoliclinicUpdateForm poliklinikGuncelleForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = poliklinikGuncelleForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadData();
                        poliklinikGuncelleForm.AccessibleName = "abcdefgh";
                        poliklinikGuncelleForm.Close();


                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AdminPanelForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (panelDoctors.Visible)
                {
                    btn_DoctorsSearch_Click(sender, e);
                }
                else if (panelPoliclinic.Visible)
                {
                    btn_Search_Click(sender, e);
                }
                else if (panelDoctorRest.Visible)
                {
                    btn_DoctorRestSearch_Click(sender, e);
                }
                else if (panelAppointmentTime.Visible)
                {
                    btn_SearchAppointmentTime_Click(sender, e);
                }
                else if (panelAdmins.Visible)
                {
                    btn_AdminSearch_Click(sender, e);
                }
                e.SuppressKeyPress = true;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (checkboxSlowSearchPoliclinic.Checked)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    PoliclinicSearch();
                }
                else
                {
                    return;
                }
            }
            else
            {
                PoliclinicSearch();
            }

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                PoliclinicSearch();
            }
        }

        private void PoliclinicSearch()
        {
            if (dataTable != null)
            {
                string filterText = txtSearch.Text.Trim().Replace("'", "''");
                if (string.IsNullOrEmpty(filterText))
                {
                    dataTable.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    dataTable.DefaultView.RowFilter = $"[Poliklinik Adı] LIKE '%{filterText}%'";
                }
                datagridPoliclinics.Rows.Clear();
                foreach (DataRow row in dataTable.DefaultView.ToTable().Rows)
                {
                    datagridPoliclinics.Rows.Add(row["Id"], row["Poliklinik Adı"]);
                }
                datagridPoliclinics.ClearSelection();
            }
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            PoliclinicSearch();
        }

        private async void doctors_Click(object sender, EventArgs e)
        {
            panelDoctors.Visible = true;
            panelPoliclinic.Visible = false;
            panelDoctorRest.Visible = false;
            panelAppointmentTime.Visible = false;
            panelAdmins.Visible = false;
            panelDashboard.Visible = false;
            panelDarkModeSetting.Visible = false;
            panelMyInformation.Visible = false;

            labelPanelName.Text = "Doktorlar";
            labelPanelName.Location = new Point(946, -3);

            await Task.Delay(1000);
            await LoadDoctorData();

        }

        private void doctorAdd_Click(object sender, EventArgs e)
        {
            doctors_Click(sender, e);
        }

        private void doctorUpdate_Click(object sender, EventArgs e)
        {
            doctors_Click(sender, e);
        }

        private void doctorDelete_Click(object sender, EventArgs e)
        {
            doctors_Click(sender, e);
        }
        private void doctorPasswordSettings_Click(object sender, EventArgs e)
        {
            doctors_Click(sender, e);
        }

        private void doctorPerDiemSettings_Click(object sender, EventArgs e)
        {
            doctors_Click(sender, e);
        }

        private async Task LoadDoctorData()
        {
            string query = "SELECT D.HaveToPassword, D.TcNo, D.Id, D.Name, D.Surname, D.Gender, D.Date, D.Email, D.PhoneNumber, D.Password, D.Username, D.Money, D.PoliclinicId, P.Id, P.[Poliklinik Adı] FROM Doctors D INNER JOIN Policlinics P ON P.Id = PoliclinicId;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataDoctorTable = new DataTable();
                            dataDoctorTable.Load(reader);

                            dataGridDoctor.Rows.Clear();

                            if (dataDoctorTable.Rows.Count > 0)
                            {
                                int sayac = 0;
                                foreach (DataRow row in dataDoctorTable.Rows)
                                {
                                    labelHaveToDoctor.Visible = false;
                                    sayac++;
                                    string ageText = "";
                                    if (DateTime.TryParse(row["Date"]?.ToString(), out DateTime birthDate))
                                    {
                                        int age = DateTime.Now.Year - birthDate.Year;

                                        if (birthDate > DateTime.Now.AddYears(-age)) // şuan ki tarihten şuan ki yaşını çıkarır ve çıakn tarih doğum tarihinden küçükte yaşından 1 azaltır. 
                                        {
                                            age--;
                                        }

                                        ageText = age.ToString();
                                    }
                                    else
                                    {
                                        ageText = "Belirlenmemiş";
                                    }

                                    Image haveToPassword;
                                    bool status = Convert.ToBoolean(row["HaveToPassword"]);
                                    if (status)
                                    {
                                        haveToPassword = Properties.Resources.greenSucces;
                                    }
                                    else
                                    {
                                        haveToPassword = Properties.Resources.redError;
                                    }

                                    dataGridDoctor.Rows.Add(sayac, row["Id"], row["TcNo"], row["Name"], row["Surname"], row["Gender"], DateTime.Parse(row["Date"].ToString()).ToShortDateString(), ageText, row["Email"], row["PhoneNumber"], row["Money"], row["Poliklinik Adı"], row["Username"], row["Password"], haveToPassword);
                                }
                            }
                            else if (dataDoctorTable.Rows.Count == 0)
                            {
                                labelHaveToDoctor.Visible = true;
                            }

                            dataGridDoctor.ClearSelection();

                            if (dataGridDoctor.Rows.Count > 15)
                            {
                                checkboxDoctorSlowSearchStatus.Checked = true;
                                checkboxDoctorSlowSearchStatus.Enabled = false;
                                labelSlowSearch.Text = "Yavaş Arama (DİKKAT) (ZORUNLU SEÇİM)";
                            }
                            else
                            {
                                checkboxDoctorSlowSearchStatus.Checked = false;
                                checkboxDoctorSlowSearchStatus.Enabled = true;
                                labelSlowSearch.Text = "Yavaş Arama (DİKKAT)";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void doctorSearchPropertyDarkOpen()
        {
            btn_DoctorsSearch.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorsSearch.IdleFillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorsSearch.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorsSearch.onHoverState.FillColor = Color.FromArgb(167, 114, 242);

            btn_DoctorsSearch.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorsSearch.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorsSearch.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorsSearch.OnPressedState.FillColor = Color.FromArgb(97, 50, 209);

            btn_DoctorsSearch.IdleIconLeftImage = Properties.Resources.search25x25White;


            txtDoctorsSearch.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorsSearch.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorsSearch.BorderColorIdle = Color.Gray;

            txtDoctorsSearch.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorsSearch.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorsSearch.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);

            txtDoctorsSearch.FillColor = Color.FromArgb(38, 38, 38);
        }

        private void doctorSearchProperyDarkClose()
        {
            btn_DoctorsSearch.IdleBorderColor = Color.Black;
            btn_DoctorsSearch.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorsSearch.onHoverState.BorderColor = Color.Black;
            btn_DoctorsSearch.onHoverState.FillColor = Color.FromArgb(235, 235, 235);

            btn_DoctorsSearch.OnIdleState.BorderColor = Color.Black;
            btn_DoctorsSearch.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorsSearch.OnPressedState.BorderColor = Color.Black;
            btn_DoctorsSearch.OnPressedState.FillColor = Color.FromArgb(200, 200, 200);

            btn_DoctorsSearch.IdleIconLeftImage = Properties.Resources.search25x25Black;


            txtDoctorsSearch.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorsSearch.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorsSearch.BorderColorIdle = Color.Gray;

            txtDoctorsSearch.OnHoverState.FillColor = Color.White;
            txtDoctorsSearch.OnIdleState.FillColor = Color.White;
            txtDoctorsSearch.OnActiveState.FillColor = Color.White;

            txtDoctorsSearch.FillColor = Color.White;
        }

        private void panelDoctorPropertyDarkOpen()
        {
            panelDoctors.BackColor = Color.FromArgb(64, 64, 64);
            dataGridDoctor.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 86, 216);
            dataGridDoctor.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 86, 216);
            dataGridDoctor.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridDoctor.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridDoctor.DefaultCellStyle.BackColor = Color.FromArgb(167, 114, 242);
            dataGridDoctor.DefaultCellStyle.ForeColor = Color.FromArgb(249, 249, 249);
            dataGridDoctor.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            dataGridDoctor.DefaultCellStyle.SelectionForeColor = Color.FromArgb(249, 249, 249);

            dataGridDoctor.GridColor = Color.FromArgb(64, 64, 64);
        }
        private void panelDoctorPropertyDarkClose()
        {
            panelDoctors.BackColor = Color.FromArgb(249, 249, 249);

            dataGridDoctor.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridDoctor.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            dataGridDoctor.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridDoctor.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridDoctor.DefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200);
            dataGridDoctor.DefaultCellStyle.ForeColor = Color.Black;
            dataGridDoctor.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 249, 249);
            dataGridDoctor.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridDoctor.GridColor = Color.FromArgb(249, 249, 249);
            dataGridDoctor.BackgroundColor = Color.FromArgb(249, 249, 249);
        }

        private void buttonDoctorPropertyDarkModeOpen()
        {
            btn_DoctorAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_DoctorAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorAdd.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorAdd.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorAdd.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //doctorUpdate

            btn_DoctorUpdate.ForeColor = Color.FromArgb(230, 230, 230);

            btn_DoctorUpdate.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorUpdate.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorUpdate.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorUpdate.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorUpdate.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorUpdate.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorUpdate.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorUpdate.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //doctorDelete

            btn_DoctorDelete.ForeColor = Color.FromArgb(230, 230, 230);

            btn_DoctorDelete.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorDelete.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorDelete.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorDelete.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorDelete.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorDelete.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorDelete.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorDelete.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //MoneyAdd

            btn_MoneyUpdate.ForeColor = Color.FromArgb(230, 230, 230);

            btn_MoneyUpdate.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_MoneyUpdate.IdleFillColor = Color.FromArgb(26, 179, 148);

            btn_MoneyUpdate.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_MoneyUpdate.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_MoneyUpdate.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_MoneyUpdate.OnIdleState.FillColor = Color.FromArgb(26, 179, 148);

            btn_MoneyUpdate.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_MoneyUpdate.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //PasswordAdd

            btn_PasswordAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_PasswordAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_PasswordAdd.IdleFillColor = Color.FromArgb(240, 173, 78);

            btn_PasswordAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_PasswordAdd.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_PasswordAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_PasswordAdd.OnIdleState.FillColor = Color.FromArgb(240, 173, 78);

            btn_PasswordAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_PasswordAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //Approved

            labelDoctorApprovedWarning.ForeColor = Color.White;
            labelDoctorApprovedWarning.BackColor = Color.FromArgb(64, 64, 64);

            checkboxApprovedDoctorDelete.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxApprovedDoctorDelete.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxApprovedDoctorDelete.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxApprovedDoctorDelete.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxApprovedDoctorDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxApprovedDoctorDelete.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxApprovedDoctorDelete.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }
        private void buttonDoctorPropertyDarkModeClose()
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

            //doctorUpdate

            btn_DoctorUpdate.ForeColor = Color.FromArgb(64, 64, 64);

            btn_DoctorUpdate.IdleBorderColor = Color.Black;
            btn_DoctorUpdate.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorUpdate.onHoverState.BorderColor = Color.Black;
            btn_DoctorUpdate.onHoverState.FillColor = Color.DimGray;

            btn_DoctorUpdate.OnIdleState.BorderColor = Color.Black;
            btn_DoctorUpdate.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorUpdate.OnPressedState.BorderColor = Color.Black;
            btn_DoctorUpdate.OnPressedState.FillColor = Color.Gray;

            //doctorDelete

            btn_DoctorDelete.ForeColor = Color.FromArgb(64, 64, 64);

            btn_DoctorDelete.IdleBorderColor = Color.Black;
            btn_DoctorDelete.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorDelete.onHoverState.BorderColor = Color.Black;
            btn_DoctorDelete.onHoverState.FillColor = Color.DimGray;

            btn_DoctorDelete.OnIdleState.BorderColor = Color.Black;
            btn_DoctorDelete.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorDelete.OnPressedState.BorderColor = Color.Black;
            btn_DoctorDelete.OnPressedState.FillColor = Color.Gray;

            //MoneyAdd

            btn_MoneyUpdate.ForeColor = Color.FromArgb(64, 64, 64);

            btn_MoneyUpdate.IdleBorderColor = Color.Black;
            btn_MoneyUpdate.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_MoneyUpdate.onHoverState.BorderColor = Color.Black;
            btn_MoneyUpdate.onHoverState.FillColor = Color.DimGray;

            btn_MoneyUpdate.OnIdleState.BorderColor = Color.Black;
            btn_MoneyUpdate.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_MoneyUpdate.OnPressedState.BorderColor = Color.Black;
            btn_MoneyUpdate.OnPressedState.FillColor = Color.Gray;

            //PasswordAdd

            btn_PasswordAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_PasswordAdd.IdleBorderColor = Color.Black;
            btn_PasswordAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_PasswordAdd.onHoverState.BorderColor = Color.Black;
            btn_PasswordAdd.onHoverState.FillColor = Color.DimGray;

            btn_PasswordAdd.OnIdleState.BorderColor = Color.Black;
            btn_PasswordAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_PasswordAdd.OnPressedState.BorderColor = Color.Black;
            btn_PasswordAdd.OnPressedState.FillColor = Color.Gray;

            //Approved
            labelDoctorApprovedWarning.ForeColor = Color.Black;
            labelDoctorApprovedWarning.BackColor = Color.FromArgb(249, 249, 249);

            checkboxApprovedDoctorDelete.OnCheck.BorderColor = Color.Black;
            checkboxApprovedDoctorDelete.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxApprovedDoctorDelete.OnCheck.CheckmarkColor = Color.Black;

            checkboxApprovedDoctorDelete.OnHoverChecked.BorderColor = Color.Black;
            checkboxApprovedDoctorDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxApprovedDoctorDelete.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxApprovedDoctorDelete.OnUncheck.BorderColor = Color.Black;
        }
        private void panelDoctorRestPropertyDarkOpen()
        {
            panelDoctorRest.BackColor = Color.FromArgb(64, 64, 64);
            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 86, 216);
            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 86, 216);
            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridDoctorRest.DefaultCellStyle.BackColor = Color.FromArgb(167, 114, 242);
            dataGridDoctorRest.DefaultCellStyle.ForeColor = Color.FromArgb(249, 249, 249);
            dataGridDoctorRest.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            dataGridDoctorRest.DefaultCellStyle.SelectionForeColor = Color.FromArgb(249, 249, 249);

            dataGridDoctorRest.GridColor = Color.FromArgb(64, 64, 64);
        }
        private void panelDoctorRestPropertyDarkClose()
        {
            panelDoctorRest.BackColor = Color.FromArgb(249, 249, 249);

            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridDoctorRest.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridDoctorRest.DefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200);
            dataGridDoctorRest.DefaultCellStyle.ForeColor = Color.Black;
            dataGridDoctorRest.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 249, 249);
            dataGridDoctorRest.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridDoctorRest.GridColor = Color.FromArgb(249, 249, 249);
            dataGridDoctorRest.BackgroundColor = Color.FromArgb(249, 249, 249);
        }

        private void buttonDoctorRestDarkOpen()
        {
            btn_DoctorRestAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorRestAdd.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorRestAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorRestAdd.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorRestAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorRestAdd.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorRestAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorRestAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //doctorDelete

            btn_DoctorRestDelete.ForeColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestDelete.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorRestDelete.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorRestDelete.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorRestDelete.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorRestDelete.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorRestDelete.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorRestDelete.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorRestDelete.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

            //Approved

            labelDoctorRestApprovedWarning.ForeColor = Color.White;
            labelDoctorRestApprovedWarning.BackColor = Color.FromArgb(64, 64, 64);

            checkboxDoctorRestApprovedDelete.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxDoctorRestApprovedDelete.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxDoctorRestApprovedDelete.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxDoctorRestApprovedDelete.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxDoctorRestApprovedDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxDoctorRestApprovedDelete.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxDoctorRestApprovedDelete.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);

            //SlowSearch

            labelSlowSearch.ForeColor = Color.White;
            labelSlowSearch.BackColor = Color.FromArgb(64, 64, 64);

            checkboxDoctorSlowSearchStatus.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxDoctorSlowSearchStatus.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxDoctorSlowSearchStatus.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxDoctorSlowSearchStatus.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxDoctorSlowSearchStatus.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxDoctorSlowSearchStatus.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxDoctorSlowSearchStatus.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }
        private void buttonDoctorRestDarkClose()
        {
            btn_DoctorRestAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_DoctorRestAdd.IdleBorderColor = Color.Black;
            btn_DoctorRestAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestAdd.onHoverState.BorderColor = Color.Black;
            btn_DoctorRestAdd.onHoverState.FillColor = Color.DimGray;

            btn_DoctorRestAdd.OnIdleState.BorderColor = Color.Black;
            btn_DoctorRestAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestAdd.OnPressedState.BorderColor = Color.Black;
            btn_DoctorRestAdd.OnPressedState.FillColor = Color.Gray;

            //doctorDelete

            btn_DoctorRestDelete.ForeColor = Color.FromArgb(64, 64, 64);

            btn_DoctorRestDelete.IdleBorderColor = Color.Black;
            btn_DoctorRestDelete.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestDelete.onHoverState.BorderColor = Color.Black;
            btn_DoctorRestDelete.onHoverState.FillColor = Color.DimGray;

            btn_DoctorRestDelete.OnIdleState.BorderColor = Color.Black;
            btn_DoctorRestDelete.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestDelete.OnPressedState.BorderColor = Color.Black;
            btn_DoctorRestDelete.OnPressedState.FillColor = Color.Gray;


            //Approved
            labelDoctorRestApprovedWarning.ForeColor = Color.Black;
            labelDoctorRestApprovedWarning.BackColor = Color.FromArgb(249, 249, 249);

            checkboxDoctorRestApprovedDelete.OnCheck.BorderColor = Color.Black;
            checkboxDoctorRestApprovedDelete.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxDoctorRestApprovedDelete.OnCheck.CheckmarkColor = Color.Black;

            checkboxDoctorRestApprovedDelete.OnHoverChecked.BorderColor = Color.Black;
            checkboxDoctorRestApprovedDelete.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxDoctorRestApprovedDelete.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxDoctorRestApprovedDelete.OnUncheck.BorderColor = Color.Black;

            //SlowSearch

            labelSlowSearch.ForeColor = Color.Black;
            labelSlowSearch.BackColor = Color.FromArgb(249, 249, 249);

            checkboxDoctorSlowSearchStatus.OnCheck.BorderColor = Color.Black;
            checkboxDoctorSlowSearchStatus.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxDoctorSlowSearchStatus.OnCheck.CheckmarkColor = Color.Black;

            checkboxDoctorSlowSearchStatus.OnHoverChecked.BorderColor = Color.Black;
            checkboxDoctorSlowSearchStatus.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxDoctorSlowSearchStatus.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxDoctorSlowSearchStatus.OnUncheck.BorderColor = Color.Black;
        }
        private void doctorRestSearchPropertyDarkOpen()
        {
            btn_DoctorRestSearch.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorRestSearch.IdleFillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorRestSearch.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorRestSearch.onHoverState.FillColor = Color.FromArgb(167, 114, 242);

            btn_DoctorRestSearch.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorRestSearch.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorRestSearch.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorRestSearch.OnPressedState.FillColor = Color.FromArgb(97, 50, 209);

            btn_DoctorRestSearch.IdleIconLeftImage = Properties.Resources.search25x25White;


            txtDoctorRestSearch.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorRestSearch.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorRestSearch.BorderColorIdle = Color.Gray;

            txtDoctorRestSearch.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorRestSearch.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorRestSearch.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);

            txtDoctorRestSearch.FillColor = Color.FromArgb(38, 38, 38);


            labelSlowSearchDoctorRest.ForeColor = Color.White;
            labelSlowSearchDoctorRest.BackColor = Color.FromArgb(64, 64, 64);

            checkboxDoctorRestSlowSearch.OnCheck.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxDoctorRestSlowSearch.OnCheck.CheckBoxColor = Color.FromArgb(64, 64, 64);
            checkboxDoctorRestSlowSearch.OnCheck.CheckmarkColor = Color.FromArgb(167, 114, 242);

            checkboxDoctorRestSlowSearch.OnHoverChecked.BorderColor = Color.FromArgb(124, 86, 216);
            checkboxDoctorRestSlowSearch.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxDoctorRestSlowSearch.OnHoverUnchecked.BorderColor = Color.FromArgb(124, 86, 216);

            checkboxDoctorRestSlowSearch.OnUncheck.BorderColor = Color.FromArgb(124, 86, 216);
        }
        private void doctorRestSearchPropertyDarkClose()
        {
            btn_DoctorRestSearch.IdleBorderColor = Color.Black;
            btn_DoctorRestSearch.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestSearch.onHoverState.BorderColor = Color.Black;
            btn_DoctorRestSearch.onHoverState.FillColor = Color.FromArgb(235, 235, 235);

            btn_DoctorRestSearch.OnIdleState.BorderColor = Color.Black;
            btn_DoctorRestSearch.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorRestSearch.OnPressedState.BorderColor = Color.Black;
            btn_DoctorRestSearch.OnPressedState.FillColor = Color.FromArgb(200, 200, 200);

            btn_DoctorRestSearch.IdleIconLeftImage = Properties.Resources.search25x25Black;


            txtDoctorRestSearch.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorRestSearch.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorRestSearch.BorderColorIdle = Color.Gray;

            txtDoctorRestSearch.OnHoverState.FillColor = Color.White;
            txtDoctorRestSearch.OnIdleState.FillColor = Color.White;
            txtDoctorRestSearch.OnActiveState.FillColor = Color.White;

            txtDoctorRestSearch.FillColor = Color.White;


            labelSlowSearchDoctorRest.ForeColor = Color.Black;
            labelSlowSearchDoctorRest.BackColor = Color.FromArgb(249, 249, 249);

            checkboxDoctorRestSlowSearch.OnCheck.BorderColor = Color.Black;
            checkboxDoctorRestSlowSearch.OnCheck.CheckBoxColor = Color.FromArgb(230, 230, 230);
            checkboxDoctorRestSlowSearch.OnCheck.CheckmarkColor = Color.Black;

            checkboxDoctorRestSlowSearch.OnHoverChecked.BorderColor = Color.Black;
            checkboxDoctorRestSlowSearch.OnHoverChecked.CheckBoxColor = Color.DimGray;

            checkboxDoctorRestSlowSearch.OnHoverUnchecked.BorderColor = Color.Black;

            checkboxDoctorRestSlowSearch.OnUncheck.BorderColor = Color.Black;
        }

        private void txtDoctorsSearch_TextChanged(object sender, EventArgs e)
        {
            if (!checkboxDoctorSlowSearchStatus.Checked)
            {
                if (dataDoctorTable != null)
                {
                    string filterText = txtDoctorsSearch.Text.Trim().Replace("'", "''").ToLower();

                    Filtrele(filterText);
                }
            }

            if (string.IsNullOrEmpty(txtDoctorsSearch.Text))
            {
                if (dataDoctorTable != null)
                {
                    string filterText = txtDoctorsSearch.Text.Trim().Replace("'", "''").ToLower();

                    Filtrele(filterText);
                }
            }

        }
        private void btn_DoctorsSearch_Click(object sender, EventArgs e)
        {
            if (dataDoctorTable != null)
            {
                string filterText = txtDoctorsSearch.Text.Trim().Replace("'", "''").ToLower(); // Güvenli hale getirme

                Filtrele(filterText);
            }
        }

        private void Filtrele(string filterText)
        {
            try
            {
                DataTable filteredTable = dataDoctorTable.Clone();
                foreach (DataRow row in dataDoctorTable.Select($"([Name] + ' ' + [Surname]) LIKE '%{filterText}%' OR [Gender] LIKE '%{filterText}%' OR [Email] LIKE '%{filterText}%' OR [PhoneNumber] LIKE '%{filterText}%' OR [Money] LIKE '%{filterText}%' OR [Poliklinik Adı] LIKE '%{filterText}%'"))
                {
                    filteredTable.ImportRow(row);
                }

                dataGridDoctor.Rows.Clear();
                int sayac = 0;
                foreach (DataRow row in filteredTable.Rows)
                {
                    sayac++;
                    string ageText = AgeText(row["Date"]);

                    Image haveToPassword = PasswordStatus(row["HaveToPassword"]);


                    dataGridDoctor.Rows.Add(sayac, row["Id"], row["TcNo"], row["Name"], row["Surname"], row["Gender"], row["Date"], ageText, row["Email"], row["PhoneNumber"], row["Money"], row["Poliklinik Adı"], row["Username"], row["Password"], haveToPassword);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtreleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridDoctor.ClearSelection();
        }

        private string AgeText(object birthDateObject)
        {
            string ageText = "";
            if (DateTime.TryParse(birthDateObject?.ToString(), out DateTime birthDate))
            {
                int age = DateTime.Now.Year - birthDate.Year;
                if (birthDate > DateTime.Now.AddYears(-age))
                {
                    age--;
                }
                ageText = age.ToString();
            }
            else
            {
                ageText = "Belirlenmemiş";
            }
            return ageText;
        }

        private Image PasswordStatus(object passwordStatus)
        {
            bool status = Convert.ToBoolean(passwordStatus);
            if (status)
            {
                return Properties.Resources.greenSucces;
            }
            else
            {
                return Properties.Resources.redError;
            }
        }

        private async void btn_DoctorAdd_Click(object sender, EventArgs e)
        {
            DoctorAddForm doctorAddForm = new DoctorAddForm();
            doctorAddForm.Show();

            await DoctorSave(doctorAddForm);
        }
        private async Task DoctorSave(DoctorAddForm doctorAddForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = doctorAddForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadDoctorData();
                        doctorAddForm.AccessibleName = "abcdefgh";
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_DoctorDelete_Click(object sender, EventArgs e)
        {
            if (checkboxApprovedDoctorDelete.Checked)
            {
                DoctorDelete();
            }
            else
            {
                if (dataGridDoctor.SelectedRows.Count > 0 && dataGridDoctor.SelectedRows.Count > maxSelectionCount)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("warning");
                    dataChooseForm.Show();
                }
                else if (dataGridDoctor.SelectedRows.Count == 0)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("delete");
                    dataChooseForm.Show();
                }
                else
                {
                    ApproveDeleteForm approveDeleteForm = new ApproveDeleteForm(dataGridDoctor, "doctor");
                    approveDeleteForm.ShowDialog();
                    if (approveDeleteForm.AccessibleName == "Yes")
                    {
                        DoctorDelete();
                    }
                    else if (approveDeleteForm.AccessibleName == "No")
                    {
                        return;
                    }
                }
            }
        }

        private async void DoctorDelete()
        {
            int sayac = 0;
            if (dataGridDoctor.SelectedRows.Count > 0 && maxSelectionCount >= dataGridDoctor.SelectedRows.Count)
            {
                foreach (DataGridViewRow row in dataGridDoctor.SelectedRows)
                {
                    sayac++;
                    int selectedId = Convert.ToInt32(row.Cells["doctorID"].Value);

                    await DoctorDelete(selectedId);
                }

                if (this.AccessibleName == "DoctorDelete")
                {
                    string operation = "delete";
                    DoctorAddUpdateDeleteSuccessForm doctorAddUpdateDeleteSuccessForm = new DoctorAddUpdateDeleteSuccessForm(operation, sayac);
                    doctorAddUpdateDeleteSuccessForm.Show();
                }
                await LoadDoctorData();
            }
            else if (dataGridDoctor.SelectedRows.Count > 0 && dataGridDoctor.SelectedRows.Count > maxSelectionCount)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("warning");
            }
        }
        private async Task DoctorDelete(int id)
        {
            string query = "DELETE FROM Doctors WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            this.AccessibleName = "DoctorDelete";
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya silinemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelDoctors_Click(object sender, EventArgs e)
        {
            dataGridDoctor.ClearSelection();
        }

        private async void btn_DoctorUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridDoctor.SelectedRows.Count > 0 && dataGridDoctor.SelectedRows.Count == 1)
            {
                int selectedId = Convert.ToInt32(dataGridDoctor.SelectedRows[0].Cells["doctorID"].Value);
                string tcNo = dataGridDoctor.SelectedRows[0].Cells["doctorTcNo"].Value.ToString();
                string doctorName = dataGridDoctor.SelectedRows[0].Cells["doctorName"].Value.ToString();
                string doctorSurname = dataGridDoctor.SelectedRows[0].Cells["doctorSurname"].Value.ToString();
                string doctorGender = dataGridDoctor.SelectedRows[0].Cells["doctorGender"].Value.ToString();
                string doctorDate = dataGridDoctor.SelectedRows[0].Cells["doctorDate"].Value.ToString();
                string doctorMail = dataGridDoctor.SelectedRows[0].Cells["doctorMail"].Value.ToString();
                string doctorPhoneNumber = dataGridDoctor.SelectedRows[0].Cells["doctorPhoneNumber"].Value.ToString();
                string policlinicNamedoctor = dataGridDoctor.SelectedRows[0].Cells["policlinicNamedoctor"].Value.ToString();
                string username = dataGridDoctor.SelectedRows[0].Cells["doctorUsername"].Value.ToString();

                DoctorUpdateForm doctorGuncelleForm = new DoctorUpdateForm(tcNo, selectedId, doctorName, doctorSurname, doctorGender, doctorDate, doctorMail, doctorPhoneNumber, policlinicNamedoctor, username);
                doctorGuncelleForm.Show();

                await DoctorVeriGuncelle(doctorGuncelleForm);
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("many");
                dataChooseForm.Show();
            }
        }

        private async Task DoctorVeriGuncelle(DoctorUpdateForm doctorGuncelleForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = doctorGuncelleForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadDoctorData();
                        doctorGuncelleForm.AccessibleName = "abcdefgh";
                        doctorGuncelleForm.Close();


                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_MoneyUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridDoctor.SelectedRows.Count > 0 && dataGridDoctor.SelectedRows.Count == 1)
            {
                int selectedId = Convert.ToInt32(dataGridDoctor.SelectedRows[0].Cells["doctorID"].Value);
                string selectedUcret = dataGridDoctor.SelectedRows[0].Cells["doctorMoney"].Value.ToString();
                DoctorPaymentAddUpdateForm doctorPaymentAddForm = new DoctorPaymentAddUpdateForm(selectedId, selectedUcret);
                doctorPaymentAddForm.Show();

                await DoctorPaymentSave(doctorPaymentAddForm);
            }
            else if (dataGridDoctor.SelectedRows.Count > 1)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("many");
                dataChooseForm.Show();
            }
            else if (dataGridDoctor.SelectedRows.Count == 0)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("update");
                dataChooseForm.Show();
            }
        }
        private async Task DoctorPaymentSave(DoctorPaymentAddUpdateForm doctorPaymentAddForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(500);
                    string status = doctorPaymentAddForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadDoctorData();
                        doctorPaymentAddForm.AccessibleName = "abcdefgh";
                        doctorPaymentAddForm.Close();
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_PasswordAdd_Click(object sender, EventArgs e)
        {
            int randomPasswordLength = RandomPassword();
            string password = GeneratePassword(randomPasswordLength);

            if (dataGridDoctor.SelectedRows.Count > 0 && dataGridDoctor.SelectedRows.Count == 1)
            {
                int selectedId = Convert.ToInt32(dataGridDoctor.SelectedRows[0].Cells["doctorID"].Value);
                string selectedName = dataGridDoctor.SelectedRows[0].Cells["doctorName"].Value.ToString();
                string selectedSurname = dataGridDoctor.SelectedRows[0].Cells["doctorSurname"].Value.ToString();
                string selectedMail = dataGridDoctor.SelectedRows[0].Cells["doctorMail"].Value.ToString();
                string selectedUsername = dataGridDoctor.SelectedRows[0].Cells["doctorUsername"].Value.ToString();
                string selectedPassword = password;

                string nameSurname = $"{selectedName} {selectedSurname}";

                AdminDoctorNewPasswordConfirmForm doctorNewPasswordConfirmForm = new AdminDoctorNewPasswordConfirmForm(selectedMail, nameSurname);
                doctorNewPasswordConfirmForm.ShowDialog();

                if (doctorNewPasswordConfirmForm.AccessibleName == "Success")
                {
                    await NewPasswordSave(selectedId, password);

                    await LoadDoctorData();

                    await SendGmailHtmlEmail(selectedMail, nameSurname, selectedUsername, selectedPassword);
                }
                else if (doctorNewPasswordConfirmForm.AccessibleName == "Negative")
                {
                    return;
                }
            }
            else if (dataGridDoctor.SelectedRows.Count > 1)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("manypassword");
                dataChooseForm.Show();
            }
            else if (dataGridDoctor.SelectedRows.Count == 0)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("password");
                dataChooseForm.Show();
            }
        }

        private int RandomPassword()
        {
            Random random = new Random();
            int code = random.Next(6, 16);
            return code;
        }
        private string GeneratePassword(int length)
        {
            string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            string specialChars = "+.";

            Random random = new Random();
            string password = "";

            password += upperChars[random.Next(upperChars.Length)];
            password += lowerChars[random.Next(lowerChars.Length)];
            password += digits[random.Next(digits.Length)];

            while (password.Length < length - 1)
            {
                string allChars = upperChars + lowerChars + digits;
                password += allChars[random.Next(allChars.Length)];
            }

            char specialChar = specialChars[random.Next(specialChars.Length)];
            int specialCharPosition = random.Next(2) == 0
                ? password.Length / 2 // ortya koy
                : password.Length; //sona koy

            password = password.Insert(specialCharPosition, specialChar.ToString());


            password = Karistir(password, random);
            return password;
        }

        static string Karistir(string input, Random random)
        {
            char[] array = input.ToCharArray();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
            return new string(array);
        }

        public async Task SendGmailHtmlEmail(string recipientEmail, string _name, string username, string password)
        {
            try
            {
                var message = new MimeMessage();
                var birim = "HBYS | Personel Destek";
                message.Subject = "Yeni Şifre Bildirimi";
                message.From.Add(new MailboxAddress(birim, "hastanebilgiyonetimsistemi@gmail.com"));
                message.To.Add(new MailboxAddress(_name, recipientEmail));

                string htmlContent = $@"
    <!DOCTYPE html>
<html lang=""tr"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Kullanıcı Adı ve Şifre</title>
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
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: rgb(124, 86, 216);
            color: white;
            padding: 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 20px;
            font-size: 16px;
            line-height: 1.5;
            color: #333333;
            text-align: center;
        }}
        .content h2 {{
            color: rgb(124, 86, 216);
            margin-bottom: 10px;
        }}
        .content p {{
            margin: 10px 0;
        }}
        .credentials {{
            font-size: 18px;
            background-color: #f9f9f9;
            border: 1px solid #dddddd;
            padding: 10px;
            border-radius: 5px;
            display: inline-block;
            margin: 20px 0;
        }}
        .footer {{
            text-align: center;
            padding: 20px;
            font-size: 12px;
            color: #777777;
            background-color: #f9f9f9;
        }}
        .footer a {{
            color: rgb(124, 86, 216);
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Hoş Geldiniz</h1>
        </div>
        <div class=""content"">
            <h2>Merhaba, {_name}</h2>
            <p>Aşağıda hesap bilgileriniz bulunmaktadır:</p>
            <div class=""credentials"">
                <strong>Kullanıcı Adı:</strong> <span>[{username}]</span><br>
                <strong>Şifre:</strong> <span>[{password}]</span>
            </div>
            <p>Eğer bu bilgileri siz talep etmediyseniz, lütfen bizimle iletişime geçin.</p>
        </div>
        <div class=""footer"">
            <p>&copy; {DateTime.Now.Year} Tüm hakları saklıdır.</p>
            <p><a href=""#"">Gizlilik Politikası</a> | <a href=""#"">Şartlar ve Koşullar</a></p>
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

        private async Task NewPasswordSave(int id, string password)
        {
            string query = "UPDATE Doctors SET Password =@Password, PasswordConfirm =@PasswordConfirm, HaveToPassword =@HaveToPassword WHERE Id =@Id;";
            string operation = "passSend";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@PasswordConfirm", password);
                        command.Parameters.AddWithValue("@HaveToPassword", true);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            DoctorAddUpdateDeleteSuccessForm doctorAddUpdateDeleteSuccessForm = new DoctorAddUpdateDeleteSuccessForm(operation, 1);
                            doctorAddUpdateDeleteSuccessForm.Show();
                            this.AccessibleName = "Success";
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

        private async void doctorRestSettings_Click(object sender, EventArgs e)
        {
            panelDoctorRest.Visible = true;
            panelPoliclinic.Visible = false;
            panelDoctors.Visible = false;
            panelAppointmentTime.Visible = false;
            panelAdmins.Visible = false;
            panelDashboard.Visible = false;
            panelDarkModeSetting.Visible = false;
            panelMyInformation.Visible = false;

            labelPanelName.Text = "Doktor İzin Ayarları";
            labelPanelName.Location = new Point(768, -3);

            await Task.Delay(1000);
            await LoadDoctorRestData();
        }

        private async Task LoadDoctorRestData()
        {
            labelHaveToDoctorRest.Visible = false;
            string query = @"SELECT U.Id, U.Name, U.Surname, D.RestDate,D.Status FROM Doctors U INNER JOIN DoctorRestDate D ON U.Id = D.DoctorId;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataDoctorRestTable = new DataTable();
                            dataDoctorRestTable.Load(reader);

                            dataGridDoctorRest.Rows.Clear();
                            if (dataDoctorRestTable.Rows.Count > 0)
                            {
                                int sayac = 0;
                                foreach (DataRow row in dataDoctorRestTable.Rows)
                                {
                                    sayac++;
                                    string restDate = "";
                                    if (DateTime.TryParse(row["RestDate"]?.ToString(), out DateTime birthDate))
                                    {

                                        restDate = DateTime.Parse(row["RestDate"].ToString()).ToShortDateString().ToString();
                                    }
                                    else
                                    {
                                        restDate = "Belirlenmemiş";
                                    }
                                    Image status = null;
                                    if (row["Status"].ToString() == "Bitmiş")
                                    {
                                        status = Properties.Resources.clockStop;
                                    }
                                    else if (row["Status"].ToString() == "Devam Ediyor")
                                    {
                                        status = isDarkMode ? Properties.Resources.clockDark : Properties.Resources.clockWhite;
                                    }

                                    dataGridDoctorRest.Rows.Add(sayac, row["ID"], row["Name"], row["Surname"], restDate, status);
                                }
                            }
                            else
                            {
                                labelHaveToDoctorRest.Visible = true;
                            }

                            dataGridDoctorRest.ClearSelection();
                        }
                    }
                    if (dataGridDoctor.Rows.Count > 20)
                    {
                        checkboxDoctorRestSlowSearch.Checked = true;
                        checkboxDoctorRestSlowSearch.Enabled = false;
                        labelSlowSearchDoctorRest.Text = "Yavaş Arama (DİKKAT) (ZORUNLU SEÇİM)";
                    }
                    else
                    {
                        checkboxDoctorRestSlowSearch.Checked = false;
                        checkboxDoctorRestSlowSearch.Enabled = true;
                        labelSlowSearchDoctorRest.Text = "Yavaş Arama (DİKKAT)";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void txtDoctorRestDateSearch_TextChange(object sender, EventArgs e)
        {
            if (checkboxDoctorRestSlowSearch.Checked)
            {
                if (string.IsNullOrEmpty(txtDoctorRestSearch.Text))
                {
                    DoctorRestSearch();
                }
                else
                {
                    return;
                }
            }
            else
            {
                DoctorRestSearch();
            }

            if (string.IsNullOrEmpty(txtDoctorRestSearch.Text))
            {
                DoctorRestSearch();
            }
        }

        private void DoctorRestSearch()
        {
            if (dataDoctorRestTable != null)
            {
                string filterText = txtDoctorRestSearch.Text.Trim().Replace("'", "''").ToLower();

                try
                {
                    dataDoctorRestTable.DefaultView.RowFilter = $@"([Name] + ' ' + [Surname]) LIKE '%{filterText}%'OR CONVERT([RestDate], 'System.String') LIKE '%{filterText}%'";
                    dataGridDoctorRest.Rows.Clear();
                    int sayac = 0;
                    foreach (DataRowView rowView in dataDoctorRestTable.DefaultView)
                    {
                        sayac++;
                        DataRow row = rowView.Row;
                        string restDate = "";

                        if (DateTime.TryParse(row["RestDate"]?.ToString(), out DateTime birthDate))
                        {
                            restDate = birthDate.ToString("dd.MM.yyyy");
                        }
                        else
                        {
                            restDate = "Belirlenmemiş";
                        }

                        dataGridDoctorRest.Rows.Add(sayac, row["Id"], row["Name"], row["Surname"], restDate);
                        dataGridDoctorRest.ClearSelection();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Filtreleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridDoctorRest.DataSource = dataDoctorRestTable;
                }
            }
        }


        private async void btn_DoctorRestAdd_Click(object sender, EventArgs e)
        {
            int selectedId;
            string operation = "";
            if (dataGridDoctorRest.SelectedRows.Count > 0 && dataGridDoctorRest.SelectedRows.Count == 1)
            {
                selectedId = Convert.ToInt32(dataGridDoctorRest.SelectedRows[0].Cells["doctorRestId"].Value);
                operation = "update";
            }
            else
            {
                selectedId = 124124124;
                operation = "add";
            }

            using (DoctorRestDateAddOrUpdateForms doctorRestDateAddOrUpdate = new DoctorRestDateAddOrUpdateForms(selectedId, operation))
            {
                doctorRestDateAddOrUpdate.Show();
                await DoctorRestSave(doctorRestDateAddOrUpdate);
            }
        }
        private async Task DoctorRestSave(DoctorRestDateAddOrUpdateForms doctorRestDateAddOrUpdate)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = doctorRestDateAddOrUpdate.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadDoctorRestData();
                        doctorRestDateAddOrUpdate.AccessibleName = "abcdefgh";
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoctorRestNotChoose(object sender, EventArgs e)
        {
            dataGridDoctorRest.ClearSelection();
        }

        private async void btn_DoctorRestDelete_Click(object sender, EventArgs e)
        {
            if (dataGridDoctorRest.SelectedRows.Count > 0 && dataGridDoctorRest.SelectedRows.Count <= 5)
            {
                List<(int UserId, DateTime RestDate)> deleteRestDate = new List<(int, DateTime)>();
                int say = 0;
                foreach (DataGridViewRow row in dataGridDoctorRest.SelectedRows)
                {
                    int selectedId = Convert.ToInt32(dataGridDoctorRest.SelectedRows[say].Cells["doctorRestId"].Value);
                    DateTime selecteRestDate = DateTime.Parse(dataGridDoctorRest.SelectedRows[say].Cells["doctorRestDate"].Value.ToString());

                    deleteRestDate.Add((selectedId, selecteRestDate));
                    say++;
                }
                if (checkboxDoctorRestApprovedDelete.Checked)
                {
                    await DeleteDoctorRestDate(deleteRestDate);
                }
                else
                {
                    string type = "doctorRest";

                    using (ApproveDeleteForm approveDeleteForm = new ApproveDeleteForm(dataGridDoctorRest, type))
                    {
                        approveDeleteForm.ShowDialog();

                        if (approveDeleteForm.AccessibleName == "Yes")
                        {
                            await DeleteDoctorRestDate(deleteRestDate);
                        }
                        else if (approveDeleteForm.AccessibleName == "No")
                        {

                        }
                    }
                }

            }
            else if (dataGridDoctorRest.SelectedRows.Count == 0)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("delete");
                dataChooseForm.Show();
            }
            else if (dataGridDoctorRest.SelectedRows.Count > 5)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("warning");
                dataChooseForm.Show();
            }
        }

        private async Task DeleteDoctorRestDate(List<(int Id, DateTime RestDate)> deleteRestDate)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                transaction = connection.BeginTransaction();

                int sayac = 0;

                foreach (var deleteRest in deleteRestDate)
                {
                    string deleteQuery = "DELETE FROM DoctorRestDate WHERE DoctorId = @DoctorId AND RestDate = @RestDate";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                    {
                        deleteCommand.Parameters.AddWithValue("@DoctorId", deleteRest.Id);
                        deleteCommand.Parameters.AddWithValue("@RestDate", deleteRest.RestDate);
                        await deleteCommand.ExecuteNonQueryAsync();
                    }
                    sayac++;
                }

                transaction.Commit();

                DoctorRestUpdateForm doctorRestUpdateForm = new DoctorRestUpdateForm("delete", sayac);
                doctorRestUpdateForm.Show();

                await LoadDoctorRestData();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        private void btn_DoctorRestSearch_Click(object sender, EventArgs e)
        {
            DoctorRestSearch();
        }

        private async void appointmentDateSettings_Click(object sender, EventArgs e)
        {
            panelAppointmentTime.Visible = true;
            panelPoliclinic.Visible = false;
            panelDoctors.Visible = false;
            panelDoctorRest.Visible = false;
            panelAdmins.Visible = false;
            panelDashboard.Visible = false;
            panelDarkModeSetting.Visible = false;
            panelMyInformation.Visible = false;

            labelPanelName.Text = "Genel Randevu Saatleri";
            labelPanelName.Location = new Point(695, -3);

            await Task.Delay(1000);
            await LoadAppointmentTimeData();
        }

        private async Task LoadAppointmentTimeData()
        {
            labelHaveToAppointmentTime.Visible = false;
            string query = "SELECT Id, AppointmentTime FROM AppointmentTime";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataAppointmentTimeTable = new DataTable();
                            dataAppointmentTimeTable.Load(reader);

                            dataGridAppointmentTime.Rows.Clear();
                            if (dataAppointmentTimeTable.Rows.Count > 0)
                            {
                                int sayac = 0;
                                foreach (DataRow row in dataAppointmentTimeTable.Rows)
                                {
                                    sayac++;
                                    if (DateTime.TryParse(row["AppointmentTime"].ToString(), out DateTime appointmentTime))
                                    {
                                        string formattedTime = appointmentTime.ToString("HH:mm");

                                        dataGridAppointmentTime.Rows.Add(sayac, row["Id"], formattedTime);
                                    }
                                    else
                                    {
                                        dataGridAppointmentTime.Rows.Add(sayac, row["Id"], "Geçersiz Zaman");
                                    }
                                }
                            }
                            else
                            {
                                labelHaveToAppointmentTime.Visible = true;
                            }

                            dataGridAppointmentTime.ClearSelection();
                        }
                    }

                    if (dataGridAppointmentTime.Rows.Count > 20)
                    {
                        checkboxSlowSearchAppointmentTime.Checked = true;
                        checkboxSlowSearchAppointmentTime.Enabled = false;
                        labelSlowSearchAppointmentTime.Text = "Yavaş Arama (DİKKAT) (ZORUNLU SEÇİM)";
                    }
                    else
                    {
                        checkboxSlowSearchAppointmentTime.Checked = false;
                        checkboxSlowSearchAppointmentTime.Enabled = true;
                        labelSlowSearchAppointmentTime.Text = "Yavaş Arama (DİKKAT)";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void txtAppointmentTimeSearch_TextChange(object sender, EventArgs e)
        {
            if (checkboxSlowSearchAppointmentTime.Checked)
            {
                if (string.IsNullOrEmpty(txtAppointmentTimeSearch.Text))
                {
                    AppointmentTimeSearch();
                }
                else
                {
                    return;
                }
            }
            else
            {
                AppointmentTimeSearch();
            }

            if (string.IsNullOrEmpty(txtAppointmentTimeSearch.Text))
            {
                AppointmentTimeSearch();
            }
        }

        private void btn_SearchAppointmentTime_Click(object sender, EventArgs e)
        {
            AppointmentTimeSearch();
        }

        private void AppointmentTimeSearch()
        {
            if (dataAppointmentTimeTable != null)
            {
                string filterText = txtAppointmentTimeSearch.Text.Trim().Replace("'", "''");
                if (string.IsNullOrEmpty(filterText))
                {
                    dataAppointmentTimeTable.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    dataAppointmentTimeTable.DefaultView.RowFilter = $"CONVERT([AppointmentTime], 'System.String') LIKE '%{filterText}%'";
                }

                dataGridAppointmentTime.Rows.Clear();
                int sayac = 0;
                foreach (DataRow row in dataAppointmentTimeTable.DefaultView.ToTable().Rows)
                {
                    sayac++;
                    string appointmentTime = DateTime.TryParse(row["AppointmentTime"].ToString(), out DateTime time) ? time.ToString("HH:mm") : "Geçersiz Saat";

                    dataGridAppointmentTime.Rows.Add(sayac, row["Id"], appointmentTime);
                }

                dataGridAppointmentTime.ClearSelection();
            }

        }

        private async void btn_AppointmentTimeAdd_Click(object sender, EventArgs e)
        {
            using (AppointmentTimeAddForm appointmentTimeAdd = new AppointmentTimeAddForm())
            {
                appointmentTimeAdd.Show();
                await AppointmentTime(appointmentTimeAdd);
            }
        }
        private async Task AppointmentTime(AppointmentTimeAddForm appointmentTimeAdd)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = appointmentTimeAdd.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadAppointmentTimeData();
                        appointmentTimeAdd.AccessibleName = "abcdefgh";
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_AppointmentTimeUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridAppointmentTime.SelectedRows.Count > 0 && dataGridAppointmentTime.SelectedRows.Count == 1)
            {
                int selectedId = Convert.ToInt32(dataGridAppointmentTime.SelectedRows[0].Cells["appointmentTimeId"].Value);
                string selectedTime = dataGridAppointmentTime.SelectedRows[0].Cells["appointmentTimeRest"].Value.ToString();

                AppointmentUpdateForm appointmentUpdateForm = new AppointmentUpdateForm(selectedId, selectedTime);
                appointmentUpdateForm.Show();

                await AppointmentTimeUpdate(appointmentUpdateForm);
                await LoadAppointmentTimeData();
            }
            else if (dataGridAppointmentTime.SelectedRows.Count == 0)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("update");
                dataChooseForm.Show();
                return;
            }
            else if (dataGridAppointmentTime.SelectedRows.Count > 1)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("many");
                dataChooseForm.Show();
                return;
            }
        }
        private async Task AppointmentTimeUpdate(AppointmentUpdateForm appointmentUpdateForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = appointmentUpdateForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadAppointmentTimeData();
                        appointmentUpdateForm.AccessibleName = "abcdefgh";
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AppointmentTimeDelete_Click(object sender, EventArgs e)
        {

            if (checkboxAppointmentApprovedDelete.Checked)
            {
                AppointmentTimeDelete();
            }
            else
            {
                if (dataGridAppointmentTime.SelectedRows.Count > 0 && dataGridAppointmentTime.SelectedRows.Count > maxSelectionCount)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("warning");
                    dataChooseForm.Show();
                }
                else if (dataGridAppointmentTime.SelectedRows.Count == 0)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("delete");
                    dataChooseForm.Show();
                }
                else
                {
                    ApproveDeleteForm approveDeleteForm = new ApproveDeleteForm(dataGridAppointmentTime, "appointmentTime");
                    approveDeleteForm.ShowDialog();
                    if (approveDeleteForm.AccessibleName == "Yes")
                    {
                        AppointmentTimeDelete();
                    }
                    else if (approveDeleteForm.AccessibleName == "No")
                    {
                        return;
                    }
                }
            }
        }
        private async void AppointmentTimeDelete()
        {
            int sayac = 0;
            if (dataGridAppointmentTime.SelectedRows.Count > 0 && maxSelectionCount >= dataGridAppointmentTime.SelectedRows.Count)
            {
                foreach (DataGridViewRow row in dataGridAppointmentTime.SelectedRows)
                {
                    sayac++;
                    int selectedId = Convert.ToInt32(row.Cells["appointmentTimeId"].Value);

                    await AppointmentTimeDelete(selectedId);
                }

                if (this.AccessibleName == "delete")
                {
                    string operation = "delete";
                    AppointmentTimeAddUpdateDeleteForm appointmentTimeAddUpdateDeleteForm = new AppointmentTimeAddUpdateDeleteForm(operation, sayac);
                    appointmentTimeAddUpdateDeleteForm.Show();
                }
                await LoadAppointmentTimeData();
            }
            else if (dataGridAppointmentTime.SelectedRows.Count > 0 && dataGridAppointmentTime.SelectedRows.Count > maxSelectionCount)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("warning");
                dataChooseForm.Show();
            }
        }
        private async Task AppointmentTimeDelete(int id)
        {
            string query = "DELETE FROM AppointmentTime WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            this.AccessibleName = "delete";
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya silinemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void adminList_Click(object sender, EventArgs e)
        {
            panelAppointmentTime.Visible = false;
            panelPoliclinic.Visible = false;
            panelDoctors.Visible = false;
            panelDoctorRest.Visible = false;
            panelAdmins.Visible = true;
            panelDashboard.Visible = false;
            panelDarkModeSetting.Visible = false;
            panelMyInformation.Visible = false;

            labelPanelName.Text = "Yöneticiler";
            labelPanelName.Location = new Point(922, -3);

            await Task.Delay(1000);
            await LoadAdminListDate();
        }

        private void adminAdd_Click(object sender, EventArgs e)
        {
            adminList_Click(sender, e);
        }

        private void adminUpdate_Click(object sender, EventArgs e)
        {
            adminList_Click(sender, e);
        }

        private void adminDelete_Click(object sender, EventArgs e)
        {
            adminList_Click(sender, e);
        }

        private async Task LoadAdminListDate()
        {
            labelHaveToAdmin.Visible = false;
            string query = @"SELECT HaveToPassword, TcNo, Id,Name,Surname,Gender,Date,Email,PhoneNumber,Password,Username FROM Admins";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataAdminTable = new DataTable();
                            dataAdminTable.Load(reader);

                            dataGridAdmin.Rows.Clear();
                            if (dataAdminTable.Rows.Count > 0)
                            {
                                int sayac = 0;
                                foreach (DataRow row in dataAdminTable.Rows)
                                {
                                    sayac++;
                                    string ageText = "";
                                    if (DateTime.TryParse(row["Date"]?.ToString(), out DateTime birthDate))
                                    {
                                        int age = DateTime.Now.Year - birthDate.Year;

                                        if (birthDate > DateTime.Now.AddYears(-age)) // şuan ki tarihten şuan ki yaşını çıkarır ve çıakn tarih doğum tarihinden küçükse yaşından 1 azaltır. 
                                        {
                                            age--;
                                        }

                                        ageText = age.ToString();
                                    }
                                    else
                                    {
                                        ageText = "Belirlenmemiş";
                                    }

                                    Image haveToPassword;
                                    bool status = Convert.ToBoolean(row["HaveToPassword"]);
                                    if (status)
                                    {
                                        haveToPassword = Properties.Resources.greenSucces;
                                    }
                                    else
                                    {
                                        haveToPassword = Properties.Resources.redError;
                                    }

                                    dataGridAdmin.Rows.Add(sayac, row["ID"], row["TcNo"], row["Name"], row["Surname"], row["Gender"], DateTime.Parse(row["Date"].ToString()).ToShortDateString(), ageText, row["Email"], row["PhoneNumber"], row["Username"], row["Password"], haveToPassword);
                                }
                            }
                            else
                            {
                                labelHaveToAdmin.Visible = true;
                            }

                            dataGridAdmin.ClearSelection();
                            if (dataGridAdmin.Rows.Count > 15)
                            {
                                checkboxAdminSlowSearch.Checked = true;
                                checkboxAdminSlowSearch.Enabled = false;
                                labelSlowAdminSearch.Text = "Yavaş Arama (DİKKAT) (ZORUNLU SEÇİM)";
                            }
                            else
                            {
                                checkboxAdminSlowSearch.Checked = false;
                                checkboxAdminSlowSearch.Enabled = true;
                                labelSlowAdminSearch.Text = "Yavaş Arama (DİKKAT)";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private async void btn_AdminAdd_Click(object sender, EventArgs e)
        {
            AdminAddForm adminAddForm = new AdminAddForm();
            adminAddForm.Show();

            await AdminSave(adminAddForm);
        }
        private async Task AdminSave(AdminAddForm adminAddForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = adminAddForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadAdminListDate();
                        adminAddForm.AccessibleName = "abcdefgh";
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_AdminUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridAdmin.SelectedRows.Count > 0 && dataGridAdmin.SelectedRows.Count == 1)
            {
                int selectedId = Convert.ToInt32(dataGridAdmin.SelectedRows[0].Cells["adminID"].Value);
                string tcNo = dataGridAdmin.SelectedRows[0].Cells["adminTcNo"].Value.ToString();
                string adminName = dataGridAdmin.SelectedRows[0].Cells["adminName"].Value.ToString();
                string adminSurname = dataGridAdmin.SelectedRows[0].Cells["adminSurname"].Value.ToString();
                string adminGender = dataGridAdmin.SelectedRows[0].Cells["adminGender"].Value.ToString();
                string adminDate = dataGridAdmin.SelectedRows[0].Cells["adminDate"].Value.ToString();
                string adminMail = dataGridAdmin.SelectedRows[0].Cells["adminMail"].Value.ToString();
                string adminPhoneNumber = dataGridAdmin.SelectedRows[0].Cells["adminPhoneNumber"].Value.ToString();
                string username = dataGridAdmin.SelectedRows[0].Cells["adminUsername"].Value.ToString();

                AdminUpdateForm adminUpdateForm = new AdminUpdateForm(tcNo, selectedId, adminName, adminSurname, adminGender, adminDate, adminMail, adminPhoneNumber, username);
                adminUpdateForm.Show();

                await AdminUpdate(adminUpdateForm);
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("many");
                dataChooseForm.Show();
            }
        }

        private async Task AdminUpdate(AdminUpdateForm adminUpdateForm)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = adminUpdateForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadAdminListDate();
                        adminUpdateForm.AccessibleName = "abcdefgh";
                        adminUpdateForm.Close();


                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelAdmins_Click(object sender, EventArgs e)
        {
            dataGridAdmin.ClearSelection();
        }

        private void btn_AdminDelete_Click(object sender, EventArgs e)
        {
            if (checkboxAdminApprovedDelete.Checked)
            {
                AdminDelete();
            }
            else
            {
                if (dataGridAdmin.SelectedRows.Count > 0 && dataGridAdmin.SelectedRows.Count > maxSelectionCount)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("warning");
                    dataChooseForm.Show();
                }
                else if (dataGridAdmin.SelectedRows.Count == 0)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("delete");
                    dataChooseForm.Show();
                }
                else
                {
                    ApproveDeleteForm approveDeleteForm = new ApproveDeleteForm(dataGridAdmin, "admin");
                    approveDeleteForm.ShowDialog();
                    if (approveDeleteForm.AccessibleName == "Yes")
                    {
                        AdminDelete();
                    }
                    else if (approveDeleteForm.AccessibleName == "No")
                    {
                        return;
                    }
                }
            }
        }

        private async void AdminDelete()
        {
            int sayac = 0;
            if (dataGridAdmin.SelectedRows.Count > 0 && maxSelectionCount >= dataGridAdmin.SelectedRows.Count)
            {
                foreach (DataGridViewRow row in dataGridAdmin.SelectedRows)
                {
                    sayac++;
                    int selectedId = Convert.ToInt32(row.Cells["adminId"].Value);

                    await AdminDelete(selectedId);
                }

                if (this.AccessibleName == "AdminDelete")
                {
                    string operation = "delete";
                    AdminAddUpdateDeleteSuccessForm adminAddUpdateDeleteSuccessForm = new AdminAddUpdateDeleteSuccessForm(operation, sayac);
                    adminAddUpdateDeleteSuccessForm.Show();
                }
                await LoadAdminListDate();
            }
            else if (dataGridAdmin.SelectedRows.Count > 0 && dataGridAdmin.SelectedRows.Count > maxSelectionCount)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("warning");
            }
        }
        private async Task AdminDelete(int id)
        {
            string query = "DELETE FROM Admins WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            this.AccessibleName = "AdminDelete";
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı veya silinemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_AdminLoginInformation_Click(object sender, EventArgs e)
        {
            int randomPasswordLength = RandomPassword();
            string password = GeneratePassword(randomPasswordLength);

            if (dataGridAdmin.SelectedRows.Count > 0 && dataGridAdmin.SelectedRows.Count == 1)
            {
                int selectedId = Convert.ToInt32(dataGridAdmin.SelectedRows[0].Cells["adminId"].Value);
                string selectedName = dataGridAdmin.SelectedRows[0].Cells["adminName"].Value.ToString();
                string selectedSurname = dataGridAdmin.SelectedRows[0].Cells["adminSurname"].Value.ToString();
                string selectedMail = dataGridAdmin.SelectedRows[0].Cells["adminMail"].Value.ToString();
                string selectedUsername = dataGridAdmin.SelectedRows[0].Cells["adminUsername"].Value.ToString();
                string selectedPassword = password;

                string nameSurname = $"{selectedName} {selectedSurname}";

                AdminDoctorNewPasswordConfirmForm adminDoctorNewPasswordConfirmForm = new AdminDoctorNewPasswordConfirmForm(selectedMail, nameSurname);
                adminDoctorNewPasswordConfirmForm.ShowDialog();

                if (adminDoctorNewPasswordConfirmForm.AccessibleName == "Success")
                {
                    await NewAdminPasswordSave(selectedId, password);

                    await LoadAdminListDate();

                    await SendGmailHtmlEmail(selectedMail, nameSurname, selectedUsername, selectedPassword);
                }
                else if (adminDoctorNewPasswordConfirmForm.AccessibleName == "Negative")
                {
                    return;
                }
            }
            else if (dataGridAdmin.SelectedRows.Count > 1)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("manypassword");
                dataChooseForm.Show();
            }
            else if (dataGridAdmin.SelectedRows.Count == 0)
            {
                DataChooseForm dataChooseForm = new DataChooseForm("adminPassword");
                dataChooseForm.Show();
            }
        }
        private async Task NewAdminPasswordSave(int id, string password)
        {
            string query = "UPDATE Admins SET Password =@Password, PasswordConfirm =@PasswordConfirm, HaveToPassword =@HaveToPassword WHERE Id =@Id;";
            string operation = "passSend";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@PasswordConfirm", password);
                        command.Parameters.AddWithValue("@HaveToPassword", true);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            AdminAddUpdateDeleteSuccessForm adminAddUpdateDeleteSuccessForm = new AdminAddUpdateDeleteSuccessForm(operation, 1);
                            adminAddUpdateDeleteSuccessForm.Show();
                            this.AccessibleName = "Success";
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
        private void txtAdminSearch_TextChanged(object sender, EventArgs e)
        {
            if (checkboxAdminSlowSearch.Checked)
            {
                if (string.IsNullOrEmpty(txtAdminSearch.Text))
                {
                    if (dataAdminTable != null)
                    {
                        string filterText = txtAdminSearch.Text.Trim().Replace("'", "''").ToLower();

                        AdminFilter(filterText);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (dataAdminTable != null)
                {
                    string filterText = txtAdminSearch.Text.Trim().Replace("'", "''").ToLower();

                    AdminFilter(filterText);
                }
            }
        }
        private void btn_AdminSearch_Click(object sender, EventArgs e)
        {
            if (dataAdminTable != null)
            {
                string filterText = txtAdminSearch.Text.Trim().Replace("'", "''").ToLower();

                AdminFilter(filterText);
            }
        }

        private void AdminFilter(string filterText)
        {
            try
            {
                DataTable filteredTable = dataAdminTable.Clone();
                foreach (DataRow row in dataAdminTable.Select($"([Name] + ' ' + [Surname]) LIKE '%{filterText}%' OR [Gender] LIKE '%{filterText}%' OR [Email] LIKE '%{filterText}%' OR [PhoneNumber] LIKE '%{filterText}%'"))
                {
                    filteredTable.ImportRow(row);
                }

                dataGridAdmin.Rows.Clear();
                int sayac = 0;
                foreach (DataRow row in filteredTable.Rows)
                {
                    sayac++;
                    string ageText = AdminAgeText(row["Date"]);

                    Image haveToPassword = AdminPasswordStatus(row["HaveToPassword"]);

                    dataGridAdmin.Rows.Add(sayac, row["Id"], row["TcNo"], row["Name"], row["Surname"], row["Gender"], row["Date"], ageText, row["Email"], row["PhoneNumber"], row["Username"], row["Password"], haveToPassword);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtreleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridAdmin.ClearSelection();
        }

        private string AdminAgeText(object birthDateObject)
        {
            string ageText = "";
            if (DateTime.TryParse(birthDateObject?.ToString(), out DateTime birthDate))
            {
                int age = DateTime.Now.Year - birthDate.Year;
                if (birthDate > DateTime.Now.AddYears(-age))
                {
                    age--;
                }
                ageText = age.ToString();
            }
            else
            {
                ageText = "Belirlenmemiş";
            }
            return ageText;
        }

        private Image AdminPasswordStatus(object passwordStatus)
        {
            bool status = Convert.ToBoolean(passwordStatus);
            if (status)
            {
                return Properties.Resources.greenSucces;
            }
            else
            {
                return Properties.Resources.redError;
            }
        }

        private void panelAppointmentTime_Click(object sender, EventArgs e)
        {
            dataGridAppointmentTime.ClearSelection();
        }

        private async void myInformation_Click(object sender, EventArgs e)
        {
            panelAppointmentTime.Visible = false;
            panelPoliclinic.Visible = false;
            panelDoctors.Visible = false;
            panelDoctorRest.Visible = false;
            panelAdmins.Visible = false;
            panelMyInformation.Visible = true;
            panelDashboard.Visible = false;
            panelDarkModeSetting.Visible = false;

            labelPanelName.Text = "Bilgilerim";
            labelPanelName.Location = new Point(942, -3);

            await LoadMyInformation(id);
        }

        private void passwordChange_Click(object sender, EventArgs e)
        {
            myInformation_Click(sender, e);
        }

        private async Task LoadMyInformation(int id)
        {
            string query = @"SELECT TcNo, Id, Name, Surname,  Gender,  Date, Email, EmailConfirm, PhoneNumber, Password, Username FROM Admins WHERE Id = @Id";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                txtAdminID.Text = reader["Id"]?.ToString();
                                txtAdminTcNo.Text = reader["TcNo"]?.ToString();
                                txtAdminName.Text = reader["Name"]?.ToString();
                                txtAdminSurname.Text = reader["Surname"]?.ToString();
                                txtAdminGender.Text = reader["Gender"]?.ToString();
                                txtAdminDate.Text = DateTime.Parse(reader["Date"].ToString()).ToShortDateString().ToString();
                                txtAdminMail.Text = reader["Email"]?.ToString();
                                bool isConfirm = Convert.ToBoolean(reader["EmailConfirm"]);
                                txtAdminPhoneNumber.Text = reader["PhoneNumber"]?.ToString();
                                txtAdminUsername.Text = reader["Username"]?.ToString();
                                txtAdminPassword.Text = reader["Password"]?.ToString();

                                ConfirmId = Convert.ToInt32(txtAdminID.Text);
                                ConfirmMail = txtAdminMail.Text;
                                ConfirmName = $"{txtAdminName.Text} {txtAdminSurname.Text}";
                                ConfirmTc = txtAdminTcNo.Text;

                                if (isConfirm)
                                {
                                    pictureBoxConfirmOrNotConfirm.Image = Properties.Resources.check2;
                                    labelConfirm.Text = "Doğrulanmış";
                                    labelConfirm.ForeColor = Color.FromArgb(26, 179, 148);
                                    labelConfirm.Location = new Point(264, 276);
                                }
                                else
                                {
                                    pictureBoxConfirmOrNotConfirm.Image = Properties.Resources.redError;
                                    labelConfirm.Text = "Doğrulanmamış";
                                    labelConfirm.ForeColor = Color.FromArgb(224, 79, 95);
                                    labelConfirm.Location = new Point(245, 276);

                                    labelCannotConfirmMail.Visible = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Kayıt bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

        }

        private async void btn_InformationUpdate_Click(object sender, EventArgs e)
        {
            int selectedId = Convert.ToInt32(txtAdminID.Text);
            string tcNo = txtAdminTcNo.Text;
            string adminName = txtAdminName.Text;
            string adminSurname = txtAdminSurname.Text;
            string adminGender = txtAdminGender.Text;
            string adminDate = txtAdminDate.Text;
            string adminMail = txtAdminMail.Text;
            string adminPhoneNumber = txtAdminPhoneNumber.Text;
            string username = txtAdminUsername.Text;

            AdminUpdateForm adminUpdateForm = new AdminUpdateForm(tcNo, selectedId, adminName, adminSurname, adminGender, adminDate, adminMail, adminPhoneNumber, username);
            adminUpdateForm.Show();

            await AdminInformationUpdate(adminUpdateForm, selectedId);
        }

        private async Task AdminInformationUpdate(AdminUpdateForm adminUpdateForm, int selectedId)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = adminUpdateForm.AccessibleName;

                    if (status == "Success")
                    {
                        await LoadMyInformation(selectedId);
                        adminUpdateForm.AccessibleName = "abcdefgh";
                        adminUpdateForm.Close();


                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_InformationPasswordChange_Click(object sender, EventArgs e)
        {
            int selectedId = Convert.ToInt32(txtAdminID.Text);
            string oldPassword = txtAdminPassword.Text;

            AdminInformationPasswordChangeForm adminInformationPasswordChangeForm = new AdminInformationPasswordChangeForm(selectedId, oldPassword);
            adminInformationPasswordChangeForm.ShowDialog();

            await AdminInformationPasswordUpdate(adminInformationPasswordChangeForm, selectedId);
        }

        private async Task AdminInformationPasswordUpdate(AdminInformationPasswordChangeForm adminInformationPasswordChangeForm, int selectedId)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = adminInformationPasswordChangeForm.AccessibleName;

                    if (status == "Success")
                    {
                        AdminAddUpdateDeleteSuccessForm adminAddUpdateDeleteSuccessForm = new AdminAddUpdateDeleteSuccessForm("adminPasswordChange", 1);
                        adminAddUpdateDeleteSuccessForm.Show();
                        await LoadMyInformation(selectedId);
                        adminInformationPasswordChangeForm.AccessibleName = "abcdefgh";
                    }
                    else if (status == "Negative")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dashboard_Click(object sender, EventArgs e)
        {
            panelDoctors.Visible = false;
            panelPoliclinic.Visible = false;
            panelDoctorRest.Visible = false;
            panelAppointmentTime.Visible = false;
            panelAdmins.Visible = false;
            panelDarkModeSetting.Visible = false;
            panelMyInformation.Visible = false;
            panelDashboard.Visible = true;

            labelPanelName.Text = "Anasayfa";
            panelDashboardLoadingData();
        }

        private async void labelCannotConfirmMail_Click(object sender, EventArgs e)
        {
            GeneralPasswordChangeWarningForm adminPasswordChangeWarningForm = new GeneralPasswordChangeWarningForm(ConfirmTc, ConfirmMail, ConfirmName, "AdminMailConfirm");
            adminPasswordChangeWarningForm.Show();

            while (true)
            {
                await Task.Delay(400);
                if (adminPasswordChangeWarningForm.AccessibleDescription == "Success")
                {
                    await LoadMyInformation(ConfirmId);
                    labelCannotConfirmMail.Visible = false;
                }
            }
        }

        private void darkModeSettings_Click(object sender, EventArgs e)
        {
            panelAppointmentTime.Visible = false;
            panelPoliclinic.Visible = false;
            panelDoctors.Visible = false;
            panelDoctorRest.Visible = false;
            panelAdmins.Visible = false;
            panelMyInformation.Visible = false;
            panelDashboard.Visible = false;
            panelDarkModeSetting.Visible = true;
        }

        private void dropDownDarkMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDownDarkMode.SelectedItem == "Açık Renk")
            {
                pictureBoxDarkMode.Image = Properties.Resources.lightMode;
                labelDarkModeWarning.Visible = false;
            }
            else if (dropDownDarkMode.SelectedItem == "Koyu Renk")
            {
                pictureBoxDarkMode.Image = Properties.Resources.darkMode;
                labelDarkModeWarning.Visible = false;
            }
        }

        private void SaveSettings(bool isDarkMode)
        {
            File.WriteAllText(settingsFilePath, isDarkMode ? "dark" : "light");
        }

        private async void bunifuButton1_Click(object sender, EventArgs e)
        {
            AdminPanelRestartForm adminPanelRestartForm = new AdminPanelRestartForm();

            if (dropDownDarkMode.SelectedItem == "Açık Renk" && isDarkMode)
            {
                adminPanelRestartForm.Show();

                SaveSettings(false);
            }
            else if (dropDownDarkMode.SelectedItem == "Koyu Renk" && !isDarkMode)
            {
                adminPanelRestartForm.Show();

                SaveSettings(true);
            }
            else
            {
                if (isDarkMode)
                {
                    labelDarkModeWarning.Text = "Halihazırda Koyu renk kullanılmaktadır..";
                    labelDarkModeWarning.Visible = true;
                    return;
                }
                else
                {
                    labelDarkModeWarning.Text = "Halihazırda Açık renk kullanılmaktadır..";
                    labelDarkModeWarning.Visible = true;
                    return;
                }
                
            }

            await Task.Delay(1500);

            this.AccessibleName = "restart";

            adminPanelRestartForm.Close();
            this.Close();


        }
    }
}