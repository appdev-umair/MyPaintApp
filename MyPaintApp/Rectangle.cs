using MyPaintApp;
using System.Drawing;
using System;

internal class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }

    // Resizing handles
    public bool IsResizing { get; set; }
    public int ResizeHandleSize { get; set; } = 8; // Size of resizing handles

    public Rectangle()
    {
        X = 0;
        Y = 0;
        Width = 0;
        Height = 0;
        OutLineColor = Color.Black;
        OutLineWidth = 1;
        FillColor = Color.Transparent; // Initialize FillColor with a default value
        IsResizing = false;
    }

    public Rectangle(int x, int y, int width, int height, Color outlineColor, int outlineWidth, Color fillColor)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        OutLineColor = outlineColor;
        OutLineWidth = outlineWidth;
        FillColor = fillColor;

        IsResizing = false;
    }

    public override void Draw(Graphics g)
    {
        Pen pen = new Pen(OutLineColor, OutLineWidth);
        SolidBrush brush = new SolidBrush(FillColor);

        int x = Math.Min(X, X + Width);
        int y = Math.Min(Y, Y + Height);
        int width = Math.Abs(Width);
        int height = Math.Abs(Height);

        g.DrawRectangle(pen, x, y, width, height);
        g.FillRectangle(brush, x, y, width, height);

        // Draw resizing handles
        if (IsResizing)
        {
            DrawResizeHandle(g, x, y);                               // Top-left
            DrawResizeHandle(g, x + width - ResizeHandleSize, y);    // Top-right
            DrawResizeHandle(g, x + width - ResizeHandleSize, y + height - ResizeHandleSize);    // Bottom-right
            DrawResizeHandle(g, x, y + height - ResizeHandleSize);    // Bottom-left
        }
    }

    private void DrawResizeHandle(Graphics g, int x, int y)
    {
        g.FillRectangle(Brushes.Black, x, y, ResizeHandleSize, ResizeHandleSize);
    }
}
