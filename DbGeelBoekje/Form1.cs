﻿using System;
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
        protected Point TB_LastPos_NL = new Point(350, 0);

        public List<TextBox> TB_EN = new List<TextBox>();
        public List<TextBox> TB_NL = new List<TextBox>();
        public List<string> NL = new List<string>();
        public List<string> EN = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            DbInit();
            Add_EN_TB();
            Add_NL_TB();
            Populate_EnTb(TB_EN);
        }
        private void Add_EN_TB()
        {
            for (int i = 0; i < EN.Count; i++)
            {
                TextBox tb = new TextBox();
                TB_LastPos_EN = new Point(TB_LastPos_EN.X, TB_LastPos_EN.Y + tb.Size.Height + 10);
                tb.Location = TB_LastPos_EN;
                tb.Visible = true;
                tb.Text = i.ToString();
                tb.Size = new Size(tb.Size.Width + 50, tb.Size.Height);
                tb.Name = "Tb_EN_" + i.ToString();
                tb.ReadOnly = true;
                TB_EN.Add(tb);
                this.Controls.Add(tb);
            }
        }

        private void Add_NL_TB()
        {
            for (int i = 0; i < NL.Count; i++)
            {
                TextBox tb = new TextBox();
                TB_LastPos_NL = new Point(TB_LastPos_NL.X, TB_LastPos_NL.Y + tb.Size.Height + 10);
                tb.Location = TB_LastPos_NL;
                tb.Visible = true;
                tb.Text = i.ToString();
                tb.Size = new Size(tb.Size.Width + 50, tb.Size.Height);
                tb.Name = "Tb_EN_" + i.ToString();
                TB_NL.Add(tb);
                this.Controls.Add(tb);
            }
        }

        private void Populate_EnTb(List<TextBox> LtB)
        {
            for (int i = 0; i < LtB.Count; i++)
            {
                LtB[i].Text = EN[i];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in TB_EN)
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
            for (int i = 0; i < TB_EN.Count; i++)
            {
                if(TB_NL[i].Text == NL[i])
                {
                    TB_NL[i].BackColor = Color.Green;
                }
                else
                {
                    TB_NL[i].BackColor = Color.Red;
                }
            }
        }
    }
}