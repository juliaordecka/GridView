using System.Data;
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

        private void ExportToCSV(DataGridView dataGridView, string filePath)
        {
            // Tworzenie nag��wka pliku CSV
            string csvContent = "Imie,Nazwisko,Wiek,Stanowisko,ID" + Environment.NewLine;
            // Dodawanie danych z DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Pomijaj wiersze niemieszcz�ce si� w DataGridView (np. wiersz zaznaczania)
                if (!row.IsNewRow)
                {
                    // Dodaj kolejne warto�ci w wierszu, oddzielone przecinkami
                    csvContent += string.Join(",", Array.ConvertAll(row.Cells.Cast<DataGridViewCell>()
                    .ToArray(), c => c.Value)) + Environment.NewLine;
                }
            }
            // Zapisanie zawarto�ci do pliku CSV
            File.WriteAllText(filePath, csvContent);
        }


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


        //zapis do csv
        private void button3_Click(object sender, EventArgs e)
        {
            // Wy�wietlanie okna dialogowego wyboru lokalizacji zapisu
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            saveFileDialog1.Title = "Wybierz lokalizacj� zapisu pliku CSV";
            saveFileDialog1.ShowDialog();
            // Je�li u�ytkownik wybierze lokalizacj� i zatwierdzi, zapisz plik CSV
            if (saveFileDialog1.FileName != "")
            {
                // U�yj metody ExportToCSV i podaj obiekt DataGridView oraz �cie�k� do pliku CSV
                ExportToCSV(dataGridView1, saveFileDialog1.FileName);
            }
        }

        //ODCZYT

        private void LoadCSVToDataGridView(string filePath)
        {
            // Sprawd�, czy plik istnieje
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Plik CSV nie istnieje.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            // Odczytaj zawarto�� pliku CSV
            string[] lines = File.ReadAllLines(filePath);
            // Tworzenie tabeli danych
            dataGridView1.Columns.Clear();
            DataTable dataTable = new DataTable();
            // Dodanie kolumn na podstawie nag��wka
            string[] headers = lines[0].Split(',');
            foreach (string header in headers)
            {
                dataTable.Columns.Add(header);
            }
            // Dodawanie wierszy do tabeli danych
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                dataTable.Rows.Add(values);
            }
            // Przypisanie tabeli danych do DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Wy�wietlenie okna dialogowego wyboru pliku CSV
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            openFileDialog1.Title = "Wybierz plik CSV do wczytania";
            openFileDialog1.ShowDialog();
            // Je�li u�ytkownik wybierze plik i zatwierdzi, wczytaj dane z pliku CSV
            if (openFileDialog1.FileName != "")
            {
                // Wywo�anie funkcji wczytuj�cej dane z pliku CSV
                LoadCSVToDataGridView(openFileDialog1.FileName);
            }
        }
    }
}
