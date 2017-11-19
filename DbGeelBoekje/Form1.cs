using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbGeelBoekje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected Point TB_LastPos_EN = new Point(10, 0);
        protected Point TB_LastPos_NL = new Point(200, 0);

        public List<TextBox> TB_Left = new List<TextBox>();
        public List<TextBox> TB_Right = new List<TextBox>();
        public List<string> NL = new List<string>();
        public List<string> EN = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            DbInit();
            Add_Left_TB();
            Add_Right_TB();
            Populate(TB_Left, EN);
            Populate(TB_Right, NL);
        }

        private void Add_Left_TB()
        {
            for (int i = 0; i < EN.Count; i++)
            {
                TextBox tb = new TextBox();
                TB_LastPos_EN = new Point(TB_LastPos_EN.X, TB_LastPos_EN.Y + tb.Size.Height + 10);
                tb.Location = TB_LastPos_EN;
                tb.Visible = true;
                tb.Text = i.ToString();
                tb.Size = new Size(tb.Size.Width + 50, tb.Size.Height);
                tb.Name = "TB_Left_" + i.ToString();
                tb.ReadOnly = true;
                TB_Left.Add(tb);
                Panel_Full.Controls.Add(tb);
            }
        }
        private void Add_Right_TB()
        {
            for (int i = 0; i < NL.Count; i++)
            {
                TextBox tb = new TextBox();
                TB_LastPos_NL = new Point(TB_LastPos_NL.X, TB_LastPos_NL.Y + tb.Size.Height + 10);
                tb.Location = TB_LastPos_NL;
                tb.Visible = true;
                tb.Text = i.ToString();
                tb.TextChanged += Tb_TextChanged; //Never put this in front of tb.Text = i.ToString; it will produce errors we don't want
                tb.Size = new Size(tb.Size.Width + 50, tb.Size.Height);
                tb.Name = "TB_Right_" + i.ToString();
                TB_Right.Add(tb);
                Panel_Full.Controls.Add(tb);
            }
        }

        private void Tb_TextChanged(object sender, EventArgs e)
        {
            Check(TB_Left, TB_Right);
        }

        private void Populate(List<TextBox> LtB, List<string> Language)
        {
            for (int i = 0; i < LtB.Count; i++)
            {
                LtB[i].Text = Language[i];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in TB_Left)
            {
                item.Text = item.Name;
            }
            DbInit();
        }
        private void DbInit()
        {
            EngelsGeelDataSetTableAdapters.AtmETableAdapter atmETable = new EngelsGeelDataSetTableAdapters.AtmETableAdapter();
            EngelsGeelDataSet.AtmEDataTable atmEData = atmETable.GetData();
            int count = 0;
            foreach (var item in atmEData)
            {
                NL.Add(item["NL"].ToString());
                EN.Add(item["EN"].ToString());
                //MessageBox.Show(NL[count] + "    " + EN[count]);
                count++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Check(TB_Left, TB_Right);
        }

        /// <summary>
        /// Check the answers
        /// </summary>
        /// <param name="EN_tb">List of TextBox with English text</param>
        /// <param name="NL_tb">List of TextBox with Dutch text</param>
        private void Check(List<TextBox> EN_tb, List<TextBox> NL_tb)
        {
            Console.WriteLine(NL.Count);

            for (int i = 0; i < EN_tb.Count; i++)
            {
                if(NL_tb[i].Text == NL[i])
                {
                    NL_tb[i].BackColor = Color.Green;
                }
                else
                {
                    NL_tb[i].BackColor = Color.Red;
                }
            }
        }
    }
}
