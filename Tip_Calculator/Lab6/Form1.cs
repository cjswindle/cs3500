using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double input;
            double percent;

            if(Double.TryParse(textBox1.Text, out input))
            {
                textBox2.Text = (input * ((1.0 + double.Parse(tipPercenet.Text) / 100))).ToString("C");
            }
        }
    }
}
