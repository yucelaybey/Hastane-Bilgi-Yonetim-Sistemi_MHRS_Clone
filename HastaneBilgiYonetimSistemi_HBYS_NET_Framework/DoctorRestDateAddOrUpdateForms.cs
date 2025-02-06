using ComponentFactory.Krypton.Toolkit;
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
    public partial class DoctorRestDateAddOrUpdateForms : DevExpress.XtraEditors.XtraForm
    {
        private int Id;
        private string operation;
        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public DoctorRestDateAddOrUpdateForms(int _selectedId, string _operation)
        {
            InitializeComponent();

            Id = _selectedId;
            operation = _operation;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private async void DoctorRestDateAddOrUpdate_Load(object sender, EventArgs e)
        {
            await LoadDoctorRestData(Id, operation);

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

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                btnDoctorRestDarkOpen();

                txtDoctorRestDarkOpen();

                panelDoctorRestPropertyDarkOpen();

                labelFooter.ForeColor = Color.FromArgb(230, 230, 230);
                labelFooter.BackColor = Color.FromArgb(64, 64, 64);

                labelChooseDate.ForeColor = Color.FromArgb(230, 230, 230);
                labelChooseDate.BackColor = Color.FromArgb(38, 38, 38);
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(230, 230, 230);

                pictureBoxDark.Visible = false;
                pictureBoxWhite.Visible = true;

                btnDoctorRestDarkClose();

                txtDoctorRestDarkClose();

                panelDoctorRestPropertyDarkClose();

                labelFooter.ForeColor = Color.Black;
                labelFooter.BackColor = Color.FromArgb(230, 230, 230);

                labelChooseDate.ForeColor = Color.Black;
                labelChooseDate.BackColor = Color.Gray;
            }
        }
        private void txtDoctorRestDarkOpen()
        {
            txtDoctorId.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorId.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorId.BorderColorIdle = Color.Gray;
            txtDoctorId.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorId.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorId.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorId.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorId.OnDisabledState.FillColor = Color.FromArgb(54, 54, 54);
            txtDoctorId.OnDisabledState.BorderColor = Color.FromArgb(124, 86, 216);
            txtDoctorId.OnDisabledState.ForeColor = Color.White;


            txtDoctorTcKimlik.BorderColorActive = Color.FromArgb(124, 86, 216);
            txtDoctorTcKimlik.BorderColorHover = Color.FromArgb(167, 114, 242);
            txtDoctorTcKimlik.BorderColorIdle = Color.Gray;
            txtDoctorTcKimlik.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorTcKimlik.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorTcKimlik.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorTcKimlik.FillColor = Color.FromArgb(38, 38, 38);


            DropdownDoctorName.ForeColor = Color.FromArgb(249, 249, 249);
            DropdownDoctorName.BackgroundColor = Color.FromArgb(38, 38, 38);
            DropdownDoctorName.ItemBackColor = Color.FromArgb(38, 38, 38);
            DropdownDoctorName.ItemBorderColor = Color.FromArgb(38, 38, 38);
        }

        private void txtDoctorRestDarkClose()
        {
            txtDoctorId.BorderColorActive = Color.FromArgb(230, 230, 230);
            txtDoctorId.BorderColorHover = Color.FromArgb(235, 235, 235);
            txtDoctorId.BorderColorIdle = Color.Gray;
            txtDoctorId.OnHoverState.FillColor = Color.White;
            txtDoctorId.OnIdleState.FillColor = Color.White;
            txtDoctorId.OnActiveState.FillColor = Color.White;
            txtDoctorId.FillColor = Color.FromArgb(38, 38, 38);
            txtDoctorId.OnDisabledState.FillColor = Color.White;
            txtDoctorId.OnDisabledState.BorderColor = Color.FromArgb(230, 230, 230);
            txtDoctorId.OnDisabledState.ForeColor = Color.Black;


            txtDoctorTcKimlik.BorderColorActive = Color.Gray;
            txtDoctorTcKimlik.BorderColorHover = Color.Gray;
            txtDoctorTcKimlik.BorderColorIdle = Color.Gray;
            txtDoctorTcKimlik.OnHoverState.FillColor = Color.White;
            txtDoctorTcKimlik.OnIdleState.FillColor = Color.White;
            txtDoctorTcKimlik.OnActiveState.FillColor = Color.White;
            txtDoctorTcKimlik.FillColor = Color.White;


            DropdownDoctorName.ForeColor = Color.Black;
            DropdownDoctorName.BackgroundColor = Color.White;
            DropdownDoctorName.ItemBackColor = Color.White;
            DropdownDoctorName.ItemBorderColor = Color.White;
            DropdownDoctorName.ItemForeColor = Color.Black;
        }
        private void btnDoctorRestDarkOpen()
        {
            btn_dataGridResultDelete.ForeColor = Color.FromArgb(230, 230, 230);

            btn_dataGridResultDelete.IdleBorderColor = Color.Transparent;
            btn_dataGridResultDelete.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_dataGridResultDelete.onHoverState.BorderColor = Color.Transparent;
            btn_dataGridResultDelete.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_dataGridResultDelete.OnIdleState.BorderColor = Color.Transparent;
            btn_dataGridResultDelete.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_dataGridResultDelete.OnPressedState.BorderColor = Color.Transparent;
            btn_dataGridResultDelete.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);


            btn_RestAdd.ForeColor = Color.FromArgb(230, 230, 230);

            btn_RestAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_RestAdd.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_RestAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_RestAdd.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_RestAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_RestAdd.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_RestAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_RestAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);


            btn_DoctorList.ForeColor = Color.FromArgb(230, 230, 230);

            btn_DoctorList.IdleBorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorList.IdleFillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorList.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
            btn_DoctorList.onHoverState.FillColor = Color.FromArgb(124, 86, 216);

            btn_DoctorList.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
            btn_DoctorList.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);

            btn_DoctorList.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
            btn_DoctorList.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);
        }
        private void btnDoctorRestDarkClose()
        {
            btn_dataGridResultDelete.ForeColor = Color.FromArgb(64, 64, 64);

            btn_dataGridResultDelete.IdleBorderColor = Color.Transparent;
            btn_dataGridResultDelete.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_dataGridResultDelete.onHoverState.BorderColor = Color.Transparent;
            btn_dataGridResultDelete.onHoverState.FillColor = Color.DimGray;

            btn_dataGridResultDelete.OnIdleState.BorderColor = Color.Transparent;
            btn_dataGridResultDelete.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_dataGridResultDelete.OnPressedState.BorderColor = Color.Transparent;
            btn_dataGridResultDelete.OnPressedState.FillColor = Color.Gray;



            btn_RestAdd.ForeColor = Color.FromArgb(64, 64, 64);

            btn_RestAdd.IdleBorderColor = Color.Black;
            btn_RestAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_RestAdd.onHoverState.BorderColor = Color.Black;
            btn_RestAdd.onHoverState.FillColor = Color.DimGray;

            btn_RestAdd.OnIdleState.BorderColor = Color.Black;
            btn_RestAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_RestAdd.OnPressedState.BorderColor = Color.Black;
            btn_RestAdd.OnPressedState.FillColor = Color.Gray;


            btn_DoctorList.ForeColor = Color.FromArgb(64, 64, 64);

            btn_DoctorList.IdleBorderColor = Color.Black;
            btn_DoctorList.IdleFillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorList.onHoverState.BorderColor = Color.Black;
            btn_DoctorList.onHoverState.FillColor = Color.DimGray;

            btn_DoctorList.OnIdleState.BorderColor = Color.Black;
            btn_DoctorList.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

            btn_DoctorList.OnPressedState.BorderColor = Color.Black;
            btn_DoctorList.OnPressedState.FillColor = Color.Gray;
        }

        private void panelDoctorRestPropertyDarkOpen()
        {
            panelGradient.GradientBottomLeft = Color.FromArgb(236, 92, 188);
            panelGradient.GradientBottomRight = Color.DeepPink;
            panelGradient.GradientTopLeft = Color.FromArgb(124, 8, 216);
            panelGradient.GradientTopRight = Color.FromArgb(198, 60, 212);

            panelInDoctorRestAdd.BackgroundColor = Color.FromArgb(64, 64, 64);

            panelResult.BorderColor = Color.White;

            dataGridResult.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 86, 216);
            dataGridResult.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 86, 216);
            dataGridResult.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridResult.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridResult.DefaultCellStyle.BackColor = Color.FromArgb(167, 114, 242);
            dataGridResult.DefaultCellStyle.ForeColor = Color.FromArgb(249, 249, 249);
            dataGridResult.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            dataGridResult.DefaultCellStyle.SelectionForeColor = Color.FromArgb(249, 249, 249);

            dataGridResult.BackgroundColor = Color.FromArgb(64, 64, 64);
            dataGridResult.GridColor = Color.FromArgb(64, 64, 64);
        }
        private void panelDoctorRestPropertyDarkClose()
        {
            panelGradient.GradientBottomLeft = Color.FromArgb(230, 230, 230);
            panelGradient.GradientBottomRight = Color.Gray;
            panelGradient.GradientTopLeft = Color.Gray;
            panelGradient.GradientTopRight = Color.FromArgb(230, 230, 230);

            panelInDoctorRestAdd.BackgroundColor = Color.FromArgb(230, 230, 230);

            panelResult.BorderColor = Color.Gray;

            dataGridResult.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridResult.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            dataGridResult.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridResult.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridResult.DefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200);
            dataGridResult.DefaultCellStyle.ForeColor = Color.Black;
            dataGridResult.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 249, 249);
            dataGridResult.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridResult.GridColor = Color.FromArgb(249, 249, 249);
            dataGridResult.BackgroundColor = Color.FromArgb(249, 249, 249);
        }

        private void monthCalendarRest_DateSelected(object sender, DateRangeEventArgs e)
        {
            string selectedDate = e.Start.ToShortDateString();

            foreach (DataGridViewRow row in dataGridResult.Rows)
            {
                if (row.Cells["doctorRest"].Value?.ToString() == selectedDate)
                {
                    DataChooseForm dataChooseForm = new DataChooseForm("doctorRestSame");
                    dataChooseForm.Show();
                    return;
                }
            }
            dataGridResult.Rows.Add(selectedDate);
            dataGridResult.ClearSelection();
        }

        private void btn_RestDelete_Click(object sender, EventArgs e)
        {
            if (dataGridResult.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in dataGridResult.SelectedRows)
                {
                    dataGridResult.Rows.Remove(selectedRow);
                }
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("delete");
                dataChooseForm.Show();
            }
        }
        private async void DoctorTckimlik_Click(object sender, EventArgs e)
        {
            string tc = txtDoctorTcKimlik.Text;

            if (string.IsNullOrWhiteSpace(tc)) return;

            string query = @"
        SELECT D.Id, D.Name, D.Surname, D.TcNo, D.PoliclinicId, P.[Poliklinik Adı]
        FROM Doctors D INNER JOIN Policlinics P ON D.PoliclinicId = P.Id WHERE D.TcNo = @TC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TC", tc);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            var doctorName = $"{reader["Name"]} {reader["Surname"]}";
                            var policlinicName = reader["Poliklinik Adı"].ToString();
                            var doctorId = reader["Id"].ToString();

                            foreach (var item in DropdownDoctorName.Items)
                            {
                                var doctor = (dynamic)item;
                                if (doctor.DoctorID.ToString() == doctorId)
                                {
                                    DropdownDoctorName.SelectedItem = doctor;
                                    break;
                                }
                            }
                            txtDoctorId.Text = doctorId;
                            txtDoctorTcKimlik.Text = tc;
                            Id = int.Parse(doctorId);
                        }
                        else
                        {
                            DropdownDoctorName.SelectedIndex = -1;
                            txtDoctorId.Clear();
                            txtDoctorTcKimlik.Clear();
                        }
                    }
                }
            }
        }
        private async void btn_RestAdd_Click(object sender, EventArgs e)
        {
            if (dataGridResult.RowCount > 0 && dataGridResult.Rows.Count > 0)
            {
                await SaveDoctorRestDate(Id);
            }
            else
            {
                DataChooseForm dataChooseForm = new DataChooseForm("doctorRestAdd");
                dataChooseForm.Show();
            }
        }
        private async Task SaveDoctorRestDate(int Id)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                transaction = connection.BeginTransaction();

                List<DateTime> existingDates = await GetExistingRestDatesAsync(connection, Id, transaction);

                List<DateTime> newDates = new List<DateTime>();
                foreach (DataGridViewRow row in dataGridResult.Rows)
                {
                    if (row.Cells["doctorRest"].Value != null)
                    {
                        if (DateTime.TryParse(row.Cells["doctorRest"].Value.ToString(), out DateTime restDate))
                        {
                            newDates.Add(restDate);
                        }
                    }
                }

                if (existingDates.Count > 0)
                {
                    var datesToDelete = existingDates.Except(newDates).ToList();
                    foreach (var date in datesToDelete)
                    {
                        string deleteQuery = "DELETE FROM DoctorRestDate WHERE DoctorId = @DoctorId AND RestDate = @RestDate";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@DoctorId", Id);
                            deleteCommand.Parameters.AddWithValue("@RestDate", date);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }

                var datesToAdd = newDates.Except(existingDates).ToList();
                foreach (var date in datesToAdd)
                {
                    string insertQuery = "INSERT INTO DoctorRestDate (DoctorId, RestDate,Status) VALUES (@DoctorId, @RestDate, @Status)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                    {
                        insertCommand.Parameters.AddWithValue("@DoctorId", Id);
                        insertCommand.Parameters.AddWithValue("@RestDate", date);
                        insertCommand.Parameters.AddWithValue("@Status", "Devam Ediyor");
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                }
                transaction.Commit();

                this.AccessibleName = "Success";

                DoctorRestUpdateForm doctorRestUpdateForm = new DoctorRestUpdateForm("update", 5);
                doctorRestUpdateForm.Show();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                this.AccessibleName = "Negative";
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

        private async Task<List<DateTime>> GetExistingRestDatesAsync(SqlConnection connection, int Id, SqlTransaction transaction)
        {
            List<DateTime> existingDates = new List<DateTime>();

            string query = "SELECT RestDate FROM DoctorRestDate WHERE DoctorId = @DoctorId";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@DoctorId", Id);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var restDate = reader["RestDate"];
                        if (restDate != DBNull.Value && DateTime.TryParse(restDate.ToString(), out DateTime date))
                        {
                            existingDates.Add(date);
                        }
                    }
                }
            }

            return existingDates;
        }

        private async Task LoadDoctorRestData(int UserId, string operation)
        {
            string query = @"SELECT D.TcNo, D.Id, D.Name, D.Surname, D.PoliclinicId, P.[Poliklinik Adı] FROM Doctors D INNER JOIN Policlinics P ON D.PoliclinicId = P.Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    DropdownDoctorName.Items.Clear();

                    while (reader.Read())
                    {
                        string doctorDisplayName = $"{reader["Name"]} {reader["Surname"]} - {reader["Poliklinik Adı"]}";

                        var item = new
                        {
                            DoctorID = reader["Id"],
                            DoctorName = doctorDisplayName,
                            PoliclinicName = reader["Poliklinik Adı"],
                            TC = reader["TcNo"]
                        };

                        DropdownDoctorName.Items.Add(item);
                    }
                }
            }
            DropdownDoctorName.DisplayMember = "DoctorName";
            DropdownDoctorName.ValueMember = "DoctorID";

            if (operation == "update")
            {

                foreach (var item in DropdownDoctorName.Items)
                {
                    dynamic doctorItem = item;

                    if (doctorItem.DoctorID == Id)
                    {
                        DropdownDoctorName.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private async void DropdownDoctorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropdownDoctorName.SelectedItem != null)
            {
                var selectedDoctor = (dynamic)DropdownDoctorName.SelectedItem;

                txtDoctorId.Text = selectedDoctor.DoctorID.ToString();
                txtDoctorTcKimlik.Text = selectedDoctor.TC.ToString();
                Id = Convert.ToInt32(selectedDoctor.DoctorID);

                await LoadDoctorRestDate(Id);
            }
        }

        private async Task LoadDoctorRestDate(int Id)
        {
            string query = "SELECT RestDate FROM DoctorRestDate WHERE DoctorId = @DoctorId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorId", Id);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataGridResult.Rows.Clear();

                            while (await reader.ReadAsync())
                            {
                                var restDate = reader["RestDate"];

                                if (restDate != DBNull.Value && DateTime.TryParse(restDate.ToString(), out DateTime date))
                                {
                                    dataGridResult.Rows.Add(date.ToShortDateString());
                                }
                            }
                        }
                    }
                    dataGridResult.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panelInDoctorRestAdd_Click(object sender, EventArgs e)
        {
            dataGridResult.ClearSelection();
        }

        private void panelGradient_Click(object sender, EventArgs e)
        {
            dataGridResult.ClearSelection();
        }

        private void DoctorRestDateAddOrUpdate_Click(object sender, EventArgs e)
        {
            dataGridResult.ClearSelection();
        }
    }
}