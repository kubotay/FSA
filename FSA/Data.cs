using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FSA
{
    public class Data
    {
        public List<Node> nodes = new List<Node>();
        public List<Arc> arcs = new List<Arc>();
        public Node currN = null;
        public Arc currA = null;
        public List<Node> currNs = new List<Node>();
        public List<Arc> currAs = new List<Arc>(); 

        public Data()
        {
        }

        public void clearCurr()
        {
            currN = null;
            currA = null;
            currNs.Clear();
            currAs.Clear();
        }

        public bool findNode(Point p)
        {
            currN = null;
            foreach (Node nd in nodes)
                if (nd.isIn(p))
                {
                    currN = nd;
                    return true;
                }
            return false;
        }

        public Node findNode(string name)
        {
            foreach (Node nd in nodes)
                if (nd.name == name)
                    return nd;
            return null;
        }

        public bool findArc(Point p)
        {
            currA = null;
            foreach (Arc arc in arcs)
                if (arc.isOn(p))
                {
                    currA = arc;
                    return true;
                }
            return false;
        }

        public void draw(PaintEventArgs e)
        {
            foreach (Node nd in nodes)
                nd.draw(nd == currN || currNs.Contains(nd) ? Pens.Red : Pens.Black, e);
            foreach (Arc arc in arcs)
                arc.draw(arc == currA || currAs.Contains(arc) ? Pens.Red : Pens.Black, e);
        }
    }
}
