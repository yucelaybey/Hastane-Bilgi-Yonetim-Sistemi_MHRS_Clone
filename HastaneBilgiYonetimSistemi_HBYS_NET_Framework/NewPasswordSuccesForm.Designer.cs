namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class NewPasswordSuccesForm
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
            this.pictureBoxSuccess = new System.Windows.Forms.PictureBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelSuccesMessage1 = new System.Windows.Forms.Label();
            this.labelSuccesMessage2 = new System.Windows.Forms.Label();
            this.labelSuccesMessage3 = new System.Windows.Forms.Label();
            this.labelSuccesMessage4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSuccess
            // 
            this.pictureBoxSuccess.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.registerSuccessTransparent;
            this.pictureBoxSuccess.Location = new System.Drawing.Point(22, 9);
            this.pictureBoxSuccess.Name = "pictureBoxSuccess";
            this.pictureBoxSuccess.Size = new System.Drawing.Size(80, 80);
            this.pictureBoxSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSuccess.TabIndex = 0;
            this.pictureBoxSuccess.TabStop = false;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelCount.Location = new System.Drawing.Point(27, 93);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 48);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "1 \r\nSaniye";
            this.labelCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSuccesMessage1
            // 
            this.labelSuccesMessage1.AutoSize = true;
            this.labelSuccesMessage1.Font = new System.Drawing.Font("Roboto", 12F);
            this.labelSuccesMessage1.Location = new System.Drawing.Point(103, 13);
            this.labelSuccesMessage1.Name = "labelSuccesMessage1";
            this.labelSuccesMessage1.Size = new System.Drawing.Size(304, 24);
            this.labelSuccesMessage1.TabIndex = 2;
            this.labelSuccesMessage1.Text = "Şifreniz Başarıyla Değiştirilmiştir.";
            // 
            // labelSuccesMessage2
            // 
            this.labelSuccesMessage2.AutoSize = true;
            this.labelSuccesMessage2.Font = new System.Drawing.Font("Roboto", 13.8F);
            this.labelSuccesMessage2.ForeColor = System.Drawing.Color.Red;
            this.labelSuccesMessage2.Location = new System.Drawing.Point(186, 40);
            this.labelSuccesMessage2.Name = "labelSuccesMessage2";
            this.labelSuccesMessage2.Size = new System.Drawing.Size(134, 28);
            this.labelSuccesMessage2.TabIndex = 3;
            this.labelSuccesMessage2.Text = "Anasayfaya";
            // 
            // labelSuccesMessage3
            // 
            this.labelSuccesMessage3.AutoSize = true;
            this.labelSuccesMessage3.Font = new System.Drawing.Font("Roboto", 13.8F);
            this.labelSuccesMessage3.Location = new System.Drawing.Point(141, 68);
            this.labelSuccesMessage3.Name = "labelSuccesMessage3";
            this.labelSuccesMessage3.Size = new System.Drawing.Size(227, 28);
            this.labelSuccesMessage3.TabIndex = 3;
            this.labelSuccesMessage3.Text = "Yönlendiriliyorsunuz.";
            // 
            // labelSuccesMessage4
            // 
            this.labelSuccesMessage4.AutoSize = true;
            this.labelSuccesMessage4.Font = new System.Drawing.Font("Roboto", 19.8F);
            this.labelSuccesMessage4.Location = new System.Drawing.Point(109, 97);
            this.labelSuccesMessage4.Name = "labelSuccesMessage4";
            this.labelSuccesMessage4.Size = new System.Drawing.Size(263, 39);
            this.labelSuccesMessage4.TabIndex = 3;
            this.labelSuccesMessage4.Text = "Lütfen Bekleyiniz";
            // 
            // NewPasswordSuccesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 150);
            this.ControlBox = false;
            this.Controls.Add(this.labelSuccesMessage4);
            this.Controls.Add(this.labelSuccesMessage3);
            this.Controls.Add(this.labelSuccesMessage2);
            this.Controls.Add(this.labelSuccesMessage1);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.pictureBoxSuccess);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Best;
            this.Name = "NewPasswordSuccesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.NewPasswordSuccesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSuccess;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelSuccesMessage1;
        private System.Windows.Forms.Label labelSuccesMessage2;
        private System.Windows.Forms.Label labelSuccesMessage3;
        private System.Windows.Forms.Label labelSuccesMessage4;
    }
}