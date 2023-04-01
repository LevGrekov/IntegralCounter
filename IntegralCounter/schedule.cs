using IntegralCounter;
using System.Windows.Forms;

internal class Schedule
{

    private string function = "0";

    private Bitmap bitmap;
    private Graphics graphics;

    private PointF centre;
    private int Width;
    private int Height;
    private int step = 20;
    private Size ContainerSize;

    private int BottomEdge;
    private int TopEdge;
    private int RightEdge;
    private int LeftEdge;

    private Pen blackPen;
    private Pen redPen;
    private Font font;
    private Brush brush;

    public Graphics Graphics
    {
        get => graphics;
        set
        {
            graphics = value;
            ContainerSize = graphics.VisibleClipBounds.Size.ToSize();
            Reload();
        }
    }
    public Schedule(int Width, int Height)
    {
        if (Height < 50 || Width < 50) return;

        this.Width = Width;
        this.Height = Height;
        this.centre = new PointF(Width/2, Height/2);

        bitmap = new Bitmap(Width, Height);
        graphics = Graphics.FromImage(bitmap);

        blackPen = new Pen(Color.Black,0.01f);
        redPen = new Pen(Color.Red,0.5f);
        redPen.Width = 2;
        font = new Font("Times New Roman", 6);
        brush = Brushes.Black;

        GenerateAxes();
    }


    public void Reload()
    {
        bitmap = new Bitmap(ContainerSize.Width, ContainerSize.Height);
        graphics = Graphics.FromImage(bitmap);

        centre = new Point(ContainerSize.Width / 2, ContainerSize.Height / 2);
        Width = ContainerSize.Width;
        Height = ContainerSize.Height;
        GenerateAxes();

        if (Height < 50 || Width < 50) return;
        //DrawFunction(function);
    }

    public void ClearPicture() => graphics.Clear(Color.White);

    private void GenerateAxes()
    {
        ClearPicture();
        // Нарисовать оси координат

        graphics.DrawLine(blackPen, Width / 2, 0, Width / 2, Height);
        graphics.DrawLine(blackPen, 0, Height / 2, Width, Height / 2);

        // Нарисовать метки на осях

        for (int x = (int)centre.X + step; x <= Width; x += step)
        {
            var serif = (x - centre.X) / step;
            graphics.DrawLine(blackPen, x, centre.Y - 5, x, centre.Y + 5);

            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, x - 10, centre.Y + 5);
            }
            RightEdge = (int)serif;
        }

        for (int x = (int)centre.X - step; x >= 0; x -= step)
        {
            var serif = (x - centre.X) / step;
            graphics.DrawLine(blackPen, x, centre.Y - 5, x, centre.Y + 5);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, x - 10, centre.Y + 5);
            }
            LeftEdge = (int)serif;
        }

        for (int y = (int)centre.Y + step; y <= Height; y += step)
        {
            var serif = (centre.Y - y) / step;
            graphics.DrawLine(blackPen, centre.X - 5, y, centre.X + 5, y);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, centre.X + 5, y - 10);
            }
            BottomEdge = (int)serif;
        }

        for (int y = (int)centre.Y - step; y >= 0; y -= step)
        {
            var serif = (centre.Y - y) / step;
            graphics.DrawLine(blackPen, centre.X - 5, y, centre.X + 5, y);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, centre.X + 5, y - 10);
            }
            TopEdge = (int)serif;
        }
    }

    public void ChangeFunction(string function)
    {
        DrawFunction(function);
    }

    private void DrawFunction(string function)
    {
        this.function = function;
        GenerateAxes();

        var TruePoints = new List<PointF>();
        var pixel = 0.01m;

        for (decimal x = LeftEdge - 1; x < RightEdge + 1; x += pixel)
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
            catch (OutOfScopeException)
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