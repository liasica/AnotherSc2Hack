using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sc2Hack.Classes.FontEnds
{
    class Custom_PictureBox : PictureBox
    {
        public Custom_PictureBox()
        {
            DrawingPoint = new PointF(0, 0);
            DrawingText = "";
            DrawingFont = Font;
            DrawingBrush = Brushes.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            pe.Graphics.DrawString(DrawingText, DrawingFont, DrawingBrush, DrawingPoint);
            pe.Graphics.DrawRectangle(new Pen(Brushes.Red, 2), 1, 1, Width - 2, Height - 2);
        }

        public PointF DrawingPoint { get; set; }
        public Font DrawingFont { get; set; }
        public Brush DrawingBrush { get; set; }
        public String DrawingText { get; set; }
    }
}
