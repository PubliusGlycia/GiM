using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height, PixelFormat.Format32bppArgb); //tworzenie bitmapy

            //Graphics ctx = Graphics.FromImage(bmp); //tworzenie kontekstu

            /*
            Matrix m = new Matrix();  
            //m.Translate(-100, 100);  // przesuwanie o wektor
            //m.Rotate(45.0f);         // obracanie obiektu
            //m.Shear(2, 1);           // zakrzywienie
            //m.Scale(2,2);            // skalowanie
            ctx.Transform = m;

            Rectangle rect = new Rectangle(10, 20, 200, 100); //rysowanie wypełnienia

            ctx.FillRectangle(Brushes.Red, rect);

            Pen p = new Pen(Color.Blue, 10.0f);

            ctx.DrawRectangle(p, rect); //rysowanie prostokąta wokół wypełnienia

            Font f = new Font(FontFamily.GenericSerif, 20.0f, FontStyle.Bold);

            ctx.DrawString("Ala ma kota", f, Brushes.Black, 20, 10);
            ctx.DrawString("Kot ma Ale", f, Brushes.Black, 40, 20);


            bmp.Save("Obraz.png");

            this.pictureBox1.Image = bmp;
            

            Bitmap bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height, PixelFormat.Format32bppArgb);
            Graphics ctx = Graphics.FromImage(bmp);

            Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            ctx.FillRectangle(Brushes.Black, rect);

            Font f = new Font(FontFamily.GenericSerif, 200.0f, FontStyle.Bold);
            int posX = 0;
            int posY = 0;

            ctx.DrawString("Ala ma kota", f, Brushes.Red, posX, posY);


            this.pictureBox1.Image = bmp;
            */
            
        }

        int posX = 0;
        int pos2X = 0;
        int posY = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Bitmap bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height, PixelFormat.Format32bppArgb);
            Graphics ctx = Graphics.FromImage(bmp);

            Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            Font f = new Font(FontFamily.GenericSerif, 200.0f, FontStyle.Bold);

            ctx.FillRectangle(Brushes.Black, rect);

            //Brush b = new SolidBrush(); - dodanie kanału alfa

            ctx.DrawString("Ala ma kota", f, Brushes.Red, posX, posY);
            ctx.DrawString("Kot ma Ale", f, Brushes.Red, pos2X, posY);

            posX += 20;
            pos2X -= 20;

            //this.pictureBox1.Left += 20;

            if (posX > this.ClientSize.Width || pos2X > this.ClientSize.Width)
            {
                posX =- this.ClientSize.Width;
                pos2X = -this.ClientSize.Width;

            }

            this.pictureBox1.Image = bmp;
        }
    }
}
