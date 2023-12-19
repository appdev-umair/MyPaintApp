using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaintApp
{
    internal class Line : Shape
    {
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(OutLineColor, OutLineWidth);
            g.DrawLine(pen, X, Y, X2, Y2);
        }

    }
}
