using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class ApproveDeleteForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private readonly string type;
        private DataGridView dataGridView;
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public ApproveDeleteForm(DataGridView _dataGridView, string _type)
        {
            InitializeComponent();

            dataGridView = _dataGridView;
            type = _type;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }

        private void ApproveDeleteForm_Load(object sender, EventArgs e)
        {
            if (type == "policlinic")
            {
                PoliclinicList(dataGridView);
            }
            else if (type == "doctor")
            {
                DoctorList(dataGridView);
            }
            else if (type == "doctorRest")
            {
                DoctorRestList(dataGridView);
            }
            else if (type == "appointmentTime")
            {
                AppointmentTimeList(dataGridView);
            }
            else if (type == "admin")
            {
                AdminList(dataGridView);
            }
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
                this.BackColor = Color.FromArgb(26, 26, 26);

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                labelQuestion.BackColor = Color.FromArgb(26, 26, 26);

                labelAnswer.ForeColor = Color.FromArgb(249, 249, 249);
                labelAnswer.BackColor = Color.FromArgb(26, 26, 26);

            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(249, 249, 249);

                pictureBoxDark.Visible = false;
                pictureBoxWhite.Visible = true;

                labelQuestion.BackColor = Color.FromArgb(249, 249, 249);

                labelAnswer.ForeColor = Color.FromArgb(26, 26, 26);
                labelAnswer.BackColor = Color.FromArgb(249, 249, 249);
            }
        }

        private void PoliclinicList(DataGridView dataGridViewPoliclinic)
        {
            StringBuilder labelText = new StringBuilder(); // stringbuilder verileri biriktirmek toplu tutmak için kullanılır.

            foreach (DataGridViewRow row in dataGridViewPoliclinic.SelectedRows)
            {
                string policlinicName = row.Cells["policlinicName"].Value?.ToString();
                if (!string.IsNullOrEmpty(policlinicName))
                {
                    labelText.AppendLine(policlinicName); //appendline ile yukarıdan aldığımız verileri alt alta ekleriz.
                }
            }
            labelAnswer.Text = labelText.ToString();
        }

        private void DoctorList(DataGridView dataGridViewDoctor)
        {
            StringBuilder labelText = new StringBuilder(); // stringbuilder verileri biriktirmek toplu tutmak için kullanılır.

            foreach (DataGridViewRow row in dataGridViewDoctor.SelectedRows)
            {
                string doctorName = row.Cells["doctorName"].Value?.ToString();
                string doctorSurname = row.Cells["doctorSurname"].Value?.ToString();
                string nameSurname = $"{doctorName} {doctorSurname}";
                if (!string.IsNullOrEmpty(nameSurname))
                {
                    labelText.AppendLine(nameSurname); //appendline ile yukarıdan aldığımız verileri alt alta ekleriz.
                }
            }
            labelAnswer.Text = labelText.ToString();
        }

        private void DoctorRestList(DataGridView dataGridViewDoctorRest)
        {
            StringBuilder labelText = new StringBuilder();

            foreach (DataGridViewRow row in dataGridViewDoctorRest.SelectedRows)
            {
                string doctorName = row.Cells["doctorRestName"].Value?.ToString();
                string doctorSurname = row.Cells["doctorRestSurname"].Value?.ToString();
                string doctorRestDate = row.Cells["doctorRestDate"].Value?.ToString();
                string nameSurnameDate = $"{doctorName} {doctorSurname} - {doctorRestDate}";
                if (!string.IsNullOrEmpty(nameSurnameDate))
                {
                    labelText.AppendLine(nameSurnameDate);
                }
            }
            labelAnswer.Text = labelText.ToString();
        }
        private void AppointmentTimeList(DataGridView dataGridViewAppointmentTime)
        {
            StringBuilder labelText = new StringBuilder();

            foreach (DataGridViewRow row in dataGridViewAppointmentTime.SelectedRows)
            {
                string appointmentTime = row.Cells["appointmentTimeRest"].Value?.ToString();
                if (!string.IsNullOrEmpty(appointmentTime))
                {
                    labelText.AppendLine(appointmentTime);
                }
            }
            labelAnswer.Text = labelText.ToString();
        }
        private void AdminList(DataGridView dataGridViewAppointmentTime)
        {
            StringBuilder labelText = new StringBuilder();

            foreach (DataGridViewRow row in dataGridViewAppointmentTime.SelectedRows)
            {
                string doctorName = row.Cells["adminName"].Value?.ToString();
                string doctorSurname = row.Cells["adminSurname"].Value?.ToString();
                string nameSurname = $"{doctorName} {doctorSurname}";
                if (!string.IsNullOrEmpty(nameSurname.ToString()))
                {
                    labelText.AppendLine(nameSurname);
                }
            }
            labelAnswer.Text = labelText.ToString();
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            this.AccessibleName = "Yes";
            this.AccessibleDescription = "FormClosingGec";
            this.Close();
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            this.AccessibleName = "No";
            this.Close();
        }

        private void ApproveDeleteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.AccessibleDescription == "FormClosingGec")
            {

            }
            else
            {
                this.AccessibleName = "No";
            }

        }
    }
}