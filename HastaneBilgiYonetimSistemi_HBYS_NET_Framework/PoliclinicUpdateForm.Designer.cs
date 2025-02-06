namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class PoliclinicUpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoliclinicUpdateForm));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.panelGradient = new Bunifu.UI.WinForms.BunifuGradientPanel();
            this.panelInPoliclinicAdd = new Bunifu.UI.WinForms.BunifuPanel();
            this.labelFooter = new System.Windows.Forms.Label();
            this.labelRequired = new System.Windows.Forms.Label();
            this.pictureBoxWhite = new System.Windows.Forms.PictureBox();
            this.labelPoliclinicName = new System.Windows.Forms.Label();
            this.btn_PoliclinicAdd = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.txtPoliclinicName = new Bunifu.UI.WinForms.BunifuTextBox();
            this.labelPoliclinicNameRequired = new System.Windows.Forms.Label();
            this.pictureBoxDark = new System.Windows.Forms.PictureBox();
            this.panelGradient.SuspendLayout();
            this.panelInPoliclinicAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGradient
            // 
            this.panelGradient.BackColor = System.Drawing.Color.Transparent;
            this.panelGradient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelGradient.BackgroundImage")));
            this.panelGradient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelGradient.BorderRadius = 30;
            this.panelGradient.Controls.Add(this.panelInPoliclinicAdd);
            this.panelGradient.GradientBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelGradient.GradientBottomRight = System.Drawing.Color.Gray;
            this.panelGradient.GradientTopLeft = System.Drawing.Color.Gray;
            this.panelGradient.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelGradient.Location = new System.Drawing.Point(22, 28);
            this.panelGradient.Name = "panelGradient";
            this.panelGradient.Quality = 10;
            this.panelGradient.Size = new System.Drawing.Size(398, 456);
            this.panelGradient.TabIndex = 7;
            // 
            // panelInPoliclinicAdd
            // 
            this.panelInPoliclinicAdd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelInPoliclinicAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelInPoliclinicAdd.BackgroundImage")));
            this.panelInPoliclinicAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelInPoliclinicAdd.BorderColor = System.Drawing.Color.Transparent;
            this.panelInPoliclinicAdd.BorderRadius = 3;
            this.panelInPoliclinicAdd.BorderThickness = 1;
            this.panelInPoliclinicAdd.Controls.Add(this.labelFooter);
            this.panelInPoliclinicAdd.Controls.Add(this.labelRequired);
            this.panelInPoliclinicAdd.Controls.Add(this.pictureBoxWhite);
            this.panelInPoliclinicAdd.Controls.Add(this.labelPoliclinicName);
            this.panelInPoliclinicAdd.Controls.Add(this.btn_PoliclinicAdd);
            this.panelInPoliclinicAdd.Controls.Add(this.txtPoliclinicName);
            this.panelInPoliclinicAdd.Controls.Add(this.labelPoliclinicNameRequired);
            this.panelInPoliclinicAdd.Controls.Add(this.pictureBoxDark);
            this.panelInPoliclinicAdd.Location = new System.Drawing.Point(25, 25);
            this.panelInPoliclinicAdd.Name = "panelInPoliclinicAdd";
            this.panelInPoliclinicAdd.ShowBorders = true;
            this.panelInPoliclinicAdd.Size = new System.Drawing.Size(348, 406);
            this.panelInPoliclinicAdd.TabIndex = 0;
            // 
            // labelFooter
            // 
            this.labelFooter.AutoSize = true;
            this.labelFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.labelFooter.Location = new System.Drawing.Point(87, 355);
            this.labelFooter.Name = "labelFooter";
            this.labelFooter.Size = new System.Drawing.Size(170, 16);
            this.labelFooter.TabIndex = 6;
            this.labelFooter.Text = "Tüm Hakları Saklıdır @ 2024";
            // 
            // labelRequired
            // 
            this.labelRequired.AutoSize = true;
            this.labelRequired.BackColor = System.Drawing.Color.Transparent;
            this.labelRequired.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelRequired.ForeColor = System.Drawing.Color.Red;
            this.labelRequired.Location = new System.Drawing.Point(136, 178);
            this.labelRequired.Name = "labelRequired";
            this.labelRequired.Size = new System.Drawing.Size(75, 18);
            this.labelRequired.TabIndex = 5;
            this.labelRequired.Text = "(*Zorunlu)";
            this.labelRequired.Visible = false;
            // 
            // pictureBoxWhite
            // 
            this.pictureBoxWhite.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxWhite.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWhite.Image")));
            this.pictureBoxWhite.Location = new System.Drawing.Point(29, 48);
            this.pictureBoxWhite.Name = "pictureBoxWhite";
            this.pictureBoxWhite.Size = new System.Drawing.Size(288, 88);
            this.pictureBoxWhite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWhite.TabIndex = 4;
            this.pictureBoxWhite.TabStop = false;
            // 
            // labelPoliclinicName
            // 
            this.labelPoliclinicName.AutoSize = true;
            this.labelPoliclinicName.BackColor = System.Drawing.Color.Transparent;
            this.labelPoliclinicName.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.labelPoliclinicName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.labelPoliclinicName.Location = new System.Drawing.Point(31, 175);
            this.labelPoliclinicName.Name = "labelPoliclinicName";
            this.labelPoliclinicName.Size = new System.Drawing.Size(106, 21);
            this.labelPoliclinicName.TabIndex = 2;
            this.labelPoliclinicName.Text = "Poliklinik Adı :";
            // 
            // btn_PoliclinicAdd
            // 
            this.btn_PoliclinicAdd.AllowAnimations = true;
            this.btn_PoliclinicAdd.AllowMouseEffects = true;
            this.btn_PoliclinicAdd.AllowToggling = false;
            this.btn_PoliclinicAdd.AnimationSpeed = 200;
            this.btn_PoliclinicAdd.AutoGenerateColors = false;
            this.btn_PoliclinicAdd.AutoRoundBorders = false;
            this.btn_PoliclinicAdd.AutoSizeLeftIcon = true;
            this.btn_PoliclinicAdd.AutoSizeRightIcon = true;
            this.btn_PoliclinicAdd.BackColor = System.Drawing.Color.Transparent;
            this.btn_PoliclinicAdd.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(242)))));
            this.btn_PoliclinicAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PoliclinicAdd.BackgroundImage")));
            this.btn_PoliclinicAdd.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_PoliclinicAdd.ButtonText = "POLİKLİNİK GÜNCELLE";
            this.btn_PoliclinicAdd.ButtonTextMarginLeft = 0;
            this.btn_PoliclinicAdd.ColorContrastOnClick = 45;
            this.btn_PoliclinicAdd.ColorContrastOnHover = 45;
            this.btn_PoliclinicAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btn_PoliclinicAdd.CustomizableEdges = borderEdges1;
            this.btn_PoliclinicAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_PoliclinicAdd.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_PoliclinicAdd.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_PoliclinicAdd.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_PoliclinicAdd.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Idle;
            this.btn_PoliclinicAdd.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_PoliclinicAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btn_PoliclinicAdd.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PoliclinicAdd.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btn_PoliclinicAdd.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btn_PoliclinicAdd.IconMarginLeft = 11;
            this.btn_PoliclinicAdd.IconPadding = 10;
            this.btn_PoliclinicAdd.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_PoliclinicAdd.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btn_PoliclinicAdd.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btn_PoliclinicAdd.IconSize = 25;
            this.btn_PoliclinicAdd.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(242)))));
            this.btn_PoliclinicAdd.IdleBorderRadius = 15;
            this.btn_PoliclinicAdd.IdleBorderThickness = 1;
            this.btn_PoliclinicAdd.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(242)))));
            this.btn_PoliclinicAdd.IdleIconLeftImage = null;
            this.btn_PoliclinicAdd.IdleIconRightImage = null;
            this.btn_PoliclinicAdd.IndicateFocus = false;
            this.btn_PoliclinicAdd.Location = new System.Drawing.Point(23, 272);
            this.btn_PoliclinicAdd.Name = "btn_PoliclinicAdd";
            this.btn_PoliclinicAdd.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_PoliclinicAdd.OnDisabledState.BorderRadius = 15;
            this.btn_PoliclinicAdd.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_PoliclinicAdd.OnDisabledState.BorderThickness = 1;
            this.btn_PoliclinicAdd.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_PoliclinicAdd.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_PoliclinicAdd.OnDisabledState.IconLeftImage = null;
            this.btn_PoliclinicAdd.OnDisabledState.IconRightImage = null;
            this.btn_PoliclinicAdd.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.btn_PoliclinicAdd.onHoverState.BorderRadius = 15;
            this.btn_PoliclinicAdd.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_PoliclinicAdd.onHoverState.BorderThickness = 1;
            this.btn_PoliclinicAdd.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.btn_PoliclinicAdd.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btn_PoliclinicAdd.onHoverState.IconLeftImage = null;
            this.btn_PoliclinicAdd.onHoverState.IconRightImage = null;
            this.btn_PoliclinicAdd.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(242)))));
            this.btn_PoliclinicAdd.OnIdleState.BorderRadius = 15;
            this.btn_PoliclinicAdd.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_PoliclinicAdd.OnIdleState.BorderThickness = 1;
            this.btn_PoliclinicAdd.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(242)))));
            this.btn_PoliclinicAdd.OnIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btn_PoliclinicAdd.OnIdleState.IconLeftImage = null;
            this.btn_PoliclinicAdd.OnIdleState.IconRightImage = null;
            this.btn_PoliclinicAdd.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btn_PoliclinicAdd.OnPressedState.BorderRadius = 15;
            this.btn_PoliclinicAdd.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_PoliclinicAdd.OnPressedState.BorderThickness = 1;
            this.btn_PoliclinicAdd.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btn_PoliclinicAdd.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btn_PoliclinicAdd.OnPressedState.IconLeftImage = null;
            this.btn_PoliclinicAdd.OnPressedState.IconRightImage = null;
            this.btn_PoliclinicAdd.Size = new System.Drawing.Size(300, 40);
            this.btn_PoliclinicAdd.TabIndex = 1;
            this.btn_PoliclinicAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_PoliclinicAdd.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btn_PoliclinicAdd.TextMarginLeft = 0;
            this.btn_PoliclinicAdd.TextPadding = new System.Windows.Forms.Padding(0);
            this.btn_PoliclinicAdd.UseDefaultRadiusAndThickness = true;
            this.btn_PoliclinicAdd.Click += new System.EventHandler(this.btn_PoliclinicAdd_Click);
            // 
            // txtPoliclinicName
            // 
            this.txtPoliclinicName.AcceptsReturn = false;
            this.txtPoliclinicName.AcceptsTab = false;
            this.txtPoliclinicName.AnimationSpeed = 200;
            this.txtPoliclinicName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPoliclinicName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPoliclinicName.BackColor = System.Drawing.Color.Transparent;
            this.txtPoliclinicName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtPoliclinicName.BackgroundImage")));
            this.txtPoliclinicName.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.txtPoliclinicName.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtPoliclinicName.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.txtPoliclinicName.BorderColorIdle = System.Drawing.Color.Silver;
            this.txtPoliclinicName.BorderRadius = 15;
            this.txtPoliclinicName.BorderThickness = 1;
            this.txtPoliclinicName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPoliclinicName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPoliclinicName.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.txtPoliclinicName.DefaultText = "";
            this.txtPoliclinicName.FillColor = System.Drawing.Color.DarkGray;
            this.txtPoliclinicName.ForeColor = System.Drawing.Color.Black;
            this.txtPoliclinicName.HideSelection = true;
            this.txtPoliclinicName.IconLeft = null;
            this.txtPoliclinicName.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPoliclinicName.IconPadding = 10;
            this.txtPoliclinicName.IconRight = null;
            this.txtPoliclinicName.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPoliclinicName.Lines = new string[0];
            this.txtPoliclinicName.Location = new System.Drawing.Point(23, 208);
            this.txtPoliclinicName.MaxLength = 32767;
            this.txtPoliclinicName.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtPoliclinicName.Modified = false;
            this.txtPoliclinicName.Multiline = false;
            this.txtPoliclinicName.Name = "txtPoliclinicName";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.txtPoliclinicName.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.txtPoliclinicName.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.txtPoliclinicName.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.DarkGray;
            stateProperties4.ForeColor = System.Drawing.Color.Black;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.txtPoliclinicName.OnIdleState = stateProperties4;
            this.txtPoliclinicName.Padding = new System.Windows.Forms.Padding(3);
            this.txtPoliclinicName.PasswordChar = '\0';
            this.txtPoliclinicName.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.txtPoliclinicName.PlaceholderText = "Poliklinik Adı";
            this.txtPoliclinicName.ReadOnly = false;
            this.txtPoliclinicName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPoliclinicName.SelectedText = "";
            this.txtPoliclinicName.SelectionLength = 0;
            this.txtPoliclinicName.SelectionStart = 0;
            this.txtPoliclinicName.ShortcutsEnabled = true;
            this.txtPoliclinicName.Size = new System.Drawing.Size(300, 41);
            this.txtPoliclinicName.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.txtPoliclinicName.TabIndex = 0;
            this.txtPoliclinicName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPoliclinicName.TextMarginBottom = 0;
            this.txtPoliclinicName.TextMarginLeft = 3;
            this.txtPoliclinicName.TextMarginTop = 0;
            this.txtPoliclinicName.TextPlaceholder = "Poliklinik Adı";
            this.txtPoliclinicName.UseSystemPasswordChar = false;
            this.txtPoliclinicName.WordWrap = true;
            this.txtPoliclinicName.TextChange += new System.EventHandler(this.txtPoliclinicName_TextChange);
            // 
            // labelPoliclinicNameRequired
            // 
            this.labelPoliclinicNameRequired.AutoSize = true;
            this.labelPoliclinicNameRequired.BackColor = System.Drawing.Color.Transparent;
            this.labelPoliclinicNameRequired.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.labelPoliclinicNameRequired.ForeColor = System.Drawing.Color.Red;
            this.labelPoliclinicNameRequired.Location = new System.Drawing.Point(16, 169);
            this.labelPoliclinicNameRequired.Name = "labelPoliclinicNameRequired";
            this.labelPoliclinicNameRequired.Size = new System.Drawing.Size(22, 23);
            this.labelPoliclinicNameRequired.TabIndex = 3;
            this.labelPoliclinicNameRequired.Text = "*";
            // 
            // pictureBoxDark
            // 
            this.pictureBoxDark.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxDark.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDark.Image")));
            this.pictureBoxDark.Location = new System.Drawing.Point(29, 48);
            this.pictureBoxDark.Name = "pictureBoxDark";
            this.pictureBoxDark.Size = new System.Drawing.Size(288, 88);
            this.pictureBoxDark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDark.TabIndex = 4;
            this.pictureBoxDark.TabStop = false;
            this.pictureBoxDark.Visible = false;
            // 
            // PoliclinicUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 513);
            this.Controls.Add(this.panelGradient);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("PoliclinicUpdateForm.IconOptions.Image")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PoliclinicUpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poliklinik Güncelleme";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PoliklinikEkleForm_FormClosing);
            this.Load += new System.EventHandler(this.PoliklinikGuncelleForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PoliclinicUpdateForm_KeyDown);
            this.panelGradient.ResumeLayout(false);
            this.panelInPoliclinicAdd.ResumeLayout(false);
            this.panelInPoliclinicAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuGradientPanel panelGradient;
        private Bunifu.UI.WinForms.BunifuPanel panelInPoliclinicAdd;
        private System.Windows.Forms.Label labelFooter;
        private System.Windows.Forms.Label labelRequired;
        private System.Windows.Forms.PictureBox pictureBoxWhite;
        private System.Windows.Forms.Label labelPoliclinicName;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btn_PoliclinicAdd;
        private Bunifu.UI.WinForms.BunifuTextBox txtPoliclinicName;
        private System.Windows.Forms.Label labelPoliclinicNameRequired;
        private System.Windows.Forms.PictureBox pictureBoxDark;
    }
}