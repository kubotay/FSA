using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSA
{
    public class Tran1
    {
        public string sig;
        public List<Node> s2 = new List<Node>();

        public Tran1(string sig)
        {
            this.sig = sig;
        }

        public void addNode(Node nd)
        {
            if (!s2.Contains(nd))
                s2.Add(nd);
        }
    }
}
