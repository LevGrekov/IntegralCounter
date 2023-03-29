namespace IntegralCounter
{
    public partial class Form1 : Form
    {
        private Integral I;
        private Schedule sch;
        public Form1()
        {
            InitializeComponent();
            sch = new Schedule(pictureBox3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            I = new Integral(100, 0, "x 2 ^");
            ValueBox.Text = I.Value.ToString();


            sch.PlacePoints(I.Points);

            sch.DrawLines();

            sch.DrawFunction("x 2 ^");


            pictureBox3.Image = sch.Bitmap;

        }

        private void pictureBox3_Resize(object sender, EventArgs e)
        {
            if (sch != null)
            {
                sch.Graphics = pictureBox3.CreateGraphics();
                sch.Reload();

            }
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            coord.Text = $"{e.X} ; {e.Y}";
        }
    }
}