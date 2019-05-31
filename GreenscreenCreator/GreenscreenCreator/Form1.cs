using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;

namespace GreenscreenCreator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int key_R = 0;
            int key_G = 128;
            int key_B = 0;

            Color Key = new Color();
            Key = Color.FromArgb(key_R, key_G, key_B);

            double KCb = -(0.168736 * Key.R) - (0.331264 * Key.G) + (0.5 * Key.B);
            double KCr = 0.5 * Key.R - 0.418688 * Key.G - 0.081312 * Key.B;

            VideoFileWriter writer = new VideoFileWriter();        

            VideoFileReader reader = new VideoFileReader();     //point
            reader.Open(@"D:\Github\GiM\chicken.mp4");

            int height = reader.Height;
            int width = reader.Width;

            writer.Open("chicken.mp4", width, height, 25, VideoCodec.MPEG4);

            Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            for (int i = 0; i < 1000; i++)
            {
                image = reader.ReadVideoFrame();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color c = image.GetPixel(x, y);

                        double Pb = -0.168736 * c.R - 0.331264 * c.G + 0.5 * c.B;
                        double Pr = 0.5 * c.R - 0.418688 * c.G - 0.081312 * c.B;

                        // Obliczanie dystansu i punktu na wykresie funkcji
                        double dist = Math.Sqrt((KCb - Pb) * (KCb - Pb) + (KCr - Pr) * (KCr - Pr));
                        double point = 1;
                        // Przedziały
                        int a1 = 40;
                        int a2 = 60;

                        if (dist <= a1)
                        {
                            point = 0;
                        }
                        else if (dist >= a2)
                        {
                            point = 1;
                        }
                        else if (dist > a1 && dist < a2)
                        {
                            point = (dist - a1) / (a2 - a1);
                        }

                        double q = 1 - point;

                        double PwR = Math.Min(Math.Max(c.R - q * Key.R, 0) + q * Color.White.R, 255);
                        double PwG = Math.Min(Math.Max(c.G - q * Key.G, 0) + q * Color.White.G, 255);
                        double PwB = Math.Min(Math.Max(c.B - q * Key.B, 0) + q * Color.White.B, 255);

                        Color FColor = new Color();
                        FColor = Color.FromArgb((int)PwR, (int)PwG, (int)PwB);

                        image.SetPixel(x, y, FColor);
                    }
                }
                writer.WriteVideoFrame(image);
                image.Dispose();
            }
            writer.Close();
            reader.Close();
        }
    }
}
