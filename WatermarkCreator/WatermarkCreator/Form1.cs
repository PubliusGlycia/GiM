using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WatermarkCreator
{
    public partial class Form1 : Form
    {
        Image img = new Bitmap(@"D:\GitHub\GiM\Polish_tv_rating_system_bo_2011.png");
        PointF ulCorner = new PointF(550.0F, 25.0F);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileVideoSource videoSource = new FileVideoSource(@"D:\Github\GiM\film1.avi");

            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();

            videoSourcePlayer1.VideoSource = videoSource;
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = eventArgs.Frame;

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(SetImageOpacity(img, 0.5f), ulCorner);


        }

        public Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();

                matrix.Matrix33 = opacity;

                ImageAttributes attributes = new ImageAttributes();

                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }
    }
}
