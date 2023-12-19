using MyPaintApp;
using System.Drawing;

internal class Ellipse : Shape
{
    public int HDiameter { get; set; }
    public int VDiameter { get; set; }

    public Color FillColor { get; set; }

    public Ellipse()
    {
        X = 0; Y = 0;
        HDiameter = 0; VDiameter = 0;
        OutLineColor = Color.Black;
        FillColor = Color.Black;
        OutLineWidth = 1;
        FillColor = Color.Transparent; // Initialize FillColor with a default value
    }

    public Ellipse(int x, int y, int hdia, int vdia, Color outlineColor, int outlineWidth, Color fillColor)
    {
        X = x;
        Y = y;
        HDiameter = hdia;
        VDiameter = vdia;
        OutLineColor = outlineColor;
        OutLineWidth = outlineWidth;
        FillColor = fillColor;
    }

    public override void Draw(Graphics g)
    {
        Pen pen = new Pen(OutLineColor, OutLineWidth);
        Brush brush = new SolidBrush(FillColor);

        g.DrawEllipse(pen, X, Y, HDiameter, VDiameter);
        g.FillEllipse(brush, X, Y, HDiameter, VDiameter);
    }
}
