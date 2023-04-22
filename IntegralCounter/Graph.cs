using IntegralCounter;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

internal class Graph
{

    //private string function = "0";

    private Function function = x => { return MathParser.Evaluate("0", x); };
    private PointF[] Area;

    private Bitmap bitmap;
    private Graphics graphics;

    public PointF center;
    private int Width;
    private int Height;
    public readonly int step = 20;
    private decimal pixel = 0.01m;

    private int BottomEdge;
    private int TopEdge;
    private int RightEdge;
    private int LeftEdge;

    private Pen blackPen;
    private Pen redPen;
    private Pen bluePen;
    private Font font;
    private Brush brush;
    private Brush HutchedBrush;

    public void ChangeFunction(string function)
    {
        this.function = x => { return MathParser.Evaluate(function, x); };
    }
    public void Reload(int Width, int Height,decimal lowerLimit,decimal upperLimit)
    {
        if (Height < 50 || Width < 50) return;

        this.Width = Width;
        this.Height = Height;
        this.center = new PointF(Width / 2, Height / 2);

        bitmap = new Bitmap(Width, Height);
        graphics = Graphics.FromImage(bitmap);

        ClearPicture();
        DrawFunction();
        DrawAreaIntegrated(lowerLimit, upperLimit);
        GenerateAxes();
    }
    public void Reload(int Width, int Height)
    {
        if (Height < 50 || Width < 50) return;

        this.Width = Width;
        this.Height = Height;
        this.center = new PointF(Width / 2, Height / 2);

        bitmap = new Bitmap(Width, Height);
        graphics = Graphics.FromImage(bitmap);

        ClearPicture();
        DrawFunction();
        GenerateAxes();
    }
    public void Reload(decimal lowerLimit, decimal upperLimit)
    {
        
        ClearPicture();
        DrawFunction();
        DrawAreaIntegrated(lowerLimit,upperLimit);
        GenerateAxes();
        //DrawAreaIntegrated(lowerLimit, upperLimit);


    }

    public Graph(int Width, int Height)
    {
        blackPen = new Pen(Color.Black, 0.1f);
        redPen = new Pen(Color.Red, 2f);
        bluePen = new Pen(Color.Red, 0.5f);
        HutchedBrush = new HatchBrush(HatchStyle.ForwardDiagonal, Color.Red, Color.White);


        font = new Font("Times New Roman", 6);
        brush = Brushes.Black;

        Reload(Width, Height);
    }

    private void ClearPicture() => graphics.Clear(Color.White);

    private void GenerateAxes()
    {
        // Нарисовать оси координат
        var ArrowPen = new Pen(brush, 0.01f);
        ArrowPen.CustomEndCap = new AdjustableArrowCap(6, 6);

        graphics.DrawLine(ArrowPen, center.X, Height, center.X, 0);
        graphics.DrawLine(ArrowPen, 0, center.Y, Width, center.Y);

        // Нарисовать метки на осях

        for (int x = (int)center.X + step; x <= Width; x += step)
        {
            var serif = (x - center.X) / step;
            graphics.DrawLine(blackPen, x, center.Y - 5, x, center.Y + 5);

            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, x - 10, center.Y + 5);
            }
            RightEdge = (int)serif;
        }

        for (int x = (int)center.X - step; x >= 0; x -= step)
        {
            var serif = (x - center.X) / step;
            graphics.DrawLine(blackPen, x, center.Y - 5, x, center.Y + 5);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, x - 10, center.Y + 5);
            }
            LeftEdge = (int)serif;
        }

        for (int y = (int)center.Y + step; y <= Height; y += step)
        {
            var serif = (center.Y - y) / step;
            graphics.DrawLine(blackPen, center.X - 5, y, center.X + 5, y);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, center.X + 5, y - 10);
            }
            BottomEdge = (int)serif;
        }

        for (int y = (int)center.Y - step; y >= 0; y -= step)
        {
            var serif = (center.Y - y) / step;
            graphics.DrawLine(blackPen, center.X - 5, y, center.X + 5, y);
            if (serif % 2 == 0)
            {
                graphics.DrawString(serif.ToString(), font, brush, center.X + 5, y - 10);
            }
            TopEdge = (int)serif;
        }
    }
    
    private PointF ReplacePoint(decimal x, decimal y) => new PointF((float)(x * step) + center.X, (float)(y * -step) + center.Y);
    private void DrawFunction(decimal lowerLimit, decimal upperLimit, Pen pen,bool isPart)
    {
        var TruePoints = new List<PointF>();

        for (decimal x = lowerLimit; x <= upperLimit; x += pixel)
        {
            try
            {
                decimal y = function(x);

                if (y > BottomEdge - 1 && y < TopEdge + 1)
                {
                    TruePoints.Add(ReplacePoint(x,y));
                }
            }
            catch (OutOfScopeException)
            {
                if (TruePoints.Count > 1)
                {
                    graphics.DrawLines(pen, TruePoints.ToArray());
                    TruePoints.Clear();
                }
                x += pixel;
            }
        }

        if (TruePoints.Count > 1)
        {
            graphics.DrawCurve(pen, TruePoints.ToArray());


            if (isPart)
            {
                
                var RX = ReplacePoint(upperLimit, 0);
                var LX = ReplacePoint(lowerLimit, 0);

                decimal RY ,LY;

                try
                {
                    RY = function(upperLimit);
                    if (RY > TopEdge)
                    {
                        TruePoints.Add(ReplacePoint(upperLimit, TopEdge + 1));
                    }
                    if (RY < BottomEdge)
                    {
                        TruePoints.Add(ReplacePoint(upperLimit, BottomEdge - 1));
                    }
                    TruePoints.Add(RX);
                }
                catch (OutOfScopeException)
                {
                    TruePoints.Add(new PointF(TruePoints[^1].X,center.Y));
                }
                try
                {
                    LY = function(lowerLimit);
                    TruePoints.Add(LX);

                    if (LY < BottomEdge)
                    {
                        TruePoints.Add(ReplacePoint(lowerLimit, BottomEdge - 1));
                    }
                    if (LY > TopEdge)
                    {
                        TruePoints.Add(ReplacePoint(lowerLimit, TopEdge + 1));
                    }
                }
                catch(OutOfScopeException)
                {
                    TruePoints.Add(new PointF(TruePoints[0].X, center.Y));
                }
;
                Area = TruePoints.ToArray();
            }
            
        }
    }

    private void DrawFunction() => DrawFunction(LeftEdge - 1, RightEdge + 1,blackPen , false) ;
    public Bitmap Bitmap => bitmap;

    public void DrawAreaIntegrated(decimal lowerLimit, decimal upperLimit)
    {

        DrawFunction(lowerLimit, upperLimit, redPen, true);
        graphics.FillPolygon(HutchedBrush, Area);
        graphics.DrawPolygon(redPen, Area);
    }

}