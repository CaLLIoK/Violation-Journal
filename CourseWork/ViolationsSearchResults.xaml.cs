using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Shapes;

namespace Journal
{
    /// <summary>
    /// Логика взаимодействия для ViolationsSearchResults.xaml
    /// </summary>
    public partial class ViolationsSearchResults : Window
    {
        string connectionString = @"Data Source=(local)\SQLEXPRESS; Initial Catalog=Journal; Integrated Security=True";
        DataTable table;

        public ViolationsSearchResults()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string criterion = string.Empty;
            StreamReader readCriterion = new StreamReader("Criterion.txt");
            criterion = readCriterion.ReadLine();
            readCriterion.Close();

            string searchCriterion = string.Empty;
            StreamReader readSearchCriterion = new StreamReader("SearchCriterion.txt");
            searchCriterion = readSearchCriterion.ReadLine();
            readSearchCriterion.Close();

            if (criterion == "Название нарушения")
            {
                string query = "SELECT ViolationName, ViolationCost FROM Violation WHERE ViolationName = '" + searchCriterion + "'";
                table = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (IDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolationsGrid.ItemsSource = table.DefaultView;
            }
            else if (criterion == "Сумма штрафа")
            {
                table = new DataTable();             
                using (SqlCommand lastCommnd = connection.CreateCommand())
                {
                    lastCommnd.CommandText = "SELECT ViolationName, ViolationCost FROM Violation WHERE ViolationCost = @cost";

                    lastCommnd.Parameters.AddWithValue("@cost", Convert.ToDouble(searchCriterion));

                    using (IDataReader rdr = lastCommnd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                ViolationsGrid.ItemsSource = table.DefaultView;
            }           
            connection.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchViolations searchViolations = new SearchViolations();
            searchViolations.Show();
            this.Close();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}