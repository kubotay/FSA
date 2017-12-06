using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FSA
{
    public class Arc
    {
        public Node n1 { get; set; }
        public Node n2 { get; set; }
        public List<Point> midPs = new List<Point>();
        public string name { get; set; }
        public Point tp = Point.Empty;

        private int findPos = -1; 
        private const int min = 8;

        public Arc() { }

        public Arc(Node n1)
        {
            this.n1 = n1;
            this.n2 = null;
            name = "";
        }

        public void setN2(Node n2)
        {
            this.n2 = n2;
        }

        public void moveMidP(Point p)
        {
            if (findPos > 0)
                midPs[findPos - 1] = p;
        }

        public void moveAllMidPs(Size size)
        {
            for (int i = 0; i < midPs.Count; i++ )
                midPs[i] += size;
        }

        public void addMidP() 
        {
            if(findPos==0)
                midPs.Add(new Point((n1.p.X+n2.p.X)/2, (n1.p.Y+n2.p.Y)/2));
            else if (findPos > 0)
            {
                Point p1 = midPs[findPos - 1];
                Point p2 = findPos < midPs.Count ? midPs[findPos] : n2.border(p1);
                midPs.Insert(findPos, new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));
                //midPs.Add(new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));
            }
            //findPos = -1;
            findPos++;
        }

        public void delMidP()
        {
            if (findPos > 0)
                midPs.RemoveAt(findPos - 1);
            findPos--;
        }

        public bool isOn(Point p1)
        {
            findPos = -1;
            int i=1;
            foreach (Point midP in midPs)
            {
                if (Node.dist(p1, midP) < min) 
                {
                    findPos = i;
                    return true;
                }
                i++;
            }
            if (i>1 || n2 == null) return false; // if minPs is not empty or n2==null, p is not on the line
            int x0 = p1.X, y0 = p1.Y, x1 = n1.p.X, y1 = n1.p.Y, x2 = n2.p.X, y2 = n2.p.Y;
            if( Math.Min(x1,x2)-5<x0 && Math.Max(x1,x2)+5>x0 && Math.Min(y1,y2)-5<y0 && Math.Max(y1,y2)+5>y0 )
            {
                int d=Math.Abs((x0*y1+x1*y2+x2*y0)-(x0*y2+x1*y0+x2*y1))/Node.dist(n1.p, n2.p);
                if(d<=min) 
                {
                    findPos = 0;
                    return true;
                }
            }
            return false;
        }

        public void draw(Pen pen, PaintEventArgs e)
        {
            Point[] ps = new Point[midPs.Count + 2];
            Brush br = pen.Color == Color.Red ? Brushes.Red : Brushes.Black;
            if (n2 == null)
            {
                if (tp == Point.Empty || (midPs.Count==0 && n1.isIn(tp))) return;
                for (int i = 0; i < midPs.Count; i++)
                    ps[i + 1] = midPs[i];
                ps[ps.Length - 1] = tp;
                ps[0] = n1.border(ps[1]);
                e.Graphics.DrawCurve(Pens.Red, ps);
            }
            else
            {
                if (n1 == n2 && midPs.Count == 1)
                {
                    drawArc(n1.p, midPs[0], pen, e);
                    return;
                }

                List<Point> mpsTmp = new List<Point>();
                foreach (Point p in midPs)
                    if (!n1.isIn(p) && !n2.isIn(p))
                        mpsTmp.Add(p);
                ps = new Point[mpsTmp.Count + 2];
                for (int i = 0; i < mpsTmp.Count; i++)
                    ps[i + 1] = mpsTmp[i];
                ps[ps.Length - 1] = n2.border( mpsTmp.Count > 0 ? ps[ps.Length - 2] : n1.p );
                ps[0] = n1.border(ps[1]);
                if (ps[0] == Point.Empty || ps[ps.Length - 1] == Point.Empty)
                    return;
                e.Graphics.DrawCurve(pen, ps);
                foreach(Point mp in midPs)
                    e.Graphics.FillEllipse(br, new Rectangle(mp.X-2, mp.Y-2,4,4));
                Point strpos = midPs.Count == 0 ? new Point((n1.p.X + n2.p.X) / 2, (n1.p.Y + n2.p.Y) / 2) : ps[ps.Length / 2];
                e.Graphics.DrawString(name, new Font("MS Pゴシック", 15), Brushes.Black, strpos);
            }

            e.Graphics.FillPolygon(br, getDelta(ps[ps.Length - 2], ps[ps.Length - 1]));
        }

        //特別な円弧のアークを描く
        // c:ノード中心、p:外の点
        private void drawArc(Point c, Point p, Pen pen, PaintEventArgs e)
        {
            int AR = 4 * Node.R / 6;

            int dr = Node.R + AR / 2;
            int r = dest(c, p);
            int x = p.X - c.X;
            int y = p.Y - c.Y;
            int dx = x * dr / r;
            int dy = y * dr / r;
            int mx = x * (dr + AR) / r + c.X;
            int my = y * (dr + AR) / r + c.Y;
            midPs[0] = new Point(mx, my);
            Rectangle rec = new Rectangle(c.X + dx - AR, c.Y + dy - AR, AR*2, AR*2);
            double dth = Math.Atan2(y, x);
            float th = (float)(dth * 180 / Math.PI);
            e.Graphics.DrawArc(pen, rec, 230+th, 260);

            Brush br = pen.Color == Color.Red ? Brushes.Red : Brushes.Black;
            e.Graphics.FillEllipse(br, new Rectangle(mx-2, my-2,4,4));

            double cos = Math.Cos(dth + Math.PI * 0.17);
            double sin = Math.Sin(dth + Math.PI * 0.17);
            double cos1 = Math.Cos(dth + Math.PI * 0.13);
            double sin1 = Math.Sin(dth + Math.PI * 0.13);
            Point p1 = new Point(c.X + (int)(cos1 * 2 * Node.R), c.Y + (int)(sin1 * 2 * Node.R));
            Point p2 = new Point(c.X + (int)(cos * Node.R), c.Y + (int)(sin * Node.R));
            e.Graphics.FillPolygon(br, getDelta(p1, p2));
            if( name != null && name != "" )
                e.Graphics.DrawString(name, new Font("MS Pゴシック", 15), Brushes.Black, midPs[0]);

        }

        private int dest(Point p1, Point p2)
        {
            return (int)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static Point[] getDelta(Point p1, Point p2) // p1からp2への矢印のp2上の三角（Point[3]）を返す
        {
            Point[] ps = new Point[3];
            int DD = 14; // 三角の大きさ
            int dx = p1.X - p2.X, dy = p1.Y - p2.Y;
            double gamma =
                dx == 0 ? (dy > 0 ? Math.PI / 2 : Math.PI * 1.5) :
                dy == 0 ? (dx >= 0 ? 0 : Math.PI) :
                dx > 0 ? Math.Atan(dy / (double)dx) : Math.Atan(dy / (double)dx) + Math.PI;

            ps[0] = p2;
            ps[1] = new Point(p2.X + (int)(DD * Math.Cos(gamma + Math.PI / 8)), p2.Y + (int)(DD * Math.Sin(gamma + Math.PI / 8)));
            ps[2] = new Point(p2.X + (int)(DD * Math.Cos(gamma - Math.PI / 8)), p2.Y + (int)(DD * Math.Sin(gamma - Math.PI / 8)));
            return ps;
        }
    }
}
