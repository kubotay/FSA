using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSA
{
    public class Delta
    {
        public List<Node> s1;
        public string sig;
        public List<Node> s2;

        public Delta(List<Node> s1, string sig, List<Node> s2)
        {
            this.s1 = s1;
            this.sig = sig;
            this.s2 = s2;
        }
    }
}
