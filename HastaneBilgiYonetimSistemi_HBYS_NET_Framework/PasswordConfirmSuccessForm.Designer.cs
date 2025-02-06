namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class PasswordConfirmSuccessForm
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
            this.labelCount = new System.Windows.Forms.Label();
            this.pictureBoxRegisterSuccess = new System.Windows.Forms.PictureBox();
            this.labelDescription2 = new System.Windows.Forms.Label();
            this.labelDescription1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegisterSuccess)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelCount.ForeColor = System.Drawing.Color.Black;
            this.labelCount.Location = new System.Drawing.Point(190, 106);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(141, 39);
            this.labelCount.TabIndex = 13;
            this.labelCount.Text = "3 Saniye";
            // 
            // pictureBoxRegisterSuccess
            // 
            this.pictureBoxRegisterSuccess.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.registerSuccessTransparent;
            this.pictureBoxRegisterSuccess.Location = new System.Drawing.Point(7, 15);
            this.pictureBoxRegisterSuccess.Name = "pictureBoxRegisterSuccess";
            this.pictureBoxRegisterSuccess.Size = new System.Drawing.Size(120, 120);
            this.pictureBoxRegisterSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRegisterSuccess.TabIndex = 12;
            this.pictureBoxRegisterSuccess.TabStop = false;
            // 
            // labelDescription2
            // 
            this.labelDescription2.AutoSize = true;
            this.labelDescription2.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription2.Location = new System.Drawing.Point(127, 67);
            this.labelDescription2.Name = "labelDescription2";
            this.labelDescription2.Size = new System.Drawing.Size(294, 39);
            this.labelDescription2.TabIndex = 9;
            this.labelDescription2.Text = "Lütfen Bekleyiniz..";
            // 
            // labelDescription1
            // 
            this.labelDescription1.AutoSize = true;
            this.labelDescription1.Font = new System.Drawing.Font("Roboto", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription1.ForeColor = System.Drawing.Color.Red;
            this.labelDescription1.Location = new System.Drawing.Point(127, 20);
            this.labelDescription1.Name = "labelDescription1";
            this.labelDescription1.Size = new System.Drawing.Size(296, 37);
            this.labelDescription1.TabIndex = 11;
            this.labelDescription1.Text = "Başarıyla Doğrulandı";
            // 
            // PasswordConfirmSuccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 154);
            this.ControlBox = false;
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.pictureBoxRegisterSuccess);
            this.Controls.Add(this.labelDescription2);
            this.Controls.Add(this.labelDescription1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Best;
            this.Name = "PasswordConfirmSuccessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PasswordConfirmSucces_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegisterSuccess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.PictureBox pictureBoxRegisterSuccess;
        private System.Windows.Forms.Label labelDescription2;
        private System.Windows.Forms.Label labelDescription1;
    }
}