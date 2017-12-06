using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSA
{
    public partial class Form1 : Form
    {
        private Data data = new Data();
        private bool isDown = false;
        private bool isFirst = true;
        private Point p1 = Point.Empty;
        private int n = 0;
        private bool step1 = true;
        private List<string> inputList = new List<string>();
        private int tn = 0; //ノード名の番号
        private const int D = 17; // アークが重なった場合にあける隙間の幅

        private float bairitu = 1;

        public Form1()
        {
            InitializeComponent();
        }

        public void setData(Data data)
        {
            this.data = data;
            //pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(bairitu, bairitu);
            data.draw(e);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            Point loc = new Point((int)(e.X / bairitu), (int)(e.Y / bairitu));
            if (trans.Checked) // 遷移の登録処理
            {
                if (isFirst)
                {
                    if (data.findNode(loc))
                    {
                        data.currA = new Arc(data.currN);
                        data.currN = null;
                        data.arcs.Add(data.currA);
                        isFirst = false;
                    }
                    else if (data.findArc(loc))
                    {
                        nameBox.Text = data.currA.name;
                    }
                }
                else if (data.currA != null)
                {
                    if (data.findNode(loc))
                    {
                        data.currA.n2 = data.currN;
                        data.currN = null;
                        if (data.currA.n1 == data.currA.n2 && data.currA.midPs.Count == 0)
                        {
                            data.arcs.Remove(data.currA);
                            data.currA = null;
                        }
                        else
                        {
                            nameBox.Text = "";
                            nameBox.Focus();
                        }
                        isFirst = true;
                    }
                    else
                        data.currA.midPs.Add(loc);
                }
                else
                    isFirst = true; // isFrstでないのにcurrAがnullのとき：何らかのエラー
            }
            else  // 状態の処理、または移動の処理
            {
                p1 = loc;
                if (!data.findNode(loc) && !data.findArc(loc))
                {
                    data.currN = new Node(loc);
                    data.currN.isInit = init.Checked;
                    data.currN.isFinal = final.Checked;
                    data.nodes.Add(data.currN);
                }
                if (data.currN != null)
                {
                    nameBox.Text = data.currN.name;
                    init.Checked = data.currN.isInit;
                    final.Checked = data.currN.isFinal;
                    data.currA = null;
                }
                else if (data.currA != null)
                    nameBox.Text = data.currA.name;
                nameBox.Focus();
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point loc = new Point((int)(e.X / bairitu), (int)(e.Y / bairitu));
            if (trans.Checked && !isFirst && data.currA != null) // 遷移登録中の処理
            {                
                data.currA.tp = loc;
                pictureBox1.Invalidate();
            }
            else // 移動の処理
            {
                if (isDown)
                {
                    if (data.currN != null) // ノードの移動
                    {
                        Size ds = new Size(loc.X - p1.X, loc.Y - p1.Y);
                        Size hds = new Size((loc.X - p1.X)/2, (loc.Y - p1.Y)/2);
                        data.currN.move(ds);
                        foreach (Arc arc in data.arcs)
                        {
                            if (arc.n1 == data.currN && arc.n2 == data.currN)
                                arc.moveAllMidPs(ds);
                            else if (arc.n1 == data.currN || arc.n2 == data.currN)
                                arc.moveAllMidPs(hds);
                        }
                        p1 = loc;
                        pictureBox1.Invalidate();
                    }
                    else if (data.currA != null)
                    {
                        data.currA.moveMidP(loc);
                        pictureBox1.Invalidate();
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            int B = 35;//Box size
            for (int i = 0; i < inputList.Count; i++)
            {
                e.Graphics.DrawRectangle(Pens.Black, B + B * i, 15, B, B);
                e.Graphics.DrawString(inputList[i], new Font("MS Pゴシック", 17), Brushes.Black, B + B * i, 15);
            }
            e.Graphics.FillRectangle(Brushes.Yellow, B + B * inputList.Count, 15, B, B);
            e.Graphics.DrawRectangle(Pens.Black, B + B * inputList.Count, 15, B, B);

            Point[] ps = { new Point(3*B/2+B*n, 15+B), new Point(B/2+B*n,15+2*B), new Point(B/2+B*n, 15+3*B),
                           new Point(5*B/2+B*n, 15+3*B), new Point(5*B/2+B*n, 15+2*B) };
            e.Graphics.DrawPolygon(Pens.Black, ps);
            string st = "";
            bool isFst = true;
            foreach(Node nd in data.currNs)
            {
                st += (isFst? "" : ",") + nd.name;
                isFst = false;
            }
            e.Graphics.DrawString(st, new Font("MS Pゴシック", 17), Brushes.Black, B / 2 + B * n, 15 + 2 * B);
        }

        private void clearTempArc()
        {
            if (data.currA != null && data.currA.n2 == null)
            {
                data.arcs.Remove(data.currA);
                data.currA = null;
                isDown = false;
                isFirst = true;
                pictureBox1.Invalidate();
            }
        }

        private void textGen_Click(object sender, EventArgs e) //オートマトンのテキスト表現生成
        {
            clearTempArc();
            data.clearCurr();
            List<string> states = new List<string>();
            List<string> fStates = new List<string>();
            string iState = "";
            List<string> sigma = new List<string>();
            List<string[]> delta = new List<string[]>();
            foreach (Node nd in data.nodes)
            {
                if (states.Contains(nd.name))
                {
                    //dup name error
                }
                else
                {
                    states.Add(nd.name);
                    if (nd.isInit)
                        if (iState != "")
                        {
                            // too many init error
                        }
                        else
                            iState = nd.name;
                    if (nd.isFinal)
                        fStates.Add(nd.name);
                }
            }
            foreach (Arc arc in data.arcs)
            {
                if (arc.name == "")
                {
                    string[] dt = { arc.n1.name, "ε", arc.n2.name };
                    delta.Add(dt);
                }
                else
                    foreach (string nm in arc.name.Split(','))
                    {
                        string sig = nm.Trim();
                        if (!sigma.Contains(sig)) sigma.Add(sig);
                        string[] dt = { arc.n1.name, sig, arc.n2.name };
                        delta.Add(dt);
                    }
            }
            states.Sort();
            fStates.Sort();
            sigma.Sort();
            delta.Sort(delegate(string[] a, string[] b) 
                { return a[0] != b[0] ? string.Compare(a[0], b[0]) : 
                         a[1] != b[1] ? string.Compare(a[1], b[1]) : string.Compare(a[2],b[2]); });

            textFSA.Text = "Q={";
            bool isFst=true;
            foreach (string s in states)
            {
                textFSA.Text += (isFst ? "" : ",") + s;
                isFst = false;
            }
            textFSA.Text += "}\r\nΣ={";
            isFst = true;
            foreach (string s in sigma)
            {
                textFSA.Text += (isFst ? "" : ",") + s;
                isFst = false;
            }
            bool dfsa = true;
            for(int i=0; i<delta.Count-1; i++)
                if(delta[i][0]==delta[i+1][0] && delta[i][1]==delta[i+1][1] && delta[i][2]!=delta[i+1][2])
                {
                    dfsa = false;
                    break;
                }

            textFSA.Text += "}\r\nδ：QХΣ→" + (dfsa ? "Q" : "P(Q)") + "\r\n";
            if (dfsa) // 決定性オートマトン
            {
                foreach (string[] dt in delta)
                    textFSA.Text += "　　δ(" + dt[0] + "," + dt[1] + ")=" + dt[2] + "\r\n";
            }
            else    // 非決定性オートマトン
            {
                string[] old = { "", "" };
                foreach (string[] dt in delta)
                {
                    if (old[0] != dt[0] || old[1] != dt[1])
                    {
                        if (old[0]!="")
                            textFSA.Text += "}\r\n";
                        old[0] = dt[0]; old[1] = dt[1];
                        textFSA.Text += "　　δ(" + dt[0] + "," + dt[1] + ")={" + dt[2];
                    }
                    else
                        textFSA.Text += "," + dt[2];
                }
                textFSA.Text += "}\r\n";
            }
            textFSA.Text += "q0=" + iState + "\r\n";
            textFSA.Text += "F={";
            isFst = true;
            foreach (string s in fStates)
            {
                textFSA.Text += (isFst ? "" : ",") + s;
                isFst = false;
            }
            textFSA.Text += "}\r\n";
        }

        private void start_Click(object sender, EventArgs e) // 実行の開始準備
        {
            clearTempArc();
            n = 0;
            step1 = true;
            data.clearCurr();
            foreach (Node nd in data.nodes)
                if (nd.isInit && !data.currNs.Contains(nd)) data.currNs.Add(nd);
            addEpsTranStates(data.currNs);
            pictureBox1.Invalidate();

            inputList.Clear();
            for(int i=0; i<input.Text.Length; i++)
                inputList.Add( input.Text.Substring(i,1));
            pictureBox2.Invalidate();
        }

        private void addEpsTranStates(List<Node> states) // statesの状態からε遷移で到達できる状態をstatesに追加する。
        {
            int k;
            do
            {
                k = states.Count;
                foreach (Arc arc in data.arcs)
                    if (arc.name == "" && states.Contains(arc.n1) && !states.Contains(arc.n2))
                        states.Add(arc.n2);
            } while (k != states.Count);
        }

        private void step_Click(object sender, EventArgs e) // ステップ実行
        {
            clearTempArc();
            if (step1)
            {
                if (n >= input.Text.Length)
                {
                    foreach(Node nd in data.currNs)
                        if (nd.isFinal)
                        {
                            MessageBox.Show("受理しました");
                            data.clearCurr();
                            pictureBox1.Invalidate();
                            return;
                        }
                    MessageBox.Show("拒否しました");
                    data.clearCurr();
                    pictureBox1.Invalidate();
                    return;
                }
                string c = input.Text.Substring(n, 1);
                data.currAs.Clear();
                foreach (Arc arc in data.arcs)
                    if( data.currNs.Contains(arc.n1) )
                        foreach (string sig in arc.name.Split(','))
                            if (sig.Trim() == c)
                                data.currAs.Add(arc);
                if (data.currAs.Count == 0)
                {
                    MessageBox.Show("拒否しました");
                    data.clearCurr();
                    pictureBox1.Invalidate();
                    return;
                }
            }
            else
            {
                data.currNs.Clear();
                foreach (Arc arc in data.currAs)
                    if( !data.currNs.Contains(arc.n2))
                        data.currNs.Add(arc.n2);
                addEpsTranStates(data.currNs);
                data.currAs.Clear();
            }
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            step1 = !step1;
            if (step1) n++;
        }

        private void run_Click(object sender, EventArgs e) // 連続実行
        {
        }

        private void timer1_Tick(object sender, EventArgs e) // タイマー処理
        {
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e) // タイマー速度調整
        {
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            clearTempArc();
            if (data.currN != null)
            {
                List<Arc> delArcs = new List<Arc>();
                foreach (Arc arc in data.arcs)
                    if (arc.n1 == data.currN || arc.n2 == data.currN)
                        delArcs.Add(arc);
                foreach (Arc arc in delArcs)
                    data.arcs.Remove(arc);
                data.nodes.Remove(data.currN);
                data.currN = null;
            }
            else if (data.currA != null)
            {
                data.arcs.Remove(data.currA);
                data.currA = null;
            }
            pictureBox1.Invalidate();
        }

        private void addMidp_Click(object sender, EventArgs e)
        {
            clearTempArc();
            if (data.currA != null)
                data.currA.addMidP();
            pictureBox1.Invalidate();
        }

        private void delMidp_Click(object sender, EventArgs e)
        {
            clearTempArc();
            if (data.currA != null)
                if(data.currA.midPs.Count>1 || data.currA.n1 != data.currA.n2)
                    data.currA.delMidP();
            pictureBox1.Invalidate();
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (data.currN != null)
                data.currN.name = nameBox.Text;
            else if (data.currA != null)
                data.currA.name = nameBox.Text;
            pictureBox1.Invalidate();
        }

        private void init_CheckedChanged(object sender, EventArgs e)
        {
            clearTempArc();
            if (data.currN != null)
            {
                data.currN.isInit = init.Checked;
                pictureBox1.Invalidate();
            }
        }

        private void final_CheckedChanged(object sender, EventArgs e)
        {
            clearTempArc();
            if (data.currN != null)
            {
                data.currN.isFinal = final.Checked;
                pictureBox1.Invalidate();
            }
        }

        private static Type[] types = { typeof(Node), typeof(Arc) };
        private System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(Data), types);

        private void load_Click(object sender, EventArgs e)
        {
            clearTempArc();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream fs = new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                data = (Data)serializer.Deserialize(fs);
                fs.Close();
                foreach (Arc arc in data.arcs)
                {
                    data.findNode(arc.n1.p);
                    arc.n1 = data.currN;
                    data.findNode(arc.n2.p);
                    arc.n2 = data.currN;
                }
                data.clearCurr();
                pictureBox1.Invalidate();
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            clearTempArc();
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream fs = new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.OpenOrCreate);
                serializer.Serialize(fs, data);
                fs.Close();
            }
        }

        private void trans_CheckedChanged(object sender, EventArgs e)
        {
            clearTempArc();
            data.clearCurr();
            pictureBox1.Invalidate();
        }

        private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e) //終了
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearTempArc();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Text = "計算機数学2017前期：有限オートマトン  : " + openFileDialog1.FileName;
                System.IO.FileStream fs = new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
                data = (Data)serializer.Deserialize(fs);
                fs.Close();
                foreach (Arc arc in data.arcs)
                {
                    data.findNode(arc.n1.p);
                    arc.n1 = data.currN;
                    data.findNode(arc.n2.p);
                    arc.n2 = data.currN;
                }
                data.currA = null;
                data.currN = null;
                pictureBox1.Invalidate();
            }
        }

        private void 保存するToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearTempArc();
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream fs = new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create);
                data.clearCurr();
                serializer.Serialize(fs, data);
                fs.Close();
            }
        }

        private void nfadfaToolStripMenuItem_Click(object sender, EventArgs e) // nfa→dfa変換
        {
            clearTempArc();
            List<List<Node>> S = new List<List<Node>>();
            List<Delta> deltas = new List<Delta>();

            List<Node> s = new List<Node>();
            foreach (Node nd in data.nodes)
                if (nd.isInit)
                    s.Add(nd);
            addEpsTranStates(s);
            S.Add(s);
            for (int k = 0; k < S.Count; k++)
            {
                List<Node> s1 = S[k];
                List<Tran1> transet = new List<Tran1>();
                foreach(Arc arc in data.arcs)
                    if(arc.name!="")
                        foreach (string sig in arc.name.Split(','))
                        {
                            string name = sig.Trim();
                            if (s1.Contains(arc.n1))
                            {
                                Tran1 tran = null;
                                foreach (Tran1 tt in transet)
                                    if (tt.sig == name)
                                        tran = tt;
                                if (tran == null)
                                {
                                    tran = new Tran1(name);
                                    tran.addNode(arc.n2);
                                    transet.Add(tran);
                                }
                                else
                                    tran.addNode(arc.n2);
                            }
                        }

                foreach (Tran1 tt in transet)
                    addEpsTranStates(tt.s2);

                foreach(Tran1 tt in transet) {
                    List<Node> sx = null;
                    foreach( List<Node> ss in S)
                        if( SetEq(tt.s2, ss) )
                            sx = ss;
                    if( sx==null )
                    {
                        S.Add(tt.s2);
                        sx = tt.s2;
                    }
                    deltas.Add( new Delta( s1, tt.sig, sx ) );
                }
            }
            foreach (List<Node> st in S)
                st.Sort(delegate(Node n1, Node n2) { return string.Compare(n1.name, n2.name); });
            //S.Sort(delegate(List<Node> ss1, List<Node> ss2) { return string.Compare(getSts(ss1), getSts(ss2)); });

            deltas.Sort(delegate(Delta d1, Delta d2)
            {
                return d1.s1 != d2.s1 ? string.Compare(getSts(d1.s1), getSts(d2.s1)) :
                       d1.s2 != d2.s2 ? string.Compare(getSts(d1.s2), getSts(d2.s2)) :
                                        string.Compare(d1.sig, d2.sig);
            });

            data = getData(S, deltas, S[0]);
            pictureBox1.Invalidate();
        }

        private void prinNds(List<Node> s)
        {
            foreach (Node n in s) textFSA.Text += n.name + ",";
            textFSA.Text += "\r\n";
        }

        private string getSts(List<Node> nodes)
        {
            string st = "";
            bool isFst = true;
            foreach (Node nd in nodes)
            {
                st += isFst ? nd.name : nd.name.Length>1? nd.name.Substring(1,nd.name.Length-1) : nd.name;
                isFst = false;
            }
            return st;
        }

        private bool SetEq(List<Node> s1, List<Node> s2)
        {
            foreach(Node nd in s1)
                if( !s2.Contains(nd) ) return false;
            foreach(Node nd in s2)
                if( !s1.Contains(nd) ) return false;
            return true;
        }

        private string getSigmas()
        {
            string s = "｛";
            List<string> sigmas = new List<string>();
            foreach (Arc arc in data.arcs)
                if (arc.name != "")
                    foreach (string sig in arc.name.Split(','))
                        if (!sigmas.Contains(sig.Trim()))
                            sigmas.Add(sig.Trim());
            sigmas.Sort();
            bool isF = true;
            foreach (string sig in sigmas)
            {
                s += (isF ? "" : "，") + sig;
                isF = false;
            }
            s += "｝";
            return s;
        }

        private Data getData(List<List<Node>> S, List<Delta> D, List<Node> initN)
        {
            Data data = new Data();
            int m = (int)Math.Sqrt(S.Count);
            m = m*m==S.Count ? m : m+1;

            for(int i=0; i<S.Count; i++)
            {
                int y = 80 + (i / m) * 120;
                int x = (i / m) % 2 == 0 ? (80 + (i % m) * 120) : (80 + (m - 1 - i % m) * 120);
                Node nd = new Node(new Point(x, y));
                nd.name = getSts(S[i]);
                if (S[i] == initN)
                    nd.isInit = true;
                foreach(Node nn in S[i])
                    if(nn.isFinal) nd.isFinal = true;
                data.nodes.Add(nd);
            }

            List<Node>[] n12 = { null, null };
            string sig = "";
            foreach (Delta delta in D)
            {
                if (delta.s1 != n12[0] || delta.s2 != n12[1])
                {
                    if (sig != "")
                       data.arcs.Add( genArc(data.nodes[S.IndexOf(n12[0])], data.nodes[S.IndexOf(n12[1])], sig) );
                    sig = delta.sig;
                    n12[0] = delta.s1; n12[1] = delta.s2;
                }
                else
                    sig += ", " + delta.sig;
            }
            if (sig != "")
                data.arcs.Add(genArc(data.nodes[S.IndexOf(n12[0])], data.nodes[S.IndexOf(n12[1])], sig));
            addMidPs(data.arcs); // 必要な中間点の挿入
            return data;
        }

        private void genGrData(string[] st, string initSt, string[] finalSt, List<List<string>> trans)
        {
            data = new Data();

            foreach (string stateName in st)
            {
                if (stateName == "") return; //名前の無いノードがある場合は生成しない
            }

            int m = (int)Math.Sqrt(st.Length);
            if (m <= 0) return;
            m = m * m == st.Length ? m : m + 1;

            for (int i = 0; i < st.Length; i++)
            {
                int y = 80 + (i / m) * 120;
                int x = (i/m) % 2 == 0 ? (80 + (i % m) * 120) : (80 + (m -1- i% m) * 120);
                Node nd = new Node(new Point(x, y));
                nd.name = st[i];
                if (st[i] == initSt)
                    nd.isInit = true;
                if ( finalSt.Contains(st[i]) ) 
                    nd.isFinal = true;
                data.nodes.Add(nd);
            }

            foreach (string[] tr in stdTrans(trans))
                data.arcs.Add(genArc(data.findNode(tr[0]), data.findNode(tr[2]), tr[1]));
            addMidPs(data.arcs);
        }

        private List<string[]> stdTrans( List<List<string>> trans )
        {
            List<string[]> std1 = new List<string[]>();
            foreach(List<string> tr in trans)
                for(int i = 2; i<tr.Count; i++)
                {
                    string[] trp  = { tr[0], (tr[1]=="ε"? "" : tr[1]), tr[i] };
                    std1.Add(trp);
                }
            std1.Sort(delegate(string[] a1, string[] a2)
            {
                return a1[0] != a2[0] ? string.Compare(a1[0], a2[0]) :
                    a1[2] != a2[2] ? string.Compare(a1[2], a2[2]) : string.Compare(a1[1], a2[1]);
            });
            List<string[]> std2 = new List<string[]>();
            string[] old = { "", "-", "" };
            foreach (string[] tr in std1)
                if (old[1]=="" || old[0] != tr[0] || old[2] != tr[2])
                {
                    if (old[0] != "") std2.Add(old);
                    old = tr;
                }
                else
                    old[1] += "," + tr[1];
            if (old[0] != "") std2.Add(old);
            return std2;
        }

        private Arc genArc(Node n1, Node n2, string sig)
        {
            Arc arc = new Arc();
            arc.n1 = n1;
            arc.n2 = n2;
            arc.name = sig;
            if (arc.n1 == arc.n2)
            {
                int R = Node.R;
                arc.midPs.Add(arc.n1.p + new Size(2*R, 2*R));
            }
            return arc;
        }

        private void 簡易化ToolStripMenuItem_Click(object sender, EventArgs e) // dfa簡易化
        {
            List<List<Node>> S1 = new List<List<Node>>();
            List<Node> snf = new List<Node>();
            List<Node> sf = new List<Node>();

            foreach (Node nd in data.nodes)
                if (nd.isFinal)
                    sf.Add(nd);
                else
                    snf.Add(nd);
            if(snf.Count>0)
                S1.Add(snf);
            if(sf.Count>0)
                S1.Add(sf);

            textFSA.Text = "";

            while (true)
            {
                textFSA.Text += getStrList(S1) + "\r\n";
                List<List<Node>> S2 = getDiv(S1);
                if (S1.Count == S2.Count)
                    break;
                S1 = S2;
            }
            foreach (List<Node> ss in S1)
                ss.Sort(delegate(Node n1, Node n2) { return string.Compare(n1.name, n2.name); });
            S1.Sort(delegate(List<Node> ss1, List<Node> ss2) { return string.Compare(getSts(ss1), getSts(ss2)); });

            List<Delta> deltas = new List<Delta>();
            foreach (Arc arc in data.arcs)
            {
                int k1 = getIndex(S1, arc.n1);
                int k2 = getIndex(S1, arc.n2);
                foreach (string sig in arc.name.Split(','))
                {
                    Delta delta = new Delta(S1[k1], sig.Trim(), S1[k2]);
                    bool fnd = false;
                    foreach (Delta dl in deltas)
                        if (dl.s1 == delta.s1 && dl.sig == delta.sig && dl.s2 == delta.s2)
                        {
                            fnd = true;
                            break;
                        }
                    if (!fnd)
                        deltas.Add(delta);
                }
            }
            deltas.Sort(delegate(Delta d1, Delta d2)
            {
                return d1.s1 != d2.s1 ? string.Compare(getSts(d1.s1), getSts(d2.s1)) :
                       d1.s2 != d2.s2 ? string.Compare(getSts(d1.s2), getSts(d2.s2)) : 
                                        string.Compare(d1.sig, d2.sig);
            });

            List<Node> initN = null;
            foreach (List<Node> ss in S1)
                foreach (Node nd in ss)
                    if (nd.isInit)
                        initN = ss;
            S1.Remove(initN);
            S1.Insert(0, initN);

            data = getData(S1, deltas, initN);
            pictureBox1.Invalidate();
        }

        private string getStrList(List<List<Node>> S)
        {
            string st = "｛";
            bool isF = true;
            foreach (List<Node> nds in S)
            {
                st += (isF ? "" : "，") + getStr(nds);
                isF = false;
            }
            st += "｝";
            return st;
        }

        private string getStr(List<Node> nds)
        {
            string st2 = "｛";
            bool isF2 = true;
            foreach (Node nd in nds)
            {
                st2 += (isF2 ? "" : "，") + nd.name;
                isF2 = false;
            }
            st2 += "｝";
            return st2;
        }

        private List<List<Node>> getDiv(List<List<Node>> S1)
        {
            List<List<Node>> S2 = new List<List<Node>>();
            foreach (List<Node> nds in S1)
            {
                if (nds.Count > 1)
                {
                    textFSA.Text += "    " + getStr(nds) + "の分割\r\n";
                    List<Tran2> tran2s = new List<Tran2>();
                    foreach (Node nd in nds)
                    {
                        Tran2 tran2 = new Tran2(nd);
                        foreach (Arc arc in data.arcs)
                            if (nd == arc.n1)
                            {
                                int index = getIndex(S1, arc.n2);
                                foreach (string sig in arc.name.Split(','))
                                    tran2.addTrs(sig.Trim(), index);
                            }
                        tran2.trs.Sort();
                        tran2s.Add(tran2);
                    }
                    tran2s.Sort(delegate(Tran2 t1, Tran2 t2) { return string.Compare(t1.getTrs(), t2.getTrs()); });
                    List<Node> ss = new List<Node>();
                    string old = "---";
                    foreach (Tran2 tran2 in tran2s)
                    {
                        textFSA.Text += "        " + tran2.nd.name + "：" + tran2.getTrs() + "\r\n";
                        if (old != tran2.getTrs())
                        {
                            old = tran2.getTrs();
                            ss = new List<Node>();
                            ss.Add(tran2.nd);
                            S2.Add(ss);
                        }
                        else
                            S2[S2.Count - 1].Add(tran2.nd);
                    }
                }
                else
                    S2.Add(nds);
                    
            }
            return S2;
        }

        private int getIndex(List<List<Node>> S, Node nd)
        {
            for (int i = 0; i < S.Count; i++)
                if (S[i].Contains(nd))
                    return i;
            return -1;
        }

        private void クリアToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "計算機数学2016前期：有限オートマトン";
            data = new Data();
            input.Text = "";
            nameBox.Text = "";
            textFSA.Text = "";
            inputList.Clear();
            isDown = false;
            isFirst = true;
            p1 = Point.Empty;
            step1 = true;
            tn = 0; //ノード名の番号
            n = 0;
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
        }

        private void addMidPs(List<Arc> arcs)
        {
            foreach (Arc arc1 in arcs)
                if(arc1.midPs.Count==0)
                    foreach(Arc arc2 in arcs)
                        if (arc1.n1 == arc2.n2 && arc1.n2 == arc2.n1)
                        {
                            Point p1 = arc1.n1.p, p2 = arc1.n2.p;
                            Point mid = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                            int d = Node.dist(p1, p2);
                            int dx = p2.X - p1.X, dy = p2.Y - p1.Y;
                            Size size = new Size(D * dy / d, D * dx / d);
                            arc1.midPs.Add(mid + size);
                            arc2.midPs.Add(mid - size);
                            break;
                        }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (trans.Checked) return;
            if (!cbREmode.Checked) return;
            Point loc = new Point((int)(e.X / bairitu), (int)(e.Y / bairitu));
            if (data.findNode(loc))
            {
                Node nd = data.currN;
                data.currN = null;
                string loop = "";
                bool isF = true;
                foreach (Arc arc in data.arcs)
                    if (arc.n1 == arc.n2 && arc.n1 == nd)
                        loop += (isF ? "" : "+") + arc.name.Replace(',', '+');
                loop = loop.Length==0? "" :
                    loop.Length==1? loop+"*" :
                    loop.Length==2 && loop.Substring(1,1)=="*" ? loop :
                    /* else */ "("+loop+")*" ;

                List<Arc> arcs = new List<Arc>();
                foreach (Arc arc1 in data.arcs)
                    if (arc1.n1 != arc1.n2 && arc1.n2 == nd)
                        foreach (Arc arc2 in data.arcs)
                            if (arc2.n1 != arc2.n2 && arc2.n1 == nd)
                            {
                                arcs.Add(genArc(arc1.n1, arc2.n2,
                                    makeKakko(arc1.name.Replace(',', '+')) + loop
                                    + makeKakko(arc2.name.Replace(',', '+'))));
                            }
                List<Arc> delArcs = new List<Arc>();
                foreach (Arc arc in data.arcs)
                    if (arc.n1 == nd || arc.n2 == nd)
                        delArcs.Add(arc);
                foreach (Arc arc in delArcs)
                    data.arcs.Remove(arc);
                data.nodes.Remove(nd);

                foreach (Arc arc in arcs)
                    data.arcs.Add(arc);
                mkREarc(data.arcs);
                pictureBox1.Invalidate();
            }
            else if(data.findArc(loc))
            {
                Arc arc = data.currA;
                if (arc.name == "") return;

                string re = getREtop(arc.name);
                textFSA.Text = re;

                char op = re[0];
                string[] elems = re.Substring(1, re.Length - 1).Split(',');
                if (op == '+')
                {
                    List<string> elms = chgElems(elems); // １文字はまとめて、a,b,..に変換する
                    List<Arc> arcs = new List<Arc>();
                    foreach (string elm in elms)
                        arcs.Add(genArc(arc.n1, arc.n2, (elm == "ε" ? "" : elm)));
                    midPosChange(arcs);
                    foreach (Arc ac in arcs)
                        data.arcs.Add(ac);
                    data.arcs.Remove(arc);
                }
                else if (op == '.')
                {
                    List<Node> nds = genNodes(arc.n1, arc.n2, elems.Length - 1);
                    for (int i = 1; i < nds.Count - 1; i++)
                        data.nodes.Add(nds[i]);
                    for (int i = 0; i < elems.Length; i++)
                        data.arcs.Add(genArc(nds[i], nds[i + 1], (elems[i] == "ε" ? "" : elems[i])));
                    data.arcs.Remove(arc);
                    addMidPs(data.arcs);
                }
                else if (op == '*')
                {
                    if (elems[0] == "ε")
                        arc.name = "";
                    else
                    {
                        List<Node> nds = genNodes(arc.n1, arc.n2, 1);
                        data.nodes.Add(nds[1]);
                        data.arcs.Add(genArc(nds[0], nds[1], ""));
                        data.arcs.Add(genArc(nds[1], nds[1], elems[0]));
                        data.arcs.Add(genArc(nds[1], nds[2], ""));
                        data.arcs.Remove(arc);
                        addMidPs(data.arcs);
                    }
                }
                else if (op == 'P' && elems[0] == "ε")
                    arc.name = "";
                pictureBox1.Invalidate();
            }
        }


        private List<Node> genNodes(Node n1, Node n2, int num) // 結果は{n1, t1,..,tnum, n2} 
        {
            List<Node> nds = new List<Node>();
            nds.Add(n1);
            Point[] ps = getPoints(n1.p, n2.p, num, n1.getR());
            for (int i = 0; i < num; i++)
            {
                Node nd = new Node(ps[i + 1]);
                nd.name = "t" + (tn++);
                nds.Add(nd);
            }
            nds.Add(n2);
            return nds;
        }

        private Point[] getPoints(Point p1, Point p2, int n, int R)
        {
            Point[] ps = new Point[n+2];
            ps[0]=p1; ps[n+1]=p2;
            if (p1.X != p2.X || p1.Y != p2.Y) // p1とp2の間に等分してn個のPointを作成
            {
                Size sz = new Size((p2.X - p1.X) / (n + 1), (p2.Y - p1.Y) / (n + 1));
                for (int i = 0; i < n; i++)
                    ps[i + 1] = ps[i] + sz;
            }
            else // p1の下(1+n)Rの点を中心とした円にn個のPointを割り振る
            {
                Point o = new Point(p1.X, p1.Y + (n+1)*R);
                double rad = -2 * Math.PI / (n+1);
                for (int i = 0; i < n; i++)
                    ps[i + 1] = round(o, ps[i], rad);
            }
            return ps;
        }

        private Point round(Point o, Point p, double rad)
        {
            double dr = getRad( new Point(p.X - o.X, p.Y - o.Y) );
            int d = Node.dist( o, p );
            Size s = new Size((int)(d * Math.Cos(dr + rad)), (int)(d * Math.Sin(dr + rad)));
            return o + s;
        }

        private double getRad(Point p) // 点pの角度を得る
        {
            if (p.X == 0)
                return p.Y > 0 ? Math.PI / 2 : 3 * Math.PI / 2;
            double r = Math.Atan((double)Math.Abs(p.Y) / (double)Math.Abs(p.X));
            return
                p.X > 0 ? (p.Y > 0 ? r : (2 * Math.PI - r)) : (p.Y > 0 ? (Math.PI - r) : (Math.PI + r));
        }

        private void midPosChange(List<Arc> arcs) // 全アークともn1,n2は同じとする
        {
            if (arcs[0].n1 == arcs[0].n2) // ループ
            {
                double rd = Math.PI * 50 / 180;
                for (int i = 1; i < arcs.Count; i++)
                    rotMidp(arcs[i], rd * i);
            }
            else // 平行
            {
                Point p1 = arcs[0].n1.p, p2 = arcs[0].n2.p;
                Point mid = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                int d = Node.dist(p1, p2);
                int dx = p2.X - p1.X, dy = p2.Y - p1.Y;
                Size size = new Size(2 * D * dy / d, 2 * D * dx / d);
                Point mid1 = new Point(mid.X + (D * dy / d) * (arcs.Count - 1), mid.Y + (D * dx / d) * (arcs.Count - 1));
                foreach (Arc arc in arcs)
                {
                    arc.midPs.Add(mid1);
                    mid1 -= size;
                }
            }
        }

        private void rotMidp(Arc arc, double rad)
        {
            Point o = arc.n1.p;
            for (int i = 0; i < arc.midPs.Count; i++)
                arc.midPs[i] = round(o, arc.midPs[i], rad);
        }

        private List<string> chgElems(string[] ss)
        {
            List<string> r = new List<string>();
            string uni = "";
            bool isF = true;
            foreach (string s in ss)
                if (s == "ε" || s.Length > 1)
                    r.Add(s);
                else
                {
                    uni += isF ? s : ("," + s);
                    isF = false;
                }
            if (uni != "")
                r.Add(uni);
            return r;
        }

        private void mkREarc(List<Arc> arcs)
        {
            List<Arc> delArcs = new List<Arc>();
            arcs.Sort(delegate(Arc a1, Arc a2)
            {
                return
                    a1.n1.name != a2.n1.name ? string.Compare(a1.n1.name, a2.n1.name) : string.Compare(a1.n2.name, a2.n2.name);
            });
            Node nd1 = null, nd2 = null;
            Arc ak1 = null;
            foreach (Arc arc in arcs)
                if (arc.n1 != nd1 || arc.n2 != nd2)
                {
                    ak1 = arc;
                    nd1 = ak1.n1;
                    nd2 = ak1.n2;
                }
                else
                {
                    ak1.name = ak1.name == "" ? "ε" : ak1.name;
                    ak1.name += "+" + ( arc.name == "" ?  "ε" : arc.name );
                    delArcs.Add(arc);
                }
            foreach (Arc arc in delArcs)
                arcs.Remove(arc);
        }

        private string makeKakko(string s)
        {
            int level = 0;
            foreach (char c in s)
            {
                level += c == '(' ? 1 : c == ')' ? -1 : 0;
                if (c == '+' && level == 0)
                    return "(" + s + ")";
            }
            return s;
        }

        private string delKakko(string s)
        {
            string ss = s.Trim();
            if (ss.Length <= 1) return ss;
            while (ss != "")
            {
                int level = 0;
                for (int i = 0; i < ss.Length; i++)
                {
                    level += ss[i] == '(' ? 1 : ss[i] == ')' ? -1 : 0;
                    if (level == 0 && i < ss.Length - 1)
                        return ss;
                }
                ss = ss.Length>1 ? ss.Substring(1, ss.Length - 2) : "";
            }
            return "";
        }

        private string getREtop(string s)
        {
            string re = "";
            string ss = delKakko(s);
            int kl = 0; 
            bool isFnd = false;
            foreach (char c in ss)
            {
                kl += c == '(' ? 1 : c == ')' ? -1 : 0;
                if (kl == 0 && c == '+')
                {
                    re += ',';
                    isFnd = true;
                }
                else
                    re += c;
            }
            if (isFnd)
                return '+' + re;
            kl = 0;
            re = "";
            for (int i=0; i<ss.Length; i++)
            {
                re += ss[i];
                kl += ss[i] == '(' ? 1 : ss[i] == ')' ? -1 : 0;
                if (kl == 0 && i!=ss.Length-1 && ss[i+1] != '*')
                {
                    re += ',';
                    isFnd = true;
                }
            }
            if (isFnd)
                return '.' + re;
            if (ss.Length == 0) return "";
            if (ss[ss.Length - 1] == '*')
            {
                string res = '*' + delKakko(ss.Substring(0, ss.Length - 1));
                return res;
            }

            return 'P' + ss;
        }

        private void ノード名変換ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int k = 1;
            foreach (Node nd in data.nodes)
                nd.name = "q" + (nd.isInit ? 0 : k++);
            pictureBox1.Invalidate();
        }

        private void graphGen_Click(object sender, EventArgs e)
        {
            clearTempArc();
            string s = remSpace(textFSA.Text);
            // get states
            int start = s.IndexOf("Q={") + 3;
            if (start < 3) return;
            int end = s.IndexOf("}", start);
            if (end < start) return;
            string[] states = s.Substring(start, end - start).Split(',');
            // get init
            start = s.IndexOf("q0=") + 3;
            if (start < 3) return;
            string initState = getWord(s, start);
            // get final
            start = s.IndexOf("F={") + 3;
            if (start < 3) return;
            end = s.IndexOf("}", start);
            if (end < start) return;
            string[] finalSt = s.Substring(start, end - start).Split(',');
            // get trans
            int n;
            end = 0;
            List<List<string>> trans = new List<List<string>>();
            while ( (n = s.IndexOf("δ(", end)) >= 0 )
            {
                start = n + 2;
                end = s.IndexOf(")=", start);
                List<string> tr = s.Substring(start, end - start).Split(',').ToList<string>();
                start = end + 2;
                if (s[start] == '{')
                {
                    end = s.IndexOf("}", start);
                    foreach (string str in s.Substring(start + 1, end - start - 1).Split(','))
                        tr.Add(str);
                }
                else
                    tr.Add( getWord(s, start) );
                trans.Add(tr);
            }
            genGrData(states, initState, finalSt, trans);

            pictureBox1.Invalidate();
            /***
            textFSA.Text += "State: ";
            foreach (string st in states)
                textFSA.Text += st + "  ";
            textFSA.Text += "\r\nq0: " + initState + "\r\nFinal: ";
            foreach (string fn in finalSt)
                textFSA.Text += fn + "  ";
            textFSA.Text += "\r\n";
            foreach (string[] tr in stdTrans(trans))
            {
                foreach (string str in tr)
                    textFSA.Text += str + "  ";
                textFSA.Text += "\r\n";
            }***/
        }

        private string getWord(string s, int start)
        {
            string ss = "";
            while (start<s.Length && char.IsLetterOrDigit(s[start]))
                ss += s[start++];
            return ss;
        }

        private string remSpace(string s) // sから空白、タブを除く
        {
            string ss = "";
            foreach (char c in s)
                if (c != ' ' && c != '\t')
                    ss += c;
            return ss;
        }

        private void cbREMode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void input_TextChanged(object sender, EventArgs e)
        {

        }

        private void 変換ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            bairitu = (float)(1.0 + (trackBar1.Value - 5) / 5.0);
            if (bairitu < 0.1) bairitu = (float)0.1;
            pictureBox1.Invalidate();
        }

        private void clearSize_Click(object sender, EventArgs e)
        {
            bairitu = 1;
            trackBar1.Value = 5;
            pictureBox1.Invalidate();
        }
    }
}

