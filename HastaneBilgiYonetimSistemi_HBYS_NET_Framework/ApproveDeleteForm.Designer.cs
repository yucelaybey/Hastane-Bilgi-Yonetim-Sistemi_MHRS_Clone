namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class ApproveDeleteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApproveDeleteForm));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.pictureBoxWhite = new System.Windows.Forms.PictureBox();
            this.pictureBoxDark = new System.Windows.Forms.PictureBox();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.labelAnswer = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btn_Yes = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btn_No = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxWhite
            // 
            this.pictureBoxWhite.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.warning;
            this.pictureBoxWhite.Location = new System.Drawing.Point(23, 33);
            this.pictureBoxWhite.Name = "pictureBoxWhite";
            this.pictureBoxWhite.Size = new System.Drawing.Size(109, 112);
            this.pictureBoxWhite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWhite.TabIndex = 0;
            this.pictureBoxWhite.TabStop = false;
            // 
            // pictureBoxDark
            // 
            this.pictureBoxDark.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.warningDark;
            this.pictureBoxDark.Location = new System.Drawing.Point(23, 33);
            this.pictureBoxDark.Name = "pictureBoxDark";
            this.pictureBoxDark.Size = new System.Drawing.Size(109, 112);
            this.pictureBoxDark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDark.TabIndex = 0;
            this.pictureBoxDark.TabStop = false;
            this.pictureBoxDark.Visible = false;
            // 
            // labelQuestion
            // 
            this.labelQuestion.AutoSize = true;
            this.labelQuestion.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelQuestion.ForeColor = System.Drawing.Color.Red;
            this.labelQuestion.Location = new System.Drawing.Point(166, 43);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(287, 31);
            this.labelQuestion.TabIndex = 1;
            this.labelQuestion.Text = "Aşağıdakiler silinecektir ?";
            // 
            // labelAnswer
            // 
            this.labelAnswer.AutoSize = true;
            this.labelAnswer.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAnswer.Location = new System.Drawing.Point(169, 86);
            this.labelAnswer.Name = "labelAnswer";
            this.labelAnswer.Size = new System.Drawing.Size(45, 16);
            this.labelAnswer.TabIndex = 2;
            this.labelAnswer.Text = "label2";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.panelFooter.Controls.Add(this.btn_Yes);
            this.panelFooter.Controls.Add(this.btn_No);
            this.panelFooter.Location = new System.Drawing.Point(-1, 193);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(485, 66);
            this.panelFooter.TabIndex = 3;
            // 
            // btn_Yes
            // 
            this.btn_Yes.AllowAnimations = true;
            this.btn_Yes.AllowMouseEffects = true;
            this.btn_Yes.AllowToggling = false;
            this.btn_Yes.AnimationSpeed = 200;
            this.btn_Yes.AutoGenerateColors = false;
            this.btn_Yes.AutoRoundBorders = false;
            this.btn_Yes.AutoSizeLeftIcon = true;
            this.btn_Yes.AutoSizeRightIcon = true;
            this.btn_Yes.BackColor = System.Drawing.Color.Transparent;
            this.btn_Yes.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(177)))), ((int)(((byte)(172)))));
            this.btn_Yes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Yes.BackgroundImage")));
            this.btn_Yes.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_Yes.ButtonText = "Evet Sil";
            this.btn_Yes.ButtonTextMarginLeft = 0;
            this.btn_Yes.ColorContrastOnClick = 45;
            this.btn_Yes.ColorContrastOnHover = 45;
            this.btn_Yes.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btn_Yes.CustomizableEdges = borderEdges1;
            this.btn_Yes.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Yes.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_Yes.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_Yes.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_Yes.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btn_Yes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_Yes.ForeColor = System.Drawing.Color.White;
            this.btn_Yes.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Yes.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btn_Yes.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btn_Yes.IconMarginLeft = 11;
            this.btn_Yes.IconPadding = 10;
            this.btn_Yes.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Yes.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btn_Yes.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btn_Yes.IconSize = 25;
            this.btn_Yes.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(177)))), ((int)(((byte)(172)))));
            this.btn_Yes.IdleBorderRadius = 1;
            this.btn_Yes.IdleBorderThickness = 1;
            this.btn_Yes.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(177)))), ((int)(((byte)(172)))));
            this.btn_Yes.IdleIconLeftImage = null;
            this.btn_Yes.IdleIconRightImage = null;
            this.btn_Yes.IndicateFocus = false;
            this.btn_Yes.Location = new System.Drawing.Point(31, 14);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_Yes.OnDisabledState.BorderRadius = 1;
            this.btn_Yes.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_Yes.OnDisabledState.BorderThickness = 1;
            this.btn_Yes.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_Yes.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_Yes.OnDisabledState.IconLeftImage = null;
            this.btn_Yes.OnDisabledState.IconRightImage = null;
            this.btn_Yes.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.btn_Yes.onHoverState.BorderRadius = 1;
            this.btn_Yes.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_Yes.onHoverState.BorderThickness = 1;
            this.btn_Yes.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(174)))));
            this.btn_Yes.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btn_Yes.onHoverState.IconLeftImage = null;
            this.btn_Yes.onHoverState.IconRightImage = null;
            this.btn_Yes.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(177)))), ((int)(((byte)(172)))));
            this.btn_Yes.OnIdleState.BorderRadius = 1;
            this.btn_Yes.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_Yes.OnIdleState.BorderThickness = 1;
            this.btn_Yes.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(177)))), ((int)(((byte)(172)))));
            this.btn_Yes.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btn_Yes.OnIdleState.IconLeftImage = null;
            this.btn_Yes.OnIdleState.IconRightImage = null;
            this.btn_Yes.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(138)))), ((int)(((byte)(134)))));
            this.btn_Yes.OnPressedState.BorderRadius = 1;
            this.btn_Yes.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_Yes.OnPressedState.BorderThickness = 1;
            this.btn_Yes.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(138)))), ((int)(((byte)(134)))));
            this.btn_Yes.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btn_Yes.OnPressedState.IconLeftImage = null;
            this.btn_Yes.OnPressedState.IconRightImage = null;
            this.btn_Yes.Size = new System.Drawing.Size(100, 40);
            this.btn_Yes.TabIndex = 0;
            this.btn_Yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Yes.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btn_Yes.TextMarginLeft = 0;
            this.btn_Yes.TextPadding = new System.Windows.Forms.Padding(0);
            this.btn_Yes.UseDefaultRadiusAndThickness = true;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.AllowAnimations = true;
            this.btn_No.AllowMouseEffects = true;
            this.btn_No.AllowToggling = false;
            this.btn_No.AnimationSpeed = 200;
            this.btn_No.AutoGenerateColors = false;
            this.btn_No.AutoRoundBorders = false;
            this.btn_No.AutoSizeLeftIcon = true;
            this.btn_No.AutoSizeRightIcon = true;
            this.btn_No.BackColor = System.Drawing.Color.Transparent;
            this.btn_No.BackColor1 = System.Drawing.Color.Red;
            this.btn_No.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_No.BackgroundImage")));
            this.btn_No.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_No.ButtonText = "Iptal Et";
            this.btn_No.ButtonTextMarginLeft = 0;
            this.btn_No.ColorContrastOnClick = 45;
            this.btn_No.ColorContrastOnHover = 45;
            this.btn_No.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.btn_No.CustomizableEdges = borderEdges2;
            this.btn_No.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_No.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_No.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_No.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_No.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btn_No.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_No.ForeColor = System.Drawing.Color.White;
            this.btn_No.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_No.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btn_No.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btn_No.IconMarginLeft = 11;
            this.btn_No.IconPadding = 10;
            this.btn_No.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_No.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btn_No.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btn_No.IconSize = 25;
            this.btn_No.IdleBorderColor = System.Drawing.Color.Red;
            this.btn_No.IdleBorderRadius = 1;
            this.btn_No.IdleBorderThickness = 1;
            this.btn_No.IdleFillColor = System.Drawing.Color.Red;
            this.btn_No.IdleIconLeftImage = null;
            this.btn_No.IdleIconRightImage = null;
            this.btn_No.IndicateFocus = false;
            this.btn_No.Location = new System.Drawing.Point(354, 13);
            this.btn_No.Name = "btn_No";
            this.btn_No.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btn_No.OnDisabledState.BorderRadius = 1;
            this.btn_No.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_No.OnDisabledState.BorderThickness = 1;
            this.btn_No.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btn_No.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btn_No.OnDisabledState.IconLeftImage = null;
            this.btn_No.OnDisabledState.IconRightImage = null;
            this.btn_No.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btn_No.onHoverState.BorderRadius = 1;
            this.btn_No.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_No.onHoverState.BorderThickness = 1;
            this.btn_No.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btn_No.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btn_No.onHoverState.IconLeftImage = null;
            this.btn_No.onHoverState.IconRightImage = null;
            this.btn_No.OnIdleState.BorderColor = System.Drawing.Color.Red;
            this.btn_No.OnIdleState.BorderRadius = 1;
            this.btn_No.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_No.OnIdleState.BorderThickness = 1;
            this.btn_No.OnIdleState.FillColor = System.Drawing.Color.Red;
            this.btn_No.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btn_No.OnIdleState.IconLeftImage = null;
            this.btn_No.OnIdleState.IconRightImage = null;
            this.btn_No.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_No.OnPressedState.BorderRadius = 1;
            this.btn_No.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btn_No.OnPressedState.BorderThickness = 1;
            this.btn_No.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_No.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btn_No.OnPressedState.IconLeftImage = null;
            this.btn_No.OnPressedState.IconRightImage = null;
            this.btn_No.Size = new System.Drawing.Size(100, 40);
            this.btn_No.TabIndex = 0;
            this.btn_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_No.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btn_No.TextMarginLeft = 0;
            this.btn_No.TextPadding = new System.Windows.Forms.Padding(0);
            this.btn_No.UseDefaultRadiusAndThickness = true;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // ApproveDeleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 258);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.labelAnswer);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.pictureBoxWhite);
            this.Controls.Add(this.pictureBoxDark);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Admin_Login;
            this.MaximizeBox = false;
            this.Name = "ApproveDeleteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Silme İşlemi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ApproveDeleteForm_FormClosing);
            this.Load += new System.EventHandler(this.ApproveDeleteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxWhite;
        private System.Windows.Forms.PictureBox pictureBoxDark;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.Label labelAnswer;
        private System.Windows.Forms.Panel panelFooter;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btn_No;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btn_Yes;
    }
}