using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace IntegralCounter
{
    internal class Schedule
    {
        private PictureBox pictureBox;

        private PointF[] points;
        private Bitmap bitmap;
        private Graphics graphics;
        private PointF centre_O;
        private int Width;
        private int Height;
        int step = 20;

        public Graphics Graphics
        {
            get => graphics;
            set => graphics = value;
        }

        public PointF[] Points
        {
            get => points;
            set => points = value;
        }

        public Schedule( PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            centre_O = new Point(pictureBox.Width / 2, pictureBox.Height / 2);
            Width = pictureBox.Width;
            Height = pictureBox.Height;
            GenerateAxes();

            pictureBox.Image = bitmap;
        }

        public void Reload()
        {
            centre_O = new Point(pictureBox.Width / 2, pictureBox.Height / 2);
            Width = pictureBox.Width;
            Height = pictureBox.Height;
            ClearPicture();

            if (Height < 50 || Width < 50) return;

            bitmap = new Bitmap(Width, Height);

            
            GenerateAxes();
            if(points != null)
            {
                DrawLines();
            }
        }

        public void ClearPicture() => graphics.Clear(Color.White);

        public void PlacePoints(PointF[] rawPoints)
        {
            List<PointF> NewPoints = new List<PointF>();

            for (int i = 0; i < rawPoints.Length; i++)
            {
                float newX = (rawPoints[i].X - centre_O.X)  + centre_O.X;
                float newY = (rawPoints[i].Y - centre_O.Y)  + centre_O.Y;

                NewPoints.Add(new PointF(newX, newY));
            }
            this.points = NewPoints.ToArray();
        }

        public void DrawLines(PointF[] points)
        {
            Pen pen = new Pen(Color.Black,3);

            for (int i = 0; i < points.Length - 1; i++)
            {
                graphics.DrawLine(pen, points[i], points[i + 1]);
            }

            graphics.DrawEllipse(pen, 0, 50, 100, 100);

            pictureBox.Image = bitmap;
        }
        public void DrawLines() => DrawLines(this.points);


        public Bitmap Bitmap => bitmap; 

        private void GenerateAxes()
        {
            // Нарисовать оси координат
            Pen axisPen = new Pen(Color.Black, 1);
            graphics.DrawLine(axisPen, Width/2, 0, Width / 2, Height);
            graphics.DrawLine(axisPen, 0, Height / 2, Width, Height / 2);

            // Нарисовать метки на осях

            Font font = new Font("Arial", 6);
            Brush brush = new SolidBrush(Color.Black);

            for (int x = (int)centre_O.X + step; x <= Width; x += step)
            {
                graphics.DrawLine(axisPen, x, centre_O.Y - 5, x, centre_O.Y + 5);
                if (((x - centre_O.X) / step)%2==0)
                {
                    graphics.DrawString(((x - centre_O.X) / step).ToString(), font, brush, x - 10, centre_O.Y + 5);
                }
            }

            for (int x = (int)centre_O.X - step; x >= 0; x -= step)
            {
                graphics.DrawLine(axisPen, x, centre_O.Y - 5, x, centre_O.Y + 5);
                if (((x - centre_O.X) / step) % 2 == 0)
                {
                    graphics.DrawString(((x - centre_O.X) / step).ToString(), font, brush, x - 10, centre_O.Y + 5);
                }
            }

            for (int y = (int)centre_O.Y + step; y <= Height; y += step)
            {
                graphics.DrawLine(axisPen, centre_O.X - 5, y, centre_O.X + 5, y);
                if (((centre_O.Y - y) / step) % 2 == 0)
                {
                    graphics.DrawString(((centre_O.Y - y) / step).ToString(), font, brush, centre_O.X + 5, y - 10);
                }
               
            }

            for (int y = (int)centre_O.Y - step; y >= 0; y -= step)
            {
                graphics.DrawLine(axisPen, centre_O.X - 5, y, centre_O.X + 5, y);
                if (((centre_O.Y - y) / step) % 2 == 0)
                {
                    graphics.DrawString(((centre_O.Y - y) / step).ToString(), font, brush, centre_O.X + 5, y - 10);
                }
                
            }
        }

        public void DrawFunction(string function)
        {
            var points = new List<PointF>();
            Pen pen = new Pen(Color.Black, 1);

            for (float x = 0; x < Width; x += (float)0.1 * step)
            {
                float y = (float)MathParser.Evaluate(function, x);
                points.Add(new PointF(x, y));
            }
            DrawLines(points.ToArray());
        }

        
    }
}
