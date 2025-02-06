namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class PatientPanelLoadingForm
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
            this.pictureBoxDark = new System.Windows.Forms.PictureBox();
            this.pictureBoxWhite = new System.Windows.Forms.PictureBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.progressPatientLoading = new ReaLTaiizor.Controls.RibbonProgressBarLeft();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDark
            // 
            this.pictureBoxDark.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.HastaLoginMiddlePictureDark;
            this.pictureBoxDark.Location = new System.Drawing.Point(76, 12);
            this.pictureBoxDark.Name = "pictureBoxDark";
            this.pictureBoxDark.Size = new System.Drawing.Size(321, 87);
            this.pictureBoxDark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDark.TabIndex = 5;
            this.pictureBoxDark.TabStop = false;
            this.pictureBoxDark.Visible = false;
            // 
            // pictureBoxWhite
            // 
            this.pictureBoxWhite.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.HastaLoginMiddlePictureWhite;
            this.pictureBoxWhite.Location = new System.Drawing.Point(76, 12);
            this.pictureBoxWhite.Name = "pictureBoxWhite";
            this.pictureBoxWhite.Size = new System.Drawing.Size(321, 87);
            this.pictureBoxWhite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWhite.TabIndex = 6;
            this.pictureBoxWhite.TabStop = false;
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
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Veriler Yükleniyor...";
            // 
            // progressPatientLoading
            // 
            this.progressPatientLoading.BackColor = System.Drawing.Color.Transparent;
            this.progressPatientLoading.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressPatientLoading.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(120)))), ((int)(((byte)(117)))));
            this.progressPatientLoading.ColorA = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(201)))), ((int)(((byte)(205)))));
            this.progressPatientLoading.ColorB = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(186)))), ((int)(((byte)(190)))));
            this.progressPatientLoading.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))), ((int)(((byte)(90)))));
            this.progressPatientLoading.ForeColor = System.Drawing.Color.Black;
            this.progressPatientLoading.HatchType = System.Drawing.Drawing2D.HatchStyle.DarkUpwardDiagonal;
            this.progressPatientLoading.Location = new System.Drawing.Point(-1, 154);
            this.progressPatientLoading.Maximum = 100;
            this.progressPatientLoading.Name = "progressPatientLoading";
            this.progressPatientLoading.PercentageText = "%";
            this.progressPatientLoading.ProgressBorderColorA = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))), ((int)(((byte)(90)))));
            this.progressPatientLoading.ProgressBorderColorB = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(151)))), ((int)(((byte)(155)))));
            this.progressPatientLoading.ProgressColorA = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(182)))), ((int)(((byte)(176)))));
            this.progressPatientLoading.ProgressColorB = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(182)))), ((int)(((byte)(176)))));
            this.progressPatientLoading.ProgressLineColorA = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressPatientLoading.ProgressLineColorB = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressPatientLoading.ShowEdge = false;
            this.progressPatientLoading.ShowPercentage = false;
            this.progressPatientLoading.Size = new System.Drawing.Size(455, 30);
            this.progressPatientLoading.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.progressPatientLoading.TabIndex = 3;
            this.progressPatientLoading.Value = 0;
            // 
            // PatientPanelLoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 183);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxDark);
            this.Controls.Add(this.pictureBoxWhite);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.progressPatientLoading);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Logo;
            this.Name = "PatientPanelLoadingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDark;
        private System.Windows.Forms.PictureBox pictureBoxWhite;
        private System.Windows.Forms.Label labelDescription;
        private ReaLTaiizor.Controls.RibbonProgressBarLeft progressPatientLoading;
    }
}