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
    public partial class DoctorPaymentAddUpdateForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly int id;
        private readonly string ucret;
        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public DoctorPaymentAddUpdateForm(int _id,string _ucret)
        {
            InitializeComponent();

            id = _id;
            ucret = _ucret;

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

                panelInDoctorPaymentAdd.BackgroundColor = Color.FromArgb(64, 64, 64);

                labelDoctorPaymentName.ForeColor = Color.FromArgb(249, 249, 249);
                labelDoctorPaymentName.BackColor = Color.FromArgb(64, 64, 64);

                txtDoctorPaymentName.ForeColor = Color.FromArgb(249, 249, 249);

                txtDoctorPaymentName.BorderColorActive = Color.FromArgb(124, 86, 216);
                txtDoctorPaymentName.BorderColorHover = Color.FromArgb(167, 114, 242);
                txtDoctorPaymentName.BorderColorIdle = Color.Gray;

                txtDoctorPaymentName.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
                txtDoctorPaymentName.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
                txtDoctorPaymentName.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);

                txtDoctorPaymentName.FillColor = Color.FromArgb(38, 38, 38);

                btn_DoctorPaymentAdd.ForeColor = Color.FromArgb(230, 230, 230);

                btn_DoctorPaymentAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
                btn_DoctorPaymentAdd.IdleFillColor = Color.FromArgb(124, 86, 216);

                btn_DoctorPaymentAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
                btn_DoctorPaymentAdd.onHoverState.FillColor = Color.FromArgb(38, 38, 38);

                btn_DoctorPaymentAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
                btn_DoctorPaymentAdd.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

                btn_DoctorPaymentAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
                btn_DoctorPaymentAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

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

                panelInDoctorPaymentAdd.BackgroundColor = Color.FromArgb(230, 230, 230);

                labelDoctorPaymentName.ForeColor = Color.FromArgb(26, 26, 26);
                labelDoctorPaymentName.BackColor = Color.FromArgb(230, 230, 230);

                txtDoctorPaymentName.ForeColor = Color.FromArgb(26, 26, 26);
                txtDoctorPaymentName.FillColor = Color.White;

                txtDoctorPaymentName.BorderColorActive = Color.FromArgb(230, 230, 230);
                txtDoctorPaymentName.BorderColorHover = Color.FromArgb(235, 235, 235);
                txtDoctorPaymentName.BorderColorIdle = Color.Gray;

                txtDoctorPaymentName.OnHoverState.FillColor = Color.White;
                txtDoctorPaymentName.OnIdleState.FillColor = Color.White;
                txtDoctorPaymentName.OnActiveState.FillColor = Color.White;


                btn_DoctorPaymentAdd.ForeColor = Color.FromArgb(64, 64, 64);

                btn_DoctorPaymentAdd.IdleBorderColor = Color.Black;
                btn_DoctorPaymentAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

                btn_DoctorPaymentAdd.onHoverState.BorderColor = Color.Black;
                btn_DoctorPaymentAdd.onHoverState.FillColor = Color.DimGray;

                btn_DoctorPaymentAdd.OnIdleState.BorderColor = Color.Black;
                btn_DoctorPaymentAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

                btn_DoctorPaymentAdd.OnPressedState.BorderColor = Color.Black;
                btn_DoctorPaymentAdd.OnPressedState.FillColor = Color.Gray;

                labelFooter.ForeColor = Color.FromArgb(26, 26, 26);
                labelFooter.BackColor = Color.FromArgb(230, 230, 230);

            }
        }

        private void DoctorPaymentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AccessibleName = "Negative";
        }

        private async void btn_DoctorPaymentAdd_Click(object sender, EventArgs e)
        {
            string operation = "moneyAdd";
            string Ucret = txtDoctorPaymentName.Text;

            if (string.IsNullOrWhiteSpace(Ucret))
            {
                labelRequired.Visible = true;
                return;
            }

            string query = "UPDATE Doctors SET Money =@Money WHERE Id =@Id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Money", Ucret);

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
            this.AccessibleName = "Success";
        }

        private void txtDoctorPaymentName_TextChange(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDoctorPaymentName.Text))
            {
                labelRequired.Visible = false;
            }
        }

        private void DoctorPaymentAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_DoctorPaymentAdd_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void DoctorPaymentAddForm_Load(object sender, EventArgs e)
        {
            txtDoctorPaymentName.Text = ucret;
        }
    }
}