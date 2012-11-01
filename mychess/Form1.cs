using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mychess
{
    public partial class Form1 : Form
    {
        public Form1(string scolor,string s = "")
        {
            InitializeComponent();
            label4.Text = String.Format("имя {0} игрока:", scolor);
            textBox1.Text = s;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string GetName()
        {
            if (textBox1.Text != "")
                return textBox1.Text;
            else
                return "Неизвестный";
        }
    }
}
