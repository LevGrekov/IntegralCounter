using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace IntegralCounter
{
    public partial class Form1 : Form
    {
        private Integral I;
        private Schedule sch;
        public Form1()
        {
            InitializeComponent();
            sch = new Schedule(pictureBox3.Width, pictureBox3.Height);
            pictureBox3.Image = sch.Bitmap;
            functionBox.Text = "sin(x)+3*x";
            upperLimit.Text = "1";
            lowerLimit.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Обработка Интеграла
            I = ParseLimits();
            ValueBox.Text = I.Value.ToString();
            upperLimit.Text = I.UpperLimit.ToString();
            lowerLimit.Text = I.LowerLimit.ToString();

            //Обработка Графика 
            sch.ChangeFunction(functionBox.Text);

            sch.Reload(I.LowerLimit, I.UpperLimit);
            pictureBox3.Image = sch.Bitmap;
        }

        private Integral ParseLimits()
        {
            decimal LowerLimitValue, UpperLimitValue;

            if (decimal.TryParse(lowerLimit.Text, out decimal LLimit))
            {
                LowerLimitValue = LLimit;
            }
            else
            {
                LowerLimitValue = 0;
                lowerLimit.Text = "0";
            }

            if (decimal.TryParse(upperLimit.Text, out decimal ULimit))
            {
                UpperLimitValue = ULimit;
            }
            else
            {
                UpperLimitValue = 1;
                upperLimit.Text = "1";
            }

            return new Integral(LowerLimitValue, UpperLimitValue, functionBox.Text,diff.Text);
        }
        private void pictureBox3_SizeChanged(object sender, EventArgs e)
        {
            if (I is not null)
            {
                sch.Reload(pictureBox3.Width, pictureBox3.Height, I.LowerLimit, I.UpperLimit);

            }
            else
            {
                sch.Reload(pictureBox3.Width, pictureBox3.Height);
            }
            pictureBox3.Image = sch.Bitmap;

        }
        private Point MouseLocation;
        private bool isOnPictureBox;
        private char isNeedMinus(double d)
        {
            if (d < 0) return '-';
            else return ' ';
        }
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            double newX = (e.X - sch.center.X) / sch.step;
            double newY = (e.Y - sch.center.Y) / (-sch.step);

            CoordinatesX.Text = $"{isNeedMinus(newX)}{Math.Round(Math.Abs(newX), 2).ToString("F2")}";
            CoordinatesY.Text = $"{isNeedMinus(newY)}{Math.Round(Math.Abs(newY), 2).ToString("F2")}";

            MouseLocation = e.Location;
            pictureBox3.Invalidate();
        }
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            var Pen = new Pen(Color.Blue, 0.01f);
            Pen.DashStyle = DashStyle.Dash;

            if (isOnPictureBox)
            {
                e.Graphics.DrawLine(Pen, MouseLocation.X, Height, MouseLocation.X, 0);
                e.Graphics.DrawLine(Pen, 0, MouseLocation.Y, Width, MouseLocation.Y);
            }

        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            isOnPictureBox = false;
            pictureBox3.Invalidate();
            CoordinatesX.Text = "";
            CoordinatesY.Text = "";
            semicolon.Text = "";

        }
        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            isOnPictureBox = true;
            semicolon.Text = ";";
        }
    }
}