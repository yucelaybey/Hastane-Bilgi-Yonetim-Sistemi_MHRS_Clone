namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    partial class AdminPanelClosingSuccessForm
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.pictureBoxGif = new System.Windows.Forms.PictureBox();
            this.panelPicture = new System.Windows.Forms.Panel();
            this.labelDescription2 = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGif)).BeginInit();
            this.panelPicture.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTitle.ForeColor = System.Drawing.Color.Red;
            this.labelTitle.Location = new System.Drawing.Point(50, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(366, 39);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Başarıyla Çıkış Yapıldı !";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription.Location = new System.Drawing.Point(62, 184);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(364, 39);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "Uygulama Kapatılıyor...";
            // 
            // pictureBoxGif
            // 
            this.pictureBoxGif.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.output_onlinegiftools;
            this.pictureBoxGif.Location = new System.Drawing.Point(-1, 1);
            this.pictureBoxGif.Name = "pictureBoxGif";
            this.pictureBoxGif.Size = new System.Drawing.Size(476, 109);
            this.pictureBoxGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxGif.TabIndex = 2;
            this.pictureBoxGif.TabStop = false;
            // 
            // panelPicture
            // 
            this.panelPicture.BackColor = System.Drawing.Color.Silver;
            this.panelPicture.Controls.Add(this.pictureBoxGif);
            this.panelPicture.Location = new System.Drawing.Point(0, 63);
            this.panelPicture.Name = "panelPicture";
            this.panelPicture.Size = new System.Drawing.Size(475, 110);
            this.panelPicture.TabIndex = 3;
            // 
            // labelDescription2
            // 
            this.labelDescription2.AutoSize = true;
            this.labelDescription2.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDescription2.Location = new System.Drawing.Point(62, 184);
            this.labelDescription2.Name = "labelDescription2";
            this.labelDescription2.Size = new System.Drawing.Size(400, 39);
            this.labelDescription2.TabIndex = 1;
            this.labelDescription2.Text = "Tekrar Görüşmek Üzere...";
            this.labelDescription2.Visible = false;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Roboto", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelCount.Location = new System.Drawing.Point(18, 184);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(36, 39);
            this.labelCount.TabIndex = 4;
            this.labelCount.Text = "3";
            this.labelCount.Visible = false;
            // 
            // AdminPanelClosingSuccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 231);
            this.ControlBox = false;
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.panelPicture);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelDescription2);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::HastaneBilgiYonetimSistemi_HBYS_NET_Framework.Properties.Resources.Admin_Login;
            this.Name = "AdminPanelClosingSuccessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.AdminPanelClosingSuccessForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGif)).EndInit();
            this.panelPicture.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.PictureBox pictureBoxGif;
        private System.Windows.Forms.Panel panelPicture;
        private System.Windows.Forms.Label labelDescription2;
        private System.Windows.Forms.Label labelCount;
    }
}