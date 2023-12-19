using MyPaintApp;
using System.Drawing;

internal class Rhombus : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Color FillColor { get; set; }

    public Rhombus()
    {
        X = 0;
        Y = 0;
        Width = 0;
        Height = 0;
        OutLineColor = Color.Black;
        OutLineWidth = 1;
        FillColor = Color.Transparent; // Initialize FillColor with a default value
    }

    public Rhombus(int x, int y, int width, int height, Color outlineColor, int outlineWidth, Color fillColor)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        OutLineColor = outlineColor;
        OutLineWidth = outlineWidth;
        FillColor = fillColor;
    }

    public override void Draw(Graphics g)
    {
        Pen pen = new Pen(OutLineColor, OutLineWidth);
        Brush brush = new SolidBrush(FillColor);

        Point[] points =
        {
            new Point(X + Width / 2, Y),
            new Point(X + Width, Y + Height / 2),
            new Point(X + Width / 2, Y + Height),
            new Point(X, Y + Height / 2)
        };
        g.DrawPolygon(pen, points);
        g.FillPolygon(brush, points);
    }
}
