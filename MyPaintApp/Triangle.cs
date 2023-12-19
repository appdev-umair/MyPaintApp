using MyPaintApp;
using System.Drawing;

internal class Triangle : Shape
{
    public int X2 { get; set; }
    public int Y2 { get; set; }
    public int X3 { get; set; }
    public int Y3 { get; set; }

    public Color FillColor { get; set; }

    public Triangle()
    {
        X = 0;
        Y = 0;
        X2 = 0;
        Y2 = 0;
        X3 = 0;
        Y3 = 0;
        OutLineColor = Color.Black;
        OutLineWidth = 1;
        FillColor = Color.Transparent; // Initialize FillColor with a default value
    }

    public Triangle(int x, int y, int x2, int y2, int x3, int y3, Color outlineColor, int outlineWidth, Color fillColor)
    {
        X = x;
        Y = y;
        X2 = x2;
        Y2 = y2;
        X3 = x3;
        Y3 = y3;
        OutLineColor = outlineColor;
        OutLineWidth = outlineWidth;
        FillColor = fillColor;
    }

    public override void Draw(Graphics g)
    {
        Pen pen = new Pen(OutLineColor, OutLineWidth);
        Brush brush = new SolidBrush(FillColor);

        Point[] points = { new Point(X, Y), new Point(X2, Y2), new Point(X3, Y3) };
        g.DrawPolygon(pen, points);
        g.FillPolygon(brush, points);
    }
}
