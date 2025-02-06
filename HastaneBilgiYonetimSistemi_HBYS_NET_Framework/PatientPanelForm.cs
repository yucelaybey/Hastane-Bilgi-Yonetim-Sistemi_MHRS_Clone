using Bunifu.UI.WinForms;
using DevExpress.LookAndFeel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnimatorNS;
using System.Windows.Forms;
using Bunifu.UI.WinForms.BunifuButton;
using HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Classes;
using DevExpress.XtraTab;
using DevExpress.XtraReports.UI;
using DevExpress.XtraScheduler;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class PatientPanelForm : DevExpress.XtraEditors.XtraForm
    {
        private Size targetPanelSize;
        private Size originalPanelSize;

        private DateTime dateAccept = new DateTime();

        private bool isMouseInsidePanel;
        private bool cannotButtonClick = true;
        private bool isAddingData = false;
        private bool isPanelMaximized = false;
        private bool isDarkMode;

        private string policlinicNameAccept;
        private string DoctorNameAccept;
        private string mail;
        private string patientName;
        private string startType;

        private readonly int id;
        private int DoctorIdAccept;
        private int PoliclinicIdAccept;
        private int currentPage = 1;
        private int currentAllActivePage = 1;
        private int currentAllPastPage = 1;
        private int itemsPerPage = 5;
        private int totalAppointmentCount = 0;
        private int totalPastAppointmentCount = 0;
        private int totalAvaibleAppointmentCount = 0;
        private int targetHeight;
        private int stepSize = 10;
        private int currentDoctorId;

        private Timer resizeTimer;

        private BunifuPanel currentOpenPanel = null;
        private BunifuButton currentOpenButton = null;
        private BunifuPanel nextPanelToOpen = null;

        private Label[] labels;
        private Label[] labelPast;

        private Animator animatorLeft;
        private Animator animator;

        private List<AppointmentDashboard> appointmentPastList = new List<AppointmentDashboard>();
        private List<AppointmentDashboard> appointmentList = new List<AppointmentDashboard>();
        private List<DoctorAvailability> allDoctorData = new List<DoctorAvailability>();

        private string connectionString = "server=YCLGAMER;database=DbHBYS_NETFRMWRK;integrated security=true;TrustServerCertificate=True";
        private string settingsFilePath = Path.Combine("C:\\Users\\yucel\\OneDrive\\Masaüstü\\Projem (SİLMEE !)\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\HastaneBilgiYonetimSistemi_HBYS_NET_Framework\\DarkMode\\DarkModeOnOff.txt");
        public PatientPanelForm(int _id,string _startType)
        {
            InitializeComponent();

            id = _id;
            startType = _startType;

            labels = new Label[] { labelPage1, labelPage2, labelPage3, labelPage4, labelPage5 };
            labelPast = new Label[] { labelPast1, labelPast2, labelPast3, labelPast4, labelPast5 };

            animatorLeft = new AnimatorNS.Animator();
            animator = new AnimatorNS.Animator();
            animatorLeft.AnimationType = AnimationType.HorizSlide;
            animator.AnimationType = AnimationType.HorizSlide;
            animatorLeft.DefaultAnimation.SlideCoeff = new PointF(-1, 0);

            originalPanelSize = panelInformation.Size;

            panelResizeTimer = new Timer();
            panelResizeTimer.Interval = 10;
            panelResizeTimer.Tick += PanelResizeTimer_Tick;

            PerformActionBasedOnSetting();
            DarkModeOpen();
        }
        private async void PatientPanelForm_Load(object sender, EventArgs e)
        {

            panelBackground.Size = new Size(1840, 810);
            panelBackground.Location = new Point(42, 133);

            var result = await PatientInformation(id);
            btn_Information.Text = $"{result.Name} {result.Surname}";
            patientName = $"{result.Name} {result.Surname}";

            AdjustButtonWidth(btn_Information);

            panelInformation.MinimumSize = new Size(btn_Information.Width, 0);

            PaneLBorder.Size = new Size(1740, 603);
            Property();
            DateTimePickerProperty();
            AppointmentListProperty();
            AppointmentListAllProperty();
            DashboardProperty();

            DarkModeSettingProperty();

            txtPoliclinicAndDoctorName.Size = new Size(466, 33);

            await LoadPoliclinicList();
            await LoadActiveAppointments(id);
            await LoadPastAppointments(id);

            labeldatePickerStart.Visible = true;
            labeldatePickerEnd.Visible = true;

            if (startType == "restart")
            {
                await Task.Delay(1000);

                DarkModeChooseSuccessForm darkModeChooseSuccessForm = new DarkModeChooseSuccessForm();
                darkModeChooseSuccessForm.Show();
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
                return File.ReadAllText(settingsFilePath).Trim();
            }
            return "light";
        }
        private void SaveSettings(bool isDarkMode)
        {
            File.WriteAllText(settingsFilePath, isDarkMode ? "dark" : "light");
        }

        private void DarkModeOpen()
        {
            if (isDarkMode)
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
                this.BackColor = Color.FromArgb(56, 56, 56);

                panelBackground.BackgroundColor = Color.Black;

                pictureBoxAppointmentLoading.BackColor = Color.Black;

                sidebarDarkModeOpenProperty();

                darkModeOpenProperty();

                dropDownPoliclinic.ForeColor = Color.Gray;

                panelsPropertyOpen();

                panelActiveAppointmentPropertyOpen();

                panelPastAppointmentPropertyOpen();

                panelActiveAllAppointmentOpenProperty();

                panelPastAllAppointmentOpenProperty();

                panelPastAllPageSliderOpenProperty();

                panelDarkModeSettingOpenProperty();

                panelAppointmentChooseOpenProperty();

                panelAppointmentListOpenProperty();

                panelDoctorAppointmentChooseOpenProperty();
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 White");
                this.BackColor = Color.FromArgb(245, 245, 245);

                pictureBoxAppointmentLoading.BackColor = Color.White;

                panelBackground.BackgroundColor = Color.White;

                sidebarDarkModeCloseProperty();

                darkModeCloseProperty();

                dropDownPoliclinic.ForeColor = Color.Black;

                panelsPropertyClose();

                panelActiveAppointmentPropertyClose();

                panelPastAppointmentPropertyClose();

                panelActiveAllAppointmentCloseProperty();

                panelPastAllAppointmentCloseProperty();

                panelPastAllPageSliderCloseProperty();

                panelDarkModeSettingCloseProperty();

                panelAppointmentChooseCloseProperty();

                panelAppointmentListCloseProperty();

                panelDoctorAppointmentChooseCloseProperty();
            }
        }

        private void DarkModeSettingProperty()
        {
            if (isDarkMode)
            {
                dropDownDarkMode.SelectedItem = "Koyu Renk";
                pictureBoxMod.Image = Properties.Resources.KoyuMod;
            }
            else
            {
                dropDownDarkMode.SelectedItem = "Açık Renk";
                pictureBoxMod.Image = Properties.Resources.AçıkMod;
            }
        }
        private void panelsPropertyOpen()
        {
            panelDashboard.BackColor = Color.Black;
            panelInformation.BackColor = Color.Black;
            panelInformationButtonOpenProperty();

            panelActivePastAppointment.BackColor = Color.Black;
            panelMyInformation.BackColor = Color.Black;
            panelMyInformationOtherOpenProperty();

            panelDarkModeSettings.BackColor = Color.Black;
            panelAppointmentChoose.BackColor = Color.Black;

            panelAppointmentList.BackColor = Color.Black;
            panelDoctorAppointmentChoose.BackColor = Color.Black;

        }
        private void panelsPropertyClose()
        {
            panelDashboard.BackColor = Color.White;
            panelInformation.BackColor = Color.White;
            panelInformationButtonCloseProperty();

            panelActivePastAppointment.BackColor = Color.White;
            panelMyInformation.BackColor = Color.White;
            panelMyInformationOtherCloseProperty();

            panelDarkModeSettings.BackColor = Color.White;
            panelAppointmentChoose.BackColor = Color.White;

            panelAppointmentList.BackColor = Color.White;
            panelDoctorAppointmentChoose.BackColor = Color.White;
        }

        private void panelMyInformationOtherOpenProperty()
        {
            pictureBoxInformation.Image = Properties.Resources.arrowLeftDark;

            labelMyInformation.ForeColor = Color.White;
            labelMyInformation.BackColor = Color.Black;

            labelConfirm.BackColor = Color.Black;
            labelCannotConfirmMail.BackColor = Color.Black;

            labelPatientTcNo.ForeColor = Color.White;
            labelPatientTcNo.BackColor = Color.Black;

            labelPatientInformationName.ForeColor = Color.White;
            labelPatientInformationName.BackColor = Color.Black;

            labelPatientInformationSurname.ForeColor = Color.White;
            labelPatientInformationSurname.BackColor = Color.Black;

            labelPatientPhoneNumber.ForeColor = Color.White;
            labelPatientPhoneNumber.BackColor = Color.Black;

            labelPatientGender.ForeColor = Color.White;
            labelPatientGender.BackColor = Color.Black;

            labelPatientInformationMail.ForeColor = Color.White;
            labelPatientInformationMail.BackColor = Color.Black;

            labelPatientDate.ForeColor = Color.White;
            labelPatientDate.BackColor = Color.Black;

            labelPatientPassword.ForeColor = Color.White;
            labelPatientPassword.BackColor = Color.Black;
        }
        private void panelMyInformationOtherCloseProperty()
        {
            pictureBoxInformation.Image = Properties.Resources.arrowLeft;

            labelMyInformation.ForeColor = Color.Black;
            labelMyInformation.BackColor = Color.White;

            labelConfirm.BackColor = Color.White;
            labelCannotConfirmMail.BackColor = Color.White;

            labelPatientTcNo.ForeColor = Color.Black;
            labelPatientTcNo.BackColor = Color.White;

            labelPatientInformationName.ForeColor = Color.Black;
            labelPatientInformationName.BackColor = Color.White;

            labelPatientInformationSurname.ForeColor = Color.Black;
            labelPatientInformationSurname.BackColor = Color.White;

            labelPatientPhoneNumber.ForeColor = Color.Black;
            labelPatientPhoneNumber.BackColor = Color.White;

            labelPatientGender.ForeColor = Color.Black;
            labelPatientGender.BackColor = Color.White;

            labelPatientInformationMail.ForeColor = Color.Black;
            labelPatientInformationMail.BackColor = Color.White;

            labelPatientDate.ForeColor = Color.Black;
            labelPatientDate.BackColor = Color.White;

            labelPatientPassword.ForeColor = Color.Black;
            labelPatientPassword.BackColor = Color.White;
        }
        private void panelInformationButtonOpenProperty()
        {
            btn_Account.IdleFillColor = Color.Black;
            btn_Account.IdleBorderColor = Color.Black;

            btn_Account.ForeColor = Color.White;
            btn_Account.BackColor = Color.Black;

            btn_Account.onHoverState.BorderColor = Color.Black;
            btn_Account.onHoverState.FillColor = Color.Black;
            btn_Account.onHoverState.ForeColor = Color.Red;

            btn_Account.OnIdleState.BorderColor = Color.Black;
            btn_Account.OnIdleState.BorderColor = Color.Black;
            btn_Account.OnIdleState.ForeColor = Color.White;

            btn_Account.OnPressedState.BorderColor = Color.Black;
            btn_Account.OnPressedState.FillColor = Color.Black;
            btn_Account.OnPressedState.ForeColor = Color.Red;

            btn_Account.IdleIconLeftImage = Properties.Resources.identity_cardDark;



            btn_ActivePastAppointment.IdleFillColor = Color.Black;
            btn_ActivePastAppointment.IdleBorderColor = Color.Black;

            btn_ActivePastAppointment.ForeColor = Color.White;
            btn_ActivePastAppointment.BackColor = Color.Black;

            btn_ActivePastAppointment.onHoverState.BorderColor = Color.Black;
            btn_ActivePastAppointment.onHoverState.FillColor = Color.Black;
            btn_ActivePastAppointment.onHoverState.ForeColor = Color.Red;

            btn_ActivePastAppointment.OnIdleState.BorderColor = Color.Black;
            btn_ActivePastAppointment.OnIdleState.BorderColor = Color.Black;
            btn_ActivePastAppointment.OnIdleState.ForeColor = Color.White;

            btn_ActivePastAppointment.OnPressedState.BorderColor = Color.Black;
            btn_ActivePastAppointment.OnPressedState.FillColor = Color.Black;
            btn_ActivePastAppointment.OnPressedState.ForeColor = Color.Red;

            btn_ActivePastAppointment.IdleIconLeftImage = Properties.Resources.calendarInformationDark;


            btn_Exit.IdleFillColor = Color.Black;
            btn_Exit.IdleBorderColor = Color.Black;

            btn_Exit.ForeColor = Color.White;
            btn_Exit.BackColor = Color.Black;

            btn_Exit.onHoverState.BorderColor = Color.Black;
            btn_Exit.onHoverState.FillColor = Color.Black;
            btn_Exit.onHoverState.ForeColor = Color.Red;

            btn_Exit.OnIdleState.BorderColor = Color.Black;
            btn_Exit.OnIdleState.BorderColor = Color.Black;
            btn_Exit.OnIdleState.ForeColor = Color.White;

            btn_Exit.OnPressedState.BorderColor = Color.Black;
            btn_Exit.OnPressedState.FillColor = Color.Black;
            btn_Exit.OnPressedState.ForeColor = Color.Red;

            btn_Exit.IdleIconLeftImage = Properties.Resources.alternative_exit;
        }
        private void panelInformationButtonCloseProperty()
        {
            btn_Account.IdleFillColor = Color.White;
            btn_Account.IdleBorderColor = Color.White;

            btn_Account.ForeColor = Color.Black;
            btn_Account.BackColor = Color.White;

            btn_Account.onHoverState.BorderColor = Color.White;
            btn_Account.onHoverState.FillColor = Color.White;
            btn_Account.onHoverState.ForeColor = Color.Red;

            btn_Account.OnIdleState.BorderColor = Color.White;
            btn_Account.OnIdleState.BorderColor = Color.White;
            btn_Account.OnIdleState.ForeColor = Color.Black;

            btn_Account.OnPressedState.BorderColor = Color.White;
            btn_Account.OnPressedState.FillColor = Color.White;
            btn_Account.OnPressedState.ForeColor = Color.Red;

            btn_Account.IdleIconLeftImage = Properties.Resources.identity_cardWhite;



            btn_ActivePastAppointment.IdleFillColor = Color.White;
            btn_ActivePastAppointment.IdleBorderColor = Color.White;

            btn_ActivePastAppointment.ForeColor = Color.Black;
            btn_ActivePastAppointment.BackColor = Color.White;

            btn_ActivePastAppointment.onHoverState.BorderColor = Color.White;
            btn_ActivePastAppointment.onHoverState.FillColor = Color.White;
            btn_ActivePastAppointment.onHoverState.ForeColor = Color.Red;

            btn_ActivePastAppointment.OnIdleState.BorderColor = Color.White;
            btn_ActivePastAppointment.OnIdleState.BorderColor = Color.White;
            btn_ActivePastAppointment.OnIdleState.ForeColor = Color.Black;

            btn_ActivePastAppointment.OnPressedState.BorderColor = Color.White;
            btn_ActivePastAppointment.OnPressedState.FillColor = Color.White;
            btn_ActivePastAppointment.OnPressedState.ForeColor = Color.Red;

            btn_ActivePastAppointment.IdleIconLeftImage = Properties.Resources.calendarInformation;



            btn_Exit.IdleFillColor = Color.White;
            btn_Exit.IdleBorderColor = Color.White;

            btn_Exit.ForeColor = Color.Black;
            btn_Exit.BackColor = Color.White;

            btn_Exit.onHoverState.BorderColor = Color.White;
            btn_Exit.onHoverState.FillColor = Color.White;
            btn_Exit.onHoverState.ForeColor = Color.Red;

            btn_Exit.OnIdleState.BorderColor = Color.White;
            btn_Exit.OnIdleState.BorderColor = Color.White;
            btn_Exit.OnIdleState.ForeColor = Color.Black;

            btn_Exit.OnPressedState.BorderColor = Color.White;
            btn_Exit.OnPressedState.FillColor = Color.White;
            btn_Exit.OnPressedState.ForeColor = Color.Red;

            btn_Exit.IdleIconLeftImage = Properties.Resources.alternative_exitWhite;
        }
        private void panelPastAppointmentPropertyOpen()
        {
            panelPastAppointment.BackColor = Color.FromArgb(38, 38, 38);

            panelPastAppointmentBorder.BackgroundColor = Color.Black;
            panelPastAppointmentBorder.BorderColor = Color.White;

            dataGridPastAppointment.BackgroundColor = Color.Black;
            dataGridPastAppointment.GridColor = Color.White;

            dataGridPastAppointment.DefaultCellStyle.BackColor = Color.Black;
            dataGridPastAppointment.DefaultCellStyle.ForeColor = Color.White;
            dataGridPastAppointment.DefaultCellStyle.SelectionBackColor = Color.Black;
            dataGridPastAppointment.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridPastAppointment.RowsDefaultCellStyle.ForeColor = Color.White;
            dataGridPastAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            panelPast1.BackColor = Color.Black;
            panelPast2.BackColor = Color.Black;
            panelPast3.BackColor = Color.Black;
            panelPast4.BackColor = Color.Black;
            panelPast5.BackColor = Color.Black;

            pictureBoxPastAppointment1.Image = Properties.Resources.price_tagDark;
            pictureBoxPastAppointment2.Image = Properties.Resources.price_tagDark;
            pictureBoxPastAppointment3.Image = Properties.Resources.price_tagDark;
            pictureBoxPastAppointment4.Image = Properties.Resources.price_tagDark;
            pictureBoxPastAppointment5.Image = Properties.Resources.price_tagDark;

            pictureBoxPastAppointment.BackColor = Color.Black;

            labelPastAppointment.BackColor = Color.Black;
            labelPastAppointment.ForeColor = Color.White;
        }
        private void panelPastAppointmentPropertyClose()
        {
            panelPastAppointment.BackColor = Color.White;

            panelPastAppointmentBorder.BackgroundColor = Color.White;
            panelPastAppointmentBorder.BorderColor = Color.FromArgb(232, 232, 232);

            dataGridPastAppointment.BackgroundColor = Color.White;
            dataGridPastAppointment.GridColor = Color.Black;

            dataGridPastAppointment.DefaultCellStyle.BackColor = Color.White;
            dataGridPastAppointment.DefaultCellStyle.ForeColor = Color.Black;
            dataGridPastAppointment.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridPastAppointment.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridPastAppointment.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridPastAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            panelPast1.BackColor = Color.White;
            panelPast2.BackColor = Color.White;
            panelPast3.BackColor = Color.White;
            panelPast4.BackColor = Color.White;
            panelPast5.BackColor = Color.White;

            pictureBoxPastAppointment1.Image = Properties.Resources.price_tag;
            pictureBoxPastAppointment2.Image = Properties.Resources.price_tag;
            pictureBoxPastAppointment3.Image = Properties.Resources.price_tag;
            pictureBoxPastAppointment4.Image = Properties.Resources.price_tag;
            pictureBoxPastAppointment5.Image = Properties.Resources.price_tag;

            pictureBoxPastAppointment.BackColor = Color.White;

            labelPastAppointment.BackColor = Color.White;
            labelPastAppointment.BackColor = Color.Black;
        }

        private void panelActiveAppointmentPropertyOpen()
        {
            pictureBoxLoadingActiveAppointmentNotFound.BackColor = Color.Black;

            panelActiveAppointment.BackColor = Color.FromArgb(38, 38, 38);
            panelActiveAppointmentBorder.BackgroundColor = Color.Black;
            panelActiveAppointmentBorder.BorderColor = Color.White;

            dataGridActiveAppointment.BackgroundColor = Color.Black;
            dataGridActiveAppointment.GridColor = Color.White;

            dataGridActiveAppointment.DefaultCellStyle.BackColor = Color.Black;
            dataGridActiveAppointment.DefaultCellStyle.ForeColor = Color.White;
            dataGridActiveAppointment.DefaultCellStyle.SelectionBackColor = Color.Black;
            dataGridActiveAppointment.DefaultCellStyle.SelectionForeColor = Color.White;


            dataGridActiveAppointment.RowsDefaultCellStyle.ForeColor = Color.White;
            dataGridActiveAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            panelActive1.BackColor = Color.Black;
            panelActive2.BackColor = Color.Black;
            panelActive3.BackColor = Color.Black;
            panelActive4.BackColor = Color.Black;
            panelActive5.BackColor = Color.Black;

            pictureBoxActive1.Image = Properties.Resources.price_tagDark;
            pictureBoxActive2.Image = Properties.Resources.price_tagDark;
            pictureBoxActive3.Image = Properties.Resources.price_tagDark;
            pictureBoxActive4.Image = Properties.Resources.price_tagDark;
            pictureBoxActive5.Image = Properties.Resources.price_tagDark;

            pictureBoxActiveAppointmentNotFound.BackColor = Color.Black;

            labelAppointmentActiveNotFound.BackColor = Color.Black;
            labelAppointmentActiveNotFound.ForeColor = Color.White;
        }
        private void panelActiveAppointmentPropertyClose()
        {
            pictureBoxLoadingActiveAppointmentNotFound.BackColor = Color.White;

            panelActiveAppointment.BackColor = Color.White;
            panelActiveAppointmentBorder.BackgroundColor = Color.White;
            panelActiveAppointmentBorder.BorderColor = Color.Black;

            dataGridActiveAppointment.BackgroundColor = Color.White;
            dataGridActiveAppointment.GridColor = Color.FromArgb(232, 232, 232);

            dataGridActiveAppointment.DefaultCellStyle.BackColor = Color.White;
            dataGridActiveAppointment.DefaultCellStyle.ForeColor = Color.Black;
            dataGridActiveAppointment.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridActiveAppointment.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridActiveAppointment.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridActiveAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            panelActive1.BackColor = Color.White;
            panelActive2.BackColor = Color.White;
            panelActive3.BackColor = Color.White;
            panelActive4.BackColor = Color.White;
            panelActive5.BackColor = Color.White;

            pictureBoxActive1.Image = Properties.Resources.price_tag;
            pictureBoxActive2.Image = Properties.Resources.price_tag;
            pictureBoxActive3.Image = Properties.Resources.price_tag;
            pictureBoxActive4.Image = Properties.Resources.price_tag;
            pictureBoxActive5.Image = Properties.Resources.price_tag;

            pictureBoxActiveAppointmentNotFound.BackColor = Color.White;

            labelAppointmentActiveNotFound.BackColor = Color.White;
            labelAppointmentActiveNotFound.ForeColor = Color.Black;
        }

        private void panelActiveAllAppointmentOpenProperty()
        {
            pictureBoxDashboardOpen.Image = Properties.Resources.arrowLeftDark;
            labelAllActiveDashboard.BackColor = Color.Black;

            labelAllPageNumber.BackColor = Color.Black;

            panelAllActiveAppointmentBorder.BackgroundColor = Color.Black;
            panelAllActiveAppointmentBorder.BorderColor = Color.White;

            dataGridAllActiveAppointment.BackgroundColor = Color.Black;
            dataGridAllActiveAppointment.GridColor = Color.White;

            dataGridAllActiveAppointment.DefaultCellStyle.BackColor = Color.Black;
            dataGridAllActiveAppointment.DefaultCellStyle.ForeColor = Color.White;
            dataGridAllActiveAppointment.DefaultCellStyle.SelectionBackColor = Color.Black;
            dataGridAllActiveAppointment.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridAllActiveAppointment.RowsDefaultCellStyle.ForeColor = Color.White;
            dataGridAllActiveAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            panelAllActive1.BackColor = Color.Black;
            panelAllActive2.BackColor = Color.Black;
            panelAllActive3.BackColor = Color.Black;
            panelAllActive4.BackColor = Color.Black;
            panelAllActive5.BackColor = Color.Black;

            pictureBoxAllActive1.Image = Properties.Resources.price_tagDark;
            pictureBoxAllActive2.Image = Properties.Resources.price_tagDark;
            pictureBoxAllActive3.Image = Properties.Resources.price_tagDark;
            pictureBoxAllActive4.Image = Properties.Resources.price_tagDark;
            pictureBoxAllActive5.Image = Properties.Resources.price_tagDark;

            pictureBoxAllActive.BackColor = Color.Black;

            labelAppointmentIsNot.BackColor = Color.Black;
            labelAppointmentIsNot.ForeColor = Color.White;
        }
        private void panelActiveAllAppointmentCloseProperty()
        {
            pictureBoxDashboardOpen.Image = Properties.Resources.arrowLeft;
            labelAllActiveDashboard.ForeColor = Color.Black;
            labelAllActiveDashboard.BackColor = Color.White;

            panelAllActiveAppointmentBorder.BackgroundColor = Color.White;
            panelAllActiveAppointmentBorder.BorderColor = Color.FromArgb(232, 232, 232);

            dataGridAllActiveAppointment.BackgroundColor = Color.White;
            dataGridAllActiveAppointment.GridColor = Color.FromArgb(232, 232, 232);

            dataGridAllActiveAppointment.DefaultCellStyle.BackColor = Color.White;
            dataGridAllActiveAppointment.DefaultCellStyle.ForeColor = Color.Black;
            dataGridAllActiveAppointment.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridAllActiveAppointment.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAllActiveAppointment.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridAllActiveAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            panelAllActive1.BackColor = Color.White;
            panelAllActive2.BackColor = Color.White;
            panelAllActive3.BackColor = Color.White;
            panelAllActive4.BackColor = Color.White;
            panelAllActive5.BackColor = Color.White;

            pictureBoxAllActive1.Image = Properties.Resources.price_tag;
            pictureBoxAllActive2.Image = Properties.Resources.price_tag;
            pictureBoxAllActive3.Image = Properties.Resources.price_tag;
            pictureBoxAllActive4.Image = Properties.Resources.price_tag;
            pictureBoxAllActive5.Image = Properties.Resources.price_tag;

            pictureBoxAllActive.BackColor = Color.White;

            labelAppointmentIsNot.BackColor = Color.White;
            labelAppointmentIsNot.ForeColor = Color.Black;
        }
        private void panelPastAllAppointmentOpenProperty()
        {
            labelAllPageNumber.ForeColor = Color.White;
            labelAllPageNumber.BackColor = Color.Black;

            panelAllPastAppointmentBorder.BackgroundColor = Color.Black;
            panelAllPastAppointmentBorder.BorderColor = Color.White;

            dataGridAllPastAppointment.BackgroundColor = Color.Black;
            dataGridAllPastAppointment.GridColor = Color.White;

            dataGridAllPastAppointment.DefaultCellStyle.BackColor = Color.Black;
            dataGridAllPastAppointment.DefaultCellStyle.ForeColor = Color.White;
            dataGridAllPastAppointment.DefaultCellStyle.SelectionBackColor = Color.Black;
            dataGridAllPastAppointment.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridAllPastAppointment.RowsDefaultCellStyle.ForeColor = Color.White;
            dataGridAllPastAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            panelAllPast1.BackColor = Color.Black;
            panelAllPast2.BackColor = Color.Black;
            panelAllPast3.BackColor = Color.Black;
            panelAllPast4.BackColor = Color.Black;
            panelAllPast5.BackColor = Color.Black;

            pictureBoxAllPast1.Image = Properties.Resources.price_tagDark;
            pictureBoxAllPast2.Image = Properties.Resources.price_tagDark;
            pictureBoxAllPast3.Image = Properties.Resources.price_tagDark;
            pictureBoxAllPast4.Image = Properties.Resources.price_tagDark;
            pictureBoxAllPast5.Image = Properties.Resources.price_tagDark;

            pictureBoxAllPastNotAppointment.BackColor = Color.Black;

            labelNotAppointment.BackColor = Color.Black;
            labelNotAppointment.ForeColor = Color.White;
        }
        private void panelPastAllAppointmentCloseProperty()
        {
            labelAllPageNumber.ForeColor = Color.Black;
            labelAllPageNumber.BackColor = Color.White;

            panelAllPastAppointmentBorder.BackgroundColor = Color.White;
            panelAllPastAppointmentBorder.BorderColor = Color.FromArgb(232, 232, 232);

            dataGridAllPastAppointment.BackgroundColor = Color.White;
            dataGridAllPastAppointment.GridColor = Color.FromArgb(232, 232, 232);

            dataGridAllPastAppointment.DefaultCellStyle.BackColor = Color.White;
            dataGridAllPastAppointment.DefaultCellStyle.ForeColor = Color.Black;
            dataGridAllPastAppointment.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridAllPastAppointment.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridAllPastAppointment.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridAllPastAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            panelAllPast1.BackColor = Color.White;
            panelAllPast2.BackColor = Color.White;
            panelAllPast3.BackColor = Color.White;
            panelAllPast4.BackColor = Color.White;
            panelAllPast5.BackColor = Color.White;

            pictureBoxAllPast1.Image = Properties.Resources.price_tag;
            pictureBoxAllPast2.Image = Properties.Resources.price_tag;
            pictureBoxAllPast3.Image = Properties.Resources.price_tag;
            pictureBoxAllPast4.Image = Properties.Resources.price_tag;
            pictureBoxAllPast5.Image = Properties.Resources.price_tag;

            pictureBoxAllPastNotAppointment.BackColor = Color.White;

            labelNotAppointment.BackColor = Color.White;
            labelNotAppointment.ForeColor = Color.Black;
        }

        private void panelPastAllPageSliderOpenProperty()
        {
            pictureBoxAllPastLeft.Image = Properties.Resources.left_arrowDark;
            pictureBoxAllPastRight.Image = Properties.Resources.right_arrowDark;

            picturePastFirstDot.Image = Properties.Resources.moreDark;
            pictureBoxPastSecondDot.Image = Properties.Resources.moreDark;

            labelPast1.ForeColor = Color.White;
            labelPast1.BackColor = Color.FromArgb(38, 38, 38);

            labelPast2.ForeColor = Color.White;
            labelPast2.BackColor = Color.FromArgb(38, 38, 38);

            labelPast3.ForeColor = Color.White;
            labelPast3.BackColor = Color.FromArgb(38, 38, 38);

            labelPast4.ForeColor = Color.White;
            labelPast4.BackColor = Color.FromArgb(38, 38, 38);

            labelPast5.ForeColor = Color.White;
            labelPast5.BackColor = Color.FromArgb(38, 38, 38);
        }
        private void panelPastAllPageSliderCloseProperty()
        {
            pictureBoxAllPastLeft.Image = Properties.Resources.left_arrow;
            pictureBoxAllPastRight.Image = Properties.Resources.right_arrow;

            picturePastFirstDot.Image = Properties.Resources.more;
            pictureBoxPastSecondDot.Image = Properties.Resources.more;

            labelPast1.ForeColor = Color.Black;
            labelPast1.BackColor = Color.White;

            labelPast2.ForeColor = Color.Black;
            labelPast2.BackColor = Color.White;

            labelPast3.ForeColor = Color.Black;
            labelPast3.BackColor = Color.White;

            labelPast4.ForeColor = Color.Black;
            labelPast4.BackColor = Color.White;

            labelPast5.ForeColor = Color.Black;
            labelPast5.BackColor = Color.White;
        }

        private void panelDarkModeSettingOpenProperty()
        {
            pictureBoxDashboard.Image = Properties.Resources.arrowLeftDark;

            labelSettings.ForeColor = Color.White;
            labelSettings.BackColor = Color.Black;

            labelTitle.ForeColor = Color.White;
            labelTitle.BackColor = Color.FromArgb(28, 46, 54);

            labelDarkModeWarning.BackColor = Color.FromArgb(28, 46, 54);

            panelDark.BackgroundColor = Color.FromArgb(28, 46, 54);
            panelDark.BorderColor = Color.FromArgb(57, 83, 99);

            panelModPicture.BackgroundColor = Color.FromArgb(28, 46, 54);
            panelModPicture.BorderColor = Color.FromArgb(204, 204, 204);

            pictureBoxMod.Image = Properties.Resources.KoyuMod;
        }
        private void panelDarkModeSettingCloseProperty()
        {
            pictureBoxDashboard.Image = Properties.Resources.arrowLeft;

            labelSettings.ForeColor = Color.Black;
            labelSettings.BackColor = Color.White;

            labelTitle.ForeColor = Color.Black;
            labelTitle.BackColor = Color.FromArgb(230, 247, 255);

            labelDarkModeWarning.BackColor = Color.FromArgb(230, 247, 255);

            panelDark.BackgroundColor = Color.FromArgb(230, 247, 255);
            panelDark.BorderColor = Color.FromArgb(145, 213, 255);

            panelModPicture.BackgroundColor = Color.FromArgb(230, 247, 255);
            panelModPicture.BorderColor = Color.FromArgb(204, 204, 204);

            pictureBoxMod.Image = Properties.Resources.AçıkMod;

        }
        private void panelAppointmentChooseOpenProperty()
        {
            pictureBoxAppointmentChoose.Image = Properties.Resources.arrowLeftDark;

            labelAppointmentChoose.ForeColor = Color.White;
            labelAppointmentChoose.BackColor = Color.Black;

            labelPoliclinicRequiredCheck.BackColor = Color.Black;

            labelPoliclinic.ForeColor = Color.White;
            labelPoliclinic.BackColor = Color.Black;

            labelDoctor.ForeColor = Color.White;
            labelDoctor.BackColor = Color.Black;

            labelStartTimeChoose.ForeColor = Color.White;
            labelStartTimeChoose.BackColor = Color.Black;

            labelEndTimeChoose.ForeColor = Color.White;
            labelEndTimeChoose.BackColor = Color.Black;

            labelNotDayOfWeek.BackColor = Color.Black;

            datePickerStartTime.BackColor = Color.Transparent;
            datePickerStartTime.Color = Color.White;
            datePickerStartTime.IconColor = Color.Gray;
            datePickerStartTime.ForeColor = Color.Black;

            labeldatePickerStart.BackColor = Color.White;
            labeldatePickerStart.ForeColor = Color.Gray;

            datePickerEndTime.BackColor = Color.Transparent;
            datePickerEndTime.Color = Color.White;
            datePickerEndTime.IconColor = Color.Gray;
            datePickerEndTime.ForeColor = Color.Black;

            labeldatePickerEnd.BackColor = Color.White;
            labeldatePickerEnd.ForeColor = Color.Gray;
        }

        private void panelAppointmentChooseCloseProperty()
        {
            pictureBoxAppointmentChoose.Image = Properties.Resources.arrowLeft;

            labelAppointmentChoose.ForeColor = Color.Black;
            labelAppointmentChoose.BackColor = Color.White;

            labelPoliclinicRequiredCheck.BackColor = Color.White;

            labelPoliclinic.ForeColor = Color.Black;
            labelPoliclinic.BackColor = Color.White;

            labelDoctor.ForeColor = Color.Black;
            labelDoctor.BackColor = Color.White;

            labelStartTimeChoose.ForeColor = Color.Black;
            labelStartTimeChoose.BackColor = Color.White;

            labelEndTimeChoose.ForeColor = Color.Black;
            labelEndTimeChoose.BackColor = Color.White;

            labelNotDayOfWeek.BackColor = Color.White;

            datePickerStartTime.BackColor = Color.WhiteSmoke;
            datePickerStartTime.Color = Color.WhiteSmoke;
            datePickerStartTime.IconColor = Color.Gray;
            datePickerStartTime.ForeColor = Color.Gray;

            labeldatePickerStart.BackColor = Color.WhiteSmoke;
            labeldatePickerStart.ForeColor = Color.Gray;

            datePickerEndTime.BackColor = Color.WhiteSmoke;
            datePickerEndTime.Color = Color.WhiteSmoke;
            datePickerEndTime.IconColor = Color.Gray;
            datePickerEndTime.ForeColor = Color.Gray;

            labeldatePickerEnd.BackColor = Color.WhiteSmoke;
            labeldatePickerEnd.ForeColor = Color.Gray;
        }
        private void panelAppointmentListOpenProperty()
        {
            pictureBoxPoliclinicChoose.Image = Properties.Resources.arrowLeftDark;

            labelPoliclinicChoose.ForeColor = Color.White;
            labelPoliclinicChoose.BackColor = Color.Black;

            labelPageNumber.ForeColor = Color.White;
            labelPageNumber.BackColor = Color.Black;

            PaneLBorder.BorderColor = Color.White;
            PaneLBorder.BackgroundColor = Color.Black;

            labelEarlyTime1.ForeColor = Color.White;
            labelEarlyTime1.BackColor = Color.Black;

            labelEarlyTime2.ForeColor = Color.White;
            labelEarlyTime2.BackColor = Color.Black;

            labelEarlyTime3.ForeColor = Color.White;
            labelEarlyTime3.BackColor = Color.Black;

            labelEarlyTime4.ForeColor = Color.White;
            labelEarlyTime4.BackColor = Color.Black;

            labelEarlyTime5.ForeColor = Color.White;
            labelEarlyTime5.BackColor = Color.Black;

            datagridAppointment.DefaultCellStyle.ForeColor = Color.White;
            datagridAppointment.DefaultCellStyle.BackColor = Color.Black;
            datagridAppointment.DefaultCellStyle.SelectionForeColor = Color.White;
            datagridAppointment.DefaultCellStyle.SelectionBackColor = Color.Black;
            datagridAppointment.GridColor = Color.White;

            datagridAppointment.RowsDefaultCellStyle.ForeColor = Color.White;
            datagridAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            pictureBoxLeftArrow.Image = Properties.Resources.left_arrowDark;
            pictureBoxRightArrow.Image = Properties.Resources.right_arrowDark;

            pictureBoxFirst3Dot.Image = Properties.Resources.moreDark;
            pictureBoxSecondDot.Image = Properties.Resources.moreDark;

            labelPage1.ForeColor = Color.White;
            labelPage1.BackColor = Color.FromArgb(26, 26, 26);

            labelPage2.ForeColor = Color.White;
            labelPage2.BackColor = Color.FromArgb(26, 26, 26);

            labelPage3.ForeColor = Color.White;
            labelPage3.BackColor = Color.FromArgb(26, 26, 26);

            labelPage4.ForeColor = Color.White;
            labelPage4.BackColor = Color.FromArgb(26, 26, 26);

            labelPage5.ForeColor = Color.White;
            labelPage5.BackColor = Color.FromArgb(26, 26, 26);
        }
        private void panelAppointmentListCloseProperty()
        {
            pictureBoxPoliclinicChoose.Image = Properties.Resources.arrowLeft;

            labelPoliclinicChoose.ForeColor = Color.Black;
            labelPoliclinicChoose.BackColor = Color.White;

            labelPageNumber.ForeColor = Color.Black;
            labelPageNumber.BackColor = Color.White;

            PaneLBorder.BorderColor = Color.FromArgb(232, 232, 232);
            PaneLBorder.BackgroundColor = Color.White;

            labelEarlyTime1.ForeColor = Color.Black;
            labelEarlyTime1.BackColor = Color.White;

            labelEarlyTime2.ForeColor = Color.Black;
            labelEarlyTime2.BackColor = Color.White;

            labelEarlyTime3.ForeColor = Color.Black;
            labelEarlyTime3.BackColor = Color.White;

            labelEarlyTime4.ForeColor = Color.Black;
            labelEarlyTime4.BackColor = Color.White;

            labelEarlyTime5.ForeColor = Color.Black;
            labelEarlyTime5.BackColor = Color.White;

            datagridAppointment.DefaultCellStyle.ForeColor = Color.Black;
            datagridAppointment.DefaultCellStyle.BackColor = Color.White;
            datagridAppointment.DefaultCellStyle.SelectionForeColor = Color.Black;
            datagridAppointment.DefaultCellStyle.SelectionBackColor = Color.White;
            datagridAppointment.GridColor = Color.FromArgb(232, 232, 232);

            datagridAppointment.RowsDefaultCellStyle.ForeColor = Color.Black;
            datagridAppointment.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            pictureBoxLeftArrow.Image = Properties.Resources.left_arrow;
            pictureBoxRightArrow.Image = Properties.Resources.right_arrow;

            pictureBoxFirst3Dot.Image = Properties.Resources.more;
            pictureBoxSecondDot.Image = Properties.Resources.more;

            labelPage1.ForeColor = Color.Black;
            labelPage1.BackColor = Color.White;

            labelPage2.ForeColor = Color.Black;
            labelPage2.BackColor = Color.White;

            labelPage3.ForeColor = Color.Black;
            labelPage3.BackColor = Color.White;

            labelPage4.ForeColor = Color.Black;
            labelPage4.BackColor = Color.White;

            labelPage5.ForeColor = Color.Black;
            labelPage5.BackColor = Color.White;
        }

        private void panelDoctorAppointmentChooseOpenProperty()
        {
            pictureBoxAppointmentList.Image = Properties.Resources.left_arrowDark;

            labelAppointmentList.ForeColor = Color.White;
            labelAppointmentList.BackColor = Color.Black;

            labelChooseDoctorName.ForeColor = Color.White;
            labelChooseDoctorName.BackColor = Color.Black;

            labelDoctorName.ForeColor = Color.White;
            labelDoctorName.BackColor = Color.Black;

            progressBarNull.BorderColor = Color.White;

            btn_EightClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_EightClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_EightClock.ForeColor = Color.White;
            btn_EightClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_EightClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_EightClock.onHoverState.ForeColor = Color.White;
            btn_EightClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_EightClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_EightClock.OnIdleState.ForeColor = Color.White;
            btn_EightClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_EightClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_EightClock.OnPressedState.ForeColor = Color.White;

            btn_NineClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_NineClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_NineClock.ForeColor = Color.White;
            btn_NineClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_NineClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_NineClock.onHoverState.ForeColor = Color.White;
            btn_NineClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_NineClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_NineClock.OnIdleState.ForeColor = Color.White;
            btn_NineClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_NineClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_NineClock.OnPressedState.ForeColor = Color.White;

            btn_TenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_TenClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_TenClock.ForeColor = Color.White;
            btn_TenClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_TenClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_TenClock.onHoverState.ForeColor = Color.White;
            btn_TenClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_TenClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_TenClock.OnIdleState.ForeColor = Color.White;
            btn_TenClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_TenClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_TenClock.OnPressedState.ForeColor = Color.White;

            btn_ElevenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_ElevenClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_ElevenClock.ForeColor = Color.White;
            btn_ElevenClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_ElevenClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_ElevenClock.onHoverState.ForeColor = Color.White;
            btn_ElevenClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_ElevenClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_ElevenClock.OnIdleState.ForeColor = Color.White;
            btn_ElevenClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_ElevenClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_ElevenClock.OnPressedState.ForeColor = Color.White;

            btn_ThirteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_ThirteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_ThirteenClock.ForeColor = Color.White;
            btn_ThirteenClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_ThirteenClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_ThirteenClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_ThirteenClock.onHoverState.ForeColor = Color.White;
            btn_ThirteenClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_ThirteenClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_ThirteenClock.OnIdleState.ForeColor = Color.White;
            btn_ThirteenClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_ThirteenClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_ThirteenClock.OnPressedState.ForeColor = Color.White;

            btn_FourteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_FourteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_FourteenClock.ForeColor = Color.White;
            btn_FourteenClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_FourteenClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_FourteenClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_FourteenClock.onHoverState.ForeColor = Color.White;
            btn_FourteenClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_FourteenClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_FourteenClock.OnIdleState.ForeColor = Color.White;
            btn_FourteenClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_FourteenClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_FourteenClock.OnPressedState.ForeColor = Color.White;

            btn_FifteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_FifteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_FifteenClock.ForeColor = Color.White;
            btn_FifteenClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_FifteenClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_FifteenClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_FifteenClock.onHoverState.ForeColor = Color.White;
            btn_FifteenClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_FifteenClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_FifteenClock.OnIdleState.ForeColor = Color.White;
            btn_FifteenClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_FifteenClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_FifteenClock.OnPressedState.ForeColor = Color.White;

            btn_SixteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_SixteenClock.IdleFillColor = Color.FromArgb(64, 64, 64);
            btn_SixteenClock.ForeColor = Color.White;
            btn_SixteenClock.IdleBorderColor = Color.FromArgb(109, 109, 109);
            btn_SixteenClock.onHoverState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_SixteenClock.onHoverState.FillColor = Color.FromArgb(64, 64, 64);
            btn_SixteenClock.onHoverState.ForeColor = Color.White;
            btn_SixteenClock.OnIdleState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_SixteenClock.OnIdleState.FillColor = Color.FromArgb(64, 64, 64);
            btn_SixteenClock.OnIdleState.ForeColor = Color.White;
            btn_SixteenClock.OnPressedState.BorderColor = Color.FromArgb(109, 109, 109);
            btn_SixteenClock.OnPressedState.FillColor = Color.FromArgb(64, 64, 64);
            btn_SixteenClock.OnPressedState.ForeColor = Color.White;

            panelEightClock.BorderColor = Color.FromArgb(109, 109, 109);
            panelNineClock.BorderColor = Color.FromArgb(109, 109, 109);
            panelTenClock.BorderColor = Color.FromArgb(109, 109, 109);
            panelElevenClock.BorderColor = Color.FromArgb(109, 109, 109);
            panelThirteenClock.BorderColor = Color.FromArgb(109, 109, 109);
            panelFourTeenClock.BorderColor = Color.FromArgb(109, 109, 109);
            panelFifteenClock.BorderColor = Color.FromArgb(109, 109, 109);
            panelSixTeenClock.BorderColor = Color.FromArgb(109, 109, 109);
        }
        private void panelDoctorAppointmentChooseCloseProperty()
        {
            pictureBoxAppointmentList.Image = Properties.Resources.left_arrow;

            labelAppointmentList.ForeColor = Color.Black;
            labelAppointmentList.BackColor = Color.White;

            labelChooseDoctorName.ForeColor = Color.Black;
            labelChooseDoctorName.BackColor = Color.White;

            labelDoctorName.ForeColor = Color.Black;
            labelDoctorName.BackColor = Color.White;

            progressBarNull.BorderColor = Color.FromArgb(232, 232, 232);

            btn_EightClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_EightClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_EightClock.ForeColor = Color.Black;
            btn_EightClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_EightClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_EightClock.onHoverState.ForeColor = Color.Black;
            btn_EightClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_EightClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_EightClock.OnIdleState.ForeColor = Color.Black;
            btn_EightClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_EightClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_EightClock.OnPressedState.ForeColor = Color.Black;

            btn_NineClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_NineClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_NineClock.ForeColor = Color.Black;
            btn_NineClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_NineClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_NineClock.onHoverState.ForeColor = Color.Black;
            btn_NineClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_NineClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_NineClock.OnIdleState.ForeColor = Color.Black;
            btn_NineClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_NineClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_NineClock.OnPressedState.ForeColor = Color.Black;

            btn_TenClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_TenClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_TenClock.ForeColor = Color.Black;
            btn_TenClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_TenClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_TenClock.onHoverState.ForeColor = Color.Black;
            btn_TenClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_TenClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_TenClock.OnIdleState.ForeColor = Color.Black;
            btn_TenClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_TenClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_TenClock.OnPressedState.ForeColor = Color.Black;

            btn_ElevenClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_ElevenClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_ElevenClock.ForeColor = Color.Black;
            btn_ElevenClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_ElevenClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_ElevenClock.onHoverState.ForeColor = Color.Black;
            btn_ElevenClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_ElevenClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_ElevenClock.OnIdleState.ForeColor = Color.Black;
            btn_ElevenClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_ElevenClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_ElevenClock.OnPressedState.ForeColor = Color.Black;

            btn_ThirteenClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_ThirteenClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_ThirteenClock.ForeColor = Color.Black;
            btn_ThirteenClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_ThirteenClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_ThirteenClock.onHoverState.ForeColor = Color.Black;
            btn_ThirteenClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_ThirteenClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_ThirteenClock.OnIdleState.ForeColor = Color.Black;
            btn_ThirteenClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_ThirteenClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_ThirteenClock.OnPressedState.ForeColor = Color.Black;

            btn_FourteenClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_FourteenClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_FourteenClock.ForeColor = Color.Black;
            btn_FourteenClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_FourteenClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_FourteenClock.onHoverState.ForeColor = Color.Black;
            btn_FourteenClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_FourteenClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_FourteenClock.OnIdleState.ForeColor = Color.Black;
            btn_FourteenClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_FourteenClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_FourteenClock.OnPressedState.ForeColor = Color.Black;

            btn_FifteenClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_FifteenClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_FifteenClock.ForeColor = Color.Black;
            btn_FifteenClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_FifteenClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_FifteenClock.onHoverState.ForeColor = Color.Black;
            btn_FifteenClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_FifteenClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_FifteenClock.OnIdleState.ForeColor = Color.Black;
            btn_FifteenClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_FifteenClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_FifteenClock.OnPressedState.ForeColor = Color.Black;

            btn_SixteenClock.IdleFillColor = Color.FromArgb(250, 250, 250);
            btn_SixteenClock.IdleBorderColor = Color.FromArgb(232, 232, 232);
            btn_SixteenClock.ForeColor = Color.Black;
            btn_SixteenClock.onHoverState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_SixteenClock.onHoverState.FillColor = Color.FromArgb(250, 250, 250);
            btn_SixteenClock.onHoverState.ForeColor = Color.Black;
            btn_SixteenClock.OnIdleState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_SixteenClock.OnIdleState.FillColor = Color.FromArgb(250, 250, 250);
            btn_SixteenClock.OnIdleState.ForeColor = Color.Black;
            btn_SixteenClock.OnPressedState.BorderColor = Color.FromArgb(232, 232, 232);
            btn_SixteenClock.OnPressedState.FillColor = Color.FromArgb(250, 250, 250);
            btn_SixteenClock.OnPressedState.ForeColor = Color.Black;

            panelEightClock.BorderColor = Color.FromArgb(232, 232, 232);
            panelNineClock.BorderColor = Color.FromArgb(232, 232, 232);
            panelTenClock.BorderColor = Color.FromArgb(232, 232, 232);
            panelElevenClock.BorderColor = Color.FromArgb(232, 232, 232);
            panelThirteenClock.BorderColor = Color.FromArgb(232, 232, 232);
            panelFourTeenClock.BorderColor = Color.FromArgb(232, 232, 232);
            panelFifteenClock.BorderColor = Color.FromArgb(232, 232, 232);
            panelSixTeenClock.BorderColor = Color.FromArgb(232, 232, 232);
        }
        private void DashboardProperty()
        {
            btn_AccountDetails.Size = new Size(572, 239);
            btn_AppointmentChoose.Size = new Size(572, 239);

            txtActiveAppointment1.Height = 28;
            txtActiveAppointment2.Height = 28;
            txtActiveAppointment3.Height = 28;
            txtActiveAppointment4.Height = 28;
            txtActiveAppointment5.Height = 28;

            txtPastAppointmentDate1.Height = 28;
            txtPastAppointmentDate2.Height = 28;
            txtPastAppointmentDate3.Height = 28;
            txtPastAppointmentDate4.Height = 28;
            txtPastAppointmentDate5.Height = 28;
        }
        private void AppointmentListAllProperty()
        {
            txtActiveAllAppointment1.Height = 28;
            txtActiveAllAppointment2.Height = 28;
            txtActiveAllAppointment3.Height = 28;
            txtActiveAllAppointment4.Height = 28;
            txtActiveAllAppointment5.Height = 28;


            txtAllPastAppointment1.Height = 28;
            txtAllPastAppointment2.Height = 28;
            txtAllPastAppointment3.Height = 28;
            txtAllPastAppointment4.Height = 28;
            txtAllPastAppointment5.Height = 28;
        }
        private void AppointmentAllActivePastCloseProperty()
        {
            //ActiveAppointment

            txtActiveAllAppointment1.Visible = false;
            panelAllActive1.Visible = false;

            txtActiveAllAppointment2.Visible = false;
            panelAllActive2.Visible = false;

            txtActiveAllAppointment3.Visible = false;
            panelAllActive3.Visible = false;

            txtActiveAllAppointment4.Visible = false;
            panelAllActive4.Visible = false;

            txtActiveAllAppointment5.Visible = false;
            panelAllActive5.Visible = false;

            //PastAppointment

            txtAllPastAppointment1.Visible = false;
            panelAllPast1.Visible = false;

            txtAllPastAppointment2.Visible = false;
            panelAllPast2.Visible = false;

            txtAllPastAppointment3.Visible = false;
            panelAllPast3.Visible = false;

            txtAllPastAppointment4.Visible = false;
            panelAllPast4.Visible = false;

            txtAllPastAppointment5.Visible = false;
            panelAllPast5.Visible = false;
        }
        private void AppointmentListProperty()
        {
            txtEarlyTime1.Size = new Size(172, 28);
            txtEartyDay1.Size = new Size(52, 28);

            txtEarlyTime2.Size = new Size(172, 28);
            txtEartyDay2.Size = new Size(52, 28);

            txtEarlyTime3.Size = new Size(172, 28);
            txtEartyDay3.Size = new Size(52, 28);

            txtEarlyTime4.Size = new Size(172, 28);
            txtEartyDay4.Size = new Size(52, 28);

            txtEarlyTime5.Size = new Size(172, 28);
            txtEartyDay5.Size = new Size(52, 28);

        }
        private void CloseListAppointmentPageSlider()
        {
            pictureBoxLeftArrow.Visible = false;
            pictureBoxRightArrow.Visible = false;

            labelPage1.Visible = false;
            labelPage2.Visible = false;
            labelPage3.Visible = false;
            labelPage4.Visible = false;
            labelPage5.Visible = false;

            pictureBoxFirst3Dot.Visible = false;

            pictureBoxSecondDot.Visible = false;
        }
        private void sidebarDarkModeOpenProperty()
        {
            panelTopBar.BackColor = Color.Black;

            pictureBoxSettings.Image = Properties.Resources.settingsDark;

            pictureBoxSaglikBakanligi.Image = Properties.Resources.sb_logo_footer;
        }

        private void sidebarDarkModeCloseProperty()
        {
            panelTopBar.BackColor = Color.FromArgb(255, 255, 255);

            pictureBoxSettings.Image = Properties.Resources.settings;

            pictureBoxSaglikBakanligi.Image = Properties.Resources.mobil_logo;
        }

        private void darkModeOpenProperty()
        {

            pictureBoxMiddleDark.Visible = true;
            pictureBoxMiddleWhite.Visible = false;
        }
        private void darkModeCloseProperty()
        {
            pictureBoxMiddleDark.Visible = false;
            pictureBoxMiddleWhite.Visible = true;
        }

        private void Property()
        {
            dropDownPoliclinic.Height = 44;
            dropDownDoctor.Height = 44;

            datePickerStartTime.Height = 50;
            datePickerEndTime.Height = 50;

            btn_AppointmentSearch.Height = 50;
            btn_AppointmentClear.Height = 50;
        }

        private async Task LoadPoliclinicList()
        {
            dropDownPoliclinic.Text = "";
            panelLoading.BackColor = Color.White;
            panelLoading.Location = new Point(751, 191);
            panelLoading.Visible = true;
            await Task.Delay(1000);
            dropDownPoliclinic.Enabled = false;
            string query = "SELECT Id, [Poliklinik Adı] FROM Policlinics";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dropDownPoliclinic.Items.Clear();

                            while (await reader.ReadAsync())
                            {
                                int poliklinikId = reader.GetInt32(0);
                                string poliklinikAd = reader.GetString(1);
                                dropDownPoliclinic.Items.Add(new PoliclinicDoctorCategory(poliklinikId, poliklinikAd));
                            }

                            dropDownPoliclinic.DisplayMember = "Name";
                            dropDownPoliclinic.ValueMember = "Id";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dropDownPoliclinic.Enabled = true;
            panelLoading.Visible = false;
            dropDownPoliclinic.Text = "Klinik Seçiniz";
        }

        private async Task LoadActiveAppointments(int id)
        {
            string query = @"SELECT p.Id AS RandevuId, p.Date AS RandevuTarihi,pl.[Poliklinik Adı] AS PoliklinikAdi,d.Id AS DoktorId,d.Name AS DoktorAdi,d.Surname AS DoktorSoyadi,at.AppointmentTime AS RandevuSaati FROM PatientAppointment p INNER JOIN Policlinics pl ON p.PoliclinicId = pl.Id INNER JOIN Doctors d ON p.DoctorId = d.Id INNER JOIN AppointmentTime at ON p.AppointmentTimeId = at.Id WHERE p.UserId = @Id AND p.Status = 'Aktif';";


            DataTable dta = new DataTable();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        await Task.Run(() => dataAdapter.Fill(dta));
                    }
                }
            }

            appointmentList.Clear();

            foreach (DataRow row in dta.Rows)
            {
                var appointment = new AppointmentDashboard
                {
                    RandevuId = Convert.ToInt32(row["RandevuId"]),
                    DoktorId = Convert.ToInt32(row["DoktorId"]),
                    DoktorAdi = row["DoktorAdi"].ToString(),
                    DoktorSoyadi = row["DoktorSoyadi"].ToString(),
                    RandevuTarihi = Convert.ToDateTime(row["RandevuTarihi"]),
                    PoliklinikAdi = row["PoliklinikAdi"].ToString(),
                    RandevuSaati = row["RandevuSaati"].ToString()
                };

                appointmentList.Add(appointment);
            }
            UpdateDataGridView(appointmentList);
        }
        private async void UpdateDataGridView(List<AppointmentDashboard> appointmentList)
        {
            if (appointmentList.Count > 0)
            {
                labelAppointmentActiveNotFound.Visible = false;
                await Task.Delay(100);
                pictureBoxLoadingActiveAppointmentNotFound.Visible = false;
                btn_ActiveAppointmentShow.Enabled = true;
                AppointmentList(appointmentList);
                AppointmentListPageAll(appointmentList);
                dataGridActiveAppointment.Visible = true;
                panelActiveAppointmentBorder.Visible = true;
                panelDoctorAppointmentChoose.Visible = false;
            }
            else
            {
                await Task.Delay(100);

                dataGridActiveAppointment.Rows.Clear();
                dataGridActiveAppointment.Visible = true;
                dataGridActiveAppointment.Height = 605;

                pictureBoxActiveAppointmentNotFound.Visible = true;
                pictureBoxActiveAppointmentNotFound.BringToFront();

                labelAppointmentActiveNotFound.Visible = true;
                labelAppointmentActiveNotFound.Location = new Point(370, 314);
                labelAppointmentActiveNotFound.BringToFront();

                btn_ActiveAppointmentShow.Enabled = false;

                panelActiveAppointmentBorder.Visible = false;
            }

        }
        private void AppointmentList(List<AppointmentDashboard> appointmentList)
        {
            dataGridActiveAppointment.Rows.Clear();
            for (int i = 0; i < appointmentList.Count; i++)
            {
                if (i < 5)
                {
                    dataGridActiveAppointment.Rows.Add(appointmentList[i].RandevuId, isDarkMode ? Properties.Resources.doctorsteteskop : Properties.Resources.doctorsteteskopWhite, appointmentList[i].DoktorId, $"{appointmentList[i].DoktorAdi} {appointmentList[i].DoktorSoyadi}", isDarkMode ? Properties.Resources.policlinicDark2 : Properties.Resources.policlinic2, appointmentList[i].PoliklinikAdi, isDarkMode ? Properties.Resources.price_tagDark : Properties.Resources.price_tag, "Muayene", Properties.Resources.AppointmentDelete);

                    if (i % 5 == 0)
                    {
                        txtActiveAppointment1.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAppointment1.Visible = true;
                        panelActive1.Visible = true;

                        panelActiveAppointmentBorder.Height = 120;
                        dataGridActiveAppointment.Height = 117;
                    }
                    else if (i % 5 == 1)
                    {
                        txtActiveAppointment2.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAppointment2.Visible = true;
                        panelActive2.Visible = true;

                        panelActiveAppointmentBorder.Height = 245;
                        dataGridActiveAppointment.Height = 243;
                    }
                    else if (i % 5 == 2)
                    {
                        txtActiveAppointment3.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAppointment3.Visible = true;
                        panelActive3.Visible = true;

                        panelActiveAppointmentBorder.Height = 365;
                        dataGridActiveAppointment.Height = 363;
                    }
                    else if (i % 5 == 3)
                    {
                        txtActiveAppointment4.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAppointment4.Visible = true;
                        panelActive4.Visible = true;

                        panelActiveAppointmentBorder.Height = 485;
                        dataGridActiveAppointment.Height = 483;
                    }
                    else if (i % 5 == 4)
                    {
                        txtActiveAppointment5.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAppointment5.Visible = true;
                        panelActive5.Visible = true;

                        panelActiveAppointmentBorder.Height = 603;
                        dataGridActiveAppointment.Height = 603;
                    }
                }
                else
                {
                    dataGridActiveAppointment.Columns[1].Width = 370;
                    dataGridActiveAppointment.Columns[3].Width = 160;
                    dataGridActiveAppointment.Columns[5].Width = 120;
                    return;
                }
                dataGridActiveAppointment.Columns[1].Width = 370;
                dataGridActiveAppointment.Columns[3].Width = 160;
                dataGridActiveAppointment.Columns[5].Width = 120;
            }
            dataGridActiveAppointment.ClearSelection();
        }
        private void UpdateAllActiveDataGridView(List<AppointmentDashboard> appointmentList)
        {
            if (appointmentList.Count > 0)
            {
                pictureBoxAllActive.Visible = false;
                labelAppointmentIsNot.Visible = false;
                AppointmentListPageAll(appointmentList);
                panelAllActiveAppointmentBorder.Visible = true;


            }
            else
            {
                dataGridAllActiveAppointment.Rows.Clear();
                dataGridAllActiveAppointment.Visible = true;
                dataGridAllActiveAppointment.Height = 605;

                pictureBoxAllActive.Visible = true;
                pictureBoxAllActive.BringToFront();
                labelAppointmentIsNot.Visible = true;
                labelAppointmentIsNot.Location = new Point(725, 314);
                labelAppointmentIsNot.BringToFront();
                panelAllActiveAppointmentBorder.Visible = false;
            }

        }
        private void AppointmentListPageAll(List<AppointmentDashboard> appointmentList)
        {
            dataGridAllActiveAppointment.Rows.Clear();
            for (int i = 0; i < appointmentList.Count; i++)
            {
                if (i < 3)
                {
                    dataGridAllActiveAppointment.Rows.Add(appointmentList[i].RandevuId, isDarkMode ? Properties.Resources.doctorsteteskop : Properties.Resources.doctorsteteskopWhite, appointmentList[i].DoktorId, $"{appointmentList[i].DoktorAdi} {appointmentList[i].DoktorSoyadi}", isDarkMode ? Properties.Resources.policlinicDark2 : Properties.Resources.policlinic2, appointmentList[i].PoliklinikAdi, isDarkMode ? Properties.Resources.price_tagDark : Properties.Resources.price_tag, "Muayene", Properties.Resources.AppointmentDelete);

                    if (i % 5 == 0)
                    {
                        txtActiveAllAppointment1.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAllAppointment1.Visible = true;
                        panelAllActive1.Visible = true;

                        panelAllActiveAppointmentBorder.Height = 120;
                        dataGridAllActiveAppointment.Height = 117;
                    }
                    else if (i % 5 == 1)
                    {
                        txtActiveAllAppointment2.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAllAppointment2.Visible = true;
                        panelAllActive2.Visible = true;

                        panelAllActiveAppointmentBorder.Height = 245;
                        dataGridAllActiveAppointment.Height = 243;
                    }
                    else if (i % 5 == 2)
                    {
                        txtActiveAllAppointment3.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAllAppointment3.Visible = true;
                        panelAllActive3.Visible = true;

                        panelAllActiveAppointmentBorder.Height = 365;
                        dataGridAllActiveAppointment.Height = 363;
                    }
                    else if (i % 5 == 3)
                    {
                        txtActiveAllAppointment4.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAllAppointment4.Visible = true;
                        panelAllActive4.Visible = true;

                        panelAllActiveAppointmentBorder.Height = 485;
                        dataGridAllActiveAppointment.Height = 483;
                    }
                    else if (i % 5 == 4)
                    {
                        txtActiveAllAppointment5.Text = $"{appointmentList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentList[i].RandevuSaati).ToString("HH:mm")}";

                        txtActiveAllAppointment5.Visible = true;
                        panelAllActive5.Visible = true;

                        panelAllActiveAppointmentBorder.Height = 603;
                        dataGridAllActiveAppointment.Height = 603;
                    }
                }
                else
                {
                    dataGridAllActiveAppointment.Columns[1].Width = 550;
                    dataGridAllActiveAppointment.Columns[3].Width = 160;
                    dataGridAllActiveAppointment.Columns[5].Width = 160;
                    return;
                }
                dataGridAllActiveAppointment.Columns[1].Width = 550;
                dataGridAllActiveAppointment.Columns[3].Width = 160;
                dataGridAllActiveAppointment.Columns[5].Width = 160;
            }
            dataGridAllActiveAppointment.ClearSelection();
        }
        private async Task LoadPastAppointments(int id)
        {
            string query = @"SELECT p.Date AS RandevuTarihi,pl.[Poliklinik Adı] AS PoliklinikAdi,d.Id AS DoktorId,d.Name AS DoktorAdi,d.Surname AS DoktorSoyadi,at.AppointmentTime AS RandevuSaati FROM PatientAppointment p INNER JOIN Policlinics pl ON p.PoliclinicId = pl.Id INNER JOIN Doctors d ON p.DoctorId = d.Id INNER JOIN AppointmentTime at ON p.AppointmentTimeId = at.Id WHERE p.UserId = @Id AND p.Status = 'Geçmiş Randevu';";


            DataTable dtpa = new DataTable();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        await Task.Run(() => dataAdapter.Fill(dtpa));
                    }
                }
            }

            appointmentPastList.Clear();

            foreach (DataRow row in dtpa.Rows)
            {
                var appointment = new AppointmentDashboard
                {
                    DoktorId = Convert.ToInt32(row["DoktorId"]),
                    DoktorAdi = row["DoktorAdi"].ToString(),
                    DoktorSoyadi = row["DoktorSoyadi"].ToString(),
                    RandevuTarihi = Convert.ToDateTime(row["RandevuTarihi"]),
                    PoliklinikAdi = row["PoliklinikAdi"].ToString(),
                    RandevuSaati = row["RandevuSaati"].ToString()
                };

                appointmentPastList.Add(appointment);
            }

            UpdateDataPastGridView(appointmentPastList);
        }
        private async void UpdateDataPastGridView(List<AppointmentDashboard> appointmentPastList)
        {
            if (appointmentPastList.Count > 0)
            {
                pictureBoxPastAppointment.Visible = false;
                labelPastAppointment.Visible = false;
                await Task.Delay(100);
                btn_PastAppointmentShow.Enabled = true;
                AppointmentPastList(appointmentPastList);
                dataGridPastAppointment.Visible = true;
                panelPastAppointmentBorder.Visible = true;
            }
            else
            {
                await Task.Delay(100);
                dataGridPastAppointment.Rows.Clear();
                dataGridPastAppointment.Visible = true;
                dataGridPastAppointment.Height = 605;

                pictureBoxPastAppointment.Visible = true;
                pictureBoxPastAppointment.BringToFront();

                labelPastAppointment.Visible = true;
                labelPastAppointment.Location = new Point(330, 314);
                labelPastAppointment.BringToFront();

                btn_PastAppointmentShow.Enabled = false;

                panelPastAppointmentBorder.Visible = false;
            }

        }

        private void AppointmentPastList(List<AppointmentDashboard> appointmentPastList)
        {
            dataGridPastAppointment.Rows.Clear();
            for (int i = 0; i < appointmentPastList.Count; i++)
            {
                if (i < 5)
                {
                    dataGridPastAppointment.Rows.Add(isDarkMode ? Properties.Resources.doctorsteteskop : Properties.Resources.doctorsteteskopWhite, appointmentPastList[i].DoktorId, $"{appointmentPastList[i].DoktorAdi} {appointmentPastList[i].DoktorSoyadi}", isDarkMode ? Properties.Resources.policlinicDark2 : Properties.Resources.policlinic2, appointmentPastList[i].PoliklinikAdi, isDarkMode ? Properties.Resources.price_tagDark : Properties.Resources.price_tag, "Muayene");

                    if (i % 5 == 0)
                    {
                        txtPastAppointmentDate1.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtPastAppointmentDate1.Visible = true;
                        panelPast1.Visible = true;

                        panelPastAppointmentBorder.Height = 120;
                        dataGridPastAppointment.Height = 117;
                    }
                    else if (i % 5 == 1)
                    {
                        txtPastAppointmentDate2.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtPastAppointmentDate2.Visible = true;
                        panelPast2.Visible = true;

                        panelPastAppointmentBorder.Height = 245;
                        dataGridPastAppointment.Height = 243;
                    }
                    else if (i % 5 == 2)
                    {
                        txtPastAppointmentDate3.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtPastAppointmentDate3.Visible = true;
                        panelPast3.Visible = true;

                        panelPastAppointmentBorder.Height = 365;
                        dataGridPastAppointment.Height = 363;
                    }
                    else if (i % 5 == 3)
                    {
                        txtPastAppointmentDate4.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtPastAppointmentDate4.Visible = true;
                        panelPast4.Visible = true;

                        panelPastAppointmentBorder.Height = 485;
                        dataGridPastAppointment.Height = 483;
                    }
                    else if (i % 5 == 4)
                    {
                        txtPastAppointmentDate5.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtPastAppointmentDate5.Visible = true;
                        panelPast5.Visible = true;

                        panelPastAppointmentBorder.Height = 603;
                        dataGridPastAppointment.Height = 603;
                    }
                }
                else
                {
                    dataGridPastAppointment.Columns[0].Width = 350;
                    dataGridPastAppointment.Columns[2].Width = 160;
                    dataGridPastAppointment.Columns[4].Width = 160;
                    return;
                }
                dataGridPastAppointment.Columns[0].Width = 350;
                dataGridPastAppointment.Columns[2].Width = 160;
                dataGridPastAppointment.Columns[4].Width = 160;
            }
            dataGridPastAppointment.ClearSelection();
        }
        private void UpdateDataAllPastGridView(List<AppointmentDashboard> appointmentPastList)
        {
            if (appointmentPastList.Count > 0)
            {
                pictureBoxAllPastNotAppointment.Visible = false;
                labelNotAppointment.Visible = false;
                AppointmentAllPastList(appointmentPastList, currentAllPastPage);
                panelAllPastAppointmentBorder.Visible = true;
            }
            else
            {
                dataGridAllPastAppointment.Rows.Clear();
                dataGridAllPastAppointment.Visible = true;
                dataGridAllPastAppointment.Height = 605;

                pictureBoxAllPastNotAppointment.Visible = true;
                pictureBoxAllPastNotAppointment.BringToFront();
                labelNotAppointment.Visible = true;
                labelNotAppointment.Location = new Point(713, 314);
                labelNotAppointment.BringToFront();
                panelAllPastAppointmentBorder.Visible = false;
            }
        }
        private void AppointmentAllPastList(List<AppointmentDashboard> appointmentPastList, int page)
        {
            dataGridAllPastAppointment.Rows.Clear();
            for (int i = 0; i < appointmentPastList.Count; i++)
            {
                if (i < page * 5 && ((page * 5) - 5 <= i))
                {
                    dataGridAllPastAppointment.Rows.Add(isDarkMode ? Properties.Resources.doctorsteteskop : Properties.Resources.doctorsteteskopWhite, appointmentPastList[i].DoktorId, $"{appointmentPastList[i].DoktorAdi} {appointmentPastList[i].DoktorSoyadi}", isDarkMode ? Properties.Resources.policlinicDark2 : Properties.Resources.policlinic2, appointmentPastList[i].PoliklinikAdi, isDarkMode ? Properties.Resources.price_tagDark : Properties.Resources.price_tag, "Muayene");

                    if (i % 5 == 0)
                    {
                        txtAllPastAppointment1.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtAllPastAppointment1.Visible = true;
                        panelAllPast1.Visible = true;

                        panelAllPastAppointmentBorder.Height = 120;
                        dataGridAllPastAppointment.Height = 117;
                    }
                    else if (i % 5 == 1)
                    {
                        txtAllPastAppointment2.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtAllPastAppointment2.Visible = true;
                        panelAllPast2.Visible = true;

                        panelAllPastAppointmentBorder.Height = 245;
                        dataGridAllPastAppointment.Height = 243;
                    }
                    else if (i % 5 == 2)
                    {
                        txtAllPastAppointment3.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtAllPastAppointment3.Visible = true;
                        panelAllPast3.Visible = true;

                        panelAllPastAppointmentBorder.Height = 365;
                        dataGridAllPastAppointment.Height = 363;
                    }
                    else if (i % 5 == 3)
                    {
                        txtAllPastAppointment4.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtAllPastAppointment4.Visible = true;
                        panelAllPast4.Visible = true;

                        panelAllPastAppointmentBorder.Height = 485;
                        dataGridAllPastAppointment.Height = 483;
                    }
                    else if (i % 5 == 4)
                    {
                        txtAllPastAppointment5.Text = $"{appointmentPastList[i].RandevuTarihi.ToLongDateString()} {DateTime.Parse(appointmentPastList[i].RandevuSaati).ToString("HH:mm")}";

                        txtAllPastAppointment5.Visible = true;
                        panelAllPast5.Visible = true;

                        panelAllPastAppointmentBorder.Height = 605;
                        dataGridAllPastAppointment.Height = 603;
                    }
                }
                else
                {
                    dataGridAllPastAppointment.Columns[0].Width = 350;
                    dataGridAllPastAppointment.Columns[2].Width = 160;
                    dataGridAllPastAppointment.Columns[4].Width = 160;
                }
                dataGridAllPastAppointment.Columns[0].Width = 350;
                dataGridAllPastAppointment.Columns[2].Width = 160;
                dataGridAllPastAppointment.Columns[4].Width = 160;
            }
            totalPastAppointmentCount = Convert.ToInt32(Math.Ceiling((double)appointmentPastList.Count / itemsPerPage));
            if (totalPastAppointmentCount == 0)
            {
                labelAllPageNumber.Text = "Sayfa 1";
                panelAllPastPageSlider.Visible = false;
            }
            else
            {
                labelAllPageNumber.Text = $"{currentAllPastPage}. Sayfa, Toplam: {totalPastAppointmentCount}";
                dataGridAllPastAppointment.ClearSelection();
                PageNumberSliderAllPast(currentAllPastPage, totalPastAppointmentCount);
            }
        }

        private async void appointmentChoose_Click(object sender, EventArgs e)
        {
            await LoadPoliclinicList();
        }

        private void DateTimePickerProperty()
        {
            datePickerStartTime.MinDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            datePickerStartTime.MaxDate = DateTime.Parse(DateTime.Now.AddDays(30).ToShortDateString());
            datePickerStartTime.Text = datePickerStartTime.MinDate.ToString();

            datePickerEndTime.MinDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            datePickerEndTime.MaxDate = DateTime.Parse(DateTime.Now.AddDays(30).ToShortDateString());
            datePickerEndTime.Text = datePickerEndTime.MaxDate.ToString();
        }

        private async void dropDownPoliclinic_SelectedIndexChanged(object sender, EventArgs e)
        {

            PoliclinicDoctorCategory policlinicCategory = dropDownPoliclinic.SelectedItem as PoliclinicDoctorCategory;
            if (policlinicCategory != null)
            {
                int selectedId = policlinicCategory.Id;
                if (selectedId != null)
                {
                    labelPoliclinicRequiredCheck.Visible = false;
                    dropDownDoctor.Enabled = false;
                    await LoadDoctorList(selectedId);
                    dropDownDoctor.Enabled = true;
                }
            }
        }

        private async Task LoadDoctorList(int id)
        {
            dropDownDoctor.Text = "";
            panelLoading.BackColor = Color.DarkGray;
            panelLoading.Location = new Point(751, 313);
            panelLoading.Visible = true;
            await Task.Delay(1000);
            dropDownDoctor.Enabled = false;
            string query = "SELECT D.Id,D.Name,D.Surname,P.Id, P.[Poliklinik Adı] FROM Doctors D INNER JOIN Policlinics P ON D.PoliclinicId = P.Id WHERE P.Id = @PoliclinicId ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PoliclinicId", id);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dropDownDoctor.Items.Clear();

                            while (await reader.ReadAsync())
                            {
                                int doctorId = reader.GetInt32(0);
                                string doctorAd = reader.GetString(1);
                                string doctorSoyad = reader.GetString(2);
                                string nameSurname = $"{doctorAd} {doctorSoyad}";
                                dropDownDoctor.Items.Add(new PoliclinicDoctorCategory(doctorId, nameSurname));
                            }

                            dropDownDoctor.DisplayMember = "Name";
                            dropDownDoctor.ValueMember = "Id";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dropDownDoctor.Enabled = true;
            panelLoading.Visible = false;
            dropDownDoctor.Text = "Hekim Seçiniz";
        }

        private void datePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            datePickerStartTime.Format = DateTimePickerFormat.Long;
            datePickerStartTime.ForeColor = Color.Black;
            labeldatePickerStart.Visible = false;
            datePickerEndTime.MinDate = datePickerStartTime.Value;

            labelNotDayOfWeek.Visible = false;
        }

        private void datePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            datePickerEndTime.Format = DateTimePickerFormat.Long;
            datePickerEndTime.ForeColor = Color.Black;
            labeldatePickerEnd.Visible = false;

            labelNotDayOfWeek.Visible = false;
        }

        private void btn_AppointmentClear_Click(object sender, EventArgs e)
        {
            dropDownPoliclinic.SelectedIndex = -1;
            dropDownPoliclinic.Text = "Klinik Seçiniz";
            dropDownDoctor.Items.Clear();
            dropDownDoctor.Text = " ";
            dropDownDoctor.SelectedIndex = -1;
            dropDownDoctor.Enabled = false;

            datePickerStartTime.Text = DateTime.Now.ToLongDateString();
            datePickerEndTime.MaxDate = DateTime.Now.AddDays(30);
            datePickerEndTime.Text = datePickerEndTime.MaxDate.ToLongDateString();

            labeldatePickerStart.Visible = true;
            labeldatePickerEnd.Visible = true;
            labelNotDayOfWeek.Visible = false;

        }

        private async void btn_AppointmentSearch_Click(object sender, EventArgs e)
        {
            panelDoctorAppointmentChoose.Visible = false;
            currentPage = 1;

            if (datePickerStartTime.Value == datePickerEndTime.Value)
            {
                DayOfWeek day = datePickerStartTime.Value.DayOfWeek;
                if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
                {
                    labelNotDayOfWeek.Visible = true;
                    return;
                }
            }

            PoliclinicDoctorCategory policlinicCategory = dropDownPoliclinic.SelectedItem as PoliclinicDoctorCategory;
            PoliclinicDoctorCategory doctorCategory = dropDownDoctor.SelectedItem as PoliclinicDoctorCategory;
            int? selectedDoctorId;
            if (policlinicCategory == null)
            {
                labelPoliclinicRequiredCheck.Visible = true;
                return;
            }

            int? selectedPoliclinicId = policlinicCategory.Id;

            if (selectedPoliclinicId.ToString() == "" || (selectedPoliclinicId.ToString() == null))
            {
                labelPoliclinicRequiredCheck.Visible = true;
                return;
            }

            Dictionary<int, ArrayList> doctorsAppointments = new Dictionary<int, ArrayList>();


            if (doctorCategory == null)
            {
                selectedDoctorId = null;
            }
            else
            {
                selectedDoctorId = doctorCategory.Id;
            }

            DateTime selectedStartTime = DateTime.Parse(datePickerStartTime.Text);
            DateTime selectedEndTime = DateTime.Parse(datePickerEndTime.Text);


            if (selectedDoctorId.HasValue)
            {
                ArrayList doctorAppointments = new ArrayList();
                doctorsAppointments.Add(selectedDoctorId.Value, doctorAppointments);

                var restDates = await GetDoctorRestDate(selectedDoctorId.Value);
                foreach (var restDate in restDates)
                {
                    doctorAppointments.Add(restDate);
                }

                var activeAppointments = await GetActiveAppointments(selectedDoctorId.Value, selectedStartTime, selectedEndTime);

                var groupedAppointments = activeAppointments.GroupBy(a => new { a.DoctorId, a.Date })
                                                            .Where(g => g.Count() == 14)
                                                            .Select(g => g.Key.Date)
                                                            .Distinct()
                                                            .ToList();

                foreach (var group in groupedAppointments)
                {
                    doctorAppointments.Add(group);
                }
            }
            else
            {
                var doctorIds = await GetDoctorIdsForPoliclinic(selectedPoliclinicId);
                if (doctorIds.Count == 0)
                {
                    AppointmentNotFoundForum appointmentNotFoundForum = new AppointmentNotFoundForum();
                    appointmentNotFoundForum.Show();
                    return;
                }

                foreach (var doctorId in doctorIds)
                {
                    ArrayList doctorData = new ArrayList();

                    var restDates = await GetDoctorRestDate(doctorId);
                    foreach (var restDate in restDates)
                    {
                        doctorData.Add(restDate);
                    }

                    var activeAppointments = await GetActiveAppointments(doctorId, selectedStartTime, selectedEndTime);

                    var groupedAppointments = activeAppointments.GroupBy(a => new { a.DoctorId, a.Date })
                                                                .Where(g => g.Count() == 14)
                                                                .Select(g => g.Key.Date)
                                                                .Distinct()
                                                                .ToList();

                    foreach (var group in groupedAppointments)
                    {
                        doctorData.Add(group);
                    }

                    if (!doctorsAppointments.ContainsKey(doctorId))
                    {
                        doctorsAppointments.Add(doctorId, doctorData);
                    }
                }
            }

            ListAvailableDoctors(doctorsAppointments, selectedStartTime, selectedEndTime);

            panelAppointmentChoose.Visible = false;

            pictureBoxAppointmentLoading.Visible = true;

            await Task.Delay(3000);

            pictureBoxAppointmentLoading.Visible = false;


            await Task.Run(() => animatorLeft.Show(panelAppointmentList, true));
        }
        private async Task<List<DateTime>> GetDoctorRestDate(int doctorId)
        {
            var restDates = new List<DateTime>();

            string query = "SELECT RestDate FROM DoctorRestDate WHERE DoctorId = @DoctorId AND Status = @Status";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@DoctorId", SqlDbType.Int) { Value = doctorId });
                        cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar) { Value = "Devam Ediyor" });

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                restDates.Add(reader.GetDateTime(0));  // İzin tarihlerini alıyoruz
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hatas: {ex.Message}");
            }

            return restDates;
        }

        private async Task<List<DoctorAppointmentData>> GetActiveAppointments(int doctorId, DateTime startDate, DateTime endDate)
        {
            var appointments = new List<DoctorAppointmentData>();

            string query = "SELECT DoctorId, Date FROM PatientAppointment WHERE DoctorId = @DoctorId AND Status = 'Aktif' AND Date BETWEEN @StartDate AND @EndDate";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@DoctorId", SqlDbType.Int).Value = doctorId;
                        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
                        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                appointments.Add(new DoctorAppointmentData
                                {
                                    DoctorId = reader.GetInt32(0),
                                    Date = reader.GetDateTime(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hatas: {ex.Message}");
            }

            return appointments;
        }


        public async Task<List<int>> GetDoctorIdsForPoliclinic(int? policlinicId)
        {
            var doctorIds = new List<int>();

            string query = "SELECT Id FROM Doctors WHERE PoliclinicId = @PoliclinicId";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@PoliclinicId", SqlDbType.Int).Value = policlinicId;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                doctorIds.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hatas: {ex.Message}");
            }
            return doctorIds;
        }

        private void ListAvailableDoctors(Dictionary<int, ArrayList> doctorsAppointments, DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDays = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                                .Select(offset => startDate.AddDays(offset))
                                                .ToList();

            allDoctorData.Clear();

            DateTime currentDateTime = DateTime.Now;
            TimeSpan cutoffTime = new TimeSpan(15, 30, 0);

            foreach (var doctorEntry in doctorsAppointments)
            {
                int doctorId = doctorEntry.Key;
                ArrayList doctorDates = doctorEntry.Value;

                var doctorInfo = GetDoctorInfoById(doctorId);

                if (doctorInfo == null) continue;

                List<DateTime> availableDays = allDays
                    .Where(day =>
                        !doctorDates.Contains(day) &&
                        day.DayOfWeek != DayOfWeek.Saturday &&
                        day.DayOfWeek != DayOfWeek.Sunday &&
                        !(day.Date == currentDateTime.Date && currentDateTime.TimeOfDay > cutoffTime))
                    .OrderBy(day => day)
                    .ToList();

                Image Gender = null;
                foreach (var availableDay in availableDays)
                {
                    string nameSurname = $"{doctorInfo.DoctorName} {doctorInfo.DoctorSurname}";

                    if (doctorInfo.Gender == "Erkek")
                    {
                        Gender = Properties.Resources.Erkek;
                    }
                    else if (doctorInfo.Gender == "Kadın")
                    {
                        Gender = Properties.Resources.Kadın;
                    }

                    allDoctorData.Add(new DoctorAvailability
                    {
                        GenderImage = Gender,
                        DoctorId = doctorId,
                        DoctorNameSurname = nameSurname,
                        PoliclinicId = doctorInfo.PoliclinicId,
                        PoliclinicName = doctorInfo.PoliclinicName,
                        AvailableDate = availableDay
                    });
                }
            }

            totalAvaibleAppointmentCount = Convert.ToInt32(Math.Ceiling((double)allDoctorData.Count / itemsPerPage));

            FillDataGrid();
        }
        private void FillDataGrid()
        {
            datagridAppointment.Rows.Clear();
            int sayac = 0;
            var sortedDoctors = allDoctorData.OrderBy(d => d.AvailableDate).ToList();

            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, allDoctorData.Count);

            datagridAppointment.Columns[0].Width = 80;
            datagridAppointment.Columns[1].Width = 700;
            datagridAppointment.Columns[2].Width = 65;
            datagridAppointment.Columns[3].Width = 500;


            Image policlinic = isDarkMode ? Properties.Resources.policlinicDark2 : Properties.Resources.policlinic2;
            isAddingData = true;
            for (int i = startIndex; i < endIndex; i++)
            {
                sayac++;

                var data = sortedDoctors[i];
                datagridAppointment.Rows.Add(

                    data.GenderImage,
                    data.DoctorNameSurname,
                    policlinic,
                    data.PoliclinicName,
                    data.DoctorId,
                    data.PoliclinicId,
                    data.AvailableDate.ToLongDateString()
                );

                if (currentPage == totalAvaibleAppointmentCount)
                {
                    if (sortedDoctors.Count % 5 == 1)
                    {
                        labelEarlyTime2.Visible = false;
                        txtEarlyTime2.Visible = false;
                        txtEartyDay2.Visible = false;

                        labelEarlyTime3.Visible = false;
                        txtEarlyTime3.Visible = false;
                        txtEartyDay3.Visible = false;

                        labelEarlyTime4.Visible = false;
                        txtEarlyTime4.Visible = false;
                        txtEartyDay4.Visible = false;

                        labelEarlyTime5.Visible = false;
                        txtEarlyTime5.Visible = false;
                        txtEartyDay5.Visible = false;

                        PaneLBorder.Size = new Size(1740, 120);
                        datagridAppointment.Height = 117;
                    }
                    else if (sortedDoctors.Count % 5 == 2)
                    {
                        labelEarlyTime2.Visible = true;
                        txtEarlyTime2.Visible = true;
                        txtEartyDay2.Visible = true;

                        labelEarlyTime3.Visible = false;
                        txtEarlyTime3.Visible = false;
                        txtEartyDay3.Visible = false;

                        labelEarlyTime4.Visible = false;
                        txtEarlyTime4.Visible = false;
                        txtEartyDay4.Visible = false;

                        labelEarlyTime5.Visible = false;
                        txtEarlyTime5.Visible = false;
                        txtEartyDay5.Visible = false;
                        PaneLBorder.Size = new Size(1740, 245);
                        datagridAppointment.Height = 243;
                    }
                    else if (sortedDoctors.Count % 5 == 3)
                    {
                        labelEarlyTime2.Visible = true;
                        txtEarlyTime2.Visible = true;
                        txtEartyDay2.Visible = true;

                        labelEarlyTime3.Visible = true;
                        txtEarlyTime3.Visible = true;
                        txtEartyDay3.Visible = true;

                        labelEarlyTime4.Visible = false;
                        txtEarlyTime4.Visible = false;
                        txtEartyDay4.Visible = false;

                        labelEarlyTime5.Visible = false;
                        txtEarlyTime5.Visible = false;
                        txtEartyDay5.Visible = false;
                        PaneLBorder.Size = new Size(1740, 365);
                        datagridAppointment.Height = 363;
                    }
                    else if (sortedDoctors.Count % 5 == 4)
                    {
                        labelEarlyTime2.Visible = true;
                        txtEarlyTime2.Visible = true;
                        txtEartyDay2.Visible = true;

                        labelEarlyTime3.Visible = true;
                        txtEarlyTime3.Visible = true;
                        txtEartyDay3.Visible = true;

                        labelEarlyTime4.Visible = true;
                        txtEarlyTime4.Visible = true;
                        txtEartyDay4.Visible = true;

                        labelEarlyTime5.Visible = false;
                        txtEarlyTime5.Visible = false;
                        txtEartyDay5.Visible = false;
                        PaneLBorder.Size = new Size(1740, 485);
                        datagridAppointment.Height = 483;
                    }
                    else
                    {
                        labelEarlyTime2.Visible = true;
                        txtEarlyTime2.Visible = true;
                        txtEartyDay2.Visible = true;

                        labelEarlyTime3.Visible = true;
                        txtEarlyTime3.Visible = true;
                        txtEartyDay3.Visible = true;

                        labelEarlyTime4.Visible = true;
                        txtEarlyTime4.Visible = true;
                        txtEartyDay4.Visible = true;

                        labelEarlyTime5.Visible = true;
                        txtEarlyTime5.Visible = true;
                        txtEartyDay5.Visible = true;
                        PaneLBorder.Size = new Size(1740, 605);
                        datagridAppointment.Height = 603;
                    }
                }
                else
                {
                    labelEarlyTime2.Visible = true;
                    txtEarlyTime2.Visible = true;
                    txtEartyDay2.Visible = true;

                    labelEarlyTime3.Visible = true;
                    txtEarlyTime3.Visible = true;
                    txtEartyDay3.Visible = true;

                    labelEarlyTime4.Visible = true;
                    txtEarlyTime4.Visible = true;
                    txtEartyDay4.Visible = true;

                    labelEarlyTime5.Visible = true;
                    txtEarlyTime5.Visible = true;
                    txtEartyDay5.Visible = true;
                    PaneLBorder.Size = new Size(1740, 605);
                    datagridAppointment.Height = 603;
                }


                if (i % 5 == 0)
                {
                    txtEarlyTime1.Text = data.AvailableDate.ToLongDateString();
                    int difference = -1 * (DateTime.Now.Date - data.AvailableDate.Date).Days;
                    if (difference == 0)
                    {
                        txtEartyDay1.Text = "Bugün";
                    }
                    else if (difference == 1)
                    {
                        txtEartyDay1.Text = "Yarın";
                    }
                    else if (difference > 1)
                    {
                        txtEartyDay1.Text = $"{difference} Gün Var";
                    }
                }
                else if (i % 5 == 1)
                {
                    txtEarlyTime2.Text = data.AvailableDate.ToLongDateString();
                    int difference = -1 * (DateTime.Now.Date - data.AvailableDate.Date).Days;
                    if (difference == 0)
                    {
                        txtEartyDay2.Text = "Bugün";
                    }
                    else if (difference == 1)
                    {
                        txtEartyDay2.Text = "Yarın";
                    }
                    else if (difference > 1)
                    {
                        txtEartyDay2.Text = $"{difference} Gün Var";
                    }

                }
                else if (i % 5 == 2)
                {
                    txtEarlyTime3.Text = data.AvailableDate.ToLongDateString();
                    int difference = -1 * (DateTime.Now.Date - data.AvailableDate.Date).Days;
                    if (difference == 0)
                    {
                        txtEartyDay3.Text = "Bugün";
                    }
                    else if (difference == 1)
                    {
                        txtEartyDay3.Text = "Yarın";
                    }
                    else if (difference > 1)
                    {
                        txtEartyDay3.Text = $"{difference} Gün Var";
                    }
                }
                else if (i % 5 == 3)
                {
                    txtEarlyTime4.Text = data.AvailableDate.ToLongDateString();
                    int difference = -1 * (DateTime.Now.Date - data.AvailableDate.Date).Days;
                    if (difference == 0)
                    {
                        txtEartyDay4.Text = "Bugün";
                    }
                    else if (difference == 1)
                    {
                        txtEartyDay4.Text = "Yarın";
                    }
                    else if (difference > 1)
                    {
                        txtEartyDay4.Text = $"{difference} Gün Var";
                    }
                }
                else if (i % 5 == 4)
                {
                    txtEarlyTime5.Text = data.AvailableDate.ToLongDateString();
                    int difference = -1 * (DateTime.Now.Date - data.AvailableDate.Date).Days;
                    if (difference == 0)
                    {
                        txtEartyDay5.Text = "Bugün";
                    }
                    else if (difference == 1)
                    {
                        txtEartyDay5.Text = "Yarın";
                    }
                    else if (difference > 1)
                    {
                        txtEartyDay5.Text = $"{difference} Gün Var";
                    }
                }

            }
            labelPageNumber.Text = $"{currentPage}. Sayfa, Toplam: {Math.Ceiling((double)allDoctorData.Count / itemsPerPage)}";
            datagridAppointment.ClearSelection();
            isAddingData = false;
            PageNumberSlider(currentPage, totalAvaibleAppointmentCount);
        }
        void PageNumberSlider(int currentPage, int totalPages)
        {
            pictureBoxRightArrow.Location = new Point(684, 14);
            pictureBoxLeftArrow.Location = new Point(457, 14);


            if (currentPage > 1)
            {
                pictureBoxLeftArrow.Visible = true;

            }
            else
            {
                pictureBoxLeftArrow.Visible = false;
            }


            if (currentPage < totalPages)
            {
                pictureBoxRightArrow.Visible = true;
            }
            else
            {
                pictureBoxRightArrow.Visible = false;
            }

            if (currentPage <= 4)
            {
                pictureBoxFirst3Dot.Visible = false;

                int limit = Math.Min(4, totalPages);
                for (int i = 1; i <= limit; i++)
                {
                    labels[i - 1].Visible = true;
                    labels[i - 1].Text = i.ToString();

                    if (i == 1)
                    {
                        labels[i - 1].Location = new Point(519, 14);
                        pictureBoxLeftArrow.Location = new Point(457, 14);
                    }
                    else if (i == 2)
                    {
                        labels[i - 1].Location = new Point(550, 14);
                    }
                    else if (i == 3)
                    {
                        labels[i - 1].Location = new Point(581, 14);
                    }
                    else if (i == 4)
                    {
                        labels[i - 1].Location = new Point(612, 14);
                    }

                    if ((i + 1) == null)
                    {
                        if (i == 1)
                        {
                            pictureBoxLeftArrow.Visible = false;
                            pictureBoxRightArrow.Visible = false;

                            labelPage1.Location = new Point(684, 14);
                        }
                        else if (i == 2)
                        {
                            pictureBoxLeftArrow.Visible = true;
                            pictureBoxRightArrow.Visible = true;

                            pictureBoxLeftArrow.Location = new Point(581, 14);

                            labelPage1.Location = new Point(612, 14);
                            labelPage2.Location = new Point(644, 14);
                        }
                        else if (i == 3)
                        {
                            pictureBoxLeftArrow.Visible = true;
                            pictureBoxRightArrow.Visible = true;

                            pictureBoxLeftArrow.Location = new Point(550, 14);

                            labelPage1.Location = new Point(581, 14);
                            labelPage2.Location = new Point(612, 14);
                            labelPage3.Location = new Point(644, 14);
                        }
                        else if (i == 4)
                        {
                            pictureBoxLeftArrow.Visible = true;
                            pictureBoxRightArrow.Visible = true;

                            pictureBoxLeftArrow.Location = new Point(519, 14);

                            labelPage1.Location = new Point(550, 14);
                            labelPage2.Location = new Point(581, 14);
                            labelPage3.Location = new Point(612, 14);
                            labelPage4.Location = new Point(644, 14);
                        }
                        return;
                    }

                }

                if (totalPages == 5)
                {
                    labelPage1.Location = new Point(519, 14);
                    labelPage2.Location = new Point(550, 14);
                    labelPage3.Location = new Point(581, 14);
                    labelPage4.Location = new Point(612, 14);

                    labelPage5.Visible = true;
                    labelPage5.Text = totalPages.ToString();
                    labelPage5.Location = new Point(644, 14);
                }

                if (totalPages > 5)
                {
                    labelPage1.Location = new Point(488, 14);
                    labelPage2.Location = new Point(519, 14);
                    labelPage3.Location = new Point(550, 14);
                    labelPage4.Location = new Point(581, 14);

                    pictureBoxSecondDot.Visible = true;
                    pictureBoxSecondDot.Location = new Point(612, 14);

                    labelPage5.Visible = true;
                    labelPage5.Text = totalPages.ToString();
                    labelPage5.Location = new Point(644, 14);
                }
            }

            else if (currentPage > 4 && currentPage <= totalPages - 3)
            {
                pictureBoxLeftArrow.Location = new Point(425, 14);

                labels[0].Visible = true;
                labels[0].Text = "1";
                labels[0].Location = new Point(459, 14);

                pictureBoxFirst3Dot.Visible = true;
                pictureBoxFirst3Dot.Location = new Point(488, 14);


                for (int i = -1; i <= 1; i++)
                {
                    labels[i + 2].Visible = true;
                    labels[i + 2].Text = (currentPage + i).ToString();

                    if (i == -1)
                    {
                        labels[i + 2].Location = new Point(519, 14);
                    }
                    else if (i == 0)
                    {
                        labels[i + 2].Location = new Point(550, 14);
                    }
                    else if (i == 1)
                    {
                        labels[i + 2].Location = new Point(581, 14);
                    }
                }


                pictureBoxSecondDot.Visible = true;
                pictureBoxSecondDot.Location = new Point(612, 14);
                labelPage5.Visible = true;
                labelPage5.Text = totalPages.ToString();
            }

            else if (currentPage > totalPages - 3)
            {
                pictureBoxLeftArrow.Location = new Point(488, 14);

                labels[0].Visible = true;
                labels[0].Text = "1";
                labels[0].Location = new Point(519, 14);

                pictureBoxFirst3Dot.Visible = true;
                pictureBoxFirst3Dot.Location = new Point(550, 14);

                pictureBoxSecondDot.Visible = false;


                int start = totalPages - 2;
                for (int i = 0; i < 3; i++)
                {
                    labels[i + 1].Visible = true;
                    labels[i + 1].Text = (start + i).ToString();

                    if (i == 0)
                    {
                        labels[i + 1].Location = new Point(581, 14);
                    }
                    else if (i == 1)
                    {
                        labels[i + 1].Location = new Point(612, 14);
                    }
                    else if (i == 2)
                    {
                        labels[i + 1].Location = new Point(644, 14);
                    }
                }
            }

            foreach (var item in labels)
            {
                if (item.Text == currentPage.ToString())
                {
                    item.BackColor = Color.Blue;
                    item.ForeColor = Color.White;
                }
                else
                {
                    if (isDarkMode)
                    {
                        item.BackColor = Color.Black;
                        item.ForeColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = Color.White;
                        item.ForeColor = Color.Black;
                    }

                }
            }
        }


        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                FillDataGrid();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < Math.Ceiling((double)allDoctorData.Count / itemsPerPage))
            {
                currentPage++;
                FillDataGrid();
            }
        }

        private void btnPageGoTo_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;

            if (label == null)
            {
                return;
            }
            currentPage = Convert.ToInt32(label.Text);
            FillDataGrid();
        }


        private DoctorAvailability GetDoctorInfoById(int doctorId)
        {
            string query = "SELECT d.Name,d.Surname,d.Gender, d.PoliclinicId, p.[Poliklinik Adı] " +
                           "FROM Doctors d " +
                           "INNER JOIN Policlinics p ON d.PoliclinicId = p.Id " +
                           "WHERE d.Id = @DoctorId";

            DoctorAvailability doctorInfo = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DoctorId", SqlDbType.Int).Value = doctorId;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doctorInfo = new DoctorAvailability
                            {
                                DoctorName = reader.GetString(0),
                                DoctorSurname = reader.GetString(1),
                                Gender = reader.GetString(2),
                                PoliclinicId = reader.GetInt32(3),
                                PoliclinicName = reader.GetString(4)
                            };
                        }
                    }
                }
            }

            return doctorInfo;
        }
        private void AppointmentList_TextChanged(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (bunifuTextBox == null)
            {
                return;
            }

            int width = TextRenderer.MeasureText(bunifuTextBox.Text, bunifuTextBox.Font).Width + 10;
            bunifuTextBox.Width = width;
        }

        private void AppointmentDashboard_TextChanged(object sender, EventArgs e)
        {
            BunifuTextBox bunifuTextBox = sender as BunifuTextBox;

            if (bunifuTextBox == null)
            {
                return;
            }

            int width = TextRenderer.MeasureText(bunifuTextBox.Text, bunifuTextBox.Font).Width + 30;
            bunifuTextBox.Width = width;
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            panelAppointmentList.Visible = false;
            CloseListAppointmentPageSlider();
            await Task.Run(() => animator.Show(panelAppointmentChoose, true));
        }

        private void pictureBoxAppointmentChoose_Click(object sender, EventArgs e)
        {
            DashboardOpen();
        }

        private void btn_AppointmentChoose_MouseEnter(object sender, EventArgs e)
        {
            btn_AppointmentChoose.Size = new Size(592, 250);
            btn_AppointmentChoose.Location = new Point(49, 273);

            pictureBoxBtnAppointment.Size = new Size(90, 90);
            pictureBoxBtnAppointment.Location = new Point(80, 355);

            labelbtnTitle.Font = new Font("Roboto", 16, FontStyle.Bold);
            labelbtnTitle.Location = new Point(170, 355);

            labelBtnDescription.Font = new Font("Roboto", 11, FontStyle.Bold);
            labelBtnDescription.Location = new Point(170, 385);
        }
        private void btn_AppointmentChoose_MouseLeave(object sender, EventArgs e)
        {
            btn_AppointmentChoose.Size = new Size(572, 239);
            btn_AppointmentChoose.Location = new Point(62, 279);

            pictureBoxBtnAppointment.Size = new Size(75, 70);
            pictureBoxBtnAppointment.Location = new Point(110, 367);

            labelbtnTitle.Font = new Font("Roboto", 14, FontStyle.Bold);
            labelbtnTitle.Location = new Point(185, 365);

            labelBtnDescription.Font = new Font("Roboto", 10, FontStyle.Bold);
            labelBtnDescription.Location = new Point(186, 396);
        }

        private void btn_AccountDetails_MouseEnter(object sender, EventArgs e)
        {
            btn_AccountDetails.Size = new Size(592, 250);
            btn_AccountDetails.Location = new Point(49, 15);

            pictureBoxAccount.Size = new Size(88, 92);
            pictureBoxAccount.Location = new Point(115, 100);

            labelbtnAccountTitle.Font = new Font("Roboto", 16, FontStyle.Bold);
            labelbtnAccountTitle.Location = new Point(198, 110);

            labelAccountDescription.Font = new Font("Roboto", 11, FontStyle.Bold);
            labelAccountDescription.Location = new Point(198, 145);
        }

        private void btn_AccountDetails_MouseLeave(object sender, EventArgs e)
        {
            btn_AccountDetails.Size = new Size(572, 239);
            btn_AccountDetails.Location = new Point(62, 21);

            pictureBoxAccount.Size = new Size(78, 82);
            pictureBoxAccount.Location = new Point(110, 100);

            labelbtnAccountTitle.Font = new Font("Roboto", 14, FontStyle.Bold);
            labelbtnAccountTitle.Location = new Point(185, 105);

            labelAccountDescription.Font = new Font("Roboto", 10, FontStyle.Bold);
            labelAccountDescription.Location = new Point(186, 138);
        }
        private void AppointmentDateInformationCloseProperty()
        {
            //ActiveAppointment

            txtActiveAppointment1.Visible = false;
            panelActive1.Visible = false;

            txtActiveAppointment2.Visible = false;
            panelActive2.Visible = false;

            txtActiveAppointment3.Visible = false;
            panelActive3.Visible = false;

            txtActiveAppointment4.Visible = false;
            panelActive4.Visible = false;

            txtActiveAppointment5.Visible = false;
            panelActive5.Visible = false;

            pictureBoxActiveAppointmentNotFound.Visible = false;

            labelAppointmentActiveNotFound.Visible = false;

            dataGridActiveAppointment.Visible = false;
            panelActiveAppointmentBorder.Visible = false;

            //PastAppointment

            txtPastAppointmentDate1.Visible = false;
            panelPast1.Visible = false;

            txtPastAppointmentDate2.Visible = false;
            panelPast2.Visible = false;

            txtPastAppointmentDate3.Visible = false;
            panelPast3.Visible = false;

            txtPastAppointmentDate4.Visible = false;
            panelPast4.Visible = false;

            txtPastAppointmentDate5.Visible = false;
            panelPast5.Visible = false;

            pictureBoxPastAppointment.Visible = false;

            labelPastAppointment.Visible = false;

            dataGridPastAppointment.Visible = false;
            panelPastAppointmentBorder.Visible = false;
        }
        private void dataGridActiveAppointment_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView dataGridView && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell?.Value is Image originalImage)
                {
                    cell.Tag = originalImage;

                    int zoomFactor = 50;
                    Image zoomedImage = new Bitmap(originalImage, new Size(originalImage.Width + zoomFactor, originalImage.Height + zoomFactor));

                    cell.Value = zoomedImage;
                }
            }
        }

        private void dataGridActiveAppointment_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView dataGridView && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell?.Tag is Image originalImage)
                {
                    cell.Value = originalImage;
                }
            }
        }

        private async void TabControlAppointment_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            cannotButtonClick = false;
            await Task.Delay(100);
            if (e.Page.Text == "Randevularım")
            {
                await Task.Run(() => animatorLeft.Hide(panelPastAppointment, true));
                await Task.Delay(500);
                await Task.Run(() => animator.Show(panelActiveAppointment, true));
            }
            else if (e.Page.Text == "Geçmiş Randevularım")
            {
                await Task.Run(() => animator.Hide(panelActiveAppointment, true));
                await Task.Delay(500);
                await Task.Run(() => animatorLeft.Show(panelPastAppointment, true));
            }
            await Task.Delay(500);
            cannotButtonClick = true;
        }

        private async void btn_AppointmentChoose_Click(object sender, EventArgs e)
        {
            if (cannotButtonClick)
            {
                panelDashboard.Visible = false;
                panelDoctorAppointmentChoose.Visible = false;
                panelActiveAppointment.Visible = false;
                panelPastAppointment.Visible = false;

                await Task.Run(() => animator.Show(panelAppointmentChoose, true));
                panelActiveAppointment.Visible = false;
                panelPastAppointment.Visible = false;

                AppointmentDateInformationCloseProperty();
            }

        }

        private async void bunifuButton1_Click(object sender, EventArgs e)
        {
            BunifuButton bunifuButton = sender as BunifuButton;
            BunifuPanel targetPanel = null;

            if (bunifuButton.Name == "btn_EightClock")
                targetPanel = panelEightClock;
            else if (bunifuButton.Name == "btn_NineClock")
                targetPanel = panelNineClock;
            else if (bunifuButton.Name == "btn_TenClock")
                targetPanel = panelTenClock;
            else if (bunifuButton.Name == "btn_ElevenClock")
                targetPanel = panelElevenClock;
            else if (bunifuButton.Name == "btn_ThirteenClock")
                targetPanel = panelThirteenClock;
            else if (bunifuButton.Name == "btn_FourteenClock")
                targetPanel = panelFourTeenClock;
            else if (bunifuButton.Name == "btn_FifteenClock")
                targetPanel = panelFifteenClock;
            else if (bunifuButton.Name == "btn_SixteenClock")
                targetPanel = panelSixTeenClock;

            if (targetPanel != null)
                await TogglePanelSizeAsync(targetPanel, bunifuButton);
        }


        private async Task TogglePanelSizeAsync(BunifuPanel targetPanel, BunifuButton targetButton)
        {
            if (currentOpenPanel != null && currentOpenPanel != targetPanel && currentOpenButton != null && currentOpenButton != targetButton)
            {
                await ResizePanelAsync(currentOpenPanel, currentOpenPanel.MinimumSize.Height);

                await Task.Delay(1);
                currentOpenButton.IdleIconLeftImage = Properties.Resources.right_arrowDark;

                if (currentOpenButton.Name == "btn_SixteenClock")
                {
                    await Task.Delay(1);
                    currentOpenButton.CustomizableEdges.BottomRight = true;
                    currentOpenButton.CustomizableEdges.BottomLeft = true;
                }

                currentOpenPanel = null;
                currentOpenButton = null;
            }

            if (!isPanelMaximized || currentOpenPanel != targetPanel)
            {
                currentOpenPanel = targetPanel;
                currentOpenButton = targetButton;
                await Task.Delay(1);

                currentOpenButton.IdleIconLeftImage = Properties.Resources.downDark;

                if (currentOpenButton.Name == "btn_SixteenClock")
                {
                    await Task.Delay(1);
                    currentOpenButton.CustomizableEdges.BottomRight = false;
                    currentOpenButton.CustomizableEdges.BottomLeft = false;
                }

                targetHeight = targetPanel.MaximumSize.Height;

                await ResizePanelAsync(targetPanel, targetHeight);
            }
            else
            {
                targetHeight = targetPanel.MinimumSize.Height;
                currentOpenPanel = null;
                currentOpenButton = null;

                await ResizePanelAsync(targetPanel, targetHeight);
            }
        }

        private async Task ResizePanelAsync(BunifuPanel panel, int targetHeight)
        {
            while (panel.Height != targetHeight)
            {
                int step = Math.Sign(targetHeight - panel.Height) * stepSize;

                panel.Height += step;

                if ((step > 0 && panel.Height >= targetHeight) || (step < 0 && panel.Height <= targetHeight))
                {
                    panel.Height = targetHeight;
                    break;
                }

                await Task.Delay(15);
            }

            isPanelMaximized = panel.Height == targetHeight;
        }
        private async Task<List<string>> GetAllAppointmentTimesAsync()
        {
            List<string> allTimes = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT AppointmentTime FROM AppointmentTime";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        string time = reader.GetTimeSpan(0).ToString(@"hh\:mm");
                        allTimes.Add(time);
                    }
                }
            }

            return allTimes;
        }


        private async Task<List<string>> GetBookedAppointmentTimesAsync(int doctorId, DateTime selectedDate)
        {
            List<string> bookedTimes = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
            SELECT AT.AppointmentTime
            FROM PatientAppointment PA
            INNER JOIN AppointmentTime AT ON PA.AppointmentTimeId = AT.Id
            WHERE PA.DoctorId = @DoctorId AND PA.Date = @Date";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorId", doctorId);
                    command.Parameters.AddWithValue("@Date", selectedDate.Date); // Sadece tarih karşılaştırması yapıyoruz

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        string time = reader.GetTimeSpan(0).ToString(@"hh\:mm");
                        bookedTimes.Add(time);
                    }
                }
            }

            return bookedTimes;
        }


        private async Task CreateButtonsForPanelsAsync(int doctorId, DateTime selectedDate)
        {

            List<string> allTimes = await GetAllAppointmentTimesAsync();
            var sortedTimes = allTimes
            .Select(time => TimeSpan.Parse(time)) // Stringleri TimeSpan'e çevir
            .OrderBy(time => time)               // Küçükten büyüğe sırala
            .Select(time => time.ToString(@"hh\:mm")) // Geri stringe çevir
            .ToList();
            List<string> bookedTimes = await GetBookedAppointmentTimesAsync(doctorId, selectedDate);


            ClearPanelControls();
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "08:00", "09:00", panelEightClock, selectedDate);
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "09:00", "10:00", panelNineClock, selectedDate);
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "10:00", "11:00", panelTenClock, selectedDate);
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "11:00", "12:00", panelElevenClock, selectedDate);
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "13:00", "14:00", panelThirteenClock, selectedDate);
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "14:00", "15:00", panelFourTeenClock, selectedDate);
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "15:00", "16:00", panelFifteenClock, selectedDate);
            await CreateButtonsForTimePeriodAsync(sortedTimes, bookedTimes, "16:00", "17:00", panelSixTeenClock, selectedDate);
        }
        private void ClearPanelControls()
        {

            panelEightClock.Controls.Clear();
            panelNineClock.Controls.Clear();
            panelTenClock.Controls.Clear();
            panelElevenClock.Controls.Clear();
            panelThirteenClock.Controls.Clear();
            panelFourTeenClock.Controls.Clear();
            panelFifteenClock.Controls.Clear();
            panelSixTeenClock.Controls.Clear();
        }
        private async Task CreateButtonsForTimePeriodAsync(List<string> allTimes, List<string> bookedTimes, string startTime, string endTime, BunifuPanel panel, DateTime selectedDate)
        {
            DateTime currentDateTime = DateTime.Now;
            bool isToday = false;

            if (selectedDate.Date == currentDateTime.Date)
            {
                isToday = true;
            }

            var timePeriod = allTimes
                .Where(time => string.Compare(time, startTime) >= 0 && string.Compare(time, endTime) < 0)
                .ToList();

            int buttonHorizontal = 38;
            foreach (var time in timePeriod)
            {

                TimeSpan buttonTime = TimeSpan.Parse(time);
                TimeSpan currentPlusOneHour = currentDateTime.TimeOfDay.Add(TimeSpan.FromHours(1));


                bool isEnabled = !bookedTimes.Contains(time);
                if (isToday && buttonTime < currentPlusOneHour)
                {
                    isEnabled = false;
                }

                var newButton = new BunifuButton
                {
                    Text = time,
                    Size = new Size(100, 40),
                    Margin = new Padding(0, 0, 0, 40),
                    IdleBorderColor = Color.FromArgb(12, 59, 103),
                    IdleFillColor = Color.FromArgb(12, 59, 103),
                    IdleBorderRadius = 20,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Roboto", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand,
                    Enabled = isEnabled,
                    onHoverState = new BunifuButton.StateProperties
                    {
                        BorderColor = Color.FromArgb(16, 78, 136),
                        FillColor = Color.FromArgb(16, 78, 136),
                        ForeColor = Color.White,
                    },
                    OnPressedState = new BunifuButton.StateProperties
                    {
                        BorderColor = Color.FromArgb(50, 150, 255),
                        FillColor = Color.FromArgb(50, 150, 255),
                        ForeColor = Color.White,
                    }
                };

                panel.Controls.Add(newButton);
                newButton.Click += new EventHandler(btnAppointmentTime_Click);
                newButton.Location = new Point(buttonHorizontal, 22);
                buttonHorizontal += newButton.Width + 50;

                await Task.Delay(10);
            }
        }
        private async void datagridAppointment_SelectionChanged(object sender, EventArgs e)
        {
            if (isAddingData)
                return;

            if (datagridAppointment.SelectedRows.Count > 0)
            {
                var selectedRow = datagridAppointment.SelectedRows[0];

                currentDoctorId = Convert.ToInt32(selectedRow.Cells["doctorId"].Value);
                var doktorAdi = selectedRow.Cells["doctorNameSurname"].Value.ToString();
                var poliklinikID = selectedRow.Cells["PoliclinicId"].Value.ToString();
                var poliklinikAdi = selectedRow.Cells["policlinicName"].Value.ToString();
                var chooseTime = selectedRow.Cells["appointmentDate"].Value.ToString();

                isAddingData = true;

                labelDoctorName.Text = doktorAdi;
                DoctorNameAccept = doktorAdi;
                DoctorIdAccept = currentDoctorId;
                policlinicNameAccept = poliklinikAdi;
                PoliclinicIdAccept = Convert.ToInt32(poliklinikID);

                txtPoliclinicAndDoctorName.Text = $"{poliklinikAdi.ToUpper()} - {doktorAdi.ToUpper()}";
                UpdateLabels(currentDoctorId, DateTime.Parse(chooseTime));
                labelDoctorChooseDate1_Click(chooseTime, e);
            }
            pictureBoxAppointmentLoading.Visible = true;
            panelAppointmentList.Visible = false;
            await Task.Delay(2500);
            pictureBoxAppointmentLoading.Visible = false;
            panelDoctorAppointmentChoose.Visible = true;
        }

        private void UpdateLabels(int doctorId, DateTime selectedDate)
        {
            var doctorAvailabilityList = allDoctorData.Where(d => d.DoctorId == doctorId).ToList();

            if (doctorAvailabilityList == null || doctorAvailabilityList.Count == 0)
                return;

            List<DateTime> doctorScheduleList = doctorAvailabilityList.Select(d => d.AvailableDate).OrderBy(d => d).ToList();

            int selectedIndex = doctorScheduleList.FindIndex(d => d.Date == selectedDate.Date);

            if (selectedIndex == -1)
                return;

            UpdateLabelDates(selectedIndex, doctorScheduleList);
        }

        private void UpdateLabelDates(int selectedIndex, List<DateTime> doctorScheduleList)
        {
            labelDoctorChooseDate1.Text = progressBarNull.Size.ToString();

            labelDoctorChooseDate1.Text = "";
            labelDoctorChooseDate2.Text = "";
            labelDoctorChooseDate3.Text = "";
            labelDoctorChooseDate4.Text = "";
            labelDoctorChooseDate5.Text = "";

            ResetLabelFonts();

            if (selectedIndex == 0)
            {
                labelDoctorChooseDate1.Text = doctorScheduleList[0].ToString("dd.MM.yyyy - dddd");
                if (doctorScheduleList.Count > 1)
                    labelDoctorChooseDate2.Text = doctorScheduleList[1].ToString("dd.MM.yyyy - dddd");
                if (doctorScheduleList.Count > 2)
                    labelDoctorChooseDate3.Text = doctorScheduleList[2].ToString("dd.MM.yyyy - dddd");
                if (doctorScheduleList.Count > 3)
                    labelDoctorChooseDate4.Text = doctorScheduleList[3].ToString("dd.MM.yyyy - dddd");
                if (doctorScheduleList.Count > 4)
                    labelDoctorChooseDate5.Text = doctorScheduleList[4].ToString("dd.MM.yyyy - dddd");

                labelDoctorChooseDate1.Font = new Font("Roboto", 10, FontStyle.Bold);
                labelDoctorChooseDate1.ForeColor = isDarkMode ? Color.White : Color.Black;
                labelDoctorChooseDate1.BackColor = isDarkMode ? Color.Black : Color.White;

                progressBarMoveIt.Size = new Size(labelDoctorChooseDate1.Size.Width, progressBarMoveIt.Size.Height);
                progressBarMoveIt.Location = new Point(labelDoctorChooseDate1.Location.X, 200);

                return;
            }

            if (selectedIndex == 1)
            {
                labelDoctorChooseDate1.Text = doctorScheduleList[0].ToString("dd.MM.yyyy - dddd");
                labelDoctorChooseDate2.Text = doctorScheduleList[1].ToString("dd.MM.yyyy - dddd");
                if (doctorScheduleList.Count > 2)
                    labelDoctorChooseDate3.Text = doctorScheduleList[2].ToString("dd.MM.yyyy - dddd");
                if (doctorScheduleList.Count > 3)
                    labelDoctorChooseDate4.Text = doctorScheduleList[3].ToString("dd.MM.yyyy - dddd");
                if (doctorScheduleList.Count > 4)
                    labelDoctorChooseDate5.Text = doctorScheduleList[4].ToString("dd.MM.yyyy - dddd");

                labelDoctorChooseDate2.Font = new Font("Roboto", 10, FontStyle.Bold);
                labelDoctorChooseDate2.ForeColor = Color.White;
                labelDoctorChooseDate2.ForeColor = isDarkMode ? Color.White : Color.Black;
                labelDoctorChooseDate2.BackColor = isDarkMode ? Color.Black : Color.White;

                progressBarMoveIt.Size = new Size(labelDoctorChooseDate2.Size.Width, progressBarMoveIt.Size.Height);
                progressBarMoveIt.Location = new Point(labelDoctorChooseDate2.Location.X, 200);

                return;
            }

            int lastIndex = doctorScheduleList.Count - 1;
            if (selectedIndex == lastIndex)
            {
                labelDoctorChooseDate5.Text = doctorScheduleList[lastIndex].ToString("dd.MM.yyyy - dddd");
                if (lastIndex > 0)
                    labelDoctorChooseDate4.Text = doctorScheduleList[lastIndex - 1].ToString("dd.MM.yyyy - dddd");
                if (lastIndex > 1)
                    labelDoctorChooseDate3.Text = doctorScheduleList[lastIndex - 2].ToString("dd.MM.yyyy - dddd");
                if (lastIndex > 2)
                    labelDoctorChooseDate2.Text = doctorScheduleList[lastIndex - 3].ToString("dd.MM.yyyy - dddd");
                if (lastIndex > 3)
                    labelDoctorChooseDate1.Text = doctorScheduleList[lastIndex - 4].ToString("dd.MM.yyyy - dddd");

                labelDoctorChooseDate5.Font = new Font("Roboto", 10, FontStyle.Bold);
                labelDoctorChooseDate5.ForeColor = Color.White;
                labelDoctorChooseDate5.ForeColor = isDarkMode ? Color.White : Color.Black;
                labelDoctorChooseDate5.BackColor = isDarkMode ? Color.Black : Color.White;

                progressBarMoveIt.Size = new Size(labelDoctorChooseDate5.Size.Width, progressBarMoveIt.Size.Height);
                progressBarMoveIt.Location = new Point(labelDoctorChooseDate5.Location.X, 200);

                return;
            }

            if (selectedIndex == lastIndex - 1)
            {
                labelDoctorChooseDate5.Text = doctorScheduleList[lastIndex].ToString("dd.MM.yyyy - dddd");
                labelDoctorChooseDate4.Text = doctorScheduleList[lastIndex - 1].ToString("dd.MM.yyyy - dddd");
                labelDoctorChooseDate3.Text = doctorScheduleList[lastIndex - 2].ToString("dd.MM.yyyy - dddd");
                if (lastIndex > 2)
                    labelDoctorChooseDate2.Text = doctorScheduleList[lastIndex - 3].ToString("dd.MM.yyyy - dddd");
                if (lastIndex > 3)
                    labelDoctorChooseDate1.Text = doctorScheduleList[lastIndex - 4].ToString("dd.MM.yyyy - dddd");

                labelDoctorChooseDate4.Font = new Font("Roboto", 10, FontStyle.Bold);
                labelDoctorChooseDate4.ForeColor = Color.White;
                labelDoctorChooseDate4.ForeColor = isDarkMode ? Color.White : Color.Black;
                labelDoctorChooseDate4.BackColor = isDarkMode ? Color.Black : Color.White;

                progressBarMoveIt.Size = new Size(labelDoctorChooseDate4.Size.Width, progressBarMoveIt.Size.Height);
                progressBarMoveIt.Location = new Point(labelDoctorChooseDate4.Location.X, 200);

                return;
            }

            labelDoctorChooseDate3.Text = doctorScheduleList[selectedIndex].ToString("dd.MM.yyyy - dddd");
            if (selectedIndex - 1 >= 0)
                labelDoctorChooseDate2.Text = doctorScheduleList[selectedIndex - 1].ToString("dd.MM.yyyy - dddd");
            if (selectedIndex - 2 >= 0)
                labelDoctorChooseDate1.Text = doctorScheduleList[selectedIndex - 2].ToString("dd.MM.yyyy - dddd");
            if (selectedIndex + 1 < doctorScheduleList.Count)
                labelDoctorChooseDate4.Text = doctorScheduleList[selectedIndex + 1].ToString("dd.MM.yyyy - dddd");
            if (selectedIndex + 2 < doctorScheduleList.Count)
                labelDoctorChooseDate5.Text = doctorScheduleList[selectedIndex + 2].ToString("dd.MM.yyyy - dddd");

            labelDoctorChooseDate3.Font = new Font("Roboto", 10, FontStyle.Bold);
            labelDoctorChooseDate3.ForeColor = Color.White;
            labelDoctorChooseDate3.ForeColor = isDarkMode ? Color.White : Color.Black;
            labelDoctorChooseDate3.BackColor = isDarkMode ? Color.Black : Color.White;

            progressBarMoveIt.Size = new Size(labelDoctorChooseDate3.Size.Width, progressBarMoveIt.Size.Height);
            progressBarMoveIt.Location = new Point(labelDoctorChooseDate3.Location.X, 200);

        }

        private void ResetLabelFonts()
        {
            labelDoctorChooseDate1.Font = new Font("Roboto", 10);
            labelDoctorChooseDate1.ForeColor = Color.FromArgb(98, 98, 98);

            labelDoctorChooseDate2.Font = new Font("Roboto", 10);
            labelDoctorChooseDate2.ForeColor = Color.FromArgb(98, 98, 98);

            labelDoctorChooseDate3.Font = new Font("Roboto", 10);
            labelDoctorChooseDate3.ForeColor = Color.FromArgb(98, 98, 98);

            labelDoctorChooseDate4.Font = new Font("Roboto", 10);
            labelDoctorChooseDate4.ForeColor = Color.FromArgb(98, 98, 98);

            labelDoctorChooseDate5.Font = new Font("Roboto", 10);
            labelDoctorChooseDate5.ForeColor = Color.FromArgb(98, 98, 98);
        }


        private void OnLabelClick(DateTime clickedDate, int doctorId, Label clickedLabel)
        {
            var doctorAvailabilityList = allDoctorData.Where(d => d.DoctorId == doctorId).ToList();
            if (doctorAvailabilityList == null || doctorAvailabilityList.Count == 0)
                return;

            List<DateTime> doctorScheduleList = doctorAvailabilityList.Select(d => d.AvailableDate).OrderBy(d => d).ToList();
            int clickedIndex = doctorScheduleList.FindIndex(d => d.Date == clickedDate.Date);

            if (clickedIndex == -1)
                return;

            if ((clickedIndex == 0 || clickedIndex == 1 || clickedIndex == doctorScheduleList.Count - 1 || clickedIndex == doctorScheduleList.Count - 2) && clickedLabel != null)
            {
                ResetLabelFonts();

                clickedLabel.Font = new Font("Roboto", 10, FontStyle.Bold);

                clickedLabel.ForeColor = isDarkMode ? Color.White : Color.Black;
                clickedLabel.BackColor = isDarkMode ? Color.Black : Color.White;

                if (clickedLabel.Name == "labelDoctorChooseDate1")
                {
                    progressBarMoveIt.Size = new Size(labelDoctorChooseDate1.Size.Width, progressBarMoveIt.Size.Height);
                    progressBarMoveIt.Location = new Point(labelDoctorChooseDate1.Location.X, 200);
                }
                else if (clickedLabel.Name == "labelDoctorChooseDate2")
                {
                    progressBarMoveIt.Size = new Size(labelDoctorChooseDate2.Size.Width, progressBarMoveIt.Size.Height);
                    progressBarMoveIt.Location = new Point(labelDoctorChooseDate2.Location.X, 200);
                }
                else if (clickedLabel.Name == "labelDoctorChooseDate4")
                {
                    progressBarMoveIt.Size = new Size(labelDoctorChooseDate4.Size.Width, progressBarMoveIt.Size.Height);
                    progressBarMoveIt.Location = new Point(labelDoctorChooseDate4.Location.X, 200);
                }
                else if (clickedLabel.Name == "labelDoctorChooseDate5")
                {
                    progressBarMoveIt.Size = new Size(labelDoctorChooseDate5.Size.Width, progressBarMoveIt.Size.Height);
                    progressBarMoveIt.Location = new Point(labelDoctorChooseDate5.Location.X, 200);
                }

                return;
            }
            UpdateLabels(doctorId, clickedDate);
        }




        private async void labelDoctorChooseDate1_Click(object sender, EventArgs e)
        {
            DateTime clickedDate;
            Label clickedLabel = sender as Label;
            if (clickedLabel == null && sender == null)
            {
                return;
            }
            else if (clickedLabel == null)
            {
                clickedDate = DateTime.Parse(sender.ToString());
            }
            else
            {
                clickedDate = DateTime.Parse(clickedLabel.Text.Replace("-", " "));
            }
            dateAccept = clickedDate;
            OnLabelClick(clickedDate, currentDoctorId, clickedLabel);

            await CreateButtonsForPanelsAsync(currentDoctorId, clickedDate);
        }

        private async void pictureBoxAppointmentList_Click(object sender, EventArgs e)
        {
            if (currentOpenPanel != null)
            {
                await ResizePanelAsync(currentOpenPanel, currentOpenPanel.MinimumSize.Height);
            }

            isAddingData = false;

            panelDoctorAppointmentChoose.Visible = false;
            await Task.Delay(100);
            animatorLeft.Show(panelAppointmentList, true);
        }

        private async void btnAppointmentTime_Click(object sender, EventArgs e)
        {
            BunifuButton bunifuButton = sender as BunifuButton;
            string timeText = bunifuButton.Text;

            int timeId = await GetTimeId(timeText);
            var result = await PatientInformation(id);
            string patientName = $"{result.Name} {result.Surname}";

            var isStatus = await CheckAppointmentStatus(id, DoctorIdAccept, PoliclinicIdAccept, dateAccept, timeId);

            if (!isStatus.isStatus && (isStatus.status == "MaxAppointment" || isStatus.status == "Day"))
            {
                if (isStatus.status == "MaxAppointment")
                {
                    AppointmentRulesWarningForm maxAppointmentWarningForm = new AppointmentRulesWarningForm("MaxAppointment");
                    maxAppointmentWarningForm.Show();
                    return;
                }
                else if (isStatus.status == "Day")
                {
                    AppointmentRulesWarningForm maxAppointmentWarningForm = new AppointmentRulesWarningForm("Day");
                    maxAppointmentWarningForm.Show();
                    return;
                }
            }

            AppointmentAcceptWarningScreenForm appointmentAcceptWarningScreenForm = new AppointmentAcceptWarningScreenForm(dateAccept);
            appointmentAcceptWarningScreenForm.ShowDialog();

            if (appointmentAcceptWarningScreenForm.AccessibleName == "Success")
            {
                AppointmentAcceptScreenForm appointmentAcceptScreenForm = new AppointmentAcceptScreenForm(dateAccept, timeText, PoliclinicIdAccept, policlinicNameAccept, DoctorIdAccept, DoctorNameAccept, patientName, mail, id);
                appointmentAcceptScreenForm.ShowDialog();

                if (appointmentAcceptScreenForm.AccessibleName == "Success")
                {
                    AppointmentSuccessForm appointmentSuccessForm = new AppointmentSuccessForm(mail);
                    appointmentSuccessForm.Show();

                    await Task.Delay(100);
                    CloseListAppointmentPageSlider();
                    await Task.Delay(100);
                    panelDoctorAppointmentChoose.Visible = false;
                    await ResizePanelAsync(currentOpenPanel, currentOpenPanel.MinimumSize.Height);
                    await Task.Delay(100);
                    DashboardOpen();
                }
            }
        }

        public async Task<(string TcNo, string Name, string Surname, string Gender, DateTime Date, string Email, bool EmailConfirm, string PhoneNumber, string Password)> PatientInformation(int patientId)
        {
            string query = "SELECT TcNo,Name, Surname,Gender, Date, Email, EmailConfirm,PhoneNumber,Password FROM Patients WHERE ID = @patientId";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@patientId", patientId);

                    try
                    {
                        await conn.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string tcno = reader["TcNo"].ToString();
                                string name = reader["Name"].ToString();
                                string surname = reader["Surname"].ToString();
                                string gender = reader["Gender"].ToString();
                                DateTime date = DateTime.Parse(reader["Date"].ToString());
                                string email = reader["Email"].ToString();
                                bool emailConfirm = Convert.ToBoolean(reader["EmailConfirm"].ToString());
                                string phoneNumber = reader["PhoneNumber"].ToString();
                                string password = reader["Password"].ToString();



                                mail = email;
                                return (tcno, name, surname, gender, date, email, emailConfirm, phoneNumber, password);
                            }
                            else
                            {
                                return (null, null, null, null, DateTime.Parse("12.12.1912"), null, false, null, null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                        return (null, null, null, null, DateTime.Parse("12.12.1912"), null, false, null, null);
                    }
                }
            }
        }

        private async void DashboardOpen()
        {
            panelDoctorAppointmentChoose.Visible = false;
            panelAppointmentChoose.Visible = false;
            panelAppointmentList.Visible = false;
            panelDarkModeSettings.Visible = false;
            panelActivePastAppointment.Visible = false;
            panelMyInformation.Visible = false;

            await Task.Delay(200);
            animator.Show(panelDashboard, true);
            cannotButtonClick = false;
            await Task.Delay(500);
            XtraTabPage selectedTab = TabControlDashboard.SelectedTabPage;
            await LoadActiveAppointments(id);
            await LoadPastAppointments(id);

            if (selectedTab.Name == "nowAppointments")
            {
                panelActiveAppointment.Visible = true;
                panelPastAppointment.Visible = false;
            }
            else if (selectedTab.Name == "oldAppointments")
            {
                panelActiveAppointment.Visible = false;
                panelPastAppointment.Visible = true;

            }

            await Task.Delay(500);
            cannotButtonClick = true;

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
        private async Task<(bool isStatus, string status)> CheckAppointmentStatus(int userId, int doctorId, int policlinicId, DateTime appointmentDate, int appointmentTimeId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();


                string query1 = @"
            SELECT COUNT(*) 
            FROM PatientAppointment
            WHERE UserId = @UserId 
              AND Status = 'Aktif'
              AND Date >= CAST(GETDATE() AS DATE)
              AND Date <= DATEADD(DAY, 30, CAST(GETDATE() AS DATE));";

                using (var command1 = new SqlCommand(query1, connection))
                {
                    command1.Parameters.AddWithValue("@UserId", userId);
                    int activeAppointmentCount = (int)await command1.ExecuteScalarAsync();

                    if (activeAppointmentCount >= 3)
                    {
                        return (false, "MaxAppointment");
                    }
                }

                string query2 = @"
            SELECT COUNT(*) 
            FROM PatientAppointment
            WHERE UserId = @UserId 
              AND Status = 'Aktif'
              AND Date = @AppointmentDate
              AND PoliclinicId = @PoliclinicId
              AND DoctorId = @DoctorId;";

                using (var command2 = new SqlCommand(query2, connection))
                {
                    command2.Parameters.AddWithValue("@UserId", userId);
                    command2.Parameters.AddWithValue("@AppointmentDate", appointmentDate.Date);
                    command2.Parameters.AddWithValue("@PoliclinicId", policlinicId);
                    command2.Parameters.AddWithValue("@DoctorId", doctorId);

                    int conflictingAppointmentCount = (int)await command2.ExecuteScalarAsync();

                    if (conflictingAppointmentCount > 0)
                    {
                        return (false, "Day");
                    }
                }
                return (true, "true");
            }
        }

        private void btn_Information_MouseEnter(object sender, EventArgs e)
        {
            targetPanelSize = panelInformation.MaximumSize;
            panelResizeTimer.Start();
            panelInformation.Visible = true;
            btn_Information.CustomizableEdges.BottomLeft = false;
            btn_Information.CustomizableEdges.BottomRight = false;
        }

        private async void btn_Information_MouseLeave(object sender, EventArgs e)
        {
            targetPanelSize = panelInformation.MinimumSize;
            panelResizeTimer.Start();
            await Task.Delay(500);
            panelInformation.Visible = false;
            btn_Information.CustomizableEdges.BottomLeft = true;
            btn_Information.CustomizableEdges.BottomRight = true;
        }
        private void PanelResizeTimer_Tick(object sender, EventArgs e)
        {
            if (panelInformation.Size != targetPanelSize)
            {
                int newWidth = panelInformation.Width + (targetPanelSize.Width - panelInformation.Width) / 10;
                int newHeight = panelInformation.Height + (targetPanelSize.Height - panelInformation.Height) / 10;

                panelInformation.Size = new Size(newWidth, newHeight);
            }
            else
            {
                panelResizeTimer.Stop();
            }
        }
        private void AdjustButtonWidth(BunifuButton button)
        {

            using (Graphics g = button.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(button.Text, button.Font);


                int padding = 70;
                int newWidth = (int)textSize.Width + padding;

                button.Size = new Size(newWidth, button.Height);
            }
        }

        private async void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            if (panelDarkModeSettings.Visible)
            {
                return;
            }

            if (currentOpenPanel != null)
            {
                await ResizePanelAsync(currentOpenPanel, currentOpenPanel.MinimumSize.Height);
            }

            panelDashboard.Visible = false;
            panelAppointmentList.Visible = false;
            panelAppointmentChoose.Visible = false;
            panelDoctorAppointmentChoose.Visible = false;
            panelActivePastAppointment.Visible = false;
            panelMyInformation.Visible = false;

            panelActiveAppointment.Visible = false;
            panelPastAppointment.Visible = false;

            pictureBoxAppointmentLoading.Visible = true;
            await Task.Delay(1500);
            pictureBoxAppointmentLoading.Visible = false;
            panelDarkModeSettings.Visible = true;

            CloseListAppointmentPageSlider();

            AppointmentDateInformationCloseProperty();
        }

        private async void btn_DarkModeSave_Click(object sender, EventArgs e)
        {
            if (dropDownDarkMode.SelectedItem == "Koyu Renk" && !isDarkMode)
            {
                bool isMode = true;
                SaveSettings(isMode);
            }
            else if (dropDownDarkMode.SelectedItem == "Açık Renk" && isDarkMode)
            {
                bool isMode = false;
                SaveSettings(isMode);
            }
            else
            {
                if (isDarkMode)
                {
                    labelDarkModeWarning.Text = "Halihazırda Koyu renk kullanılmaktadır.";
                    labelDarkModeWarning.Visible = true;
                    return;
                }
                else
                {
                    labelDarkModeWarning.Text = "Halihazırda Açık renk kullanılmaktadır.";
                    labelDarkModeWarning.Visible = true;
                    return;
                }
            }

            panelDarkModeSettings.Visible = false;
            pictureBoxAppointmentLoading.Visible = true;
            await Task.Delay(1500);
            pictureBoxAppointmentLoading.Visible = false;

            this.AccessibleName = "restart";
            this.Close();
        }
        private void pictureBoxDashboard_Click(object sender, EventArgs e)
        {
            DashboardOpen();
        }

        private void PatientPanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.AccessibleName == "restart")
            {
                return;
            }
            PatientPanelClosingForm patientPanelClosingForn = new PatientPanelClosingForm();
            patientPanelClosingForn.ShowDialog();

            if (patientPanelClosingForn.AccessibleName == "Cancel")
            {
                e.Cancel = true;
            }
            else if (patientPanelClosingForn.AccessibleName == "LoginOpen")
            {
                this.AccessibleName = "LoginOpen";
                return;
            }
            else if (patientPanelClosingForn.AccessibleName == "Close")
            {
                this.AccessibleName = "Close";
                return;
            }
        }

        private void PageNumberSliderAllPast(int currentPage, int totalPages)
        {
            pictureBoxAllPastRight.Location = new Point(684, 14);
            pictureBoxAllPastLeft.Location = new Point(457, 14);


            if (currentPage > 1)
            {
                pictureBoxAllPastLeft.Visible = true;

            }
            else
            {
                pictureBoxAllPastLeft.Visible = false;
            }


            if (currentPage < totalPages)
            {
                pictureBoxAllPastRight.Visible = true;
            }
            else
            {
                pictureBoxAllPastRight.Visible = false;
            }

            if (currentPage <= 4)
            {
                picturePastFirstDot.Visible = false;

                int limit = Math.Min(4, totalPages);
                for (int i = 1; i <= limit; i++)
                {
                    labelPast[i - 1].Visible = true;
                    labelPast[i - 1].Text = i.ToString();

                    if (i == 1)
                    {
                        labelPast[i - 1].Location = new Point(519, 14);
                        pictureBoxAllPastLeft.Location = new Point(457, 14);
                    }
                    else if (i == 2)
                    {
                        labelPast[i - 1].Location = new Point(550, 14);
                    }
                    else if (i == 3)
                    {
                        labelPast[i - 1].Location = new Point(581, 14);
                    }
                    else if (i == 4)
                    {
                        labelPast[i - 1].Location = new Point(612, 14);
                    }

                    if ((i + 1) == null)
                    {
                        if (i == 1)
                        {
                            pictureBoxAllPastLeft.Visible = false;
                            pictureBoxAllPastRight.Visible = false;

                            labelPast1.Location = new Point(684, 14);
                        }
                        else if (i == 2)
                        {
                            pictureBoxAllPastLeft.Visible = true;
                            pictureBoxAllPastRight.Visible = true;

                            pictureBoxAllPastLeft.Location = new Point(581, 14);

                            labelPast1.Location = new Point(612, 14);
                            labelPast2.Location = new Point(644, 14);
                        }
                        else if (i == 3)
                        {
                            pictureBoxAllPastLeft.Visible = true;
                            pictureBoxAllPastRight.Visible = true;

                            pictureBoxAllPastLeft.Location = new Point(550, 14);

                            labelPast1.Location = new Point(581, 14);
                            labelPast2.Location = new Point(612, 14);
                            labelPast3.Location = new Point(644, 14);
                        }
                        else if (i == 4)
                        {
                            pictureBoxAllPastLeft.Visible = true;
                            pictureBoxAllPastRight.Visible = true;

                            pictureBoxAllPastLeft.Location = new Point(519, 14);

                            labelPast1.Location = new Point(550, 14);
                            labelPast2.Location = new Point(581, 14);
                            labelPast3.Location = new Point(612, 14);
                            labelPast4.Location = new Point(644, 14);
                        }
                        return;
                    }

                }

                if (totalPages == 5)
                {
                    labelPast1.Location = new Point(519, 14);
                    labelPast2.Location = new Point(550, 14);
                    labelPast3.Location = new Point(581, 14);
                    labelPast4.Location = new Point(612, 14);

                    labelPast5.Visible = true;
                    labelPast5.Text = totalPages.ToString();
                    labelPast5.Location = new Point(644, 14);
                }


                if (totalPages > 5)
                {
                    labelPast1.Location = new Point(488, 14);
                    labelPast2.Location = new Point(519, 14);
                    labelPast3.Location = new Point(550, 14);
                    labelPast4.Location = new Point(581, 14);

                    pictureBoxPastSecondDot.Visible = true;
                    pictureBoxPastSecondDot.Location = new Point(612, 14);

                    labelPast5.Visible = true;
                    labelPast5.Text = totalPages.ToString();
                    labelPast5.Location = new Point(644, 14);
                }
            }

            else if (currentPage > 4 && currentPage <= totalPages - 3)
            {
                pictureBoxAllPastLeft.Location = new Point(425, 14);

                labelPast[0].Visible = true;
                labelPast[0].Text = "1";
                labelPast[0].Location = new Point(459, 14);

                picturePastFirstDot.Visible = true;
                picturePastFirstDot.Location = new Point(488, 14);


                for (int i = -1; i <= 1; i++)
                {
                    labelPast[i + 2].Visible = true;
                    labelPast[i + 2].Text = (currentPage + i).ToString();

                    if (i == -1)
                    {
                        labelPast[i + 2].Location = new Point(519, 14);
                    }
                    else if (i == 0)
                    {
                        labelPast[i + 2].Location = new Point(550, 14);
                    }
                    else if (i == 1)
                    {
                        labelPast[i + 2].Location = new Point(581, 14);
                    }
                }


                pictureBoxPastSecondDot.Visible = true;
                pictureBoxPastSecondDot.Location = new Point(612, 14);
                labelPast5.Visible = true;
                labelPast5.Text = totalPages.ToString();
            }

            else if (currentPage > totalPages - 3)
            {
                pictureBoxAllPastLeft.Location = new Point(488, 14);

                labelPast[0].Visible = true;
                labelPast[0].Text = "1";
                labelPast[0].Location = new Point(519, 14);

                picturePastFirstDot.Visible = true;
                picturePastFirstDot.Location = new Point(550, 14);

                pictureBoxPastSecondDot.Visible = false;


                int start = totalPages - 2;
                for (int i = 0; i < 3; i++)
                {
                    labelPast[i + 1].Visible = true;
                    labelPast[i + 1].Text = (start + i).ToString();

                    if (i == 0)
                    {
                        labelPast[i + 1].Location = new Point(581, 14);
                    }
                    else if (i == 1)
                    {
                        labelPast[i + 1].Location = new Point(612, 14);
                    }
                    else if (i == 2)
                    {
                        labelPast[i + 1].Location = new Point(644, 14);
                    }
                }
            }

            foreach (var item in labelPast)
            {
                if (item.Text == currentPage.ToString())
                {
                    item.BackColor = Color.Blue;
                    item.ForeColor = Color.White;
                }
                else
                {
                    if (isDarkMode)
                    {
                        item.BackColor = Color.FromArgb(38, 38, 38);
                        item.ForeColor = Color.White;
                    }
                    else
                    {
                        item.BackColor = Color.White;
                        item.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if (currentAllPastPage > 1)
            {
                currentAllPastPage--;
                UpdateDataAllPastGridView(appointmentPastList);
            }
        }
        private void Next_Click(object sender, EventArgs e)
        {
            if (currentAllPastPage < Math.Ceiling((double)appointmentPastList.Count / itemsPerPage))
            {
                currentAllPastPage++;
                UpdateDataAllPastGridView(appointmentPastList);
            }
        }
        private void PageGoTo_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;

            if (label == null)
            {
                return;
            }
            currentAllPastPage = Convert.ToInt32(label.Text);
            AppointmentAllActivePastCloseProperty();
            UpdateDataAllPastGridView(appointmentPastList);
        }

        private async void btn_ActiveAppointmentShow_Click(object sender, EventArgs e)
        {
            if (currentOpenPanel != null)
            {
                await ResizePanelAsync(currentOpenPanel, currentOpenPanel.MinimumSize.Height);
            }

            CloseListAppointmentPageSlider();
            AppointmentAllActivePastCloseProperty();
            dataGridAllActiveAppointment.Rows.Clear();
            panelDashboard.Visible = false;
            panelActiveAppointment.Visible = false;
            panelPastAppointment.Visible = false;
            panelDoctorAppointmentChoose.Visible = false;
            panelAppointmentChoose.Visible = false;
            panelAppointmentList.Visible = false;
            panelMyInformation.Visible = false;
            panelDarkModeSettings.Visible = false;

            await LoadActiveAppointments(id);
            await LoadPastAppointments(id);
            UpdateAllActiveDataGridView(appointmentList);
            UpdateDataAllPastGridView(appointmentPastList);
            btn_Information_MouseLeave(null, null);
            animator.Show(panelActivePastAppointment, true);
            XtraTabPage tabPage = tabControlAppointmentPastCancel.TabPages[0];
            tabControlAppointmentPastCancel.SelectedTabPage = tabPage;

            AppointmentDateInformationCloseProperty();
        }

        private void tabControlAppointmentPastCancel_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            XtraTabPage selectedTab = tabControlAppointmentPastCancel.SelectedTabPage;
            if (selectedTab.Name == "tabActive")
            {
                labelAllPageNumber.Visible = false;
            }
            else if (selectedTab.Name == "tabPast")
            {
                if (totalPastAppointmentCount == 0)
                {
                    labelAllPageNumber.Visible = false;
                    panelAllPastPageSlider.Visible = false;
                }
                else
                {
                    labelAllPageNumber.Visible = true;
                    panelAllPastPageSlider.Visible = true;
                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnPastAppointmentShow_Click(object sender, EventArgs e)
        {
            dataGridAllActiveAppointment.Rows.Clear();
            panelDashboard.Visible = false;
            panelActiveAppointment.Visible = false;
            panelPastAppointment.Visible = false;
            panelDoctorAppointmentChoose.Visible = false;
            panelAppointmentChoose.Visible = false;
            panelAppointmentList.Visible = false;
            panelMyInformation.Visible = false;

            await LoadActiveAppointments(id);
            await LoadPastAppointments(id);

            UpdateAllActiveDataGridView(appointmentList);
            UpdateDataAllPastGridView(appointmentPastList);

            animator.Show(panelActivePastAppointment, true);
            XtraTabPage tabPage = tabControlAppointmentPastCancel.TabPages[1];
            tabControlAppointmentPastCancel.SelectedTabPage = tabPage;
        }

        private async void dataGridActiveAppointment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridActiveAppointment.Columns[e.ColumnIndex].Name == "DeleteAppointment")
                {
                    DataGridViewRow selectedRow = dataGridActiveAppointment.Rows[e.RowIndex];

                    int randevuId = Convert.ToInt32(selectedRow.Cells["appointmentId"].Value);
                    string poliklinikAdi = selectedRow.Cells["policlinicNameDashboard"].Value?.ToString();
                    string doctorNameSurname = selectedRow.Cells["doctorNameSurnameDashboard"].Value?.ToString();
                    string appointmentDateTime = null;

                    if (e.RowIndex == 0)
                    {
                        appointmentDateTime = txtActiveAppointment1.Text;
                    }
                    else if (e.RowIndex == 1)
                    {
                        appointmentDateTime = txtActiveAppointment2.Text;
                    }
                    else if (e.RowIndex == 2)
                    {
                        appointmentDateTime = txtActiveAppointment3.Text;
                    }
                    else if (e.RowIndex == 3)
                    {
                        appointmentDateTime = txtActiveAppointment4.Text;
                    }
                    else if (e.RowIndex == 4)
                    {
                        appointmentDateTime = txtActiveAppointment5.Text;
                    }

                    DeleteAppointmentForm deleteAppointmentForm = new DeleteAppointmentForm(randevuId, poliklinikAdi, doctorNameSurname, appointmentDateTime, mail, id, patientName);
                    deleteAppointmentForm.ShowDialog();

                    if (deleteAppointmentForm.AccessibleName == "delete")
                    {
                        deleteAppointmentForm.Close();
                        DeleteAppointmentLoadingForm deleteAppointmentLoadingForm = new DeleteAppointmentLoadingForm();
                        deleteAppointmentLoadingForm.ShowDialog();
                        CloseDashboardActiveAndPastAppoinmentPanelProperty();
                        AppointmentAllActivePastCloseProperty();
                        appointmentList.Clear();
                        appointmentPastList.Clear();
                        await LoadActiveAppointments(id);
                        await LoadPastAppointments(id);
                        UpdateAllActiveDataGridView(appointmentList);
                        UpdateDataAllPastGridView(appointmentPastList);
                        AppointmentDeleteSuccessForm appointmentDeleteSuccessForm = new AppointmentDeleteSuccessForm();
                        appointmentDeleteSuccessForm.Show();
                    }
                }
                else if (dataGridAllActiveAppointment.Columns[e.ColumnIndex].Name == "AppointmentDelete")
                {
                    DataGridViewRow selectedRow = dataGridAllActiveAppointment.Rows[e.RowIndex];

                    int randevuId = Convert.ToInt32(selectedRow.Cells["appointmentAllId"].Value);
                    string poliklinikAdi = selectedRow.Cells["policlinicName2"].Value?.ToString();
                    string doctorNameSurname = selectedRow.Cells["doctorNameSurname2"].Value?.ToString();
                    string appointmentDateTime = null;

                    if (e.RowIndex == 0)
                    {
                        appointmentDateTime = txtActiveAllAppointment1.Text;
                    }
                    else if (e.RowIndex == 1)
                    {
                        appointmentDateTime = txtActiveAllAppointment2.Text;
                    }
                    else if (e.RowIndex == 2)
                    {
                        appointmentDateTime = txtActiveAllAppointment3.Text;
                    }
                    else if (e.RowIndex == 3)
                    {
                        appointmentDateTime = txtActiveAllAppointment4.Text;
                    }
                    else if (e.RowIndex == 4)
                    {
                        appointmentDateTime = txtActiveAllAppointment5.Text;
                    }

                    DeleteAppointmentForm deleteAppointmentForm = new DeleteAppointmentForm(randevuId, poliklinikAdi, doctorNameSurname, appointmentDateTime, mail, id, patientName);
                    deleteAppointmentForm.ShowDialog();

                    if (deleteAppointmentForm.AccessibleName == "delete")
                    {
                        deleteAppointmentForm.Close();
                        DeleteAppointmentLoadingForm deleteAppointmentLoadingForm = new DeleteAppointmentLoadingForm();
                        deleteAppointmentLoadingForm.ShowDialog();
                        CloseDashboardActiveAndPastAppoinmentPanelProperty();
                        AppointmentAllActivePastCloseProperty();
                        await LoadActiveAppointments(id);
                        await LoadPastAppointments(id);
                        UpdateAllActiveDataGridView(appointmentList);
                        UpdateDataAllPastGridView(appointmentPastList);
                        AppointmentDeleteSuccessForm appointmentDeleteSuccessForm = new AppointmentDeleteSuccessForm();
                        appointmentDeleteSuccessForm.Show();
                    }
                }
            }
        }

        private void CloseDashboardActiveAndPastAppoinmentPanelProperty()
        {
            //ActiveDashboard
            txtActiveAppointment1.Visible = false;
            panelActive1.Visible = false;

            txtActiveAppointment2.Visible = false;
            panelActive2.Visible = false;

            txtActiveAppointment3.Visible = false;
            panelActive3.Visible = false;

            txtActiveAppointment4.Visible = false;
            panelActive4.Visible = false;

            txtActiveAppointment5.Visible = false;
            panelActive5.Visible = false;

            //PastDashboard

            txtPastAppointmentDate1.Visible = false;
            panelPast1.Visible = false;

            txtPastAppointmentDate2.Visible = false;
            panelPast2.Visible = false;

            txtPastAppointmentDate3.Visible = false;
            panelPast3.Visible = false;

            txtPastAppointmentDate4.Visible = false;
            panelPast4.Visible = false;

            txtPastAppointmentDate5.Visible = false;
            panelPast5.Visible = false;
        }

        private async void btn_AccountDetails_Click(object sender, EventArgs e)
        {
            if (currentOpenPanel != null)
            {
                await ResizePanelAsync(currentOpenPanel, currentOpenPanel.MinimumSize.Height);
            }

            CloseListAppointmentPageSlider();
            AppointmentDateInformationCloseProperty();

            panelDashboard.Visible = false;
            panelActivePastAppointment.Visible = false;
            panelActiveAppointment.Visible = false;
            panelPastAppointment.Visible = false;
            panelDoctorAppointmentChoose.Visible = false;
            panelAppointmentChoose.Visible = false;
            panelAppointmentList.Visible = false;
            panelDarkModeSettings.Visible = false;

            var myInformation = await PatientInformation(id);

            txtPatientTcNo.Text = myInformation.TcNo.ToString();
            txtPatientName.Text = myInformation.Name;
            txtPatientSurname.Text = myInformation.Surname;
            txtPatientGender.Text = myInformation.Gender;
            txtPatientMail.Text = myInformation.Email;
            txtPatientDate.Text = myInformation.Date.ToString("dd-MM-yyyy");
            txtPatientPhoneNumber.Text = myInformation.PhoneNumber.ToString();
            txtPatientPassword.Text = myInformation.Password;

            if (myInformation.EmailConfirm)
            {
                labelCannotConfirmMail.Visible = false;

                labelConfirm.Text = "Doğrulanmış";
                labelConfirm.ForeColor = Color.FromArgb(26, 179, 148);
                labelConfirm.Location = new Point(429, 374);

                pictureBoxConfirmOrNotConfirm.Image = Properties.Resources.greenSucces;
            }
            else
            {
                labelCannotConfirmMail.Visible = true;

                labelConfirm.Text = "Doğrulanmamış";
                labelConfirm.ForeColor = Color.FromArgb(224, 79, 95);
                labelConfirm.Location = new Point(410, 374);

                pictureBoxConfirmOrNotConfirm.Image = Properties.Resources.redError;
            }

            btn_Information_MouseLeave(null, null);
            await Task.Run(() => animator.Show(panelMyInformation, true));
        }

        private async void pictureBoxMiddleWhite_Click(object sender, EventArgs e)
        {
            if (panelDashboard.Visible)
            {
                return;
            }

            if (currentOpenPanel != null)
            {
                await ResizePanelAsync(currentOpenPanel, currentOpenPanel.MinimumSize.Height);
            }

            DashboardOpen();
        }

        private async void labelCannotConfirmMail_Click(object sender, EventArgs e)
        {
            GeneralPasswordChangeWarningForm generalPasswordChangeWarningForm = new GeneralPasswordChangeWarningForm(txtPatientTcNo.Text, txtPatientMail.Text, $"{txtPatientName.Text} {txtPatientSurname.Text}", "mailConfirm");
            generalPasswordChangeWarningForm.Show();

            while (true)
            {
                await Task.Delay(400);
                if (generalPasswordChangeWarningForm.AccessibleDescription == "Success")
                {
                    btn_AccountDetails_Click(sender, e);
                    labelCannotConfirmMail.Visible = false;
                }
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            DashboardOpen();
        }

        private async void btn_InformationPasswordChange_Click(object sender, EventArgs e)
        {
            PatientInformationPasswordChangeForm patientInformationPasswordChangeForm = new PatientInformationPasswordChangeForm(id, txtPatientPassword.Text);
            patientInformationPasswordChangeForm.ShowDialog();

            await PatientInformationPasswordUpdate(patientInformationPasswordChangeForm, id);
        }

        private async Task PatientInformationPasswordUpdate(PatientInformationPasswordChangeForm patientInformationPasswordChangeForm, int selectedId)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);
                    string status = patientInformationPasswordChangeForm.AccessibleName;

                    if (status == "Success")
                    {
                        PatientInformationAddUpdateDeleteForm patientInformationAddUpdateDeleteForm = new PatientInformationAddUpdateDeleteForm("update");
                        patientInformationAddUpdateDeleteForm.Show();
                        btn_AccountDetails_Click(null, null);
                        patientInformationPasswordChangeForm.AccessibleName = "abcdefgh";
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

        private void btn_InformationUpdate_Click(object sender, EventArgs e)
        {
            int selectedId = id;
            string tcNo = txtPatientTcNo.Text;
            string patientName = txtPatientName.Text;
            string patientSurname = txtPatientSurname.Text;
            string patientGender = txtPatientGender.Text;
            string patientDate = txtPatientDate.Text;
            string patientMail = txtPatientMail.Text;
            string patientPhoneNumber = txtPatientPhoneNumber.Text;

            PatientInformationUpdateForm patientInformationUpdate = new PatientInformationUpdateForm(tcNo, selectedId, patientName, patientSurname, patientGender, patientDate, patientMail, patientPhoneNumber);
            patientInformationUpdate.ShowDialog();

            string status = patientInformationUpdate.AccessibleName;

            if (status == "Success")
            {
                PatientInformationAddUpdateDeleteForm patientInformationAddUpdateDeleteForm = new PatientInformationAddUpdateDeleteForm("updateInformation");
                patientInformationAddUpdateDeleteForm.Show();
                patientInformationUpdate.AccessibleName = "abcdefgh";
                patientInformationUpdate.Close();
                btn_AccountDetails_Click(null, null);
            }
        }

        private void dropDownDarkMode_SelectedValueChanged(object sender, EventArgs e)
        {
            BunifuDropdown bunifuDropdown = sender as BunifuDropdown;
            if (bunifuDropdown.SelectedItem == "Açık Renk")
            {
                pictureBoxMod.Image = Properties.Resources.AçıkMod;
                labelDarkModeWarning.Visible = false;
            }
            else if (bunifuDropdown.SelectedItem == "Koyu Renk")
            {
                pictureBoxMod.Image = Properties.Resources.KoyuMod;
                labelDarkModeWarning.Visible = false;
            }
        }
    }
}