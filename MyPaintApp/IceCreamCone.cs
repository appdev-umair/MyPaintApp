// WaterDrop.cs File

using System;
using System.Drawing;

namespace MyPaintApp
{
    internal class IceCreamCone : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public IceCreamCone()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            OutLineColor = Color.Black;
            OutLineWidth = 1;
        }

        public IceCreamCone(int x, int y, int width, int height, Color color, int outlineWidth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            OutLineColor = color;
            OutLineWidth = outlineWidth;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(OutLineColor, OutLineWidth);

            Point[] points =
            {
                        // Top point
                new Point(X + Width, Y + Height / 2),                   // Right side
                new Point(X + Width / 2, Y + Height),                   // Bottom point
                new Point(X, Y + Height / 2)                           // Left side
            };

            g.DrawArc(pen, X, Y, Width, Height, 0, -180);

            g.DrawPolygon(pen, points);
        }
    }
}
