namespace IntegralCounter
{
    public partial class Form1 : Form
    {
        private Integral I;
        private Schedule sch;
        private double lowLimit;
        private double upLimit;
        public Form1()
        {
            InitializeComponent();
            sch = new Schedule(pictureBox3);
            functionBox.Text = "x ln";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ParseLimits();
            I = new Integral(lowLimit, upLimit, functionBox.Text);
            ValueBox.Text = I.Value.ToString();
            sch.DrawFunction(functionBox.Text);


            pictureBox3.Image = sch.Bitmap;

        }

        private void ParseLimits()
        {
            double LowerLimitValue, UpperLimitValue;

            if (double.TryParse(lowerLimit.Text, out double LLimit))
            {
                LowerLimitValue = LLimit;
            }
            else
            {
                LowerLimitValue = 0;
                lowerLimit.Text = "0";
            }

            if (double.TryParse(upperLimit.Text, out double ULimit))
            {
                UpperLimitValue = ULimit;
            }
            else
            {
                UpperLimitValue = 1;
                upperLimit.Text = "1";
            }

            if (UpperLimitValue < LowerLimitValue)
            {
                lowLimit = UpperLimitValue;
                upLimit = LowerLimitValue;
            }
            else
            {
                lowLimit = LowerLimitValue;
                upLimit = UpperLimitValue;
            }
        }

        private void pictureBox3_Resize(object sender, EventArgs e)
        {
            if (sch != null)
            {
                sch.Graphics = pictureBox3.CreateGraphics();
                sch.Reload();
                pictureBox3.Image = sch.Bitmap;

            }
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            coord.Text = $"{e.X} ; {e.Y}";
        }
    }
}