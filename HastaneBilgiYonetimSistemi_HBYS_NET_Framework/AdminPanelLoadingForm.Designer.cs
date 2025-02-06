namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class AdminPanelLoadingForm
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
            this.progressAdminLoading = new ReaLTaiizor.Controls.RibbonProgressBarLeft();
            this.labelDescription = new System.Windows.Forms.Label();
            this.pictureBoxWhite = new System.Windows.Forms.PictureBox();
            this.pictureBoxDark = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).BeginInit();
            this.SuspendLayout();
            // 
            // progressAdminLoading
            // 
            this.progressAdminLoading.BackColor = System.Drawing.Color.Transparent;
            this.progressAdminLoading.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressAdminLoading.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(120)))), ((int)(((byte)(117)))));
            this.progressAdminLoading.ColorA = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(201)))), ((int)(((byte)(205)))));
            this.progressAdminLoading.ColorB = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(186)))), ((int)(((byte)(190)))));
            this.progressAdminLoading.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))), ((int)(((byte)(90)))));
            this.progressAdminLoading.ForeColor = System.Drawing.Color.Black;
            this.progressAdminLoading.HatchType = System.Drawing.Drawing2D.HatchStyle.DarkUpwardDiagonal;
            this.progressAdminLoading.Location = new System.Drawing.Point(-1, 154);
            this.progressAdminLoading.Maximum = 100;
            this.progressAdminLoading.Name = "progressAdminLoading";
            this.progressAdminLoading.PercentageText = "%";
            this.progressAdminLoading.ProgressBorderColorA = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))), ((int)(((byte)(90)))));
            this.progressAdminLoading.ProgressBorderColorB = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(151)))), ((int)(((byte)(155)))));
            this.progressAdminLoading.ProgressColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(242)))));
            this.progressAdminLoading.ProgressColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(114)))), ((int)(((byte)(242)))));
            this.progressAdminLoading.ProgressLineColorA = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressAdminLoading.ProgressLineColorB = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressAdminLoading.ShowEdge = false;
            this.progressAdminLoading.ShowPercentage = false;
            this.progressAdminLoading.Size = new System.Drawing.Size(455, 30);
            this.progressAdminLoading.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.progressAdminLoading.TabIndex = 0;
            this.progressAdminLoading.Value = 0;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.labelDescription.Font = new System.Drawing.Font("Roboto", 19.8F);
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.labelDescription.Location = new System.Drawing.Point(60, 110);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(293, 39);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "Veriler Yükleniyor...";
            // 
            // pictureBoxWhite
            // 
            this.pictureBoxWhite.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.AdminWhiteMode;
            this.pictureBoxWhite.Location = new System.Drawing.Point(76, 12);
            this.pictureBoxWhite.Name = "pictureBoxWhite";
            this.pictureBoxWhite.Size = new System.Drawing.Size(321, 87);
            this.pictureBoxWhite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWhite.TabIndex = 2;
            this.pictureBoxWhite.TabStop = false;
            // 
            // pictureBoxDark
            // 
            this.pictureBoxDark.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.mhrsLogoAdminDarkMod;
            this.pictureBoxDark.Location = new System.Drawing.Point(76, 12);
            this.pictureBoxDark.Name = "pictureBoxDark";
            this.pictureBoxDark.Size = new System.Drawing.Size(321, 87);
            this.pictureBoxDark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDark.TabIndex = 2;
            this.pictureBoxDark.TabStop = false;
            this.pictureBoxDark.Visible = false;
            // 
            // AdminPanelLoadingForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 183);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxDark);
            this.Controls.Add(this.pictureBoxWhite);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.progressAdminLoading);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Admin_Login;
            this.Name = "AdminPanelLoadingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.RibbonProgressBarLeft progressAdminLoading;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.PictureBox pictureBoxWhite;
        private System.Windows.Forms.PictureBox pictureBoxDark;
    }
}