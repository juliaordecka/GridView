using System.Windows.Forms;

namespace julka3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Inicjalizacja DataGridView
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.Dock = DockStyle.Fill;
            // Utworzenie kolumn
            dataGridView1.Columns.Add("Column1", "Column 1");
            dataGridView1.Columns.Add("Column2", "Column 2");
            // Dodanie danych do siatki
            dataGridView1.Rows.Add(new object[] { "Data 1", "Value 1" });
            dataGridView1.Rows.Add(new object[] { "Data 2", "Value 2" });
            // Dodanie DataGridView do formularza
            Controls.Add(dataGridView1);

        }

            private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
