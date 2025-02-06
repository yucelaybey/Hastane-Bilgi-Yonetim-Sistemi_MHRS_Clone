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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class PoliclinicAddForm : DevExpress.XtraEditors.XtraForm
    {
        private bool isDarkMode;
        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public PoliclinicAddForm()
        {
            InitializeComponent();
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
                this.BackColor = Color.FromArgb(38, 38, 38);

                pictureBoxDark.Visible = true;
                pictureBoxWhite.Visible = false;

                panelGradient.GradientBottomLeft = Color.FromArgb(236, 92, 188);
                panelGradient.GradientBottomRight = Color.DeepPink;
                panelGradient.GradientTopLeft = Color.FromArgb(124, 8, 216);
                panelGradient.GradientTopRight = Color.FromArgb(198, 60, 212);

                panelInPoliclinicAdd.BackgroundColor = Color.FromArgb(64, 64, 64);

                labelPoliclinicName.ForeColor = Color.FromArgb(249, 249, 249);
                labelPoliclinicName.BackColor = Color.FromArgb(64, 64, 64);

                txtPoliclinicName.ForeColor = Color.FromArgb(249, 249, 249);

                txtPoliclinicName.BorderColorActive = Color.FromArgb(124, 86, 216);
                txtPoliclinicName.BorderColorHover = Color.FromArgb(167, 114, 242);
                txtPoliclinicName.BorderColorIdle = Color.Gray;

                txtPoliclinicName.OnIdleState.FillColor = Color.FromArgb(38, 38, 38);
                txtPoliclinicName.OnHoverState.FillColor = Color.FromArgb(38, 38, 38);
                txtPoliclinicName.OnActiveState.FillColor = Color.FromArgb(38, 38, 38);

                txtPoliclinicName.FillColor = Color.FromArgb(38, 38, 38);

                btn_PoliclinicAdd.ForeColor = Color.FromArgb(230, 230, 230);

                btn_PoliclinicAdd.IdleBorderColor = Color.FromArgb(124, 86, 216);
                btn_PoliclinicAdd.IdleFillColor = Color.FromArgb(124, 86, 216);

                btn_PoliclinicAdd.onHoverState.BorderColor = Color.FromArgb(167, 114, 242);
                btn_PoliclinicAdd.onHoverState.FillColor = Color.FromArgb(38, 38, 38);

                btn_PoliclinicAdd.OnIdleState.BorderColor = Color.FromArgb(124, 86, 216);
                btn_PoliclinicAdd.OnIdleState.FillColor = Color.FromArgb(124, 86, 216);

                btn_PoliclinicAdd.OnPressedState.BorderColor = Color.FromArgb(97, 50, 209);
                btn_PoliclinicAdd.OnPressedState.FillColor = Color.FromArgb(124, 86, 216);

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

                panelInPoliclinicAdd.BackgroundColor = Color.FromArgb(230, 230, 230);

                labelPoliclinicName.ForeColor = Color.FromArgb(26, 26, 26);
                labelPoliclinicName.BackColor = Color.FromArgb(230, 230, 230);

                txtPoliclinicName.ForeColor = Color.FromArgb(26, 26, 26);
                txtPoliclinicName.FillColor = Color.White;

                txtPoliclinicName.BorderColorActive = Color.FromArgb(230, 230, 230);
                txtPoliclinicName.BorderColorHover = Color.FromArgb(235, 235, 235);
                txtPoliclinicName.BorderColorIdle = Color.Gray;

                txtPoliclinicName.OnHoverState.FillColor = Color.White;
                txtPoliclinicName.OnIdleState.FillColor = Color.White;
                txtPoliclinicName.OnActiveState.FillColor = Color.White;


                btn_PoliclinicAdd.ForeColor = Color.FromArgb(64, 64, 64);

                btn_PoliclinicAdd.IdleBorderColor = Color.Black;
                btn_PoliclinicAdd.IdleFillColor = Color.FromArgb(230, 230, 230);

                btn_PoliclinicAdd.onHoverState.BorderColor = Color.Black;
                btn_PoliclinicAdd.onHoverState.FillColor = Color.DimGray;

                btn_PoliclinicAdd.OnIdleState.BorderColor = Color.Black;
                btn_PoliclinicAdd.OnIdleState.FillColor = Color.FromArgb(230, 230, 230);

                btn_PoliclinicAdd.OnPressedState.BorderColor = Color.Black;
                btn_PoliclinicAdd.OnPressedState.FillColor = Color.Gray;

                labelFooter.ForeColor = Color.FromArgb(26, 26, 26);
                labelFooter.BackColor = Color.FromArgb(230, 230, 230);

            }
        }

        private void PoliklinikEkleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.AccessibleName = "Negative";
        }

        private async void btn_PoliclinicAdd_Click(object sender, EventArgs e)
        {
            string operation = "add";
            string poliklinikAdi = txtPoliclinicName.Text;

            if (string.IsNullOrWhiteSpace(poliklinikAdi))
            {
                labelRequired.Visible = true;
                return;
            }

            string query = "INSERT INTO Policlinics ([Poliklinik Adı]) VALUES (@PoliclinicName)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PoliclinicName", poliklinikAdi);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            this.Hide();
                            PoliclinicAddUpdateDeleteSuccessForm poliklinikEkleSilGuncelleSuccessForm = new PoliclinicAddUpdateDeleteSuccessForm(operation, 1);
                            poliklinikEkleSilGuncelleSuccessForm.Show();

                            while (true)
                            {
                                await Task.Delay(100);
                                if (poliklinikEkleSilGuncelleSuccessForm.IsDisposed)
                                {

                                    this.AccessibleName = "Success";
                                    txtPoliclinicName.Clear();
                                    this.Show();
                                    break;
                                }
                                GC.Collect();
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

        private void txtPoliclinicName_TextChange(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPoliclinicName.Text))
            {
                labelRequired.Visible = false;
            }
        }

        private void PoliclinicAddForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_PoliclinicAdd_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}