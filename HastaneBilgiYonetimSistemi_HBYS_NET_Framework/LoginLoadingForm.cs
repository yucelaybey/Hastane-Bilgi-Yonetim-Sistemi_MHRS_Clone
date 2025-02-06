using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HastaneBilgiYonetimSistemi_HBYS_NET_Framework
{
    public partial class LoginLoadingForm : XtraForm
    {
        private Timer animationTimer;
        private int progressValue;

        public LoginLoadingForm()
        {
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 500);
            this.BackColor = Color.White;
            Bitmap bitmap = Properties.Resources.Best;
            this.Icon = Icon.FromHandle(bitmap.GetHicon());
            
            this.DoubleBuffered = true;

            
            this.Region = new Region(CreateRoundedRectanglePath(this.ClientSize.Width, this.ClientSize.Height, 200));

            
            animationTimer = new Timer();
            animationTimer.Interval = 50;
            animationTimer.Tick += new EventHandler(AnimateProgressBar);
            animationTimer.Start();

            
            this.Paint += LoginLoadingForm_Paint;
        }

        private GraphicsPath CreateRoundedRectanglePath(int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, width, height);
            return path;
        }

        private void AnimateProgressBar(object sender, EventArgs e)
        {
            if (progressValue < 100)
            {
                progressValue += 1;
                this.Invalidate();
            }
            else
            {
                animationTimer.Stop();
                this.Close();
            }
        }

        private void LoginLoadingForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            
            Rectangle progressRect = new Rectangle(0, 0, this.Width, this.Height);
            using (Brush backgroundBrush = new SolidBrush(Color.White))
            {
                g.FillEllipse(backgroundBrush, progressRect);
            }

            
            int diameter = Math.Min(this.Width, this.Height);
            Rectangle progressFill = new Rectangle(0, this.Height - (progressValue * (diameter / 100)), diameter, progressValue * (diameter / 100));
            using (Brush progressBrush = new SolidBrush(Color.DarkRed))
            {
                g.FillEllipse(progressBrush, progressFill);
            }

            
            Image picture = Properties.Resources.BestLoading;
            Rectangle pictureRect = new Rectangle(-6, -6, 513, 513);
            g.DrawImage(picture, pictureRect);
        }
    }
}
