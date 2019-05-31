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

namespace BackgroundEraser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(@"D:\Github\GiM\green2_resized.png");


            // Creating Key Pixel
            int key_R = 0;
            int key_G = 128;
            int key_B = 0;

            Color Key = new Color();
            Key = Color.FromArgb(key_R, key_G, key_B);

            double KCb = -(0.168736 * Key.R) - (0.331264 * Key.G) + (0.5 * Key.B);
            double KCr = 0.5 * Key.R - 0.418688 * Key.G - 0.081312 * Key.B;

            int height = bmp.Height;
            int width = bmp.Width;
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Color c = bmp.GetPixel(x, y);

                    double Pb = -0.168736 * c.R - 0.331264 * c.G + 0.5 * c.B;
                    double Pr = 0.5 * c.R - 0.418688 * c.G - 0.081312 * c.B;

                    // Obliczanie dystansu i punktu na wykresie funkcji
                    double dist = Math.Sqrt((KCb - Pb) * (KCb - Pb) + (KCr - Pr) * (KCr - Pr));
                    double point = 1;
                    // Przedziały
                    int a1 = 40;
                    int a2 = 60;

                    if(dist <= a1)
                    {
                        point = 0;
                    }
                    else if(dist >= a2)
                    {
                        point = 1;
                    }
                    else if(dist > a1 && dist < a2)
                    {
                        point = (dist - a1) / (a2 - a1);
                    }

                    double q = 1 - point;

                    double PwR = Math.Min(Math.Max(c.R - q * Key.R, 0) + q * Color.White.R, 255);
                    double PwG = Math.Min(Math.Max(c.G - q * Key.G, 0) + q * Color.White.G, 255);
                    double PwB = Math.Min(Math.Max(c.B - q * Key.B, 0) + q * Color.White.B, 255);

                    Color FColor = new Color();
                    FColor = Color.FromArgb((int)PwR, (int)PwG, (int)PwB);

                    bmp.SetPixel(x, y, FColor);

                    /*
                    int cMax = Math.Max(Math.Max(c.R,c.G),c.B);
                    int cMin = Math.Min(Math.Min(c.R, c.G), c.B);
                    if (c.G != cMin && (c.G == cMax || cMax - c.G < 8) && (cMax - cMin) > 25){
                        c = Color.White;

                        bmp.SetPixel(x, y, c);
                    }
                    */
                }
            }


            pictureBox1.Image = bmp;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
