using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace EuroGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = "datasource=127.0.0.1;port=3310;username=root;password=;database=eurovizio;";
        MySqlConnection connection;

        List<Dal> dalok;
        
            public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dalok = new List<Dal>();
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string parancs = "SELECT ev,eloado,cim,helyezes,pontszam FROM dal";
            MySqlCommand lekerdezes = new MySqlCommand(parancs,connection);
            lekerdezes.CommandTimeout = 60;
            MySqlDataReader reader = lekerdezes.ExecuteReader();
            while (reader.Read())
            {
                dalok.Add(new Dal(reader));
            }
            reader.Close();
            connection.Close();
            dataGrid.ItemsSource = dalok;
        }


        private void btn_4_Click(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string parancs = "SELECT COUNT(*), helyezes FROM dal WHERE orszag LIKE 'Magyarország'ORDER BY helyezes LIMIT 1";
            MySqlCommand lekerdezes = new MySqlCommand(parancs, connection);
            lekerdezes.CommandTimeout = 60;
            MySqlDataReader reader = lekerdezes.ExecuteReader();
            
            MessageBox.Show($"Magyar versenyzők száma: " + reader.GetInt16(0) + ", Magyar versenyzők legjobb helyezése: " + reader.GetInt16(1));
            
            reader.Close();
            connection.Close();
        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string parancs = "SELECT AVG(pontszam) FROM dal WHERE orszag LIKE 'Németország'";
            MySqlCommand lekerdezes = new MySqlCommand(parancs, connection);
            lekerdezes.CommandTimeout = 60;
            MySqlDataReader reader = lekerdezes.ExecuteReader();
            
            MessageBox.Show($"Német versenyzők átlagpontszáma: " + reader.GetFloat(0).ToString("#.##"));
            
            reader.Close();
            connection.Close();
        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string parancs = "SELECT eloado,cim FROM dal WHERE cim LIKE '%Luck%'";
            MySqlCommand lekerdezes = new MySqlCommand(parancs, connection);
            lekerdezes.CommandTimeout = 60;
            MySqlDataReader reader = lekerdezes.ExecuteReader();
            while (reader.Read())
            {
                MessageBox.Show(reader.GetString(0)+" - "+reader.GetString(1));

            }

            reader.Close();
            connection.Close();
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            lb_eredmeny.ItemsSource = "";
            List<string> keresettDalok = new List<string>();
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string eloado = tb_keresett_nev.Text;
            string parancs = $"SELECT eloado,cim FROM dal WHERE eloado LIKE '%{eloado}%'ORDER BY eloado, cim";
            MySqlCommand lekerdezes = new MySqlCommand(parancs, connection);
            lekerdezes.CommandTimeout = 60;
            MySqlDataReader reader = lekerdezes.ExecuteReader();
            while (reader.Read())
            {
                keresettDalok.Add(reader.GetString(0)+","+reader.GetString(1));

            }
            lb_eredmeny.ItemsSource = keresettDalok;
            reader.Close();
            connection.Close();
        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string eloado = tb_keresett_nev.Text;
            int kivalasztottIndex = dataGrid.SelectedIndex;
            Dal kivalasztott = dalok[kivalasztottIndex];
            string parancs = $"SELECT datum FROM verseny INNER JOIN dal ON verseny.ev = dal.ev WHERE dal.eloado LIKE '{kivalasztott.Eloado}' AND dal.cim LIKE '{kivalasztott.Cim}'";
            MySqlCommand lekerdezes = new MySqlCommand(parancs, connection);
            lekerdezes.CommandTimeout = 60;
            MySqlDataReader reader = lekerdezes.ExecuteReader();


            while (reader.Read())
            {
                lbl_datum.Content = reader.GetString(0);

            }

            reader.Close();
            connection.Close();
        }
    }
}
