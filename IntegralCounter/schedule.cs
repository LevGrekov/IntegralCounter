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
    private int step = 20;

    private int BottomEdge;
    private int TopEdge;

    private int RightEdge;
    private int LeftEdge;

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
            var serif = (x - centre.X) / step;
            graphics.DrawLine(axisPen, x, centre.Y - 5, x, centre.Y + 5);

            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, x - 10, centre.Y + 5);
            }
            RightEdge = (int)serif;
        }

        for (int x = (int)centre.X - step; x >= 0; x -= step)
        {
            var serif = (x - centre.X) / step;
            graphics.DrawLine(axisPen, x, centre.Y - 5, x, centre.Y + 5);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, x - 10, centre.Y + 5);
            }
            LeftEdge = (int)serif;
        }

        for (int y = (int)centre.Y + step; y <= Height; y += step)
        {
            var serif = (centre.Y - y) / step;
            graphics.DrawLine(axisPen, centre.X - 5, y, centre.X + 5, y);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, centre.X + 5, y - 10);
            }
            BottomEdge = (int)serif;
        }

        for (int y = (int)centre.Y - step; y >= 0; y -= step)
        {
            var serif = (centre.Y - y) / step;
            graphics.DrawLine(axisPen, centre.X - 5, y, centre.X + 5, y);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, centre.X + 5, y - 10);
            }
            TopEdge = (int)serif;
        }
    }

    public void DrawFunction(string function)
    {
        this.function = function;
        ClearPicture();
        GenerateAxes();

        var TruePoints = new List<PointF>();
        var pixel = 0.01m;

        for (decimal x = LeftEdge-1; x < RightEdge+1; x+=pixel)
        {

            try
            {
                decimal y = MathParser.Evaluate(function, x);

                if (y > BottomEdge - 1 && y < TopEdge + 1)
                {
                    var newX = (float)(x * step) + centre.X;
                    var newY = (float)(y * -step) + centre.Y;
                    TruePoints.Add(new PointF(newX, newY));
                }
            }
            catch(OutOfScopeException)
            {
                if (TruePoints.Count > 1)
                {
                    graphics.DrawLines(new Pen(Color.Black), TruePoints.ToArray());
                    TruePoints.Clear();   
                }
                x += pixel;
            }
        }
        graphics.DrawCurve(new Pen(Color.Black), TruePoints.ToArray());

    }

    public Bitmap Bitmap => bitmap;


}