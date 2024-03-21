using System.Windows.Forms;

namespace julka3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        public int index = 1;


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            f2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public DataGridView GetDataGridView()
        {
            return dataGridView1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        }
    }
}
