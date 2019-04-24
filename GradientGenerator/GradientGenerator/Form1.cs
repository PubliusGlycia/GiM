using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradientGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height, PixelFormat.Format32bppArgb);
            Graphics ctx = Graphics.FromImage(bmp);

            int rMin = Color.Blue.R;
            int rMax = Color.Black.R;
            int gMin = Color.Blue.G;
            int gMax = Color.Black.G;
            int bMin = Color.Blue.B;
            int bMax = Color.Black.B;

            Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            for (int i = 0; i < pictureBox1.Width * 0.6; i++)
            {
                double divider = (i / (float)pictureBox1.Width) * 1.6;

                var rAverage = (int)(rMin * (1 - divider) + rMax * divider);
                var gAverage = (int)(gMin * (1 - divider) + gMax * divider);
                var bAverage = (int)(bMin * (1 - divider) + bMax * divider);

                for (int Ycount = 0; Ycount < pictureBox1.Height; Ycount++)
                {
                    bmp.SetPixel(i, Ycount, Color.FromArgb(rAverage, gAverage, bAverage));
                }
            }

            rMin = Color.Black.R;
            rMax = Color.Red.R;
            gMin = Color.Black.G;
            gMax = Color.Red.G;
            bMin = Color.Black.B;
            bMax = Color.Red.B;

            for (int i = (int)(pictureBox1.Width * 0.6); i < pictureBox1.Width-1; i++)
            {
                double divider = ((i - pictureBox1.Width * 0.6) / ((float)pictureBox1.Width - pictureBox1.Width * 0.6));
                //Reason for the substraction is that the divider in the start of black-red for loop must be set to 0 in order to achieve desired color  

                var rAverage = (int)(rMin * (1 - divider) + rMax * divider);
                var gAverage = (int)(gMin * (1 - divider) + gMax * divider);
                var bAverage = (int)(bMin * (1 - divider) + bMax * divider);

                for (int Ycount = 0; Ycount < pictureBox1.Height; Ycount++)
                {
                    bmp.SetPixel(i, Ycount, Color.FromArgb(rAverage, gAverage, bAverage));
                }
            }

            Color pixelColor = bmp.GetPixel(pictureBox1.Width - 1, pictureBox1.Height - 1);
            SolidBrush pixelBrush = new SolidBrush(pixelColor);

            ctx.FillRectangle(pixelBrush, rect);
            this.pictureBox1.Image = bmp;
        }
    }
}