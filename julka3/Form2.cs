using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace julka3
{
    public partial class Form2 : Form
    {
        private Form1 form1Instance;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            form1Instance = form1;
        }
        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //dodaj nowy wiersz
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridView dataGridViewFromForm1 = form1Instance.GetDataGridView();
            dataGridViewFromForm1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, form1Instance.index);
            form1Instance.index++;

            this.Close();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
