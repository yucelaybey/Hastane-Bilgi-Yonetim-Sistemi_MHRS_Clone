namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class RegisterSuccessForm
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
            this.labelDescription1 = new System.Windows.Forms.Label();
            this.labelDescription2 = new System.Windows.Forms.Label();
            this.labelDescription3 = new System.Windows.Forms.Label();
            this.labelDescription4 = new System.Windows.Forms.Label();
            this.pictureBoxRegisterSuccess = new System.Windows.Forms.PictureBox();
            this.labelCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegisterSuccess)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDescription1
            // 
            this.labelDescription1.AutoSize = true;
            this.labelDescription1.Font = new System.Drawing.Font("Roboto", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription1.Location = new System.Drawing.Point(104, 6);
            this.labelDescription1.Name = "labelDescription1";
            this.labelDescription1.Size = new System.Drawing.Size(320, 37);
            this.labelDescription1.TabIndex = 0;
            this.labelDescription1.Text = "Başarıyla Kayıt Olundu";
            // 
            // labelDescription2
            // 
            this.labelDescription2.AutoSize = true;
            this.labelDescription2.Font = new System.Drawing.Font("Roboto", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription2.ForeColor = System.Drawing.Color.Red;
            this.labelDescription2.Location = new System.Drawing.Point(174, 48);
            this.labelDescription2.Name = "labelDescription2";
            this.labelDescription2.Size = new System.Drawing.Size(173, 37);
            this.labelDescription2.TabIndex = 0;
            this.labelDescription2.Text = "Anasayfaya";
            // 
            // labelDescription3
            // 
            this.labelDescription3.AutoSize = true;
            this.labelDescription3.Font = new System.Drawing.Font("Roboto", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription3.Location = new System.Drawing.Point(104, 94);
            this.labelDescription3.Name = "labelDescription3";
            this.labelDescription3.Size = new System.Drawing.Size(321, 37);
            this.labelDescription3.TabIndex = 0;
            this.labelDescription3.Text = "Yönlendiriliyorsunuz...";
            // 
            // labelDescription4
            // 
            this.labelDescription4.AutoSize = true;
            this.labelDescription4.Font = new System.Drawing.Font("Roboto", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription4.Location = new System.Drawing.Point(20, 142);
            this.labelDescription4.Name = "labelDescription4";
            this.labelDescription4.Size = new System.Drawing.Size(379, 52);
            this.labelDescription4.TabIndex = 0;
            this.labelDescription4.Text = "Lütfen Bekleyiniz..";
            // 
            // pictureBoxRegisterSuccess
            // 
            this.pictureBoxRegisterSuccess.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.registerSuccessTransparent;
            this.pictureBoxRegisterSuccess.Location = new System.Drawing.Point(4, 1);
            this.pictureBoxRegisterSuccess.Name = "pictureBoxRegisterSuccess";
            this.pictureBoxRegisterSuccess.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxRegisterSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRegisterSuccess.TabIndex = 1;
            this.pictureBoxRegisterSuccess.TabStop = false;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Roboto", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelCount.ForeColor = System.Drawing.Color.Black;
            this.labelCount.Location = new System.Drawing.Point(4, 114);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(100, 28);
            this.labelCount.TabIndex = 2;
            this.labelCount.Text = "3 Saniye";
            // 
            // RegisterSuccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 199);
            this.ControlBox = false;
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.pictureBoxRegisterSuccess);
            this.Controls.Add(this.labelDescription2);
            this.Controls.Add(this.labelDescription4);
            this.Controls.Add(this.labelDescription3);
            this.Controls.Add(this.labelDescription1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Best;
            this.Name = "RegisterSuccessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.RegisterSuccess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegisterSuccess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDescription1;
        private System.Windows.Forms.Label labelDescription2;
        private System.Windows.Forms.Label labelDescription3;
        private System.Windows.Forms.Label labelDescription4;
        private System.Windows.Forms.PictureBox pictureBoxRegisterSuccess;
        private System.Windows.Forms.Label labelCount;
    }
}