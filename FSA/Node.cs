using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FSA
{
    public class Node
    {
        public Point p { get; set; }       // 中心点
        public string name { get; set; }   // 名前
        public bool isInit { get; set; }   // 初期状態か?
        public bool isFinal { get; set; }  // 受理状態か?

        public static int R = 25; 　　　　// 円の半径
        private int fontSize = 12;  // フォントサイズ

        public Node() { }

        public Node(Point p) 
        { 
            this.p = p;
            isInit = false;
            isFinal = false;
            name = "";
        }
        public void changeSize(int R, int fontSize)
        {
            Node.R = R; 
            this.fontSize = fontSize;
        }

        public int getR() { return R; }

        public void move(Size s) { p += s; }

        public bool isIn(Point p1) { return dist(p,p1)<=R; }

        public Point border(Point p1)
        {
            if (isIn(p1)) return Point.Empty;
            int dx = R * (p1.X - p.X) / dist(p, p1);
            int dy = R * (p1.Y - p.Y) / dist(p, p1);
            return new Point(p.X + dx, p.Y + dy);
        }

        public static int dist(Point p1, Point p2)
        {
            return (int)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        public void draw(Pen pen, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(pen, rect(0));
            if (isFinal) e.Graphics.DrawEllipse(pen, rect(6));
            if (isInit) drawArrow(pen, e);
            e.Graphics.DrawString(name, new Font("MS Pゴシック", fontSize), Brushes.Black, new Point(p.X - 10, p.Y - 10));
        }

        private void drawArrow(Pen pen, PaintEventArgs e) // 初期状態を示す２重線の矢印の描画
        {
            int Rdivr2 = (int)(R / 1.414213);
            int D = 4; // ２重線の幅をきめる
            e.Graphics.DrawLine(pen, p.X - Rdivr2, p.Y - Rdivr2, p.X - Rdivr2, p.Y - Rdivr2 - R / 3);
            e.Graphics.DrawLine(pen, p.X - Rdivr2, p.Y - Rdivr2, p.X - Rdivr2 - R / 3, p.Y - Rdivr2);
            e.Graphics.DrawLine(pen, p.X - Rdivr2, p.Y - Rdivr2 - D, p.X - Rdivr2 * 2, p.Y - Rdivr2 * 2 - D);
            e.Graphics.DrawLine(pen, p.X - Rdivr2 - D, p.Y - Rdivr2, p.X - Rdivr2 * 2 - D, p.Y - Rdivr2 * 2);
        }
        
        private Rectangle rect(int dip)
        {
            return new Rectangle(p.X - R + dip, p.Y - R + dip, (R-dip)*2, (R-dip)*2);
        }
    }
}
