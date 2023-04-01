using IntegralCounter;

internal class Schedule
{
    private PictureBox pictureBox;

    private string function;
    private Bitmap bitmap;
    private Graphics graphics;
    private PointF centre;
    private int Width;
    private int Height;
    int step = 20;

    public Graphics Graphics
    {
        get => graphics;
        set => graphics = value;
    }
    public Schedule(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox;
        bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
        graphics = Graphics.FromImage(bitmap);
        centre = new Point(pictureBox.Width / 2, pictureBox.Height / 2);
        Width = pictureBox.Width;
        Height = pictureBox.Height;
        GenerateAxes();

        pictureBox.Image = bitmap;
    }

    public void Reload()
    {
        centre = new Point(pictureBox.Width / 2, pictureBox.Height / 2);
        Width = pictureBox.Width;
        Height = pictureBox.Height;
        ClearPicture();

        if (Height < 50 || Width < 50) return;

        bitmap = new Bitmap(Width, Height);


        GenerateAxes();
        if (function != null)
        {
            DrawFunction(function);
        }
    }

    public void ClearPicture() => graphics.Clear(Color.White);

    private void GenerateAxes()
    {
        // Нарисовать оси координат
        Pen axisPen = new Pen(Color.Black, 1);
        graphics.DrawLine(axisPen, Width / 2, 0, Width / 2, Height);
        graphics.DrawLine(axisPen, 0, Height / 2, Width, Height / 2);

        // Нарисовать метки на осях

        Font font = new Font("Arial", 6);
        Brush brush = new SolidBrush(Color.Black);

        for (int x = (int)centre.X + step; x <= Width; x += step)
        {
            graphics.DrawLine(axisPen, x, centre.Y - 5, x, centre.Y + 5);
            if (((x - centre.X) / step) % 2 == 0)
            {
                graphics.DrawString(((x - centre.X) / step).ToString(), font, brush, x - 10, centre.Y + 5);
            }
        }

        for (int x = (int)centre.X - step; x >= 0; x -= step)
        {
            graphics.DrawLine(axisPen, x, centre.Y - 5, x, centre.Y + 5);
            if (((x - centre.X) / step) % 2 == 0)
            {
                graphics.DrawString(((x - centre.X) / step).ToString(), font, brush, x - 10, centre.Y + 5);
            }
        }

        for (int y = (int)centre.Y + step; y <= Height; y += step)
        {
            graphics.DrawLine(axisPen, centre.X - 5, y, centre.X + 5, y);
            if (((centre.Y - y) / step) % 2 == 0)
            {
                graphics.DrawString(((centre.Y - y) / step).ToString(), font, brush, centre.X + 5, y - 10);
            }

        }

        for (int y = (int)centre.Y - step; y >= 0; y -= step)
        {
            graphics.DrawLine(axisPen, centre.X - 5, y, centre.X + 5, y);
            if (((centre.Y - y) / step) % 2 == 0)
            {
                graphics.DrawString(((centre.Y - y) / step).ToString(), font, brush, centre.X + 5, y - 10);
            }

        }
    }

    public void DrawFunction(string function)
    {
        this.function = function;
        ClearPicture();
        GenerateAxes();

        var TruePoints = new List<PointF>();

        decimal x = -50;

        for (int i = 0; i < 10100; i++, x+=0.01m)
        {

            decimal y = MathParser.Evaluate(function, x);
            if (!double.IsNaN(y))
            {
                var newX = (float)(x * step) + centre.X;
                var newY = (float)(y * -step) + centre.Y;
                TruePoints.Add(new PointF(newX, newY));
            }
            else
            {
                if (!(TruePoints.Count < 1))
                {
                    graphics.DrawCurve(new Pen(Color.Black), TruePoints.ToArray());
                    TruePoints.Clear();
                    x++;
                }

            }

        }
        graphics.DrawCurve(new Pen(Color.Black), TruePoints.ToArray());

    }

    public Bitmap Bitmap => bitmap;


}