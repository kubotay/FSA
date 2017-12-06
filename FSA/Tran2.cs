using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSA
{
    public class Tran2
    {
        public Node nd;
        public List<string> trs;

        public Tran2(Node nd)
        {
            this.nd = nd;
            trs = new List<string>();
        }

        public void addTrs(string name, int n)
        {
            string tr = name + "→" + n;
            if (!trs.Contains(tr)) trs.Add(tr);
        }

        public string getTrs()
        {
            string ss = "";
            foreach (string tr in trs)
                ss += tr + "．";
            return ss;
        }
    }
}
