namespace IntegralCounter
{
    public partial class Form1 : Form
    {
        private Integral I;
        private Schedule sch;
        private decimal lowLimit;
        private decimal upLimit;
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