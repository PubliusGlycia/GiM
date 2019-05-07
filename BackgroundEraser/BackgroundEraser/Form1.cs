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
            Bitmap bmp = new Bitmap(@"C:\Users\Admin\Documents\Github\GiM\green2_resized.png");

            int key_G;
            int key_B;

            int height = bmp.Height;
            int width = bmp.Width;
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Color c = bmp.GetPixel(x, y);

                    double Pb = -0.168736 * c.R - 0.331264 * c.G + 0.5 * c.B;
                    double Pr = 0.5 * c.R - 0.418688 * c.G - 0.081312 * c.B;

                    int cMax = Math.Max(Math.Max(c.R,c.G),c.B);
                    int cMin = Math.Min(Math.Min(c.R, c.G), c.B);
                    if (c.G != cMin && (c.G == cMax || cMax - c.G < 8) && (cMax - cMin) > 25){
                        c = Color.White;

                        bmp.SetPixel(x, y, c);
                    }
                }
            }


            pictureBox1.Image = bmp;
        }
    }
}
