namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class AppointmentAcceptScreenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppointmentAcceptScreenForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.panelBack = new ReaLTaiizor.Controls.LostBorderPanel();
            this.panelLoading = new Bunifu.UI.WinForms.BunifuPanel();
            this.pictureBoxAppointmentLoading = new System.Windows.Forms.PictureBox();
            this.labelAppointmentSuccess = new System.Windows.Forms.Label();
            this.datagridAppointmentAccept = new System.Windows.Forms.DataGridView();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelAppointmentAccept = new System.Windows.Forms.Label();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.btn_AppointmentAccept = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.panelBack.SuspendLayout();
            this.panelLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAppointmentLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridAppointmentAccept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBack
            // 
            this.panelBack.BackColor = System.Drawing.Color.Black;
            this.panelBack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.panelBack.Controls.Add(this.panelLoading);
            this.panelBack.Controls.Add(this.datagridAppointmentAccept);
            this.panelBack.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.panelBack.ForeColor = System.Drawing.Color.White;
            this.panelBack.Location = new System.Drawing.Point(-5, 87);
            this.panelBack.Name = "panelBack";
            this.panelBack.Padding = new System.Windows.Forms.Padding(5);
            this.panelBack.ShowText = true;
            this.panelBack.Size = new System.Drawing.Size(709, 675);
            this.panelBack.TabIndex = 0;
            // 
            // panelLoading
            // 
            this.panelLoading.BackgroundColor = System.Drawing.Color.Black;
            this.panelLoading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLoading.BackgroundImage")));
            this.panelLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelLoading.BorderColor = System.Drawing.Color.White;
            this.panelLoading.BorderRadius = 0;
            this.panelLoading.BorderThickness = 2;
            this.panelLoading.Controls.Add(this.pictureBoxAppointmentLoading);
            this.panelLoading.Controls.Add(this.labelAppointmentSuccess);
            this.panelLoading.Location = new System.Drawing.Point(129, 230);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.ShowBorders = true;
            this.panelLoading.Size = new System.Drawing.Size(444, 157);
            this.panelLoading.TabIndex = 19;
            this.panelLoading.Visible = false;
            // 
            // pictureBoxAppointmentLoading
            // 
            this.pictureBoxAppointmentLoading.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.loading1;
            this.pictureBoxAppointmentLoading.Location = new System.Drawing.Point(14, 14);
            this.pictureBoxAppointmentLoading.Name = "pictureBoxAppointmentLoading";
            this.pictureBoxAppointmentLoading.Size = new System.Drawing.Size(144, 130);
            this.pictureBoxAppointmentLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAppointmentLoading.TabIndex = 13;
            this.pictureBoxAppointmentLoading.TabStop = false;
            // 
            // labelAppointmentSuccess
            // 
            this.labelAppointmentSuccess.AutoSize = true;
            this.labelAppointmentSuccess.Font = new System.Drawing.Font("Segoe UI", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAppointmentSuccess.Location = new System.Drawing.Point(155, 66);
            this.labelAppointmentSuccess.Name = "labelAppointmentSuccess";
            this.labelAppointmentSuccess.Size = new System.Drawing.Size(268, 30);
            this.labelAppointmentSuccess.TabIndex = 14;
            this.labelAppointmentSuccess.Text = "Randevu Oluşturuluyor...";
            // 
            // datagridAppointmentAccept
            // 
            this.datagridAppointmentAccept.AllowUserToAddRows = false;
            this.datagridAppointmentAccept.AllowUserToDeleteRows = false;
            this.datagridAppointmentAccept.AllowUserToOrderColumns = true;
            this.datagridAppointmentAccept.AllowUserToResizeRows = false;
            this.datagridAppointmentAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datagridAppointmentAccept.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridAppointmentAccept.BackgroundColor = System.Drawing.Color.Black;
            this.datagridAppointmentAccept.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(86)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(86)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridAppointmentAccept.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.datagridAppointmentAccept.ColumnHeadersHeight = 300;
            this.datagridAppointmentAccept.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.datagridAppointmentAccept.ColumnHeadersVisible = false;
            this.datagridAppointmentAccept.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.title,
            this.result});
            this.datagridAppointmentAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridAppointmentAccept.DefaultCellStyle = dataGridViewCellStyle4;
            this.datagridAppointmentAccept.EnableHeadersVisualStyles = false;
            this.datagridAppointmentAccept.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.datagridAppointmentAccept.Location = new System.Drawing.Point(52, 55);
            this.datagridAppointmentAccept.Margin = new System.Windows.Forms.Padding(30);
            this.datagridAppointmentAccept.Name = "datagridAppointmentAccept";
            this.datagridAppointmentAccept.ReadOnly = true;
            this.datagridAppointmentAccept.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.datagridAppointmentAccept.RowHeadersVisible = false;
            this.datagridAppointmentAccept.RowHeadersWidth = 51;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.datagridAppointmentAccept.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.datagridAppointmentAccept.RowTemplate.Height = 112;
            this.datagridAppointmentAccept.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.datagridAppointmentAccept.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridAppointmentAccept.Size = new System.Drawing.Size(602, 564);
            this.datagridAppointmentAccept.TabIndex = 18;
            this.datagridAppointmentAccept.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.datagridAppointmentAccept_CellFormatting);
            // 
            // title
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.title.DefaultCellStyle = dataGridViewCellStyle2;
            this.title.HeaderText = "Title";
            this.title.MinimumWidth = 6;
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // result
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.result.DefaultCellStyle = dataGridViewCellStyle3;
            this.result.HeaderText = "Result";
            this.result.MinimumWidth = 6;
            this.result.Name = "result";
            this.result.ReadOnly = true;
            // 
            // labelAppointmentAccept
            // 
            this.labelAppointmentAccept.AutoSize = true;
            this.labelAppointmentAccept.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAppointmentAccept.ForeColor = System.Drawing.Color.White;
            this.labelAppointmentAccept.Location = new System.Drawing.Point(24, 35);
            this.labelAppointmentAccept.Name = "labelAppointmentAccept";
            this.labelAppointmentAccept.Size = new System.Drawing.Size(155, 24);
            this.labelAppointmentAccept.TabIndex = 1;
            this.labelAppointmentAccept.Text = "Randevu Onayla";
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxClose.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.close_darkMode;
            this.pictureBoxClose.Location = new System.Drawing.Point(644, 24);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(27, 41);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 2;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // btn_AppointmentAccept
            // 
            this.btn_AppointmentAccept.AllowAnimations = true;
            this.btn_AppointmentAccept.AllowMouseEffects = true;
            this.btn_AppointmentAccept.AllowToggling = false;
            this.btn_AppointmentAccept.AnimationSpeed = 200;
            this.btn_AppointmentAccept.AutoGenerateColors = false;
            this.btn_AppointmentAccept.AutoRoundBorders = false;
            this.btn_AppointmentAccept.AutoSizeLeftIcon = true;
            this.btn_AppointmentAccept.AutoSizeRightIcon = true;
            this.btn_AppointmentAccept.BackColor = System.Drawing.Color.Transparent;
            this.btn_AppointmentAccept.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(103)))));
            this.btn_AppointmentAccept.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AppointmentAccept.BackgroundImage")));
            this.btn_AppointmentAccept.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_AppointmentAccept.ButtonText = "Randevu Onayla";
            this.btn_AppointmentAccept.ButtonTextMarginLeft = 0;
            this.btn_AppointmentAccept.ColorContrastOnClick = 45;
            this.btn_AppointmentAccept.ColorContrastOnHover = 45;
            this.btn_AppointmentAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btn_AppointmentAccept.CustomizableEdges = borderEdges1;
            this.btn_AppointmentAccept.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_AppointmentAccept.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_AppointmentAccept.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_AppointmentAccept.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_AppointmentAccept.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Idle;
            this.btn_AppointmentAccept.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_AppointmentAccept.ForeColor = System.Drawing.Color.White;
            this.btn_AppointmentAccept.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_AppointmentAccept.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btn_AppointmentAccept.IconLeftPadding = new System.Windows.Forms.Padding(330, 0, 0, 0);
            this.btn_AppointmentAccept.IconMarginLeft = 11;
            this.btn_AppointmentAccept.IconPadding = 15;
            this.btn_AppointmentAccept.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AppointmentAccept.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btn_AppointmentAccept.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btn_AppointmentAccept.IconSize = 1;
            this.btn_AppointmentAccept.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(103)))));
            this.btn_AppointmentAccept.IdleBorderRadius = 25;
            this.btn_AppointmentAccept.IdleBorderThickness = 1;
            this.btn_AppointmentAccept.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(103)))));
            this.btn_AppointmentAccept.IdleIconLeftImage = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.search25x25Black1;
            this.btn_AppointmentAccept.IdleIconRightImage = null;
            this.btn_AppointmentAccept.IndicateFocus = false;
            this.btn_AppointmentAccept.Location = new System.Drawing.Point(487, 781);
            this.btn_AppointmentAccept.Name = "btn_AppointmentAccept";
            this.btn_AppointmentAccept.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_AppointmentAccept.OnDisabledState.BorderRadius = 25;
            this.btn_AppointmentAccept.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_AppointmentAccept.OnDisabledState.BorderThickness = 1;
            this.btn_AppointmentAccept.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_AppointmentAccept.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_AppointmentAccept.OnDisabledState.IconLeftImage = null;
            this.btn_AppointmentAccept.OnDisabledState.IconRightImage = null;
            this.btn_AppointmentAccept.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(78)))), ((int)(((byte)(136)))));
            this.btn_AppointmentAccept.onHoverState.BorderRadius = 25;
            this.btn_AppointmentAccept.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_AppointmentAccept.onHoverState.BorderThickness = 1;
            this.btn_AppointmentAccept.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(78)))), ((int)(((byte)(136)))));
            this.btn_AppointmentAccept.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btn_AppointmentAccept.onHoverState.IconLeftImage = null;
            this.btn_AppointmentAccept.onHoverState.IconRightImage = null;
            this.btn_AppointmentAccept.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(103)))));
            this.btn_AppointmentAccept.OnIdleState.BorderRadius = 25;
            this.btn_AppointmentAccept.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_AppointmentAccept.OnIdleState.BorderThickness = 1;
            this.btn_AppointmentAccept.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(103)))));
            this.btn_AppointmentAccept.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btn_AppointmentAccept.OnIdleState.IconLeftImage = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.search25x25Black1;
            this.btn_AppointmentAccept.OnIdleState.IconRightImage = null;
            this.btn_AppointmentAccept.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(78)))), ((int)(((byte)(136)))));
            this.btn_AppointmentAccept.OnPressedState.BorderRadius = 25;
            this.btn_AppointmentAccept.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_AppointmentAccept.OnPressedState.BorderThickness = 1;
            this.btn_AppointmentAccept.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(78)))), ((int)(((byte)(136)))));
            this.btn_AppointmentAccept.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btn_AppointmentAccept.OnPressedState.IconLeftImage = null;
            this.btn_AppointmentAccept.OnPressedState.IconRightImage = null;
            this.btn_AppointmentAccept.Size = new System.Drawing.Size(190, 45);
            this.btn_AppointmentAccept.TabIndex = 8;
            this.btn_AppointmentAccept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_AppointmentAccept.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btn_AppointmentAccept.TextMarginLeft = 0;
            this.btn_AppointmentAccept.TextPadding = new System.Windows.Forms.Padding(0);
            this.btn_AppointmentAccept.UseDefaultRadiusAndThickness = true;
            this.btn_AppointmentAccept.Click += new System.EventHandler(this.btn_AppointmentAccept_Click);
            // 
            // AppointmentAcceptScreenForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 844);
            this.ControlBox = false;
            this.Controls.Add(this.btn_AppointmentAccept);
            this.Controls.Add(this.panelBack);
            this.Controls.Add(this.pictureBoxClose);
            this.Controls.Add(this.labelAppointmentAccept);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Logo;
            this.Name = "AppointmentAcceptScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AppointmentAcceptScreenForm_Load);
            this.panelBack.ResumeLayout(false);
            this.panelLoading.ResumeLayout(false);
            this.panelLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAppointmentLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridAppointmentAccept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.LostBorderPanel panelBack;
        private System.Windows.Forms.Label labelAppointmentAccept;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.DataGridView datagridAppointmentAccept;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btn_AppointmentAccept;
        private Bunifu.UI.WinForms.BunifuPanel panelLoading;
        public System.Windows.Forms.PictureBox pictureBoxAppointmentLoading;
        private System.Windows.Forms.Label labelAppointmentSuccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
    }
}