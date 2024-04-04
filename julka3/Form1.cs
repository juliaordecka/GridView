using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using static julka3.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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
            // Tworzenie nag³ówka pliku CSV
            string csvContent = "Imie,Nazwisko,Wiek,Stanowisko,ID" + Environment.NewLine;
            // Dodawanie danych z DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Pomijaj wiersze niemieszcz¹ce siê w DataGridView (np. wiersz zaznaczania)
                if (!row.IsNewRow)
                {
                    // Dodaj kolejne wartoœci w wierszu, oddzielone przecinkami
                    csvContent += string.Join(",", Array.ConvertAll(row.Cells.Cast<DataGridViewCell>()
                    .ToArray(), c => c.Value)) + Environment.NewLine;
                }
            }
            // Zapisanie zawartoœci do pliku CSV
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
            // Wyœwietlanie okna dialogowego wyboru lokalizacji zapisu
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            saveFileDialog1.Title = "Wybierz lokalizacjê zapisu pliku CSV";
            saveFileDialog1.ShowDialog();
            // Jeœli u¿ytkownik wybierze lokalizacjê i zatwierdzi, zapisz plik CSV
            if (saveFileDialog1.FileName != "")
            {
                // U¿yj metody ExportToCSV i podaj obiekt DataGridView oraz œcie¿kê do pliku CSV
                ExportToCSV(dataGridView1, saveFileDialog1.FileName);
            }
        }

        //ODCZYT

        private void LoadCSVToDataGridView(string filePath)
        {
            // SprawdŸ, czy plik istnieje
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Plik CSV nie istnieje.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Odczytaj zawartoœæ pliku CSV
            string[] lines = File.ReadAllLines(filePath);
            // Tworzenie tabeli danych
            dataGridView1.Columns.Clear();
            DataTable dataTable = new DataTable();
            // Dodanie kolumn na podstawie nag³ówka
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
            // Wyœwietlenie okna dialogowego wyboru pliku CSV
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            openFileDialog1.Title = "Wybierz plik CSV do wczytania";
            openFileDialog1.ShowDialog();
            // Jeœli u¿ytkownik wybierze plik i zatwierdzi, wczytaj dane z pliku CSV
            if (openFileDialog1.FileName != "")
            {
                // Wywo³anie funkcji wczytuj¹cej dane z pliku CSV
                LoadCSVToDataGridView(openFileDialog1.FileName);
            }
        }

        ////////DODAWANIE KLASY OSOBA
        [Serializable]
        public class Osoba
        {
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public int Wiek { get; set; }
            public string Stanowisko { get; set; }
            public int ID { get; set; }

            /// Konstruktor klasy Osoba

            public Osoba(string imie, string nazwisko, int wiek, string stanowisko, int id)
            {
                Imie = imie;
                Nazwisko = nazwisko;
                Wiek = wiek;
                Stanowisko = stanowisko;
                ID = id;
            }

            //konstruktor bezparametrowy
            public bool IsInitialized;
            public Osoba()
            {
                IsInitialized = true;
            }


            //SERIALIZACJA DO XML - jedna osoba
            public void SerializeToXML(string fileName)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Osoba));
                using (TextWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, this);
                }
            }

            //DESERIALIZACJA LISTY Z XML
            public static List<Osoba> DeserializeFromXML(string fileName)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Osoba>));
                using (TextReader reader = new StreamReader(fileName))
                {
                    List<Osoba> osoby = (List<Osoba>)serializer.Deserialize(reader);
                    return osoby;
                }
            }

            // Metoda do wyœwietlania informacji o osobie
            public void DisplayInfo(DataGridView dataGridView)
            {
                int rowIndex = dataGridView.Rows.Add();

                dataGridView.Rows[rowIndex].Cells[0].Value = Imie;
                dataGridView.Rows[rowIndex].Cells[1].Value = Nazwisko;
                dataGridView.Rows[rowIndex].Cells[2].Value = Wiek;
                dataGridView.Rows[rowIndex].Cells[3].Value = Stanowisko;
                dataGridView.Rows[rowIndex].Cells[4].Value = ID;

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = dataGridView1;

            List<Osoba> osoby = new List<Osoba>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string imie = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string nazwisko = dataGridView1.Rows[i].Cells[1].Value.ToString();
                int wiek = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                string stanowisko = dataGridView1.Rows[i].Cells[3].Value.ToString();
                int id = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                Osoba osoba = new Osoba(imie, nazwisko, wiek, stanowisko, id);
                osoby.Add(osoba);
            }

            //serializacja listy osob
            XmlSerializer serializer = new XmlSerializer(typeof(List<Osoba>));
            using (TextWriter writer = new StreamWriter("osoby.xml"))
            {
                serializer.Serialize(writer, osoby);
            }


        }

        //button do deserializacji listy osob
        private void button6_Click(object sender, EventArgs e)
        {
            List<Osoba> osoby = Osoba.DeserializeFromXML("osoby.xml");
            foreach (Osoba osoba in osoby)
            {
                osoba.DisplayInfo(dataGridView1);
            }

        }
    }
}
